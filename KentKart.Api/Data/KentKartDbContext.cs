using KentKart.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentKart.Api.Data;

public class KentKartDbContext : DbContext
{
    public KentKartDbContext(DbContextOptions<KentKartDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<CardType> CardTypes => Set<CardType>();
    public DbSet<CardApplication> CardApplications => Set<CardApplication>();
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<BusLine> BusLines => Set<BusLine>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<LineStation> LineStations => Set<LineStation>();
    public DbSet<FareRule> FareRules => Set<FareRule>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<LostCardReport> LostCardReports => Set<LostCardReport>();

    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<CardSubscription> CardSubscriptions => Set<CardSubscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.RoleId);

            entity.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(r => r.Description)
                .HasMaxLength(200);

            entity.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasIndex(r => r.Name)
                .IsUnique();

            entity.HasData(
                new Role
                {
                    RoleId = 1,
                    Name = "Admin",
                    Description = "Sistem yöneticisi",
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Role
                {
                    RoleId = 2,
                    Name = "User",
                    Description = "Normal kullanıcı",
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);

            entity.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(u => u.IsActive)
                .HasDefaultValue(true);

            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.HasKey(ct => ct.CardTypeId);

            entity.Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(ct => ct.Description)
                .HasMaxLength(200);

            entity.Property(ct => ct.DiscountRate)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            entity.Property(ct => ct.IsActive)
                .HasDefaultValue(true);

            entity.Property(ct => ct.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasIndex(ct => ct.Name)
                .IsUnique();

            entity.ToTable(t =>
                t.HasCheckConstraint("CK_CardTypes_DiscountRate", "[DiscountRate] >= 0 AND [DiscountRate] <= 100"));

            entity.HasData(
                new CardType
                {
                    CardTypeId = 1,
                    Name = "Tam Kart",
                    Description = "Standart tam ücretli kart",
                    DiscountRate = 0,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new CardType
                {
                    CardTypeId = 2,
                    Name = "Ogrenci Kart",
                    Description = "Öğrenciler için indirimli kart",
                    DiscountRate = 50,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new CardType
                {
                    CardTypeId = 3,
                    Name = "Indirimli Kart",
                    Description = "Belirli kullanıcılar için indirimli kart",
                    DiscountRate = 30,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );
        });

        modelBuilder.Entity<CardApplication>(entity =>
        {
            entity.HasKey(ca => ca.CardApplicationId);

            entity.Property(ca => ca.ApplicationDate)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(ca => ca.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.Property(ca => ca.AdminNote)
                .HasMaxLength(300);

            entity.ToTable(t =>
                t.HasCheckConstraint("CK_CardApplications_Status", "[Status] IN ('Pending', 'Approved', 'Rejected')"));

            entity.HasOne(ca => ca.User)
                .WithMany(u => u.CardApplications)
                .HasForeignKey(ca => ca.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ca => ca.CardType)
                .WithMany(ct => ct.CardApplications)
                .HasForeignKey(ca => ca.CardTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(c => c.CardId);

            entity.Property(c => c.CardNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(c => c.Balance)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            entity.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Active");

            entity.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasIndex(c => c.CardNumber)
                .IsUnique();

            entity.HasIndex(c => c.UserId);

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Cards_Balance", "[Balance] >= 0");
                t.HasCheckConstraint("CK_Cards_Status", "[Status] IN ('Active', 'Passive', 'Lost')");
            });

            entity.HasOne(c => c.User)
                .WithMany(u => u.Cards)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.CardType)
                .WithMany(ct => ct.Cards)
                .HasForeignKey(c => c.CardTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.PaymentId);

            entity.Property(p => p.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            entity.Property(p => p.PaymentMethod)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(p => p.PaymentType)
                .IsRequired()
                .HasMaxLength(30)
                .HasDefaultValue("BalanceLoad");

            entity.Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Success");

            entity.Property(p => p.PaymentDate)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(p => p.Description)
                .HasMaxLength(300);

            entity.HasIndex(p => p.CardId);

            entity.HasIndex(p => p.PaymentDate);

            entity.ToTable(t =>
            {
                t.HasTrigger("trg_AfterPaymentInsert_UpdateCardBalance");

                t.HasCheckConstraint("CK_Payments_Amount", "[Amount] > 0");
                t.HasCheckConstraint("CK_Payments_Status", "[Status] IN ('Success', 'Failed', 'Cancelled')");
                t.HasCheckConstraint("CK_Payments_Method", "[PaymentMethod] IN ('CreditCard', 'DebitCard', 'BankTransfer')");
                t.HasCheckConstraint("CK_Payments_Type", "[PaymentType] IN ('BalanceLoad', 'Subscription')");
            });

            entity.HasOne(p => p.Card)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CardId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BusLine>(entity =>
        {
            entity.HasKey(bl => bl.BusLineId);

            entity.Property(bl => bl.LineCode)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(bl => bl.LineName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(bl => bl.Description)
                .HasMaxLength(300);

            entity.Property(bl => bl.IsActive)
                .HasDefaultValue(true);

            entity.Property(bl => bl.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasIndex(bl => bl.LineCode)
                .IsUnique();

            entity.HasData(
                new BusLine
                {
                    BusLineId = 1,
                    LineCode = "41K",
                    LineName = "Umuttepe - İzmit Otogar",
                    Description = "Umuttepe kampüsü ile İzmit Otogar arası hat",
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new BusLine
                {
                    BusLineId = 2,
                    LineCode = "200",
                    LineName = "İzmit - Gebze",
                    Description = "İzmit ve Gebze arası şehir içi ulaşım hattı",
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new BusLine
                {
                    BusLineId = 3,
                    LineCode = "145",
                    LineName = "Körfez - İzmit",
                    Description = "Körfez ve İzmit merkezi arası hat",
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(s => s.StationId);

            entity.Property(s => s.StationName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(s => s.District)
                .HasMaxLength(100);

            entity.Property(s => s.IsActive)
                .HasDefaultValue(true);

            entity.Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasIndex(s => s.StationName);

            entity.HasData(
                new Station { StationId = 1, StationName = "Umuttepe Kampüs", District = "İzmit", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) },
                new Station { StationId = 2, StationName = "Yahya Kaptan", District = "İzmit", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) },
                new Station { StationId = 3, StationName = "Anıtpark", District = "İzmit", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) },
                new Station { StationId = 4, StationName = "İzmit Otogar", District = "İzmit", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) },
                new Station { StationId = 5, StationName = "Gebze Merkez", District = "Gebze", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) },
                new Station { StationId = 6, StationName = "Körfez Merkez", District = "Körfez", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) }
            );
        });

        modelBuilder.Entity<LineStation>(entity =>
        {
            entity.HasKey(ls => ls.LineStationId);

            entity.Property(ls => ls.StationOrder)
                .IsRequired();

            entity.HasIndex(ls => new { ls.BusLineId, ls.StationOrder })
                .IsUnique();

            entity.HasIndex(ls => new { ls.BusLineId, ls.StationId })
                .IsUnique();

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_LineStations_StationOrder", "[StationOrder] > 0");
            });

            entity.HasOne(ls => ls.BusLine)
                .WithMany(bl => bl.LineStations)
                .HasForeignKey(ls => ls.BusLineId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ls => ls.Station)
                .WithMany(s => s.LineStations)
                .HasForeignKey(ls => ls.StationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new LineStation { LineStationId = 1, BusLineId = 1, StationId = 1, StationOrder = 1 },
                new LineStation { LineStationId = 2, BusLineId = 1, StationId = 2, StationOrder = 2 },
                new LineStation { LineStationId = 3, BusLineId = 1, StationId = 3, StationOrder = 3 },
                new LineStation { LineStationId = 4, BusLineId = 1, StationId = 4, StationOrder = 4 },

                new LineStation { LineStationId = 5, BusLineId = 2, StationId = 4, StationOrder = 1 },
                new LineStation { LineStationId = 6, BusLineId = 2, StationId = 5, StationOrder = 2 },

                new LineStation { LineStationId = 7, BusLineId = 3, StationId = 6, StationOrder = 1 },
                new LineStation { LineStationId = 8, BusLineId = 3, StationId = 3, StationOrder = 2 },
                new LineStation { LineStationId = 9, BusLineId = 3, StationId = 4, StationOrder = 3 }
            );
        });

        modelBuilder.Entity<FareRule>(entity =>
        {
            entity.HasKey(fr => fr.FareRuleId);

            entity.Property(fr => fr.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            entity.Property(fr => fr.ValidFrom)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(fr => fr.IsActive)
                .HasDefaultValue(true);

            entity.HasIndex(fr => fr.CardTypeId);

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_FareRules_Price", "[Price] > 0");
                t.HasCheckConstraint("CK_FareRules_DateRange", "[ValidTo] IS NULL OR [ValidTo] >= [ValidFrom]");
            });

            entity.HasOne(fr => fr.CardType)
                .WithMany(ct => ct.FareRules)
                .HasForeignKey(fr => fr.CardTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new FareRule
                {
                    FareRuleId = 1,
                    CardTypeId = 1,
                    Price = 20,
                    ValidFrom = new DateTime(2026, 1, 1),
                    IsActive = true
                },
                new FareRule
                {
                    FareRuleId = 2,
                    CardTypeId = 2,
                    Price = 10,
                    ValidFrom = new DateTime(2026, 1, 1),
                    IsActive = true
                },
                new FareRule
                {
                    FareRuleId = 3,
                    CardTypeId = 3,
                    Price = 14,
                    ValidFrom = new DateTime(2026, 1, 1),
                    IsActive = true
                }
            );
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(t => t.TripId);

            entity.Property(t => t.FareAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            entity.Property(t => t.TripDate)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Completed");

            entity.HasIndex(t => t.CardId);

            entity.HasIndex(t => t.TripDate);

            entity.HasIndex(t => new { t.BusLineId, t.TripDate });

            entity.ToTable(t =>
            {
                t.HasTrigger("trg_AfterTripInsert_DeductCardBalance");

                t.HasCheckConstraint("CK_Trips_FareAmount", "[FareAmount] >= 0");
                t.HasCheckConstraint("CK_Trips_Status", "[Status] IN ('Completed', 'Failed', 'Cancelled')");
            });


            entity.HasOne(t => t.Card)
                .WithMany(c => c.Trips)
                .HasForeignKey(t => t.CardId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.BusLine)
                .WithMany(bl => bl.Trips)
                .HasForeignKey(t => t.BusLineId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Station)
                .WithMany(s => s.Trips)
                .HasForeignKey(t => t.StationId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LostCardReport>(entity =>
        {
            entity.HasKey(lcr => lcr.LostCardReportId);

            entity.Property(lcr => lcr.ReportDate)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(lcr => lcr.Reason)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(lcr => lcr.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Reported");

            entity.HasIndex(lcr => lcr.CardId);

            entity.HasIndex(lcr => lcr.UserId);

            entity.ToTable(t =>
            {
                t.HasTrigger("trg_AfterLostCardReportInsert_MarkCardLost");

                t.HasCheckConstraint("CK_LostCardReports_Status", "[Status] IN ('Reported', 'Reviewed', 'Rejected')");
            });

            entity.HasOne(lcr => lcr.Card)
                .WithMany(c => c.LostCardReports)
                .HasForeignKey(lcr => lcr.CardId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(lcr => lcr.User)
                .WithMany(u => u.LostCardReports)
                .HasForeignKey(lcr => lcr.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(sp => sp.SubscriptionPlanId);

            entity.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(sp => sp.Description)
                .HasMaxLength(300);

            entity.Property(sp => sp.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            entity.Property(sp => sp.RideCount)
                .IsRequired();

            entity.Property(sp => sp.ValidityDays)
                .IsRequired();

            entity.Property(sp => sp.IsActive)
                .HasDefaultValue(true);

            entity.Property(sp => sp.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasIndex(sp => sp.Name)
                .IsUnique();

            entity.HasIndex(sp => sp.CardTypeId);

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_SubscriptionPlans_Price", "[Price] > 0");
                t.HasCheckConstraint("CK_SubscriptionPlans_RideCount", "[RideCount] > 0");
                t.HasCheckConstraint("CK_SubscriptionPlans_ValidityDays", "[ValidityDays] > 0");
            });

            entity.HasOne(sp => sp.CardType)
                .WithMany(ct => ct.SubscriptionPlans)
                .HasForeignKey(sp => sp.CardTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new SubscriptionPlan
                {
                    SubscriptionPlanId = 1,
                    Name = "Ogrenci Aylik 100 Binis",
                    Description = "Öğrenci kartlar için 30 gün geçerli 100 binişlik abonman",
                    CardTypeId = 2,
                    Price = 250,
                    RideCount = 100,
                    ValidityDays = 30,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new SubscriptionPlan
                {
                    SubscriptionPlanId = 2,
                    Name = "Tam Aylik 60 Binis",
                    Description = "Tam kartlar için 30 gün geçerli 60 binişlik abonman",
                    CardTypeId = 1,
                    Price = 500,
                    RideCount = 60,
                    ValidityDays = 30,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new SubscriptionPlan
                {
                    SubscriptionPlanId = 3,
                    Name = "Indirimli Aylik 80 Binis",
                    Description = "İndirimli kartlar için 30 gün geçerli 80 binişlik abonman",
                    CardTypeId = 3,
                    Price = 350,
                    RideCount = 80,
                    ValidityDays = 30,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );
        });

        modelBuilder.Entity<CardSubscription>(entity =>
        {
            entity.HasKey(cs => cs.CardSubscriptionId);

            entity.Property(cs => cs.StartDate)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(cs => cs.EndDate)
                .IsRequired();

            entity.Property(cs => cs.RemainingRideCount)
                .IsRequired();

            entity.Property(cs => cs.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Active");

            entity.Property(cs => cs.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            entity.HasIndex(cs => cs.CardId);

            entity.HasIndex(cs => cs.SubscriptionPlanId);

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_CardSubscriptions_RemainingRideCount", "[RemainingRideCount] >= 0");
                t.HasCheckConstraint("CK_CardSubscriptions_DateRange", "[EndDate] >= [StartDate]");
                t.HasCheckConstraint("CK_CardSubscriptions_Status", "[Status] IN ('Active', 'Expired', 'Cancelled')");
            });

            entity.HasOne(cs => cs.Card)
                .WithMany(c => c.CardSubscriptions)
                .HasForeignKey(cs => cs.CardId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(cs => cs.SubscriptionPlan)
                .WithMany(sp => sp.CardSubscriptions)
                .HasForeignKey(cs => cs.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

}