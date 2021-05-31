using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoralTest.Models
{
    public class UserViewModel
    {
        public ContactInfoViewModel ContactInfo { get; set; }
        public AreasViewModel Areas { get; set; }
        public AddressViewModel Address { get; set; }
        public PasswordViewModel Password { get; set; }

        public UserViewModel()
        {
            ContactInfo = new ContactInfoViewModel();
            Areas = new AreasViewModel();
            Address = new AddressViewModel();
            Password = new PasswordViewModel();
        }

        public void Reset()
        {
            ContactInfo = new ContactInfoViewModel();
            Areas = new AreasViewModel();
            Address = new AddressViewModel();
            Password = new PasswordViewModel();
        }
    }
}
