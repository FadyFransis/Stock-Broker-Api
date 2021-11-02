using App.Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
    public class Order : BaseEntity
    {
        public long Quantity { get; set; }
        public double Price { get; set; }
        public double Commission { get; set; }
        public long PersonId { get; set; }
        public long StockId { get; set; }
        public Person Person { get; set; }
        public Stock Stock { get; set; }

    }
}
