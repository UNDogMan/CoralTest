using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoralTest.Entity;
using CoralTest.Validation;

namespace CoralTest.Models
{
    public class AreasViewModel : ICloneable
    {
        [NotNullOrEmptyCollection(ErrorMessage = "Please select your personal business area")]
        public IList<BusinessAreas> BusinessAreas { get; set; }

        [Required(ErrorMessage = "Comments is required")]
        public string Comments { get; set; }

        public AreasViewModel()
        {
            BusinessAreas = new List<BusinessAreas>();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
