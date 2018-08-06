using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebApplication1.Models
{
    public class LoginSignupModel : ILoginSignup
    {
        
        public int Id { get; set; }
        
        [Display(Name="UserName")]
        [StringLength(25 , MinimumLength = 5)] 
        [Required(ErrorMessage ="Username should not be Empty")]
        public string UserName{ get; set; }
        
        [Required(ErrorMessage ="Password should not be Empty")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]  
        [StringLength(25 , MinimumLength = 5)] 
        public string Password{ get; set; }

        [Display(Name = "FirstName")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "FirstName should not be Empty")]
        public string FirstName{ get; set; }

        [Display(Name = "MiddleName")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "MiddleName should not be Empty")]
        public string MiddleName { get; set; }

        [Display(Name = "SureName")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "SureName should not be Empty")]
        public string SureName { get; set; }

        [Display(Name = "Qualification")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Qualification should not be Empty")]
        public string Qualification { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Address should not be Empty")]
        public string  Address{ get; set; }

        [Display(Name = "Contact")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Password should not be Empty")]
        public string Contact { get; set; }

        [Display(Name = "Department")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Password should not be Empty")]
        public string Department { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Password should not be Empty")]
        public string Email { get; set; }
    }
    public class Login
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Username should not be Empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password should not be Empty")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]        
        public string Password { get; set; }
    }
}
