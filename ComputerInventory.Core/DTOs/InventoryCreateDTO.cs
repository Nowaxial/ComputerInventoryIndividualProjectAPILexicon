using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInventory.Core.DTOs
{
    public class InventoryCreateDTO
    {


        [Required(ErrorMessage = "Inventory name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
        public string Address { get; init; }

        [Required(ErrorMessage = "City is a required field.")]
        [MaxLength(15, ErrorMessage = "Maximum length for City is 15 characters")]
        public string City { get; init; }

    }
}
