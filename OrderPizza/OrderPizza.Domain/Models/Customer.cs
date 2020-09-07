using System.Collections.Generic;

namespace OrderPizza.Domain.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }

        public string Cpf { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string Phone { get; set; }

        public virtual Address Address { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
