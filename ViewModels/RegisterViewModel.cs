using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Aditek.Utilities;

namespace Aditek.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")] //cim upisemo mail odmah provjeri da li se koristi
        //[ValidEmailDomain(allowedDomain: "gmail.com",
        //    ErrorMessage = "Email domain must be gmail.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
    }
}
