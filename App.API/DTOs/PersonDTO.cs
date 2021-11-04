using System.Collections.Generic;

namespace App.API.DTOs
{
    public class PersonDTO : BaseDTO
    {
        public string Name { get; set; }
        public long BrokerId { get; set; }
        public List<OrderDTO> Orders { get; set; }
        public BrokerDTO Broker { get; set; }
    }
}
