using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class CVGSContext : DbContext
    {
        public CVGSContext()
        {
        }

        public CVGSContext(DbContextOptions<CVGSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePayCategory> EmployeePayCategories { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }
        public virtual DbSet<EsrbContentDescriptor> EsrbContentDescriptors { get; set; }
        public virtual DbSet<EsrbRating> EsrbRatings { get; set; }
        public virtual DbSet<EventLog> EventLogs { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameCategory> GameCategories { get; set; }
        public virtual DbSet<GameCompany> GameCompanies { get; set; }
        public virtual DbSet<GameEsrbContentDescriptor> GameEsrbContentDescriptors { get; set; }
        public virtual DbSet<GamePerspective> GamePerspectives { get; set; }
        public virtual DbSet<GameStatus> GameStatuses { get; set; }
        public virtual DbSet<GameSubCategory> GameSubCategories { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationType> LocationTypes { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Population> Populations { get; set; }
        public virtual DbSet<PopulationClassification> PopulationClassifications { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Sku> Skus { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierContact> SupplierContacts { get; set; }
        public virtual DbSet<CartItem> ShoppingCartItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=BANGDITO\\SQLEXPRESS;Initial Catalog=CVGS;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AI");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("Country_PK");

                entity.ToTable("Country");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.Alpha2Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("CreditCard_PK");

                entity.ToTable("CreditCard");

                entity.Property(e => e.Code)
                    .HasMaxLength(15)
                    .IsFixedLength(true);

                entity.Property(e => e.CardNumberPrefixList)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength(true);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("Department_PK");

                entity.ToTable("Department");

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("Employee_PK");

                entity.ToTable("Employee");

                entity.Property(e => e.PersonId).ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.Gln)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsFixedLength(true);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.PayCategoryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.PositionCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.TerminationDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Unknown')");

                entity.HasOne(d => d.DepartmentCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Department_FK");

                entity.HasOne(d => d.GlnNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Gln)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Location_FK");

                entity.HasOne(d => d.PayCategoryCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PayCategoryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_EmployeePayCategory_FK");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Person_FK");

                entity.HasOne(d => d.PositionCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_EmployeePosition_FK");
            });

            modelBuilder.Entity<EmployeePayCategory>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("EmployeePayCategory_PK");

                entity.ToTable("EmployeePayCategory");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<EmployeePosition>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("EmployeePosition_PK");

                entity.ToTable("EmployeePosition");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EsrbContentDescriptor>(entity =>
            {
                entity.ToTable("EsrbContentDescriptor");

                entity.Property(e => e.EnglishDescriptor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FrenchDescriptor)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EsrbRating>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("EsrbRatingCode_PK");

                entity.ToTable("EsrbRating");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishRating)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FrenchRating)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("EventLog_PK")
                    .IsClustered(false);

                entity.ToTable("EventLog");

                entity.HasIndex(e => e.Date, "EventLog_Date_IX")
                    .IsClustered();

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasDefaultValueSql("('INFORMATION')")
                    .IsFixedLength(true);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Detail).HasMaxLength(4000);

                entity.Property(e => e.Event).HasMaxLength(6);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("Game_PK");

                entity.ToTable("Game");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.EnglishDescription).HasMaxLength(4000);

                entity.Property(e => e.EnglishDetail).HasMaxLength(4000);

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.EnglishPlayerCount).HasMaxLength(30);

                entity.Property(e => e.EnglishTrailer).HasMaxLength(4000);

                entity.Property(e => e.EsrbRatingCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsFixedLength(true);

                entity.Property(e => e.FrenchDescription).HasMaxLength(4000);

                entity.Property(e => e.FrenchDetail).HasMaxLength(4000);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.FrenchPlayerCount).HasMaxLength(30);

                entity.Property(e => e.FrenchTrailer).HasMaxLength(4000);

                entity.Property(e => e.GamePerspectiveCode)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.GameStatusCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Unknown')");

                entity.HasOne(d => d.EsrbRatingCodeNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.EsrbRatingCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Game_EsrbRating_FK");

                entity.HasOne(d => d.GameCategory)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GameCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Game_GameCategory_FK");

                entity.HasOne(d => d.GamePerspectiveCodeNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GamePerspectiveCode)
                    .HasConstraintName("Game_GamePerspective_FK");

                entity.HasOne(d => d.GameStatusCodeNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GameStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Game_GameStatus_FK");

                entity.HasOne(d => d.GameSubCategory)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GameSubCategoryId)
                    .HasConstraintName("Game_GameSubCategory_FK");

                entity.Property(e => e.MSRP).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<GameCategory>(entity =>
            {
                entity.ToTable("GameCategory");

                entity.Property(e => e.EnglishCategory)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FrenchCategory)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<GameCompany>(entity =>
            {
                entity.ToTable("GameCompany");

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<GameEsrbContentDescriptor>(entity =>
            {
                entity.HasKey(e => new { e.GameGuid, e.EsrbContentDescriptorId })
                    .HasName("GameEsrbContentDescriptor_PK");

                entity.ToTable("GameEsrbContentDescriptor");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Unknown')");

                entity.HasOne(d => d.EsrbContentDescriptor)
                    .WithMany(p => p.GameEsrbContentDescriptors)
                    .HasForeignKey(d => d.EsrbContentDescriptorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GameEsrbContentDescriptor_EsrbContentDescriptor_FK");

                entity.HasOne(d => d.GameGu)
                    .WithMany(p => p.GameEsrbContentDescriptors)
                    .HasForeignKey(d => d.GameGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GameEsrbContentDescriptor_Game_FK");
            });

            modelBuilder.Entity<GamePerspective>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("GamePerspective_PK");

                entity.ToTable("GamePerspective");

                entity.Property(e => e.Code)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishPerspectiveName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsFixedLength(true);

                entity.Property(e => e.FrenchPerspectiveName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<GameStatus>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("GameStatus_PK");

                entity.ToTable("GameStatus");

                entity.Property(e => e.Code)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishCategory)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.FrenchCategory)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<GameSubCategory>(entity =>
            {
                entity.ToTable("GameSubCategory");

                entity.Property(e => e.EnglishCategory)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FrenchCategory)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.ProductGuid, e.LocationGln })
                    .HasName("Inventory_PK");

                entity.ToTable("Inventory");

                entity.Property(e => e.LocationGln)
                    .HasMaxLength(11)
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Unknown')");

                entity.HasOne(d => d.LocationGlnNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.LocationGln)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Inventory_Location_FK");

                entity.HasOne(d => d.ProductGu)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ProductGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Inventory_Product_FK");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Gln)
                    .HasName("Location_PK");

                entity.ToTable("Location");

                entity.Property(e => e.Gln)
                    .HasMaxLength(11)
                    .HasDefaultValueSql("('?')")
                    .IsFixedLength(true);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.Fax).HasMaxLength(22);

                entity.Property(e => e.LocalPhone)
                    .IsRequired()
                    .HasMaxLength(22);

                entity.Property(e => e.LocationTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ProvinceCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.Sequence).ValueGeneratedOnAdd();

                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.TollFreePhone).HasMaxLength(22);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Unknown')");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Location_Country_FK");

                entity.HasOne(d => d.LocationTypeCodeNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.LocationTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Location_LocationType_FK");

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.ProvinceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Location_Province_FK");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Location_Region_FK");
            });

            modelBuilder.Entity<LocationType>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("LocationType_PK");

                entity.ToTable("LocationType");

                entity.Property(e => e.Code)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.HasIndex(e => new { e.Surname, e.GivenName }, "Person_SurnameGivenName_IX");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.Email).HasMaxLength(60);

                entity.Property(e => e.Extension).HasMaxLength(6);

                entity.Property(e => e.Fax).HasMaxLength(22);

                entity.Property(e => e.GivenName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LandLine).HasMaxLength(22);

                entity.Property(e => e.Mobile).HasMaxLength(22);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Unknown')");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Person_Country_FK");

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.ProvinceCode)
                    .HasConstraintName("Person_Province_FK");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("Platform_PK");

                entity.ToTable("Platform");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Population>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("Population_PK")
                    .IsClustered(false);

                entity.ToTable("Population");

                entity.HasIndex(e => e.City, "Population_City_IX");

                entity.HasIndex(e => new { e.Surname, e.GivenName }, "Population_SurnameGivenName_IX");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Amex)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.BankCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.BranchAddress).HasMaxLength(60);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.Email).HasMaxLength(60);

                entity.Property(e => e.Extension).HasMaxLength(6);

                entity.Property(e => e.Fax).HasMaxLength(22);

                entity.Property(e => e.FinancialInstitution).HasMaxLength(50);

                entity.Property(e => e.GivenName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Hin)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.LandLine).HasMaxLength(22);

                entity.Property(e => e.MasterCard)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Mobile).HasMaxLength(22);

                entity.Property(e => e.PopulationClassificationCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.Sequence).ValueGeneratedOnAdd();

                entity.Property(e => e.Sin)
                    .HasMaxLength(9)
                    .IsFixedLength(true);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransitNumber)
                    .HasMaxLength(5)
                    .IsFixedLength(true);

                entity.Property(e => e.Visa)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Populations)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Population_Country_FK");

                entity.HasOne(d => d.PopulationClassificationCodeNavigation)
                    .WithMany(p => p.Populations)
                    .HasForeignKey(d => d.PopulationClassificationCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Population_PopulationClassification_FK");

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.Populations)
                    .HasForeignKey(d => d.ProvinceCode)
                    .HasConstraintName("Population_Province_FK");
            });

            modelBuilder.Entity<PopulationClassification>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PopulationClassification_PK");

                entity.ToTable("PopulationClassification");

                entity.Property(e => e.Code)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("Product_PK")
                    .IsClustered(false);

                entity.ToTable("Product");

                entity.HasIndex(e => e.Gtin, "Product_Gtin_IX")
                    .IsClustered();

                entity.HasIndex(e => e.Gtin, "Product_Gtin_Unique")
                    .IsUnique();

                entity.HasIndex(e => e.NewSku, "Product_NewSku_IX");

                entity.HasIndex(e => e.NewSku, "Product_NewSku_Unique")
                    .IsUnique();

                entity.HasIndex(e => e.UsedSku, "Product_UsedSku_IX");

                entity.HasIndex(e => e.UsedSku, "Product_UsedSku_Unique")
                    .IsUnique();

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.AcceptUsed).HasDefaultValueSql("((0))");

                entity.Property(e => e.GameCompanyPartNumber).HasMaxLength(20);

                entity.Property(e => e.Gtin)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsFixedLength(true);

                entity.Property(e => e.Msrp).HasColumnType("money");

                entity.Property(e => e.NewSku)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasDefaultValueSql("('?')")
                    .IsFixedLength(true);

                entity.Property(e => e.NewStorePrice).HasColumnType("money");

                entity.Property(e => e.NewWebPrice).HasColumnType("money");

                entity.Property(e => e.PlatformCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.UsedCustomerCredit)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UsedSku)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasDefaultValueSql("('?')")
                    .IsFixedLength(true);

                entity.Property(e => e.UsedStorePrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UsedWebPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Unknown')");

                entity.HasOne(d => d.Developer)
                    .WithMany(p => p.ProductDevelopers)
                    .HasForeignKey(d => d.DeveloperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_GameCompany_Developer_FK");

                entity.HasOne(d => d.GameGu)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.GameGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_Game_FK");

                entity.HasOne(d => d.PlatformCodeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PlatformCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_Platform_FK");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.ProductPublishers)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_GameCompany_Publisher_FK");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("ProvinceLookup_PK");

                entity.ToTable("Province");

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FederalTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.FederalTaxAcronym)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProvincialTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProvincialTaxAcronym)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.PstOnGst).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FrenchName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedNever()
                    .HasColumnName("reviewId");

                entity.Property(e => e.Approved).HasColumnName("approved");

                entity.Property(e => e.GameGuid).HasColumnName("gameGuid");

                entity.Property(e => e.ReviewContent)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("reviewContent");

                entity.Property(e => e.ReviewDate)
                    .HasColumnType("date")
                    .HasColumnName("reviewDate");

                entity.Property(e => e.ReviewRaiting).HasColumnName("reviewRaiting");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.GameGu)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.GameGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Game");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Person");
            });

            modelBuilder.Entity<Sku>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sku");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.Fax).HasMaxLength(22);

                entity.Property(e => e.LocalPhone)
                    .IsRequired()
                    .HasMaxLength(22);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ProvinceCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.TollFreePhone).HasMaxLength(22);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Unknown')");

                entity.Property(e => e.WebSite).HasMaxLength(60);

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Supplier_Country_FK");

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.ProvinceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Supplier_Province_FK");
            });

            modelBuilder.Entity<SupplierContact>(entity =>
            {
                entity.ToTable("SupplierContact");

                entity.Property(e => e.Email).HasMaxLength(60);

                entity.Property(e => e.Extension).HasMaxLength(6);

                entity.Property(e => e.GivenName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LandLine).HasMaxLength(22);

                entity.Property(e => e.Mobile).HasMaxLength(22);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Unknown')");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierContacts)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SupplierContact_Supplier_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
