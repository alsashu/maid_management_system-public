using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaidManagementSolutions.ViewModel
{
    // Enums/LanguageEnum.cs
    public enum LanguageEnum
    {
        Hindi, English, Kannada, Malayalam, Telugu, Tamil, Gujarati, Bengali, Nepali
        // Add other languages as needed
    }

    // Enums/GenderEnum.cs
    public enum GenderEnum
    {
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Female")]
        Female,
        [Display(Name = "Other")]
        Other,
    }

    // Enums/WorkingHoursEnum.cs
    public enum WorkingHoursEnum
    {
        [Display(Name = "Par tTime ( Hourly basis)")]
        PartTime,
        [Display(Name = "FullTime ( 8-10 Hours)")]
        FullTime,
        [Display(Name = "Live-in ( 24 hours, Stay with Client)")]
        Live_in,
    }

    // Enums/MaritalStatusEnum.cs
    public enum MaritalStatusEnum
    {
        [Display(Name = "Married")]
        Married,
        [Display(Name = "Unmarried")]
        Unmarried,
        [Display(Name = "Divorced")]
        Divorced,
        [Display(Name = "Widowed")]
        Widowed,
    }

    // Enums/ServicesEnum.cs
    public enum ServicesEnum
    {
        [Display(Name = "Cooking")]
        Cooking,
        [Display(Name = "Patient Care")]
        PatientCare,
        [Display(Name = "Cleaning")]
        Cleaning,
        [Display(Name = "Elder Care")]
        ElderCare,
        [Display(Name = "Kitchen Helper")]
        KitchenHelper,
        [Display(Name = "Baby Sitter")]
        BabySitter,
        [Display(Name = "Cloth Washing")]
        ClothWashing,
        [Display(Name = "Japa Maid")]
        JapaMaid,
    }

}
