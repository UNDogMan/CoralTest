using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoralTest.Validation
{
    public class IsTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(bool)) throw new InvalidOperationException("Can only be used on boolean properties.");

            return (bool)value;
        }
    }
}
