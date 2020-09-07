using System;
using System.Collections.Generic;
using System.Text;

namespace OrderPizza.Domain.Models
{
    public class PizzaFlavor
    {
        public int PizzaId { get; set; }

        public virtual Pizza Pizza { get; set; }

        public int FlavorId { get; set; }

        public virtual Flavor Flavor { get; set; }
    }
}
