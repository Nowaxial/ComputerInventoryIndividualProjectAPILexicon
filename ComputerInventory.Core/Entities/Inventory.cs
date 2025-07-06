using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInventory.Core.Entities
{
    public class Inventory
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Inventory name is a required field.")]
        //[MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        //[Required(ErrorMessage = "Address is a required field.")]
        //[MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
        public string? Address { get; set; }

        //[Required(ErrorMessage = "City is a required field.")]
        //[MaxLength(25, ErrorMessage = "Maximum length for the City is 25 characters")]
        public string? City { get; set; }

        public DateTime StartDateForCheckingInventory { get; set; } = DateTime.Now;

        // Navigation property to the collection of employees
        public ICollection<User>? Users { get; set; }
    }
}