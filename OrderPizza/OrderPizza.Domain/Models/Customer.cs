﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OrderPizza.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Cpf { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string Phone { get; set; }

        public virtual Address Address { get; set; }

        public List<Order> Orders { get; set; }
    }
}
