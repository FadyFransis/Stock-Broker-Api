using App.Core.Entities.Base;
using System.Collections.Generic;

namespace App.Core.Entities
{
    public class Stock : BaseEntity
    {
        public string Name { get; set; }
        public ICollection <Order> Orders { get; set; }
    }
}
