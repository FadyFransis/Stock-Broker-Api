using System.Collections.Generic;

namespace App.Core.Models
{
    public class StockModel : BaseModel
    {
        public string Name { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
