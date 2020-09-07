using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderPizza.Data.Contexts;
using OrderPizza.Domain.Models;

namespace OrderPizza.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Customer> GetAllCustomers()
        {
            IQueryable<Customer> query = _context.Customers;

            query = query.Include(c => c.Address)
                         .Include(x=> x.Orders)
                         .ThenInclude(p => p.Pizzas)
                         .ThenInclude(f => f.PizzaFlavors)
                         .ThenInclude(f=> f.Flavor);

            query = query.AsNoTracking().OrderBy(c => c.Id);

            return query.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            IQueryable<Customer> query = _context.Customers;

            query = query.Include(c => c.Address);

            query = query.AsNoTracking().OrderBy(c => c.Id).Where(x => x.Id == id);

            return query.FirstOrDefault();
        }

        public Customer GetCustomerByCpf(string cpf)
        {
            IQueryable<Customer> query = _context.Customers;

            query = query.Include(c => c.Address);

            query = query.AsNoTracking().OrderBy(c => c.Id).Where(x => x.Cpf == cpf);

            return query.FirstOrDefault();
        }

        public List<Flavor> GetAllFlavors()
        {
            IQueryable<Flavor> query = _context.Flavors;

            query = query.Include(c => c.PizzaFlavors);

            query = query.AsNoTracking().OrderBy(c => c.Id);

            return query.ToList();
        }

        public Flavor GetFlavorById(int id)
        {
            IQueryable<Flavor> query = _context.Flavors;

            query = query.Include(c => c.PizzaFlavors);

            query = query.AsNoTracking().OrderBy(c => c.Id).Where(x => x.Id == id);

            return query.FirstOrDefault();
        }

        public List<Order> GetAllOrders()
        {
            IQueryable<Order> query = _context.Orders;

            query = query.Include(c => c.Customer)
                         .ThenInclude(ad => ad.Address)
                         .Include(c => c.Pizzas)
                         .ThenInclude(p => p.PizzaFlavors)
                         .ThenInclude(f=> f.Flavor);

            query = query.AsNoTracking().OrderBy(c => c.Id);

            return query.ToList();
        }

        public Order GetOrderById(int id)
        {
            IQueryable<Order> query = _context.Orders;

            query = query
                .Include(c => c.Customer)
                .ThenInclude(ad => ad.Address)
                .Include(c => c.Pizzas)
                .ThenInclude(p => p.PizzaFlavors)
                .ThenInclude(f => f.Flavor);

            query = query.AsNoTracking().OrderBy(c => c.Id).Where(x => x.Id == id);

            return query.FirstOrDefault();
        }
    }
}
