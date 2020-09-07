using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OrderPizza.Domain.Models
{
    public class Order
    {
        public Order()
        {
            Pizzas = new List<Pizza>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual List<Pizza> Pizzas { get; set; }

        private decimal _totalValue;  // Backing store

        public decimal TotalValue
        {
            get => _totalValue;
            set
            {
                _totalValue = Pizzas.Sum(x => x.Value);
            }
        }

        public DateTime OrderDate { get; set; }

        public StatusPizza Status { get; set; }
    }

    public enum StatusPizza
    {
        [Description("Em Andamento")]
        InProgress = 1,

        [Description("Em Preparação")]
        InPreparation = 2,

        [Description("Pronto")]
        Done = 3,

        [Description("Saiu para Entrega")]
        OutForDelivery = 4,

        [Description("Entregue")]
        Delivered = 5,

        [Description("Cancelado")]
        Canceled = 6
    }
}
