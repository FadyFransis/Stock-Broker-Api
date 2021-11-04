using System.Collections.Generic;

namespace App.API.DTOs
{
    public class BrokerDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<PersonDTO> Persons { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
