SELECT CardId, UserId, CardNumber, Balance, Status
FROM dbo.Cards;

INSERT INTO dbo.Payments 
(CardId, Amount, PaymentMethod, PaymentType, Status, PaymentDate, Description)
VALUES 
(1, 50, 'CreditCard', 'BalanceLoad', 'Success', GETDATE(), 'İlk test bakiye yükleme');