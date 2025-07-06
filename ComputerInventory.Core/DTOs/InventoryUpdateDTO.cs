using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInventory.Core.DTOs
{
    public class InventoryUpdateDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public int Id { get; set; }
    }
}