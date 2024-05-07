using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaidManagementSolutions.ViewModel
{
    public class CompositeViewModel
    {
        public MaidViewModel MaidViewModel { get; set; }
        /// <summary>
        /// Represents the first attached document.
        /// </summary>
        public Documents? Document1 { get; set; }

        /// <summary>
        /// Represents the second attached document.
        /// </summary>
        public Documents? Document2 { get; set; }
    }
    public class MaidViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number. Please enter a 10-digit numeric number.")]
        public string ContactNumber { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Community { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;

        public string[] PreferredLocation { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid pincode. Please enter a 6-digit numeric pincode.")]
        public string? Pincode { get; set; }

        [DisplayName("Language")]
        public List<LanguageEnum> SelectedLanguage { get; set; }
        [DisplayName("Gender")]
        public GenderEnum SelectedGender { get; set; }
        [DisplayName("Working Hours")]
        public WorkingHoursEnum SelectedWorkingHours { get; set; }
        [DisplayName("Marital Status")]
        public MaritalStatusEnum SelectedMaritalStatus { get; set; }
        [DisplayName("Services")]
        public List<ServicesEnum> SelectedServices { get; set; }
        public SelectList? LanguageList { get; set; }
        public SelectList? GenderList { get; set; }
        public SelectList? WorkingHoursList { get; set; }
        public SelectList? MaritalStatusList { get; set; }
        public SelectList? ServicesList { get; set; }

        // Additional properties for file uploads
        public IFormFile? File1 { get; set; }
        public IFormFile? File2 { get; set; }
        public bool IsVerified { get; set; }
        public bool IsBlockListed { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public string ModifiedDate { get; set; } = string.Empty;
        public string Eduction { get; set; } = string.Empty;

        [Display(Name = "Available now")]
        public bool isAvailable { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 99, ErrorMessage = "Age must be between 18 and 99")]
        public int Age { get; set; }

        //[Display(Name = "Available year")]
        //[RegularExpression(@"^\d+$", ErrorMessage = "Invalid year. Please enter a numeric value.")]
        //[ValidYear(ErrorMessage = "Please enter a valid 4-digit year.")]
        public DateTime AvailableFrom { get; set; }

        //[Display(Name = "Available month")]
        //[RegularExpression(@"^\d+$", ErrorMessage = "Invalid month. Please enter a numeric value.")]
        //[ValidMonth(ErrorMessage = "Please enter a valid month (1-12).")]
        //public int? AvailableFromMonths { get; set; }

        [Required(ErrorMessage = "Experience in years is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid year. Please enter a numeric value.")]
        //[ValidYear(ErrorMessage = "Please enter a valid 4-digit year.")]
        public int? ExperienceInYears { get; set; }

        [Required(ErrorMessage = "Experience in Month is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid month. Please enter a numeric value.")]
        //[ValidMonth(ErrorMessage = "Please enter a valid month (1-12).")]
        public int? ExperienceInMonths { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string PreviousEmployerName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number. Please enter a 10-digit numeric number.")]
        public string PreviousEmployerContactNumber { get; set; } = string.Empty;

        public string Availability { get; set; }
    }

    /// <summary>
    /// Represents a document with associated file information.
    /// </summary>
    public class Documents
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string? FileName { get; set; }
        /// <summary>
        /// Gets or sets the binary data of the file.
        /// </summary>
        public byte[]? FileData { get; set; }
        /// <summary>
        /// Gets or sets the type of the file.
        /// </summary>
        public string? FileType { get; set; }
        
    }

    // Enums/LanguageEnum.cs
    
    
}
