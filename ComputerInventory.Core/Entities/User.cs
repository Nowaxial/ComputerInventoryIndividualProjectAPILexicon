using System.ComponentModel.DataAnnotations;

namespace ComputerInventory.Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "IP is a required field.")]
        //public string? IP { get; set; }

        //[Required(ErrorMessage = "MACAddress is a required field.")]
        //public string? MAC { get; set; }

        //[Required(ErrorMessage = "Computer name is a required field.")]
        //[MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? ComputerName { get; set; }


        //[Required(ErrorMessage = "Name is a required field.")]
        //[MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; set; }

        //[Required(ErrorMessage = "Position is a required field.")]
        //[MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string? Position { get; set; }

        //[Required(ErrorMessage = "Email is a required field.")]
        //[EmailAddress]
        //[MaxLength(50, ErrorMessage = "Maximum length for the Email is 50 characters.")]
        public string? Email { get; set; }

        public int InventoryId { get; set; }

        public DateTime ComputerLeasingTimeEnd { get; set; } =DateTime.Now.AddMonths(3);

    }
}