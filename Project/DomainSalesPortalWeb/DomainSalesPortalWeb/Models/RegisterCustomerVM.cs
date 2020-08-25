using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DomainSalesPortalWeb.Models
{
    public class RegisterCustomerVM
    {


        [Required(ErrorMessage = "Please enter Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter SurName")]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public DateTime RegisteredDate { get; set; }
    }
}
