using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoralTest.Validation
{
    public class EqualsAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public EqualsAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);

            if (value is null || comparisonValue is null)
                return new ValidationResult(ErrorMessage);

            if (!value.Equals(comparisonValue))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
