﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInventory.Core.DTOs
{
    public class UserDTO
    {
        public string Name { get; init; }
        public DateTime ComputerLeasingTimeEnd { get; init; }
        public string ComputerName { get; init; }
        public string Position { get; init; }
        public int InventoryId { get; init; } 
        public int Id { get; init; }
    }
}