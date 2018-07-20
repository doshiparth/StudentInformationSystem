using System;

using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Entities.ViewModels
{
    public class StudentInfoModel
    {
        public System.Guid StudentID { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
      
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
