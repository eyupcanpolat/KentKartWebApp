/* =========================================================
   KentKart Web App - Test Queries
   Bu dosyada veritabanı kontrol ve test sorguları bulunmaktadır.
   ========================================================= */


/* 1) Tablolardaki kayıt sayılarını kontrol eder */
SELECT 'Roles' AS TableName, COUNT(*) AS RecordCount FROM dbo.Roles
UNION ALL
SELECT 'Users', COUNT(*) FROM dbo.Users
UNION ALL
SELECT 'CardTypes', COUNT(*) FROM dbo.CardTypes
UNION ALL
SELECT 'CardApplications', COUNT(*) FROM dbo.CardApplications
UNION ALL
SELECT 'Cards', COUNT(*) FROM dbo.Cards
UNION ALL
SELECT 'Payments', COUNT(*) FROM dbo.Payments
UNION ALL
SELECT 'BusLines', COUNT(*) FROM dbo.BusLines
UNION ALL
SELECT 'Stations', COUNT(*) FROM dbo.Stations
UNION ALL
SELECT 'LineStations', COUNT(*) FROM dbo.LineStations
UNION ALL
SELECT 'FareRules', COUNT(*) FROM dbo.FareRules
UNION ALL
SELECT 'Trips', COUNT(*) FROM dbo.Trips
UNION ALL
SELECT 'LostCardReports', COUNT(*) FROM dbo.LostCardReports
UNION ALL
SELECT 'SubscriptionPlans', COUNT(*) FROM dbo.SubscriptionPlans
UNION ALL
SELECT 'CardSubscriptions', COUNT(*) FROM dbo.CardSubscriptions;


/* 2) Indexleri listeler */
SELECT 
    t.name AS TableName,
    ind.name AS IndexName,
    ind.type_desc AS IndexType,
    col.name AS ColumnName,
    ind.is_unique AS IsUnique
FROM sys.indexes ind
INNER JOIN sys.index_columns ic 
    ON ind.object_id = ic.object_id 
    AND ind.index_id = ic.index_id
INNER JOIN sys.columns col 
    ON ic.object_id = col.object_id 
    AND ic.column_id = col.column_id
INNER JOIN sys.tables t 
    ON ind.object_id = t.object_id
WHERE ind.is_primary_key = 0
  AND ind.name IS NOT NULL
ORDER BY t.name, ind.name;


/* 3) Constraintleri listeler */
SELECT 
    tc.TABLE_NAME AS TableName,
    tc.CONSTRAINT_NAME AS ConstraintName,
    tc.CONSTRAINT_TYPE AS ConstraintType
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
ORDER BY tc.TABLE_NAME, tc.CONSTRAINT_TYPE;


/* 4) Check constraint detaylarını gösterir */
SELECT 
    t.name AS TableName,
    cc.name AS CheckConstraintName,
    cc.definition AS Definition
FROM sys.check_constraints cc
INNER JOIN sys.tables t 
    ON cc.parent_object_id = t.object_id
ORDER BY t.name;


/* 5) Foreign key ilişkilerini gösterir */
SELECT
    fk.name AS ForeignKeyName,
    tp.name AS ParentTable,
    cp.name AS ParentColumn,
    tr.name AS ReferencedTable,
    cr.name AS ReferencedColumn
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fkc 
    ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.tables tp 
    ON fkc.parent_object_id = tp.object_id
INNER JOIN sys.columns cp 
    ON fkc.parent_object_id = cp.object_id 
    AND fkc.parent_column_id = cp.column_id
INNER JOIN sys.tables tr 
    ON fkc.referenced_object_id = tr.object_id
INNER JOIN sys.columns cr 
    ON fkc.referenced_object_id = cr.object_id 
    AND fkc.referenced_column_id = cr.column_id
ORDER BY tp.name;


/* 6) View listesini gösterir */
SELECT 
    name AS ViewName,
    create_date,
    modify_date
FROM sys.views
ORDER BY name;


/* 7) Trigger listesini gösterir */
SELECT 
    tr.name AS TriggerName,
    OBJECT_NAME(tr.parent_id) AS TableName,
    tr.create_date,
    tr.modify_date
FROM sys.triggers tr
ORDER BY tr.name;


/* 8) Stored procedure listesini gösterir */
SELECT 
    name AS ProcedureName,
    create_date,
    modify_date
FROM sys.procedures
ORDER BY name;


/* 9) View testleri */
SELECT * FROM dbo.vw_UserCardSummary;
SELECT * FROM dbo.vw_DailyRevenue;
SELECT * FROM dbo.vw_MostUsedBusLines;


/* 10) Stored procedure testleri */
EXEC dbo.sp_GetUserDashboard @UserId = 1;
EXEC dbo.sp_GetDailyRevenueReport;
EXEC dbo.sp_GetMostUsedBusLinesReport;


/* 11) Güçlü JOIN sorgusu: kullanıcı, kart, kart tipi ve yolculuk sayısı */
SELECT 
    u.FirstName + ' ' + u.LastName AS FullName,
    u.Email,
    c.CardNumber,
    ct.Name AS CardType,
    c.Balance,
    c.Status AS CardStatus,
    COUNT(t.TripId) AS TripCount
FROM dbo.Users u
INNER JOIN dbo.Cards c ON u.UserId = c.UserId
INNER JOIN dbo.CardTypes ct ON c.CardTypeId = ct.CardTypeId
LEFT JOIN dbo.Trips t ON c.CardId = t.CardId
GROUP BY 
    u.FirstName,
    u.LastName,
    u.Email,
    c.CardNumber,
    ct.Name,
    c.Balance,
    c.Status
ORDER BY TripCount DESC;


/* 12) Ödeme geçmişi detay sorgusu */
SELECT
    p.PaymentId,
    u.FirstName + ' ' + u.LastName AS FullName,
    c.CardNumber,
    p.Amount,
    p.PaymentMethod,
    p.PaymentType,
    p.Status,
    p.PaymentDate,
    p.Description
FROM dbo.Payments p
INNER JOIN dbo.Cards c ON p.CardId = c.CardId
INNER JOIN dbo.Users u ON c.UserId = u.UserId
ORDER BY p.PaymentDate DESC;


/* 13) Yolculuk geçmişi detay sorgusu */
SELECT
    t.TripId,
    u.FirstName + ' ' + u.LastName AS FullName,
    c.CardNumber,
    bl.LineCode,
    bl.LineName,
    s.StationName,
    t.FareAmount,
    t.TripDate,
    t.Status
FROM dbo.Trips t
INNER JOIN dbo.Cards c ON t.CardId = c.CardId
INNER JOIN dbo.Users u ON c.UserId = u.UserId
INNER JOIN dbo.BusLines bl ON t.BusLineId = bl.BusLineId
INNER JOIN dbo.Stations s ON t.StationId = s.StationId
ORDER BY t.TripDate DESC;


/* 14) Aktif abonmanları gösterir */
SELECT
    cs.CardSubscriptionId,
    u.FirstName + ' ' + u.LastName AS FullName,
    c.CardNumber,
    sp.Name AS SubscriptionPlan,
    cs.RemainingRideCount,
    cs.StartDate,
    cs.EndDate,
    cs.Status
FROM dbo.CardSubscriptions cs
INNER JOIN dbo.Cards c ON cs.CardId = c.CardId
INNER JOIN dbo.Users u ON c.UserId = u.UserId
INNER JOIN dbo.SubscriptionPlans sp ON cs.SubscriptionPlanId = sp.SubscriptionPlanId
ORDER BY cs.CreatedAt DESC;


/* 15) Kayıp kart bildirimlerini gösterir */
SELECT
    lcr.LostCardReportId,
    u.FirstName + ' ' + u.LastName AS FullName,
    c.CardNumber,
    lcr.ReportDate,
    lcr.Reason,
    lcr.Status AS ReportStatus,
    c.Status AS CardStatus
FROM dbo.LostCardReports lcr
INNER JOIN dbo.Cards c ON lcr.CardId = c.CardId
INNER JOIN dbo.Users u ON lcr.UserId = u.UserId
ORDER BY lcr.ReportDate DESC;