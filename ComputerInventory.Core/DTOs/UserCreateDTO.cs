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
        public string Name { get; set; }
        public string? Position { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Email is 30 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string? ComputerName { get; set; }
    }
}