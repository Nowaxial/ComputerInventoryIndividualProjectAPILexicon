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
        public string? Name { get; init; }

        [Required(ErrorMessage = "Address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
        public string? Address { get; init; }

        //[Required(ErrorMessage = "City is a required field.")]
        //[MaxLength(25, ErrorMessage = "Maximum length for the City is 25 characters")]
        public string? City { get; init; }

    }
}
