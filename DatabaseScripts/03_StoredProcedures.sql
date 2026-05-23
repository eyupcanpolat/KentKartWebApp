/* =========================================================
   KentKart Web App - Stored Procedures
   Bu dosyada hazır rapor ve işlem sorguları bulunmaktadır.
   ========================================================= */


/* Kullanıcının kart özet bilgilerini getirir */
CREATE OR ALTER PROCEDURE dbo.sp_GetUserDashboard
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.UserId,
        u.FirstName + ' ' + u.LastName AS FullName,
        u.Email,
        c.CardId,
        c.CardNumber,
        ct.Name AS CardTypeName,
        c.Balance,
        c.Status AS CardStatus
    FROM dbo.Users u
    INNER JOIN dbo.Cards c ON u.UserId = c.UserId
    INNER JOIN dbo.CardTypes ct ON c.CardTypeId = ct.CardTypeId
    WHERE u.UserId = @UserId;
END;
GO


/* Günlük ödeme gelir raporunu getirir */
CREATE OR ALTER PROCEDURE dbo.sp_GetDailyRevenueReport
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RevenueDate,
        PaymentCount,
        TotalRevenue
    FROM dbo.vw_DailyRevenue
    ORDER BY RevenueDate DESC;
END;
GO


/* En çok kullanılan hatları raporlar */
CREATE OR ALTER PROCEDURE dbo.sp_GetMostUsedBusLinesReport
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        BusLineId,
        LineCode,
        LineName,
        TripCount,
        TotalFareAmount
    FROM dbo.vw_MostUsedBusLines
    ORDER BY TripCount DESC;
END;
GO