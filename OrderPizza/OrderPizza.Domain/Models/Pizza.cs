using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace OrderPizza.Domain.Models
{
    public class Pizza
    {
        public Pizza()
        {
            PizzaFlavors = new List<PizzaFlavor>();
        }
        public int Id { get; set; }

        public virtual List<PizzaFlavor> PizzaFlavors { get; set; }

        public int Orderid { get; set; }

        public virtual Order Order { get; set; }

        public decimal Value { get; set; }
        
    }
}
