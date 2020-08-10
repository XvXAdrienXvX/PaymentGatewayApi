using BusinessEntites.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public partial class PaymentDBContext : DbContext
    {
        public PaymentDBContext()
        {
        }

        public PaymentDBContext(DbContextOptions<PaymentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CardDetails> CardDetails { get; set; }
        public virtual DbSet<CardType> CardType { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Merchant> Merchant { get; set; }
        public virtual DbSet<MerchantAccount> MerchantAccount { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=PaymentDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<CardDetails>(entity =>
            {
                entity.Property(e => e.CardDetailsId).HasColumnName("CardDetailsID");

                entity.Property(e => e.CardTypeId).HasColumnName("CardTypeID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Cvv).HasColumnName("CVV");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.HasOne(d => d.CardType)
                    .WithMany(p => p.CardDetails)
                    .HasForeignKey(d => d.CardTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CardDetails_CardTypeID_CardType_CardTypeID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CardDetails)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CardDetails_CustomerID_Customer_CustomerID");
            });

            modelBuilder.Entity<CardType>(entity =>
            {
                entity.Property(e => e.CardTypeId).HasColumnName("CardTypeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.Property(e => e.MerchantId).HasColumnName("MerchantID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MerchantAccount>(entity =>
            {
                entity.Property(e => e.MerchantAccountId).HasColumnName("MerchantAccountID");

                entity.Property(e => e.MerchantId).HasColumnName("MerchantID");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.MerchantAccount)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MerchantAccount_MerchantID_Merchant_MerchantID");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.CardDetailsId).HasColumnName("CardDetailsID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.MerchantId).HasColumnName("MerchantID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProcessedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CardDetails)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CardDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_CardTypeID_CardDetails_CardDetailsID");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_CurrencyID_Currency_CurrencyID");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_MerchantID_Merchant_MerchantID");
            });
        }
    }
}
