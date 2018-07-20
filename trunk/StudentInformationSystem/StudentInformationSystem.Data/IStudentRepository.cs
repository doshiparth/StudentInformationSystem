using System;
using System.Collections.Generic;

using StudentInformationSystem.Entities.ViewModels;

namespace StudentInformationSystem.Data
{
    internal interface IStudentRepository
    {
        //Select Method for all students
        //Display students on HomePage
        List<StudentInfoModel> GetAllStudents();

        //Select Method for single student
        //ViewStudent page and UpdateStudent page
        StudentModel GetStudent(string EmailAddress);

        //Insert Method
        //Add button clicked from AddStudent page
        bool InsertStudent(StudentModel studentModel);

        //Update Method
        //Update button clicked from UpdateStudent page
        bool UpdateStudent(StudentModel studentModel);

        //Delete Method
        //Delete button clicked from ViewStudentPage
        bool DeleteStudent(string EmailAddress);
    }
}
