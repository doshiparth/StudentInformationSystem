using System;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Entities.ViewModels
{
    public class CollegeDetailsModel
    {
        public bool IsDeleted { get; set; }

        public System.Guid CollegeID { get; set; }
     
        [Required]
        [Display(Name = "College Code")]
        public string CollegeCode { get; set; }

        [Required]
        [Display(Name = "College Name")]
        public string CollegeName { get; set; }
    
    }
}
