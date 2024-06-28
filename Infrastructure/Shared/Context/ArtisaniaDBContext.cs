using Domain.Publishing.Models.Entities;
using Domain.Publishing.Models.Entities.Orders;
using Domain.Publishing.Models.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Shared.Context;

public class ArtisaniaDBContext:DbContext
{
    public ArtisaniaDBContext()
    {
    }
    
    public ArtisaniaDBContext(DbContextOptions<ArtisaniaDBContext> options):base(options)
    {
    }
    
    public DbSet<Customer>Customers { get; set; }
    public DbSet<Product>Products { get; set; }
    public DbSet<ValorParametro> ValorParametros { get; set; }
    public DbSet<ParametrosPersonalizacion> ParametrosPersonalizaciones { get; set; }
    public DbSet<Caracteristica> Caracteristicas { get; set; }
    public DbSet<Parametro> Parametros { get; set; }
    public DbSet<Imagen> Imagenes { get; set; } 
    
    public DbSet<Order> Orders { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=localhost;Database=test;Uid=root;Pwd=12345678;Port=3307;", serVersion);
        }
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Customer>().ToTable("Customers");
        builder.Entity<Customer>().HasKey(c=>c.Id);
        builder.Entity<Customer>().Property(c=>c.Usuario).IsRequired();
        builder.Entity<Customer>().Property(c=>c.ContraseñaHashed).IsRequired();
        builder.Entity<Customer>().Property(c=>c.Correo).IsRequired();
        builder.Entity<Customer>().Property(c=>c.IsArtisan).IsRequired();
        
        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>()
            .HasOne(p => p.ParametrosPersonalizacion)
            .WithOne()
            .HasForeignKey<ParametrosPersonalizacion>(p => p.ProductId);

        
        builder.Entity<ParametrosPersonalizacion>().ToTable("ParametrosPersonalizacion");
        builder.Entity<ParametrosPersonalizacion>().HasKey(p => p.Id);
        builder.Entity<ParametrosPersonalizacion>()
            .HasMany(p => p.Parametros)
            .WithOne()
            .HasForeignKey(p => p.ParametrosPersonalizacionId);

        builder.Entity<Parametro>().ToTable("Parametro");
        builder.Entity<Parametro>().HasKey(p => p.Id);
        builder.Entity<Parametro>()
            .HasMany(p => p.Valores)
            .WithOne()
            .HasForeignKey(v => v.ParametroId);
        
        builder.Entity<Product>()
            .HasMany(p => p.Caracteristicas)
            .WithOne()
            .HasForeignKey(c => c.ProductId);
        
        builder.Entity<Product>()
            .HasMany(p => p.ImagenesDetalle)
            .WithOne()
            .HasForeignKey(i => i.ProductId);
        
        builder.Entity<Order>().ToTable("Ordenes");
        builder.Entity<Order>().HasKey(o => o.Id);
        builder.Entity<Order>()
            .HasMany(o => o.Parameters)
            .WithOne()
            .HasForeignKey(p => p.OrderId); 
        
        builder.Entity<OrderParameter>().ToTable("OrderParameters");
        builder.Entity<OrderParameter>().HasKey(p => p.Id);
        
    }
}