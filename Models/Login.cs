using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DeliveryMarketplace.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please Enter The Phone Number.")]
        [Display(Name = "Email Address:")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter The Password.")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}