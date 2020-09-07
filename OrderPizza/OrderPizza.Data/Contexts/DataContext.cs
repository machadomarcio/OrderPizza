using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrderPizza.Domain.Models;

namespace OrderPizza.Data.Contexts
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Flavor> Flavours { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<PizzaFlavor> PizzaFlavors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasMany(c => c.Pizzas)
                .WithOne(e => e.Order);

            builder.Entity<Pizza>()
                .HasOne(e => e.Order)
                .WithMany(c => c.Pizzas);

            builder.Entity<Customer>()
                .HasOne(a => a.Address)
                .WithOne(b => b.Customer)
                .HasForeignKey<Address>(b => b.CustomerId);

            builder.Entity<PizzaFlavor>()
                .HasKey(bc => new { bc.PizzaId, bc.FlavorId });
            builder.Entity<PizzaFlavor>()
                .HasOne(bc => bc.Pizza)
                .WithMany(b => b.PizzaFlavors)
                .HasForeignKey(bc => bc.PizzaId);
            builder.Entity<PizzaFlavor>()
                .HasOne(bc => bc.Flavor)
                .WithMany(c => c.PizzaFlavors)
                .HasForeignKey(bc => bc.FlavorId);

            builder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(e => e.Customer);

            builder.Entity<Order>()
                .HasOne(e => e.Customer)
                .WithMany(c => c.Orders);
        }

    }
}
