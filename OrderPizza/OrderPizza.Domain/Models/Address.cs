using System;
using System.Collections.Generic;
using System.Text;

namespace OrderPizza.Domain.Models
{
    class Address
    {

        public int Id { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }

        public string Complement { get; set; }

        public string City { get; set; }

        public String State { get; set; }
    }
}
