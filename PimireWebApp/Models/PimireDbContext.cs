using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PimireWebApp.Models;

public partial class PimireDbContext : DbContext
{
    public PimireDbContext()
    {
    }

    public PimireDbContext(DbContextOptions<PimireDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Enquiry> Enquiries { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductSubCategory> ProductSubCategories { get; set; }

    public virtual DbSet<ProductSubCategoryDetail> ProductSubCategoryDetails { get; set; }

    public virtual DbSet<RoleType> RoleTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //Scaffold-DbContext "Server=.\SQLExpress;Database=PimireDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
        //=> optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=PimireDB;Trusted_Connection=True;TrustServerCertificate=True");
    => optionsBuilder.UseSqlServer("Data Source=SQL8006.site4now.net;Initial Catalog=db_aa4d54_pimiredb;User Id=db_aa4d54_pimiredb_admin;Password=Business!@#0302");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enquiry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Enquiry__3214EC073AE1F277");

            entity.ToTable("Enquiry");

            entity.Property(e => e.Comments).HasMaxLength(2000);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerAddress).HasMaxLength(500);
            entity.Property(e => e.CustomerEmail).HasMaxLength(60);
            entity.Property(e => e.CustomerMobile).HasMaxLength(12);
            entity.Property(e => e.CustomerName).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Enquiries)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enquiry__Categor__33D4B598");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Enquiries)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enquiry__SubCate__34C8D9D1");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ErrorLog__3214EC07E21AB101");

            entity.ToTable("ErrorLog");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductC__3214EC0765561E5C");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductSubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductS__3214EC0759AF638D");

            entity.ToTable("ProductSubCategory");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.ProductSubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSu__Categ__35BCFE0A");
        });

        modelBuilder.Entity<ProductSubCategoryDetail>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId).HasName("PK__ProductS__26BE5B1929B980F3");

            entity.Property(e => e.SubCategoryId).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("ImageURL");
            entity.Property(e => e.MfgDate).HasColumnType("date");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.SubCategory).WithOne(p => p.ProductSubCategoryDetail)
                .HasForeignKey<ProductSubCategoryDetail>(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSu__SubCa__36B12243");
        });

        modelBuilder.Entity<RoleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleType__3214EC0780626984");

            entity.ToTable("RoleType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0758E32FB5");

            entity.ToTable("User");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(60);
            entity.Property(e => e.MobileNumber).HasMaxLength(12);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.RoleType).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleTypeId__37A5467C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
