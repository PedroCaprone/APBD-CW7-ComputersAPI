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
    }
}