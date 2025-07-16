using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInventory.Core.DTOs
{
    public class InventoryUpdateDTO
    {
        [Required(ErrorMessage = " Name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Adress is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Address is 30 characters.")]
        public string Address { get; set; }
        [Required(ErrorMessage = " City is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the City is 30 characters.")]
        public string City { get; set; }

        
    }
}