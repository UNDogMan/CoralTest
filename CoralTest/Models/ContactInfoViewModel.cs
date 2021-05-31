using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoralTest.Validation;

namespace CoralTest.Models
{
    public class ContactInfoViewModel : ICloneable
    {   
        [MaxLength(50)]
        [Required(ErrorMessage = "Salutation is required")]
        public string Salutation { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Company is required")]
        public string Company { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [MaxLength(320)]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Confirm Email")]
        [MaxLength(320)]
        [EmailAddress]
        [Required(ErrorMessage = "Confirm Email")]
        [Equals(comparisonProperty: "Email", ErrorMessage = "Emails shoud be equals")]
        public string ConfirmEmail { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [MaxLength(20)]
        public string Fax { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
