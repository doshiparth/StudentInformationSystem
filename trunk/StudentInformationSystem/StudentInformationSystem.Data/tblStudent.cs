//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentInformationSystem.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblStudent
    {
        public tblStudent()
        {
            this.tblStudentHistory = new HashSet<tblStudentHistory>();
        }
    
        public System.Guid StudentID { get; set; }
        public string StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public System.Guid CityID { get; set; }
    
        public virtual tblCity tblCity { get; set; }
        public virtual ICollection<tblStudentHistory> tblStudentHistory { get; set; }
    }
}
