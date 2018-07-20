using System;
using System.Collections.Generic;

using StudentInformationSystem.Entities;
using StudentInformationSystem.Entities.ViewModels;

namespace StudentInformationSystem.Data
{
    internal interface IDropdownRepository
    {
        List<State> GetState();

        List<College> GetCollege();

        List<City> GetCity();

        List<Department> GetDepartment();

        List<City> GetCityId(Guid stateId);

        List<Department> GetDepartmentById(Guid collegeId);
    }
}
