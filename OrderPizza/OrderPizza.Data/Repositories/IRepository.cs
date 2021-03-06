﻿using System.Collections.Generic;
using OrderPizza.Domain.Models;

namespace OrderPizza.Data.Repositories
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        bool SaveChanges();

        List<Customer> GetAllCustomers();

        Customer GetCustomerById(int id);

        Customer GetCustomerByCpf(string cpf);

        List<Flavor> GetAllFlavors();

        Flavor GetFlavorById(int id);

        List<Order> GetAllOrders();

        Order GetOrderById(int id);
    }
}
