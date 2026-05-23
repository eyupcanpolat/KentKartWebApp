/* =========================================================
   KentKart Web App - Dummy Data
   Bu dosyada test amaçlı örnek veriler bulunmaktadır.
   ========================================================= */


/* =========================
   1) Roles
   ========================= */

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Driver')
INSERT INTO dbo.Roles (Name, Description, CreatedAt)
VALUES ('Driver', 'Şoför kullanıcısı', GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Inspector')
INSERT INTO dbo.Roles (Name, Description, CreatedAt)
VALUES ('Inspector', 'Denetim görevlisi', GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Support')
INSERT INTO dbo.Roles (Name, Description, CreatedAt)
VALUES ('Support', 'Destek personeli', GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Finance')
INSERT INTO dbo.Roles (Name, Description, CreatedAt)
VALUES ('Finance', 'Finans personeli', GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Operator')
INSERT INTO dbo.Roles (Name, Description, CreatedAt)
VALUES ('Operator', 'Operasyon personeli', GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Reporter')
INSERT INTO dbo.Roles (Name, Description, CreatedAt)
VALUES ('Reporter', 'Raporlama yetkilisi', GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Guest')
INSERT INTO dbo.Roles (Name, Description, CreatedAt)
VALUES ('Guest', 'Misafir kullanıcı', GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Auditor')
INSERT INTO dbo.Roles (Name, Description, CreatedAt)
VALUES ('Auditor', 'Denetçi rolü', GETDATE());


/* =========================
   2) Users
   Not: Dummy kullanıcıların PasswordHash alanı gerçek giriş için değildir.
   ========================= */

DECLARE @UserRoleId INT = (SELECT RoleId FROM dbo.Roles WHERE Name = 'User');

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Email = 'ali@test.com')
INSERT INTO dbo.Users (RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber, CreatedAt, IsActive)
VALUES (@UserRoleId, 'Ali', 'Yilmaz', 'ali@test.com', 'DummyPasswordHash_NotForLogin', '05550000001', GETDATE(), 1);

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Email = 'ayse@test.com')
INSERT INTO dbo.Users (RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber, CreatedAt, IsActive)
VALUES (@UserRoleId, 'Ayse', 'Demir', 'ayse@test.com', 'DummyPasswordHash_NotForLogin', '05550000002', GETDATE(), 1);

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Email = 'mehmet@test.com')
INSERT INTO dbo.Users (RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber, CreatedAt, IsActive)
VALUES (@UserRoleId, 'Mehmet', 'Kaya', 'mehmet@test.com', 'DummyPasswordHash_NotForLogin', '05550000003', GETDATE(), 1);

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Email = 'zeynep@test.com')
INSERT INTO dbo.Users (RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber, CreatedAt, IsActive)
VALUES (@UserRoleId, 'Zeynep', 'Sahin', 'zeynep@test.com', 'DummyPasswordHash_NotForLogin', '05550000004', GETDATE(), 1);

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Email = 'burak@test.com')
INSERT INTO dbo.Users (RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber, CreatedAt, IsActive)
VALUES (@UserRoleId, 'Burak', 'Aydin', 'burak@test.com', 'DummyPasswordHash_NotForLogin', '05550000005', GETDATE(), 1);

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Email = 'elif@test.com')
INSERT INTO dbo.Users (RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber, CreatedAt, IsActive)
VALUES (@UserRoleId, 'Elif', 'Arslan', 'elif@test.com', 'DummyPasswordHash_NotForLogin', '05550000006', GETDATE(), 1);

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Email = 'mert@test.com')
INSERT INTO dbo.Users (RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber, CreatedAt, IsActive)
VALUES (@UserRoleId, 'Mert', 'Celik', 'mert@test.com', 'DummyPasswordHash_NotForLogin', '05550000007', GETDATE(), 1);

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Email = 'deniz@test.com')
INSERT INTO dbo.Users (RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber, CreatedAt, IsActive)
VALUES (@UserRoleId, 'Deniz', 'Kurt', 'deniz@test.com', 'DummyPasswordHash_NotForLogin', '05550000008', GETDATE(), 1);


/* =========================
   3) CardTypes
   ========================= */

IF NOT EXISTS (SELECT 1 FROM dbo.CardTypes WHERE Name = 'Ogretmen Kart')
INSERT INTO dbo.CardTypes (Name, Description, DiscountRate, IsActive, CreatedAt)
VALUES ('Ogretmen Kart', 'Öğretmenler için indirimli kart', 40, 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.CardTypes WHERE Name = 'Yasli Kart')
INSERT INTO dbo.CardTypes (Name, Description, DiscountRate, IsActive, CreatedAt)
VALUES ('Yasli Kart', 'Yaşlı vatandaşlar için kart', 60, 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.CardTypes WHERE Name = 'Engelli Kart')
INSERT INTO dbo.CardTypes (Name, Description, DiscountRate, IsActive, CreatedAt)
VALUES ('Engelli Kart', 'Engelli vatandaşlar için kart', 70, 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.CardTypes WHERE Name = 'Personel Kart')
INSERT INTO dbo.CardTypes (Name, Description, DiscountRate, IsActive, CreatedAt)
VALUES ('Personel Kart', 'Belediye personeli kartı', 80, 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.CardTypes WHERE Name = 'Misafir Kart')
INSERT INTO dbo.CardTypes (Name, Description, DiscountRate, IsActive, CreatedAt)
VALUES ('Misafir Kart', 'Geçici kullanım kartı', 0, 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.CardTypes WHERE Name = 'Turist Kart')
INSERT INTO dbo.CardTypes (Name, Description, DiscountRate, IsActive, CreatedAt)
VALUES ('Turist Kart', 'Turistler için ulaşım kartı', 0, 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.CardTypes WHERE Name = 'Kurumsal Kart')
INSERT INTO dbo.CardTypes (Name, Description, DiscountRate, IsActive, CreatedAt)
VALUES ('Kurumsal Kart', 'Kurum çalışanları için kart', 20, 1, GETDATE());


/* =========================
   4) BusLines
   ========================= */

IF NOT EXISTS (SELECT 1 FROM dbo.BusLines WHERE LineCode = '10')
INSERT INTO dbo.BusLines (LineCode, LineName, Description, IsActive, CreatedAt)
VALUES ('10', 'Merkez - Otogar', 'Merkez ve otogar arası hat', 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.BusLines WHERE LineCode = '21')
INSERT INTO dbo.BusLines (LineCode, LineName, Description, IsActive, CreatedAt)
VALUES ('21', 'Yahya Kaptan - Merkez', 'Yahya Kaptan hattı', 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.BusLines WHERE LineCode = '33')
INSERT INTO dbo.BusLines (LineCode, LineName, Description, IsActive, CreatedAt)
VALUES ('33', 'Derince - İzmit', 'Derince İzmit hattı', 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.BusLines WHERE LineCode = '44')
INSERT INTO dbo.BusLines (LineCode, LineName, Description, IsActive, CreatedAt)
VALUES ('44', 'Başiskele - İzmit', 'Başiskele İzmit hattı', 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.BusLines WHERE LineCode = '55')
INSERT INTO dbo.BusLines (LineCode, LineName, Description, IsActive, CreatedAt)
VALUES ('55', 'Gölcük - İzmit', 'Gölcük İzmit hattı', 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.BusLines WHERE LineCode = '66')
INSERT INTO dbo.BusLines (LineCode, LineName, Description, IsActive, CreatedAt)
VALUES ('66', 'Kartepe - İzmit', 'Kartepe İzmit hattı', 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.BusLines WHERE LineCode = '77')
INSERT INTO dbo.BusLines (LineCode, LineName, Description, IsActive, CreatedAt)
VALUES ('77', 'Çayırova - Gebze', 'Çayırova Gebze hattı', 1, GETDATE());


/* =========================
   5) Stations
   ========================= */

IF NOT EXISTS (SELECT 1 FROM dbo.Stations WHERE StationName = 'Derince Merkez')
INSERT INTO dbo.Stations (StationName, District, IsActive, CreatedAt)
VALUES ('Derince Merkez', 'Derince', 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Stations WHERE StationName = 'Başiskele Sahil')
INSERT INTO dbo.Stations (StationName, District, IsActive, CreatedAt)
VALUES ('Başiskele Sahil', 'Başiskele', 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Stations WHERE StationName = 'Gölcük Merkez')
INSERT INTO dbo.Stations (StationName, District, IsActive, CreatedAt)
VALUES ('Gölcük Merkez', 'Gölcük', 1, GETDATE());

IF NOT EXISTS (SELECT 1 FROM dbo.Stations WHERE StationName = 'Kartepe Merkez')
INSERT INTO dbo.Stations (StationName, District, IsActive, CreatedAt)
VALUES ('Kartepe Merkez', 'Kartepe', 1, GETDATE());


/* =========================
   6) FareRules
   ========================= */

INSERT INTO dbo.FareRules (CardTypeId, Price, ValidFrom, ValidTo, IsActive)
SELECT CardTypeId,
       CASE 
           WHEN Name = 'Ogretmen Kart' THEN 12
           WHEN Name = 'Yasli Kart' THEN 8
           WHEN Name = 'Engelli Kart' THEN 6
           WHEN Name = 'Personel Kart' THEN 5
           WHEN Name = 'Misafir Kart' THEN 20
           WHEN Name = 'Turist Kart' THEN 22
           WHEN Name = 'Kurumsal Kart' THEN 16
       END,
       GETDATE(),
       NULL,
       1
FROM dbo.CardTypes ct
WHERE Name IN ('Ogretmen Kart', 'Yasli Kart', 'Engelli Kart', 'Personel Kart', 'Misafir Kart', 'Turist Kart', 'Kurumsal Kart')
  AND NOT EXISTS (
      SELECT 1 
      FROM dbo.FareRules fr 
      WHERE fr.CardTypeId = ct.CardTypeId
  );


/* =========================
   7) Cards
   ========================= */

IF NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000002')
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT UserId, (SELECT CardTypeId FROM dbo.CardTypes WHERE Name = 'Tam Kart'), '41000000002', 0, 'Active', GETDATE()
FROM dbo.Users WHERE Email = 'ali@test.com';

IF NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000003')
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT UserId, (SELECT CardTypeId FROM dbo.CardTypes WHERE Name = 'Ogrenci Kart'), '41000000003', 0, 'Active', GETDATE()
FROM dbo.Users WHERE Email = 'ayse@test.com';

IF NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000004')
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT UserId, (SELECT CardTypeId FROM dbo.CardTypes WHERE Name = 'Indirimli Kart'), '41000000004', 0, 'Active', GETDATE()
FROM dbo.Users WHERE Email = 'mehmet@test.com';

IF NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000005')
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT UserId, (SELECT CardTypeId FROM dbo.CardTypes WHERE Name = 'Ogretmen Kart'), '41000000005', 0, 'Active', GETDATE()
FROM dbo.Users WHERE Email = 'zeynep@test.com';

IF NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000006')
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT UserId, (SELECT CardTypeId FROM dbo.CardTypes WHERE Name = 'Yasli Kart'), '41000000006', 0, 'Active', GETDATE()
FROM dbo.Users WHERE Email = 'burak@test.com';

IF NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000007')
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT UserId, (SELECT CardTypeId FROM dbo.CardTypes WHERE Name = 'Engelli Kart'), '41000000007', 0, 'Active', GETDATE()
FROM dbo.Users WHERE Email = 'elif@test.com';

IF NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000008')
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT UserId, (SELECT CardTypeId FROM dbo.CardTypes WHERE Name = 'Personel Kart'), '41000000008', 0, 'Active', GETDATE()
FROM dbo.Users WHERE Email = 'mert@test.com';

IF NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000009')
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT UserId, (SELECT CardTypeId FROM dbo.CardTypes WHERE Name = 'Turist Kart'), '41000000009', 0, 'Active', GETDATE()
FROM dbo.Users WHERE Email = 'deniz@test.com';

IF NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000010')
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT UserId, (SELECT CardTypeId FROM dbo.CardTypes WHERE Name = 'Kurumsal Kart'), '41000000010', 0, 'Active', GETDATE()
FROM dbo.Users WHERE Email = 'admin@test.com';


/* =========================
   8) CardApplications
   ========================= */

INSERT INTO dbo.CardApplications (UserId, CardTypeId, ApplicationDate, Status, AdminNote, ApprovedAt)
SELECT u.UserId, c.CardTypeId, GETDATE(), 'Approved', 'Dummy başvuru onaylandı.', GETDATE()
FROM dbo.Users u
INNER JOIN dbo.Cards c ON u.UserId = c.UserId
WHERE NOT EXISTS (
    SELECT 1
    FROM dbo.CardApplications ca
    WHERE ca.UserId = u.UserId
);


/* =========================
   9) LineStations
   ========================= */

DECLARE @StationAnitpark INT = (SELECT StationId FROM dbo.Stations WHERE StationName = 'Anıtpark');
DECLARE @StationOtogar INT = (SELECT StationId FROM dbo.Stations WHERE StationName = 'İzmit Otogar');
DECLARE @StationDerince INT = (SELECT StationId FROM dbo.Stations WHERE StationName = 'Derince Merkez');
DECLARE @StationBasiskele INT = (SELECT StationId FROM dbo.Stations WHERE StationName = 'Başiskele Sahil');
DECLARE @StationGolcuk INT = (SELECT StationId FROM dbo.Stations WHERE StationName = 'Gölcük Merkez');
DECLARE @StationKartepe INT = (SELECT StationId FROM dbo.Stations WHERE StationName = 'Kartepe Merkez');
DECLARE @StationGebze INT = (SELECT StationId FROM dbo.Stations WHERE StationName = 'Gebze Merkez');

INSERT INTO dbo.LineStations (BusLineId, StationId, StationOrder)
SELECT BusLineId, @StationAnitpark, 1 FROM dbo.BusLines WHERE LineCode = '10'
AND NOT EXISTS (SELECT 1 FROM dbo.LineStations ls INNER JOIN dbo.BusLines bl ON ls.BusLineId = bl.BusLineId WHERE bl.LineCode = '10' AND ls.StationId = @StationAnitpark);

INSERT INTO dbo.LineStations (BusLineId, StationId, StationOrder)
SELECT BusLineId, @StationOtogar, 2 FROM dbo.BusLines WHERE LineCode = '10'
AND NOT EXISTS (SELECT 1 FROM dbo.LineStations ls INNER JOIN dbo.BusLines bl ON ls.BusLineId = bl.BusLineId WHERE bl.LineCode = '10' AND ls.StationId = @StationOtogar);

INSERT INTO dbo.LineStations (BusLineId, StationId, StationOrder)
SELECT BusLineId, @StationDerince, 1 FROM dbo.BusLines WHERE LineCode = '33'
AND NOT EXISTS (SELECT 1 FROM dbo.LineStations ls INNER JOIN dbo.BusLines bl ON ls.BusLineId = bl.BusLineId WHERE bl.LineCode = '33' AND ls.StationId = @StationDerince);

INSERT INTO dbo.LineStations (BusLineId, StationId, StationOrder)
SELECT BusLineId, @StationAnitpark, 2 FROM dbo.BusLines WHERE LineCode = '33'
AND NOT EXISTS (SELECT 1 FROM dbo.LineStations ls INNER JOIN dbo.BusLines bl ON ls.BusLineId = bl.BusLineId WHERE bl.LineCode = '33' AND ls.StationId = @StationAnitpark);

INSERT INTO dbo.LineStations (BusLineId, StationId, StationOrder)
SELECT BusLineId, @StationBasiskele, 1 FROM dbo.BusLines WHERE LineCode = '44'
AND NOT EXISTS (SELECT 1 FROM dbo.LineStations ls INNER JOIN dbo.BusLines bl ON ls.BusLineId = bl.BusLineId WHERE bl.LineCode = '44' AND ls.StationId = @StationBasiskele);

INSERT INTO dbo.LineStations (BusLineId, StationId, StationOrder)
SELECT BusLineId, @StationOtogar, 2 FROM dbo.BusLines WHERE LineCode = '44'
AND NOT EXISTS (SELECT 1 FROM dbo.LineStations ls INNER JOIN dbo.BusLines bl ON ls.BusLineId = bl.BusLineId WHERE bl.LineCode = '44' AND ls.StationId = @StationOtogar);


/* =========================
   10) Payments
   Trigger, BalanceLoad tipindeki ödemeler için kart bakiyesini artırır.
   ========================= */

INSERT INTO dbo.Payments (CardId, Amount, PaymentMethod, PaymentType, Status, PaymentDate, Description)
SELECT CardId, 100, 'CreditCard', 'BalanceLoad', 'Success', GETDATE(), 'Dummy bakiye yükleme'
FROM dbo.Cards c
WHERE CardNumber IN ('41000000002', '41000000003', '41000000004', '41000000005', '41000000006', '41000000007', '41000000008', '41000000009', '41000000010')
  AND NOT EXISTS (
      SELECT 1 FROM dbo.Payments p 
      WHERE p.CardId = c.CardId 
        AND p.Description = 'Dummy bakiye yükleme'
  );


/* =========================
   11) Trips
   Trigger, Completed yolculuklarda kart bakiyesinden FareAmount kadar düşer.
   ========================= */

INSERT INTO dbo.Trips (CardId, BusLineId, StationId, FareAmount, TripDate, Status)
SELECT c.CardId, bl.BusLineId, s.StationId, 10, GETDATE(), 'Completed'
FROM dbo.Cards c
CROSS JOIN dbo.BusLines bl
CROSS JOIN dbo.Stations s
WHERE c.CardNumber = '41000000002'
  AND bl.LineCode = '10'
  AND s.StationName = 'Anıtpark'
  AND NOT EXISTS (SELECT 1 FROM dbo.Trips WHERE CardId = c.CardId AND BusLineId = bl.BusLineId AND StationId = s.StationId);

INSERT INTO dbo.Trips (CardId, BusLineId, StationId, FareAmount, TripDate, Status)
SELECT c.CardId, bl.BusLineId, s.StationId, 10, GETDATE(), 'Completed'
FROM dbo.Cards c
CROSS JOIN dbo.BusLines bl
CROSS JOIN dbo.Stations s
WHERE c.CardNumber = '41000000003'
  AND bl.LineCode = '33'
  AND s.StationName = 'Derince Merkez'
  AND NOT EXISTS (SELECT 1 FROM dbo.Trips WHERE CardId = c.CardId AND BusLineId = bl.BusLineId AND StationId = s.StationId);

INSERT INTO dbo.Trips (CardId, BusLineId, StationId, FareAmount, TripDate, Status)
SELECT c.CardId, bl.BusLineId, s.StationId, 10, GETDATE(), 'Completed'
FROM dbo.Cards c
CROSS JOIN dbo.BusLines bl
CROSS JOIN dbo.Stations s
WHERE c.CardNumber = '41000000004'
  AND bl.LineCode = '44'
  AND s.StationName = 'Başiskele Sahil'
  AND NOT EXISTS (SELECT 1 FROM dbo.Trips WHERE CardId = c.CardId AND BusLineId = bl.BusLineId AND StationId = s.StationId);


/* =========================
   12) LostCardReports
   Trigger, kayıp bildirimi yapılan kartın Status değerini Lost yapar.
   ========================= */

INSERT INTO dbo.LostCardReports (CardId, UserId, ReportDate, Reason, Status)
SELECT c.CardId, c.UserId, GETDATE(), 'Dummy kayıp kart bildirimi', 'Reported'
FROM dbo.Cards c
WHERE c.CardNumber IN ('41000000008', '41000000009')
  AND NOT EXISTS (
      SELECT 1 FROM dbo.LostCardReports lcr
      WHERE lcr.CardId = c.CardId
        AND lcr.Reason = 'Dummy kayıp kart bildirimi'
  );


/* =========================
   13) CardSubscriptions
   ========================= */

INSERT INTO dbo.CardSubscriptions (CardId, SubscriptionPlanId, StartDate, EndDate, RemainingRideCount, Status, CreatedAt)
SELECT c.CardId, sp.SubscriptionPlanId, GETDATE(), DATEADD(DAY, sp.ValidityDays, GETDATE()), sp.RideCount, 'Active', GETDATE()
FROM dbo.Cards c
INNER JOIN dbo.SubscriptionPlans sp ON c.CardTypeId = sp.CardTypeId
WHERE c.CardNumber IN ('41896505073', '41000000003', '41000000004')
  AND NOT EXISTS (
      SELECT 1 
      FROM dbo.CardSubscriptions cs
      WHERE cs.CardId = c.CardId
        AND cs.Status = 'Active'
  );