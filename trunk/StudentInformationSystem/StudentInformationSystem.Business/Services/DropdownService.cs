using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Data;

namespace StudentInformationSystem.Business.Services
{
    public static class DropdownService
    {
        static private StudentInformationSystemEntities _context = new StudentInformationSystemEntities();
        static DropdownRepository dropDownRepository = new DropdownRepository(_context);

        public static List<State> GetState()
        {
            List<tblState> state = dropDownRepository.GetState();
            List<State> list = new List<State>();
            foreach (var item in state)
            {
                State stateobj = new State()
                {
                    StateID = item.StateID,
                    StateName = item.StateName
                };
                list.Add(stateobj);
            }
            return list;
        }

        public static List<City> GetCity()
        {
            List<tblCity> city = dropDownRepository.GetCity();
            List<City> list = new List<City>();
            foreach (var item in city)
            {
                City cityobj = new City()
                {
                    CityID = item.CityID,
                    CityName = item.CityName
                };
                if (item.IsDeleted)
                {
                }
                else
                    list.Add(cityobj);
            }
            return list;
        }
        public static List<College> GetCollege()
        {
            List<tblCollege> college = dropDownRepository.GetCollege();
            List<College> list = new List<College>();
            foreach (var item in college)
            {
                College collegeobj = new College()
                {
                    CollegeID = item.CollegeID,
                    CollegeName = item.CollegeName
                };
                if (item.IsDeleted)
                {
                }
                else
                    list.Add(collegeobj);
            }
            return list;
        }
        public static List<Department> GetDepartment()
        {
            List<tblDepartment> department = dropDownRepository.GetDepartment();
            List<Department> list = new List<Department>();
            foreach (var item in department)
            {
                Department departmentobj = new Department()
                {
                    DepartmentID = item.DepartmentID,
                    DepartmentName = item.DepartmentName
                };
                if (item.IsDeleted)
                {
                }
                else
                    list.Add(departmentobj);
            }
            return list;
        }

        public static List<City> GetCityById(Guid stateID)
        {
            List<tblCity> city = dropDownRepository.GetCityId(stateID);
            List<City> list = new List<City>();
            foreach (var item in city)
            {
                City cityobj = new City()
                {
                    CityID = item.CityID,
                    CityName = item.CityName
                };
                list.Add(cityobj);
            }
            return list;
        }

        public static List<Department> GetDepartmentById(Guid collegeId)
        {
            List<tblDepartment> department = dropDownRepository.GetDepartmentById(collegeId);
            List<Department> list = new List<Department>();
            foreach (var item in department)
            {
                Department departmentobj = new Department()
                {
                    DepartmentID = item.DepartmentID,
                    DepartmentName = item.DepartmentName
                };
                list.Add(departmentobj);
            }
            return list;
        }
    
    }
}
