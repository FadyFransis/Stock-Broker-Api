using System.Collections.Generic;

namespace App.Core.Models
{
    public class BrokerModel : BaseModel
    {
        public string Name { get; set; }
        public List<PersonModel> Persons { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
