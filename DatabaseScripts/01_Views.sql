/* =========================================================
   KentKart Web App - Views
   Bu dosyada raporlama amaçlý SQL View yapýlarý bulunmaktadýr.
   ========================================================= */

CREATE OR ALTER VIEW dbo.vw_UserCardSummary
AS
SELECT
    u.UserId,
    u.FirstName + ' ' + u.LastName AS FullName,
    u.Email,
    c.CardId,
    c.CardNumber,
    ct.Name AS CardTypeName,
    ct.DiscountRate,
    c.Balance,
    c.Status AS CardStatus,
    c.CreatedAt AS CardCreatedAt
FROM dbo.Cards c
INNER JOIN dbo.Users u ON c.UserId = u.UserId
INNER JOIN dbo.CardTypes ct ON c.CardTypeId = ct.CardTypeId;
GO


CREATE OR ALTER VIEW dbo.vw_DailyRevenue
AS
SELECT
    CAST(p.PaymentDate AS date) AS RevenueDate,
    COUNT(p.PaymentId) AS PaymentCount,
    SUM(p.Amount) AS TotalRevenue
FROM dbo.Payments p
WHERE p.Status = 'Success'
GROUP BY CAST(p.PaymentDate AS date);
GO


CREATE OR ALTER VIEW dbo.vw_MostUsedBusLines
AS
SELECT
    bl.BusLineId,
    bl.LineCode,
    bl.LineName,
    COUNT(t.TripId) AS TripCount,
    ISNULL(SUM(t.FareAmount), 0) AS TotalFareAmount
FROM dbo.BusLines bl
LEFT JOIN dbo.Trips t ON bl.BusLineId = t.BusLineId
GROUP BY bl.BusLineId, bl.LineCode, bl.LineName;
GO