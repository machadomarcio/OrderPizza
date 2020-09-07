using System;
using System.Collections.Generic;
using System.Text;

namespace OrderPizza.Domain.Models
{
    class Flavor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }
    }
}
