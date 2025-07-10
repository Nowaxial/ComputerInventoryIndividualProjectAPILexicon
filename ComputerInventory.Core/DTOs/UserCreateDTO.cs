using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInventory.Core.DTOs
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = " Name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; init; }

        public string? Position { get; init; }

        [Required(ErrorMessage = "Email is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Email is 30 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; init; }

        [Required(ErrorMessage = "InventoryId is a required field.")]
        public int InventoryId { get; init; }
        //public int Id { get; init; }
    }
}