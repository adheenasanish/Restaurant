using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurant.Models
{
    public partial class RestaurantContext : DbContext
    {
        public RestaurantContext()
        {
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<FoodItem> FoodItem { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server= DESKTOP-UQ8S2PA\\SQLEXPRESS;Database=Restaurant;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("customer_Id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileStatus).HasColumnName("profileStatus");

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("userId");
            });

            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.HasKey(e => e.FoodId);

                entity.ToTable("foodItem");

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(250);

                entity.Property(e => e.ItemCategory)
                    .HasColumnName("itemCategory")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("unitPrice")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__Menu__food_id__2B0A656D");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.OrderItemId).HasColumnName("orderItemId");

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.OrderId).HasColumnName("order_Id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("money");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__OrderItem__food___2DE6D218");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__order__2EDAF651");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).HasColumnName("order_Id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("orderDate")
                    .HasColumnType("date");

                entity.Property(e => e.PickupTime).HasColumnName("pickupTime");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__customer__1DB06A4F");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("paymentDate")
                    .HasColumnType("date");

                entity.Property(e => e.PaymentId)
                    .HasColumnName("payment_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PaymentType)
                    .HasColumnName("paymentType")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Payment__custome__245D67DE");

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.Payment)
                    .HasForeignKey<Payment>(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payment__order_i__25518C17");
            });
        }
    }
}
