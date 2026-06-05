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

/* admin@test.com karti yalnizca o kullanici mevcutsa eklenir (yoksa atlanir, hata vermez) */
IF EXISTS (SELECT 1 FROM dbo.Users WHERE Email = 'admin@test.com')
   AND NOT EXISTS (SELECT 1 FROM dbo.Cards WHERE CardNumber = '41000000010')
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
WHERE c.CardNumber IN ('41000000003', '41000000004')
  AND NOT EXISTS (
      SELECT 1
      FROM dbo.CardSubscriptions cs
      WHERE cs.CardId = c.CardId
        AND cs.Status = 'Active'
  );


/* =========================================================
   14) 10+ Dummy Data Garantisi
   PDF isteri: her tabloda en az 10 anlamli test verisi.
   Bu bolum Users, Cards, CardApplications, Payments, Trips,
   LostCardReports, SubscriptionPlans ve CardSubscriptions
   tablolarini 10+ satira tamamlar.
   - Tum insertler NOT EXISTS ile korunmustur; script tekrar
     tekrar calistirilabilir (idempotent).
   - Ana test karti 41896505073 bu bolumde KULLANILMAZ.
   - Aktif kartlar '46' prefixli, kayip kartlar '47' prefixlidir.
   ========================================================= */


/* 14.1) Ek dummy kullanicilar (12 adet) */
;WITH DummyUsers AS (
    SELECT * FROM (VALUES
        ('Dummy01', 'Kullanici', 'dummyuser01@kentkart.test', '05559000001'),
        ('Dummy02', 'Kullanici', 'dummyuser02@kentkart.test', '05559000002'),
        ('Dummy03', 'Kullanici', 'dummyuser03@kentkart.test', '05559000003'),
        ('Dummy04', 'Kullanici', 'dummyuser04@kentkart.test', '05559000004'),
        ('Dummy05', 'Kullanici', 'dummyuser05@kentkart.test', '05559000005'),
        ('Dummy06', 'Kullanici', 'dummyuser06@kentkart.test', '05559000006'),
        ('Dummy07', 'Kullanici', 'dummyuser07@kentkart.test', '05559000007'),
        ('Dummy08', 'Kullanici', 'dummyuser08@kentkart.test', '05559000008'),
        ('Dummy09', 'Kullanici', 'dummyuser09@kentkart.test', '05559000009'),
        ('Dummy10', 'Kullanici', 'dummyuser10@kentkart.test', '05559000010'),
        ('Dummy11', 'Kullanici', 'dummyuser11@kentkart.test', '05559000011'),
        ('Dummy12', 'Kullanici', 'dummyuser12@kentkart.test', '05559000012')
    ) v(FirstName, LastName, Email, PhoneNumber)
)
INSERT INTO dbo.Users (RoleId, FirstName, LastName, Email, PasswordHash, PhoneNumber, CreatedAt, IsActive)
SELECT (SELECT RoleId FROM dbo.Roles WHERE Name = 'User'),
       d.FirstName, d.LastName, d.Email, 'DummyPasswordHash_NotForLogin', d.PhoneNumber, GETDATE(), 1
FROM DummyUsers d
WHERE NOT EXISTS (SELECT 1 FROM dbo.Users u WHERE u.Email = d.Email);


/* 14.2) Ek dummy aktif kartlar (12 adet, '46' prefixli) */
;WITH DummyCards AS (
    SELECT * FROM (VALUES
        ('dummyuser01@kentkart.test', '4600000000001', 1),
        ('dummyuser02@kentkart.test', '4600000000002', 2),
        ('dummyuser03@kentkart.test', '4600000000003', 3),
        ('dummyuser04@kentkart.test', '4600000000004', 1),
        ('dummyuser05@kentkart.test', '4600000000005', 2),
        ('dummyuser06@kentkart.test', '4600000000006', 3),
        ('dummyuser07@kentkart.test', '4600000000007', 1),
        ('dummyuser08@kentkart.test', '4600000000008', 2),
        ('dummyuser09@kentkart.test', '4600000000009', 3),
        ('dummyuser10@kentkart.test', '4600000000010', 1),
        ('dummyuser11@kentkart.test', '4600000000011', 2),
        ('dummyuser12@kentkart.test', '4600000000012', 3)
    ) v(Email, CardNumber, CardTypeId)
)
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT u.UserId, d.CardTypeId, d.CardNumber, 0, 'Active', GETDATE()
FROM DummyCards d
INNER JOIN dbo.Users u ON u.Email = d.Email
WHERE NOT EXISTS (SELECT 1 FROM dbo.Cards c WHERE c.CardNumber = d.CardNumber);


/* 14.3) Ek dummy kart basvurulari (her '46' kart icin 1 adet) */
INSERT INTO dbo.CardApplications (UserId, CardTypeId, ApplicationDate, Status, AdminNote, ApprovedAt)
SELECT c.UserId, c.CardTypeId, GETDATE(), 'Approved', 'Dummy 10+ basvuru', GETDATE()
FROM dbo.Cards c
WHERE c.CardNumber LIKE '46%'
  AND NOT EXISTS (
      SELECT 1 FROM dbo.CardApplications ca
      WHERE ca.UserId = c.UserId AND ca.AdminNote = 'Dummy 10+ basvuru'
  );


/* 14.4) Ek dummy bakiye yuklemeleri (Payments / BalanceLoad).
   Trigger bu insertlerde kart bakiyesini artirir (200 TL). */
INSERT INTO dbo.Payments (CardId, Amount, PaymentMethod, PaymentType, Status, PaymentDate, Description)
SELECT c.CardId, 200, 'CreditCard', 'BalanceLoad', 'Success', GETDATE(), 'Dummy 10+ bakiye yukleme'
FROM dbo.Cards c
WHERE c.CardNumber LIKE '46%'
  AND NOT EXISTS (
      SELECT 1 FROM dbo.Payments p
      WHERE p.CardId = c.CardId AND p.Description = 'Dummy 10+ bakiye yukleme'
  );


/* 14.5) Ek dummy yolculuklar (Trips / Completed).
   14.4 sonrasi bakiye 200 TL oldugu icin 10 TL ucret guvenle dusulur. */
INSERT INTO dbo.Trips (CardId, BusLineId, StationId, FareAmount, TripDate, Status)
SELECT c.CardId,
       (SELECT MIN(BusLineId) FROM dbo.BusLines),
       (SELECT MIN(StationId) FROM dbo.Stations),
       10, GETDATE(), 'Completed'
FROM dbo.Cards c
WHERE c.CardNumber LIKE '46%'
  AND NOT EXISTS (SELECT 1 FROM dbo.Trips t WHERE t.CardId = c.CardId);


/* 14.6) Ek dummy abonman planlari (9 adet; seed ile birlikte toplam 12). */
;WITH DummyPlans AS (
    SELECT * FROM (VALUES
        ('Dummy Abonman 01', 'Dummy aylik plan',    1,  300, 60,  30),
        ('Dummy Abonman 02', 'Dummy aylik plan',    2,  200, 80,  30),
        ('Dummy Abonman 03', 'Dummy aylik plan',    3,  250, 70,  30),
        ('Dummy Abonman 04', 'Dummy haftalik plan', 1,  120, 20,   7),
        ('Dummy Abonman 05', 'Dummy haftalik plan', 2,   90, 30,   7),
        ('Dummy Abonman 06', 'Dummy haftalik plan', 3,  100, 25,   7),
        ('Dummy Abonman 07', 'Dummy yillik plan',   1, 2800, 700, 365),
        ('Dummy Abonman 08', 'Dummy yillik plan',   2, 2000, 900, 365),
        ('Dummy Abonman 09', 'Dummy yillik plan',   3, 2400, 800, 365)
    ) v(Name, Descr, CardTypeId, Price, RideCount, ValidityDays)
)
INSERT INTO dbo.SubscriptionPlans (Name, Description, CardTypeId, Price, RideCount, ValidityDays, IsActive, CreatedAt)
SELECT d.Name, d.Descr, d.CardTypeId, d.Price, d.RideCount, d.ValidityDays, 1, GETDATE()
FROM DummyPlans d
WHERE NOT EXISTS (SELECT 1 FROM dbo.SubscriptionPlans sp WHERE sp.Name = d.Name);


/* 14.7) Ek dummy kart abonmanlari (her '46' kart icin 1 adet). */
INSERT INTO dbo.CardSubscriptions (CardId, SubscriptionPlanId, StartDate, EndDate, RemainingRideCount, Status, CreatedAt)
SELECT c.CardId, p.SubscriptionPlanId, GETDATE(), DATEADD(DAY, p.ValidityDays, GETDATE()), p.RideCount, 'Active', GETDATE()
FROM dbo.Cards c
CROSS APPLY (
    SELECT TOP 1 sp.SubscriptionPlanId, sp.ValidityDays, sp.RideCount
    FROM dbo.SubscriptionPlans sp
    ORDER BY sp.SubscriptionPlanId
) p
WHERE c.CardNumber LIKE '46%'
  AND NOT EXISTS (SELECT 1 FROM dbo.CardSubscriptions cs WHERE cs.CardId = c.CardId);


/* 14.8) Kayip kart bildirimleri icin ayri dummy kartlar (12 adet, '47' prefixli).
   Bu kartlar yalnizca kayip bildirimi icindir; aktif '46' kartlar ve
   ana test karti 41896505073 etkilenmez. */
;WITH DummyLostCards AS (
    SELECT * FROM (VALUES
        ('dummyuser01@kentkart.test', '4700000000001', 1),
        ('dummyuser02@kentkart.test', '4700000000002', 2),
        ('dummyuser03@kentkart.test', '4700000000003', 3),
        ('dummyuser04@kentkart.test', '4700000000004', 1),
        ('dummyuser05@kentkart.test', '4700000000005', 2),
        ('dummyuser06@kentkart.test', '4700000000006', 3),
        ('dummyuser07@kentkart.test', '4700000000007', 1),
        ('dummyuser08@kentkart.test', '4700000000008', 2),
        ('dummyuser09@kentkart.test', '4700000000009', 3),
        ('dummyuser10@kentkart.test', '4700000000010', 1),
        ('dummyuser11@kentkart.test', '4700000000011', 2),
        ('dummyuser12@kentkart.test', '4700000000012', 3)
    ) v(Email, CardNumber, CardTypeId)
)
INSERT INTO dbo.Cards (UserId, CardTypeId, CardNumber, Balance, Status, CreatedAt)
SELECT u.UserId, d.CardTypeId, d.CardNumber, 0, 'Active', GETDATE()
FROM DummyLostCards d
INNER JOIN dbo.Users u ON u.Email = d.Email
WHERE NOT EXISTS (SELECT 1 FROM dbo.Cards c WHERE c.CardNumber = d.CardNumber);


/* 14.9) Kayip kart bildirimleri (12 adet).
   Trigger, '47' prefixli kartlarin Status degerini Lost yapar. */
INSERT INTO dbo.LostCardReports (CardId, UserId, ReportDate, Reason, Status)
SELECT c.CardId, c.UserId, GETDATE(), 'Dummy 10+ kayip kart bildirimi', 'Reported'
FROM dbo.Cards c
WHERE c.CardNumber LIKE '47%'
  AND NOT EXISTS (SELECT 1 FROM dbo.LostCardReports lcr WHERE lcr.CardId = c.CardId);