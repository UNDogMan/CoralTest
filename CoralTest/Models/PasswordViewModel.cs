using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoralTest.Validation;

namespace CoralTest.Models
{
    public class PasswordViewModel : ICloneable
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])([^\s]){8,}$",
            ErrorMessage = "Password shoud be stroger")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password")]
        [DataType(DataType.Password)]
        [Equals(comparisonProperty: "Password", ErrorMessage = "Passwords shoud be equals")]
        public string ConfirmPassword { get; set; }

        [IsTrue(ErrorMessage = "Confirm Requared")]
        public bool TermOfUse { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
