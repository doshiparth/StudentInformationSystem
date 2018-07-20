using System;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Entities.ViewModels
{
    public class CollegeModel
    {
        public System.Guid CollegeID { get; set; }

        public System.Guid DepartmentID { get; set; }

        public string CollegeCode { get; set; }

        //[Required(ErrorMessage = "College Name is required")]
        [Display(Name = "College Name")]
        public string CollegeName { get; set; }

        [Required(ErrorMessage = "Department Code is required")]
        [Display(Name = "Department Code")]
        public string DepartmentCode { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Total Semester is required")]
        [Display(Name = "Total Semester")]
        public int TotalSem { get; set; }
    }
}
