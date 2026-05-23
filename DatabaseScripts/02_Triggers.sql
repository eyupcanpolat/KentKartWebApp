/* =========================================================
   KentKart Web App - Triggers
   Bu dosyada otomatik çalışan SQL trigger yapıları bulunmaktadır.
   ========================================================= */


/* Ödeme kaydı eklendiğinde kart bakiyesini artırır */
IF OBJECT_ID('dbo.trg_AfterPaymentInsert_UpdateCardBalance', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_AfterPaymentInsert_UpdateCardBalance;
END;
GO

CREATE TRIGGER dbo.trg_AfterPaymentInsert_UpdateCardBalance
ON dbo.Payments
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE c
    SET c.Balance = c.Balance + x.TotalAmount
    FROM dbo.Cards c
    INNER JOIN (
        SELECT 
            CardId,
            SUM(Amount) AS TotalAmount
        FROM inserted
        WHERE Status = 'Success'
          AND PaymentType = 'BalanceLoad'
        GROUP BY CardId
    ) x ON c.CardId = x.CardId;
END;
GO


/* Yolculuk kaydı eklendiğinde kart bakiyesinden ücreti düşer */
IF OBJECT_ID('dbo.trg_AfterTripInsert_DeductCardBalance', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_AfterTripInsert_DeductCardBalance;
END;
GO

CREATE TRIGGER dbo.trg_AfterTripInsert_DeductCardBalance
ON dbo.Trips
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN dbo.Cards c ON c.CardId = i.CardId
        WHERE i.Status = 'Completed'
          AND c.Balance < i.FareAmount
    )
    BEGIN
        RAISERROR('Kart bakiyesi yetersiz olduğu için yolculuk işlemi yapılamaz.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    UPDATE c
    SET c.Balance = c.Balance - x.TotalFare
    FROM dbo.Cards c
    INNER JOIN (
        SELECT 
            CardId,
            SUM(FareAmount) AS TotalFare
        FROM inserted
        WHERE Status = 'Completed'
        GROUP BY CardId
    ) x ON c.CardId = x.CardId;
END;
GO


/* Kayıp kart bildirimi eklendiğinde kart durumunu Lost yapar */
IF OBJECT_ID('dbo.trg_AfterLostCardReportInsert_MarkCardLost', 'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_AfterLostCardReportInsert_MarkCardLost;
END;
GO

CREATE TRIGGER dbo.trg_AfterLostCardReportInsert_MarkCardLost
ON dbo.LostCardReports
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE c
    SET c.Status = 'Lost'
    FROM dbo.Cards c
    INNER JOIN inserted i ON c.CardId = i.CardId
    WHERE i.Status = 'Reported';
END;
GO