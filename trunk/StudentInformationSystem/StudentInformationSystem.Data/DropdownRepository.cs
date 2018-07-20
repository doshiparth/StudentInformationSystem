using System;
using System.Collections.Generic;
using System.Linq;

using StudentInformationSystem.Entities;
using StudentInformationSystem.Entities.ViewModels;

namespace StudentInformationSystem.Data
{
    public class DropdownRepository
    {
        private StudentInformationSystemEntities _context;

        public DropdownRepository(StudentInformationSystemEntities context)
        {
            this._context = context;
        }

        public List<tblState> GetState()
        {
            var state = (from s in _context.tblState select s).ToList();
            return state;
        }

        public List<tblCity> GetCity()
        {
            var city = (from s in _context.tblCity select s).ToList();
            return city;
        }

        public List<tblCollege> GetCollege()
        {
            var college = (from s in _context.tblCollege select s).ToList();
            return college;
        }

        public List<tblDepartment> GetDepartment()
        {
            var department = (from s in _context.tblDepartment select s).ToList();
            return department;
        }

        //get city list of dropdown from respective state
        public List<tblCity> GetCityId(Guid stateId)
        {
            var city = (from s in _context.tblCity
                        where s.StateID == stateId
                        select s).ToList();
            return city;
        }

        //get department list of dropdown from respective college
        public List<tblDepartment> GetDepartmentById(Guid collegeId)
        {
            var department = (from s in _context.tblDepartment
                              where s.CollegeId == collegeId
                              select s).ToList();
            return department;
        }
    }
}
