using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CarMarket.Models
{
    public partial class CarDatabaseContext : DbContext
    {
        public CarDatabaseContext()
        {
        }

        public CarDatabaseContext(DbContextOptions<CarDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CarImages> CarImages { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=CarDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.CarId).HasColumnName("carId");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasColumnName("brand")
                    .HasMaxLength(50);

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50);

                entity.Property(e => e.DeliveryId).HasColumnName("deliveryId");

                entity.Property(e => e.Engine)
                    .HasColumnName("engine")
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasMaxLength(50);

                entity.Property(e => e.OilType)
                    .HasColumnName("oilType")
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Delivery");
            });

            modelBuilder.Entity<CarImages>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.CarId).HasColumnName("carId");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.Image).HasColumnName("imageName");

                entity.HasOne(d => d.ImageNavigation)
                    .WithMany(p => p.CarImages)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarImages_Car");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.Property(e => e.DeliveryId).HasColumnName("deliveryId");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Provider)
                    .IsRequired()
                    .HasColumnName("provider")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
