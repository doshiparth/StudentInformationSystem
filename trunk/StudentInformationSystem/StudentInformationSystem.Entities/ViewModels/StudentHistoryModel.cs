using System;

using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Entities.ViewModels
{
    public class StudentHistoryModel
    {
        public System.Guid StudentID { get; set; }

        ////[Required(ErrorMessage = "First Name is Required")]
        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }

        ////[Required(ErrorMessage = "Last Name is Required")]
        //[Display(Name = "Last Name")]
        //public string LastName { get; set; }
        
        //[Display(Name = "Name")]
        //public string FullName
        //{
        //    get { return FirstName + " " + LastName; }
        //}
        
        //public System.Guid StudentHistoryID { get; set; }
        
        public int Semester { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateOfJoining { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Display(Name = "College Name")]
        public string CollegeName { get; set; }

        public string EmailAddress { get; set; }
    }
}
