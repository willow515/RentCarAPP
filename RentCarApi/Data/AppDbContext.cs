
using Microsoft.EntityFrameworkCore;
using RentCarApi.Models;

namespace RentCarApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
        public DbSet<MsCustomer> MsCustomers { get; set; }
        public DbSet<MsCar> MsCars { get; set; }
        public DbSet<MsCarImages> MsCarImages { get; set; }
        public DbSet<MsEmployee> MsEmployees { get; set; }
        public DbSet<TrRental> TrRentals { get; set; }
        public DbSet<LtPayment> LtPayments { get; set; }
        public DbSet<TrMaintenance> TrMaintenances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<MsCustomer>(entity =>
            {
                entity.HasKey(e => e.Customer_id);
                entity.Property(e => e.Customer_id).HasMaxLength(36).ValueGeneratedNever();
            });

            modelBuilder.Entity<MsCar>(entity =>
            {
                entity.HasKey(e => e.Car_id);
                entity.Property(e => e.Car_id).HasMaxLength(36).ValueGeneratedNever();
                entity.Property(e => e.Price_per_day).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<MsCarImages>(entity =>
            {
                entity.HasKey(e => e.Image_car_id);
                entity.Property(e => e.Image_car_id).HasMaxLength(36).ValueGeneratedNever();
                entity.Property(e => e.Car_id).HasMaxLength(36);
                
                entity.HasOne<MsCar>()
                      .WithMany()
                      .HasForeignKey(e => e.Car_id)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MsEmployee>(entity =>
            {
                entity.HasKey(e => e.Employee_id);
                entity.Property(e => e.Employee_id).HasMaxLength(36).ValueGeneratedNever();
            });

            modelBuilder.Entity<TrRental>(entity =>
            {
                entity.HasKey(e => e.Rental_id);
                entity.Property(e => e.Rental_id).HasMaxLength(36).ValueGeneratedNever();
                entity.Property(e => e.Total_price).HasColumnType("decimal(18,2)");

                
                entity.HasOne<MsCustomer>()
                      .WithMany()
                      .HasForeignKey(e => e.Customer_id)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne<MsCar>()
                      .WithMany()
                      .HasForeignKey(e => e.Car_id)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<LtPayment>(entity =>
            {
                entity.HasKey(e => e.Payment_id);
                entity.Property(e => e.Payment_id).HasMaxLength(36).ValueGeneratedNever();
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.HasOne<TrRental>()
                      .WithMany()
                      .HasForeignKey(e => e.Rental_id)
                      .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<TrMaintenance>(entity =>
            {
                entity.HasKey(e => e.Maintenance_id);
                entity.Property(e => e.Maintenance_id).HasMaxLength(36).ValueGeneratedNever();
                entity.Property(e => e.Cost).HasColumnType("decimal(18,2)");

                entity.HasOne<MsCar>()
                      .WithMany()
                      .HasForeignKey(e => e.Car_id)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne<MsEmployee>()
                      .WithMany()
                      .HasForeignKey(e => e.Employee_id)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            
            modelBuilder.Entity<MsCar>().HasData(
                new MsCar { Car_id = "C001", Name = "Daihatsu Ayla", Model = "AT", Year = 2019, License_plate = "B 1234 ABC", Number_of_car_seats = 5, Transmission = "Automatic", Price_per_day = 300000M, Status = 1 },
                new MsCar { Car_id = "C002", Name = "Suzuki Ertiga", Model = "GL", Year = 2019, License_plate = "B 5678 DEF", Number_of_car_seats = 7, Transmission = "Automatic", Price_per_day = 450000M, Status = 1 },
                new MsCar { Car_id = "C003", Name = "Honda HR-V", Model = "AT", Year = 2023, License_plate = "B 9012 GHI", Number_of_car_seats = 5, Transmission = "Automatic", Price_per_day = 900000M, Status = 1 }
            );

            modelBuilder.Entity<MsCarImages>().HasData(
                new MsCarImages { Image_car_id = "IMG001", Car_id = "C001", Image_link = "https://www.shutterstock.com/image-photo/silver-hatchback-daihatsu-ayla-12r-260nw-2404487523.jpg" },
                new MsCarImages { Image_car_id = "IMG002", Car_id = "C002", Image_link = "https://thumbs.dreamstime.com/b/bronze-suzuki-ertiga-gl-street-medan-north-sumatra-indonesia-aug-suzuki-ertiga-gl-398161807.jpg" },
                new MsCarImages { Image_car_id = "IMG003", Car_id = "C003", Image_link = "https://cdn.jdpower.com/JDP_2023%20Honda%20HR-V%20EX-L%20Nordic%20Green%20Pearl%20Front%20Quarter%20View.jpg" }
            );

            modelBuilder.Entity<MsCustomer>().HasData(
                new MsCustomer { Customer_id = "CUST001", Email = "putra44@gmail.com", Password = "password123", Name = "Putra", Phone_number = "08123456789", Address = "Jl. Sudirman No. 1" }
            );
        }
    }
}