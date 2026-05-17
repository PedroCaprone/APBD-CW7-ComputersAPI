using APBD_CW7_ComputersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW7_ComputersAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PC> PCs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PC>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Weight)
                .HasColumnType("float(5)");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasColumnType("char(10)")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            entity.HasOne(e => e.ComponentManufacturer)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ComponentType)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.HasKey(e => new { e.PCId, e.ComponentCode });

            entity.Property(e => e.ComponentCode)
                .HasColumnType("char(10)")
                .IsRequired();

            entity.HasOne(e => e.PC)
                .WithMany(e => e.PCComponents)
                .HasForeignKey(e => e.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Component)
                .WithMany(e => e.PCComponents)
                .HasForeignKey(e => e.ComponentCode)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(e => e.FullName)
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(e => e.FoundationDate).HasColumnType("date");
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();
        });
        
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1,
                Abbreviation = "ASUS",
                FullName = "ASUS Corporation",
                FoundationDate = new DateTime(1989, 4, 2)
            },
            new ComponentManufacturer
            {
                Id = 2,
                Abbreviation = "MSI",
                FullName = "MSI Corporation",
                FoundationDate = new DateTime(1986, 8, 4)
            },
            new ComponentManufacturer
            {
                Id = 3,
                Abbreviation = "INTEL",
                FullName = "Intel Corporation",
                FoundationDate = new DateTime(1968, 7, 18)
            }
        );

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType
            {
                Id = 1,
                Abbreviation = "CPU",
                Name = "Processor"
            },
            new ComponentType
            {
                Id = 2,
                Abbreviation = "GPU",
                Name = "Graphics Card"
            },
            new ComponentType
            {
                Id = 3,
                Abbreviation = "RAM",
                Name = "Memory"
            }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "CPU001",
                Name = "Intel Core i9",
                Description = "High-end gaming processor",
                ComponentManufacturerId = 3,
                ComponentTypeId = 1
            },
            new Component
            {
                Code = "GPU001",
                Name = "MSI RTX 4080",
                Description = "Gaming graphics card",
                ComponentManufacturerId = 2,
                ComponentTypeId = 2
            },
            new Component
            {
                Code = "RAM001",
                Name = "ASUS DDR5 32GB",
                Description = "DDR5 RAM memory",
                ComponentManufacturerId = 1,
                ComponentTypeId = 3
            }
        );

        modelBuilder.Entity<PC>().HasData(
            new PC
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new PC
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new PC
            {
                Id = 3,
                Name = "Streamer Ultra",
                Weight = 9.8,
                Warranty = 48,
                CreatedAt = new DateTime(2026, 3, 1, 10, 15, 0),
                Stock = 3
            }
        );

        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent
            {
                PCId = 1,
                ComponentCode = "CPU001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 1,
                ComponentCode = "GPU001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 2,
                ComponentCode = "RAM001",
                Amount = 2
            }
        );
        
    }
}