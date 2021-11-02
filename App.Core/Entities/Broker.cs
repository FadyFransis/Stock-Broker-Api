using App.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
    public class Broker : BaseEntity
    {
        public string Name { get; set; }
        public List <Person> Persons { get; set; }
        public ICollection<Order>  Orders{ get; set; }
    }
}
