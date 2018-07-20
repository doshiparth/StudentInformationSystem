using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace StudentInformationSystem.Entities.ViewModels
{
    public class StudentModel
    {
        //From tblStudent - Main table
        public System.Guid StudentID { get; set; }
      
        [Required(ErrorMessage="Student Code is Required")]
        [Display(Name = "Student Code")]
        public string StudentCode { get; set; }
        
        [Required(ErrorMessage = "First Name is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Contact Number is Required")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Enter a 10 digit Mobile Number.")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        
        [Required(ErrorMessage = "Email Address is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        
        [Required(ErrorMessage = "Date of Birth is Required")]
        [Display(Name = "Date Of Birth")]
        //[DisplayFormat(ApplyFormatInEditMode = true)]
        public System.DateTime DateOfBirth { get; set; }
        
        //[Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        //From tblState
        [Display(Name = "State Name")]
        [Required(ErrorMessage = "State Name is Required")]
        public string StateName { get; set; }

        //From tblCity
        [Display(Name = "City Name")]
        //[Required(ErrorMessage = "City Name is Required")]
        public string CityName { get; set; }

        //From tblDepartment
        [Display(Name = "Department Name")]
        //[Required(ErrorMessage = "Department Name is Required")]
        public string DepartmentName { get; set; }

        //From tblCollege
        [Display(Name = "College Name")]
        //[Required(ErrorMessage = "College Name is Required")]
        public string CollegeName { get; set; }

        //From tblStudentHistory
        [Required(ErrorMessage = "Semester is Required")]
        [Display(Name = "Current Semester")]
        public int Semester { get; set; }
        
        [Required(ErrorMessage = "Date Of Joining is Required")]
        [Display(Name = "Date Of Joining")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateOfJoining { get; set; }

        public List<Item> GetState { get { return HelperModel.State; } }
        public List<Item> GetCity { get { return HelperModel.City; } }
        public List<Item> GetDepartment { get { return HelperModel.Department; } }
        public List<Item> GetCollege { get { return HelperModel.College; } }

        //For getting values back to insert and update in the database
        public System.Guid CityID { get; set; }
        public System.Guid CollegeID { get; set; }
        public System.Guid DepartmentID { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

    public class State
    {
        public System.Guid StateID { get; set; }
        public string StateName { get; set; }
    }
    public class City
    {
        public System.Guid CityID { get; set; }
        public string CityName { get; set; }
    }
    public class Department
    {
        public System.Guid DepartmentID { get; set; }
        //public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
    }
    public class College
    {
        public System.Guid CollegeID { get; set; }
        //public string CollegeCode { get; set; }
        public string CollegeName { get; set; }
    }
   
}
