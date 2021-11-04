using System;
using System.Collections.Generic;

namespace App.Core.Models
{
    public class PersonModel : BaseModel
    {
        public string Name { get; set; }
        public long BrokerId { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
        public BrokerModel Broker { get; set; }

    }
}
