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
    
    public partial class tblException
    {
        public System.Guid LogID { get; set; }
        public string FileName { get; set; }
        public Nullable<System.DateTime> ExceptionDate { get; set; }
        public string ExceptionMsg { get; set; }
        public Nullable<long> ExceptionErrorCode { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}