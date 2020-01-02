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
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<CategoryFoodType> CategoryFoodType { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<FoodCategory> FoodCategory { get; set; }
        public virtual DbSet<FoodItem> FoodItem { get; set; }
        public virtual DbSet<FoodType> FoodType { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }

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

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.Property(e => e.CartItemId).HasColumnName("cartItemId");

                entity.Property(e => e.CartId).HasColumnName("cartId");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("date");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItem)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__CartItem__cartId__6CD828CA");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItem)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__CartItem__produc__6BE40491");
            });

            modelBuilder.Entity<CategoryFoodType>(entity =>
            {
                entity.ToTable("category_foodType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.TypeId).HasColumnName("typeId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryFoodType)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__category___categ__3F115E1A");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CategoryFoodType)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__category___typeI__40058253");
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

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("foodCategory");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("categoryName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.HasKey(e => e.FoodId);

                entity.ToTable("foodItem");

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.FoodTypeId).HasColumnName("foodType_id");

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

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("unitPrice")
                    .HasColumnType("money");

                entity.HasOne(d => d.FoodType)
                    .WithMany(p => p.FoodItem)
                    .HasForeignKey(d => d.FoodTypeId)
                    .HasConstraintName("FK__foodItem__foodTy__3493CFA7");
            });

            modelBuilder.Entity<FoodType>(entity =>
            {
                entity.ToTable("foodType");

                entity.Property(e => e.FoodTypeId).HasColumnName("foodType_id");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.TypeName)
                    .HasColumnName("typeName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FoodType)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__foodType__catego__3C34F16F");
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

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderItemId);

                entity.Property(e => e.OrderItemId).HasColumnName("orderItemId");

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.Hstgst)
                    .HasColumnName("hstgst")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OrderId).HasColumnName("order_Id");

                entity.Property(e => e.Pst)
                    .HasColumnName("pst")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__OrderItem__food___2DE6D218");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__order__2EDAF651");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).HasColumnName("order_Id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.PayementStatus)
                    .HasColumnName("payementStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__customer__1DB06A4F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__userId__6FB49575");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.Property(e => e.PaymentId)
                    .HasColumnName("paymentId")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cart)
                    .HasColumnName("cart")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Custom)
                    .HasColumnName("custom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Intent)
                    .HasColumnName("intent")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayerCountryCode)
                    .HasColumnName("payerCountryCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayerEmail)
                    .HasColumnName("payerEmail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayerFirstName)
                    .HasColumnName("payerFirstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayerId)
                    .HasColumnName("payerID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PayerLastName)
                    .HasColumnName("payerLastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayerMiddleName)
                    .HasColumnName("payerMiddleName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayerStatus)
                    .HasColumnName("payerStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMethod)
                    .HasColumnName("paymentMethod")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentState)
                    .HasColumnName("paymentState")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.CartId);

                entity.Property(e => e.CartId).HasColumnName("cartId");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("date");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasMaxLength(450);
            });
        }
    }
}
