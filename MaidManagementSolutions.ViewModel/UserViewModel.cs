using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaidManagementSolutions.ViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; } 
        
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number. Please enter a 10-digit numeric number.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "You must accept the terms and conditions.")]
        public bool IsTermsAccepted { get; set; }

        // Add other properties as needed
    }
}
