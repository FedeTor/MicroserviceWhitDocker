using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class DbContextSqlServer : DbContext
{
    //public DbSet<Product> Products { get; set; }
    //public DbContextSqlServer(DbContextOptions<DbContextSqlServer> options) : base(options)
    //{
    //    try
    //    {
    //        if (!Database.CanConnect())
    //            Database.EnsureCreated();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //    }
    //}


    public DbContextSqlServer()
    {
    }

    public DbContextSqlServer(DbContextOptions<DbContextSqlServer> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC0725FA5D19");

            entity.ToTable("Product");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
