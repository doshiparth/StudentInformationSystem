using System;

using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Entities.ViewModels
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string EmailAddress { get; set; }
        
        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
        
        public int RoleID { get; set; }

        public string Role { get; set; }
    }
}
