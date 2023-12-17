using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Market.DAL;

public class ApplicationDbContext : DbContext
{
    #region BackendAndMigrationsConfig

    

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    #endregion

    public DbSet<User> User { get; set; }

    public DbSet<Studia> Studia { get; set; }

    public DbSet<Assortment> Assortments { get; set; }

    public DbSet<Service> Services { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
    
        modelBuilder.Entity<Assortment>()
            .HasOne(a => a.Studia)
            .WithMany(s => s.Assortments)
            .HasForeignKey(a => a.StudiaId);

        modelBuilder.Entity<Service>()
            .HasOne(s => s.Studia)
            .WithMany(st => st.Services)
            .HasForeignKey(s => s.StudiaId); 
        
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Studia) 
            .WithMany() 
            .HasForeignKey(o => o.StudiaId);
    }
    
}

//TODO сделать корзину без связей, но в сервисах через ленивую загрузку по айдишникам стягивать заказы. 

/*
INSERT INTO "Studia" ("Name", "City", "DataCreate", "MedianPrice", "Rating", "TypeStudia", "TypeAdvantages")
VALUES ('Название 1', 'Город 1', '2023-11-25', 100.00, 4.5, 1, 0),
       ('Название 2', 'Город 2', '2023-11-26', 150.00, 4.7, 2, 1);

-- Заполнение таблицы assortment, связанной с studia
INSERT INTO "Assortments" ("Id", "Name", "Price", "AssortmentId")
VALUES (1, 'Товар 1', '50.00', 1),
       (2, 'Товар 2', '75.00', 1),
       (3, 'Товар 3', '80.00', 2);
 */