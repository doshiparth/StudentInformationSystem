using System;
using System.Collections.Generic;

using StudentInformationSystem.Entities.ViewModels;

namespace StudentInformationSystem.Entities
{
    public static class HelperModel
    {
        public static List<Item> State { get; set; }
        public static List<Item> City { get; set; }
        public static List<Item> Department { get; set; }
        public static List<Item> College { get; set; }

        public static void SetState(List<State> item)
        {
            State = new List<Item>();

            item.ForEach(x => State.Add(new Item()
            {
                Value = x.StateID.ToString(),
                DisplayName = x.StateName.ToString(),
            }
            ));

        }

        public static void SetCity(List<City> item)
        {

            City = new List<Item>();
            item.ForEach(x => City.Add(new Item()
            {
                Value = x.CityID.ToString(),
                DisplayName = x.CityName.ToString(),
            }
            ));

        }

        public static void SetDepartment(List<Department> item)
        {

            Department = new List<Item>();

            item.ForEach(x => Department.Add(new Item()
            {
                Value = x.DepartmentID.ToString(),
                DisplayName = x.DepartmentName.ToString(),
            }
            ));

        }

        public static void SetCollege(List<College> item)
        {
            College = new List<Item>();

            item.ForEach(x => College.Add(new Item()
            {
                Value = x.CollegeID.ToString(),
                DisplayName = x.CollegeName.ToString(),
            }
            ));
        }
    }

    public class Item
    {
        public string Value { get; set; }
        public string DisplayName { get; set; }
    }
}
