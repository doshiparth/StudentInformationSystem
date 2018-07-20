using System;
using System.Collections.Generic;

using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Data;

namespace StudentInformationSystem.Business.Services
{
    public static class StudentService
    {
        static private StudentInformationSystemEntities _context = new StudentInformationSystemEntities();
        static private StudentRepository studentRepository = new StudentRepository(_context);

        public static List<StudentInfoModel> GetAllStudents()
        {
            
            return studentRepository.GetAllStudents();
        }

        public static StudentModel GetStudent(string emailAddress)
        {
            return studentRepository.GetStudent(emailAddress);
        }

        public static bool InsertStudent(StudentModel studentModel)
        {
            return studentRepository.InsertStudent(studentModel);
        }

        public static bool UpdateStudent(StudentModel studentModel)
        {
            return studentRepository.UpdateStudent(studentModel);
        }

        public static bool DeleteStudent(string emailAddress)
        {
            return studentRepository.DeleteStudent(emailAddress);
        }

        public static List<StudentHistoryModel> GetStudentHistory(Guid studentId)
        {
            return studentRepository.GetStudentHistory(studentId);
        }
    }
}
