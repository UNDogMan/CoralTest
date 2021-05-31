using CoralTest.Models;
using CoralTest.Entity;
using CoralTest.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using AspNetCore.ReCaptcha;

namespace CoralTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetFormByName(string name)
        {
            switch (name)
            {
                case "ContactInfo":
                    return ContactInfo();
                case "Areas":
                    return Areas();
                case "Address":
                    return Address();
                case "Password":
                    return Password();
                case "Completed":
                    return Completed();
                default:
                    return NotFound();
            }
        }

        [HttpGet]
        public IActionResult ContactInfo()
        {
            Response.Headers["Number"] = "1";
            Response.Headers["CurrentForm"] = "Contact Info";
            var contactInfo = HttpContext.Session.Get<ContactInfoViewModel>("ContactInfo");
            return View("ContactInfo", contactInfo);
        }

        [HttpPost]
        public IActionResult ContactInfo([Bind("Salutation", "FirstName", "LastName", "MiddleName", "Company", 
            "Title", "Email", "ConfirmEmail", "Phone", "Fax", "Mobile")]ContactInfoViewModel contactInfo)
        {
            Response.Headers["Number"] = "1";
            Response.Headers["CurrentForm"] = "Contact Info";
            if (ModelState.IsValid)
            {
                HttpContext.Session.Set("ContactInfo", contactInfo);
                string form = Request.Headers["action"];
                return GetFormByName(form);
            }
            return View(contactInfo);
        }

        [HttpGet]
        public IActionResult Areas()
        {
            Response.Headers["Number"] = "2";
            Response.Headers["CurrentForm"] = "Areas";
            var areas = HttpContext.Session.Get<AreasViewModel>("Areas");
            return View("Areas", areas);
        }

        [HttpPost]
        public IActionResult Areas([Bind("Comments", "BusinessAreas")]AreasViewModel areas)
        {
            Response.Headers["Number"] = "2";
            Response.Headers["CurrentForm"] = "Areas";
            if (ModelState.IsValid)
            {
                HttpContext.Session.Set("Areas", areas);
                string form = Request.Headers["action"];
                return GetFormByName(form);
            }
            return View(areas);
        }

        [HttpGet]
        public IActionResult Address()
        {
            Response.Headers["Number"] = "3";
            Response.Headers["CurrentForm"] = "Address";

            var addressView = HttpContext.Session.Get<AddressViewModel>("Address");
            return View("Address", addressView);
        }

        [HttpPost]
        public IActionResult Address([Bind("Country", "OfficeName", "Address", 
            "PostalCode", "City", "State")] AddressViewModel addressView)
        {
            Response.Headers["Number"] = "3";
            Response.Headers["CurrentForm"] = "Address";
            if (ModelState.IsValid)
            {
                HttpContext.Session.Set("Address", addressView);
                string form = Request.Headers["action"];
                return GetFormByName(form);
            }
            return View(addressView);
        }

        [HttpGet]
        public IActionResult Password()
        {
            Response.Headers["Number"] = "4";
            Response.Headers["CurrentForm"] = "Password";
            var password = HttpContext.Session.Get<PasswordViewModel>("Password");
            return View("Password", password);

        }

        [ValidateReCaptcha]
        [HttpPost]
        public IActionResult Password([Bind("Password", "ConfirmPassword", "TermOfUse")]PasswordViewModel passwordView)
        {
            Response.Headers["Number"] = "4";
            Response.Headers["CurrentForm"] = "Password";
            if (ModelState.IsValid)
            {
                HttpContext.Session.Set("Password", passwordView);
                string form = Request.Headers["action"];
                return GetFormByName(form);
            }
            return View(passwordView);
        }

        public IActionResult Completed()
        {
            Response.Headers["Number"] = "5";
            Response.Headers["CurrentForm"] = "Completed";

            User user = new User();
            var contactInfo = HttpContext.Session.Get<ContactInfoViewModel>("ContactInfo");
            var areas = HttpContext.Session.Get<AreasViewModel>("Areas");
            var address = HttpContext.Session.Get<AddressViewModel>("Address");
            var password = HttpContext.Session.Get<PasswordViewModel>("Password");

            var contactInfoContext = new ValidationContext(contactInfo);
            var areasContext = new ValidationContext(areas);
            var addressContext = new ValidationContext(address);
            var passwordContext = new ValidationContext(password);
            if (!Validator.TryValidateObject(contactInfo, contactInfoContext, null) ||
                !Validator.TryValidateObject(areas, areasContext, null) ||
                !Validator.TryValidateObject(address, addressContext, null) ||
                !Validator.TryValidateObject(password, passwordContext, null))
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }

            user.Salutation = contactInfo.Salutation;
            user.FirstName = contactInfo.FirstName;
            user.LastName = contactInfo.LastName;
            user.MiddleName = contactInfo.MiddleName;
            user.Company = contactInfo.Company;
            user.Title = contactInfo.Title;
            user.Email = contactInfo.Email;
            user.Phone = contactInfo.Phone;
            user.Fax = contactInfo.Fax;
            user.Mobile = contactInfo.Mobile;

            user.Comments = areas.Comments;
            user.BusinessAreas = areas.BusinessAreas;

            user.Country = address.Country;
            user.OfficeName = address.OfficeName;
            user.Address = address.Address;
            user.PostalCode = address.PostalCode;
            user.City = address.City;
            user.State = address.State;

            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(Encoding.UTF8.GetBytes(password.Password));
            user.Password = Convert.ToBase64String(sha1data);

            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.Clear();

            return View("Completed");
        }
    }
}
