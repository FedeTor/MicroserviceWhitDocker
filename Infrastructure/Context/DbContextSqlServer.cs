using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

public partial class DbContextSqlServer : DbContext
{
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

//public partial class DbContextSqlServer : DbContext
//{
//    private readonly IConfiguration _configuration;

//    public DbContextSqlServer()
//    {
//    }

//    public DbContextSqlServer(DbContextOptions<DbContextSqlServer> options, IConfiguration configuration)
//        : base(options)
//    {
//        _configuration = configuration;
//    }

//    public virtual DbSet<Product> Products { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        if (!optionsBuilder.IsConfigured)
//        {
//            // Agrega logging y opciones de conexión
//            optionsBuilder
//                .EnableSensitiveDataLogging()
//                .EnableDetailedErrors()
//                .UseSqlServer(
//                    _configuration.GetConnectionString("CadenaSQL"),
//                    sqlServerOptionsAction: sqlOptions =>
//                    {
//                        sqlOptions.EnableRetryOnFailure(
//                            maxRetryCount: 5,
//                            maxRetryDelay: TimeSpan.FromSeconds(30),
//                            errorNumbersToAdd: null);
//                    }
//                );
//        }
//        base.OnConfiguring(optionsBuilder);
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Product>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC0725FA5D19");
//            entity.ToTable("Product");
//            entity.Property(e => e.Name).HasMaxLength(100);
//            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
//        });
//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
