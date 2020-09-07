using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrderPizza.Data.Contexts;
using OrderPizza.Domain.Models;

namespace OrderPizza.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Customer> GetAllCustomers()
        {
            IQueryable<Customer> query = context.Customers;

            query = query.Include(c => c.Address);

            query = query.AsNoTracking().OrderBy(c => c.Id);

            return query.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            IQueryable<Customer> query = context.Customers;

            query = query.Include(c => c.Address);

            query = query.AsNoTracking().OrderBy(c => c.Id).Where(x => x.Id == id);

            return query.FirstOrDefault();
        }

        public Customer GetCustomerByCpf(string cpf)
        {
            IQueryable<Customer> query = context.Customers;

            query = query.Include(c => c.Address);

            query = query.AsNoTracking().OrderBy(c => c.Id).Where(x => x.Cpf == cpf);

            return query.FirstOrDefault();
        }
    }
}
