using App.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public long BrokerId { get; set; }
        public ICollection<Order>  Orders{ get; set; }
        public Broker Broker { get; set; }
    }
}
