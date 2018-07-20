using System;
using System.Collections.Generic;
using System.Linq;

using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Entities;

using System.Security.Cryptography;
using StudentInformationSystem.Common;

namespace StudentInformationSystem.Data
{
    public class StudentRepository : IStudentRepository
    {
        private StudentInformationSystemEntities _context;

        //Public constructor of the Repository
        public StudentRepository(StudentInformationSystemEntities context)
        {
            this._context = context;
        }

        public List<StudentInfoModel> GetAllStudents()
        {
            var getAllStudents = from allStudents in _context.tblStudent
                                 select allStudents;

            //var getAllStudents = _context.tblStudent.Where(student => student.).FirstOrDefault();

            List<StudentInfoModel> studentsList = new List<StudentInfoModel>();

            foreach (var singleStudent in getAllStudents)
            {
                StudentInfoModel student = new StudentInfoModel()
                {
                    StudentID = singleStudent.StudentID,
                    FirstName = singleStudent.FirstName,
                    LastName = singleStudent.LastName,
                    ContactNumber = singleStudent.ContactNumber,
                    EmailAddress = singleStudent.EmailAddress
                };

                studentsList.Add(student);
            }

            return studentsList;
            //return _context.tblStudent.ToList();
        }

        public StudentModel GetStudent(string emailAddress)
        {
            //var getStudent = (from student in _context.tblStudent
            //                  where student.EmailAddress == emailAddress
            //                  select student).ToList().FirstOrDefault();

            var getStudent = _context.tblStudent.Where(s => s.EmailAddress.Equals(emailAddress)).FirstOrDefault();

            var getStudentHistory = (from studentHistory in _context.tblStudentHistory
                                     where studentHistory.StudentID == getStudent.StudentID
                                     orderby studentHistory.Semester descending
                                     select studentHistory).ToList().FirstOrDefault();

            //var getStudentCity = (from studentCity in _context.tblCity
            //                      where studentCity.CityID == getStudent.CityID
            //                      select studentCity).ToList().FirstOrDefault();

            var getStudentCity = _context.tblCity.Where(c => c.CityID.Equals(getStudent.CityID)).FirstOrDefault();

            var getStudentState = (from studentState in _context.tblState
                                   where studentState.StateID == getStudentCity.StateID
                                   select studentState.StateName).ToList().FirstOrDefault();

            var getStudentCollege = (from studentCollege in _context.tblCollege
                                     where studentCollege.CollegeID == getStudentHistory.CollegeID
                                     select studentCollege).ToList().FirstOrDefault();

            var getStudentDepartment = (from studentDepartment in _context.tblDepartment
                                     where studentDepartment.DepartmentID == getStudentHistory.DepartmentID
                                     select studentDepartment).ToList().FirstOrDefault();

            //var getSingle = getStudent.FirstOrDefault();
            
            StudentModel singleStudent = new StudentModel()
            {
                StudentID = getStudent.StudentID,
                FirstName = getStudent.FirstName,
                LastName = getStudent.LastName,
                ContactNumber = getStudent.ContactNumber,
                EmailAddress = getStudent.EmailAddress,
                DateOfBirth = getStudent.DateOfBirth,
                Address = getStudent.Address,
                DateOfJoining = getStudentHistory.DateOfJoining,
                CollegeName = getStudentCollege.CollegeName,
                StateName = getStudentState,
                StudentCode = getStudent.StudentCode,
                Semester = getStudentHistory.Semester,
                DepartmentName = getStudentDepartment.DepartmentName,
                CityName = getStudentCity.CityName,
                CityID = getStudentCity.CityID,
                CollegeID = getStudentCollege.CollegeID,
                DepartmentID = getStudentDepartment.DepartmentID
            };
            return singleStudent;
        }

        public bool InsertStudent(StudentModel studentModel)
        {
            var checkIfStudentExists = from studentCheck in _context.tblStudent
                                       where studentCheck.StudentCode == studentModel.StudentCode
                                       select studentCheck;

            if (checkIfStudentExists.Any())
                return false;

            Guid newGuid = Guid.NewGuid();

            //var getStudentsCityId = (from studentCity in _context.tblCity
            //                        where studentCity.CityName == studentModel.CityName
            //                        select studentCity.CityID).FirstOrDefault();

            tblStudent student = new tblStudent()
            {
                StudentID = newGuid,
                StudentCode = studentModel.StudentCode,
                FirstName = studentModel.FirstName,
                LastName = studentModel.LastName,
                ContactNumber = studentModel.ContactNumber,
                EmailAddress = studentModel.EmailAddress,
                DateOfBirth = studentModel.DateOfBirth,
                Address = studentModel.Address,
                CityID = studentModel.CityID
            };

            //var getStudentCollegeID = (from studentCollege in _context.tblCollege
            //                            where studentCollege.CollegeName == studentModel.CollegeName
            //                            select studentCollege.CollegeID).FirstOrDefault();

            //var getStudentDepartmentID = (from studentDept in _context.tblDepartment
            //                               where studentDept.DepartmentName == studentModel.DepartmentName
            //                               select studentDept.DepartmentID).FirstOrDefault();
            Guid newGuid2 = Guid.NewGuid();
            tblStudentHistory studentHistory = new tblStudentHistory()
            {
                StudentHistoryID = newGuid2,
                DepartmentID = studentModel.DepartmentID,
                CollegeID = studentModel.CollegeID,
                Semester = studentModel.Semester,
                DateOfJoining = studentModel.DateOfJoining,
                StudentID = newGuid,
            };

            Guid newUserID = Guid.NewGuid();
            
            //Generating a new MD5 encrypted password
            MD5 md5Hash = MD5.Create();
            string newPassword = "student";
            string newEncryptedPassword = Encryptor.GetMd5Hash(md5Hash, newPassword);
            
            tblUser newStudentUser = new tblUser()
            {
                UserID = newUserID,
                EmailAddress = studentModel.EmailAddress,
                Password = newEncryptedPassword,
                RoleID = 3
            };

            
            _context.tblStudent.Add(student);
            _context.tblStudentHistory.Add(studentHistory);
            _context.tblUser.Add(newStudentUser);

            _context.SaveChanges();

            return true;
        }

        public bool UpdateStudent(StudentModel studentModel)
        {
            //_context.Entry(studentModel).State = EntityState.Modified;
            tblStudent newStudent = new tblStudent();
            newStudent = _context.tblStudent.FirstOrDefault(i => i.StudentID == studentModel.StudentID);
            //newStudent = _context.tblStudent.Find(studentModel.StudentID);

            if (newStudent.Equals(null))
                return false;

            //Adding to Student Info
            newStudent.FirstName = studentModel.FirstName;
            newStudent.LastName = studentModel.LastName;
            newStudent.ContactNumber = studentModel.ContactNumber;
            newStudent.EmailAddress = studentModel.EmailAddress;
            newStudent.DateOfBirth = studentModel.DateOfBirth;
            newStudent.Address = studentModel.Address;
            newStudent.CityID = studentModel.CityID;

            var newStudentHistory = from studentHistoryAll in _context.tblStudentHistory
                                    where studentModel.StudentID == studentHistoryAll.StudentID
                                    select studentHistoryAll;

            bool entryNotExists = true;
            foreach (var entry in newStudentHistory)
            {
                if (studentModel.CollegeID.Equals(entry.CollegeID)
                    && studentModel.DepartmentID.Equals(entry.DepartmentID)
                    && studentModel.DateOfJoining.Equals(entry.DateOfJoining)
                    && studentModel.Semester.Equals(entry.Semester))
                {
                    entryNotExists = false;
                    break;
                }
            }

            
            ////New entry in student history table if any changes in its fields
            //if(!studentModel.CollegeID.Equals(newStudentHistory.CollegeID)
            //    || !studentModel.DepartmentID.Equals(newStudentHistory.DepartmentID)
            //    || !studentModel.Semester.Equals(newStudentHistory.Semester)
            //    || !studentModel.DateOfJoining.Equals(newStudentHistory.DateOfJoining))
            //{

            if (entryNotExists)
            {
                Guid newStudentHistoryID = Guid.NewGuid();
                tblStudentHistory newStudentHistoryEntry = new tblStudentHistory();
                newStudentHistoryEntry.StudentHistoryID = newStudentHistoryID;
                newStudentHistoryEntry.CollegeID = studentModel.CollegeID;
                newStudentHistoryEntry.DepartmentID = studentModel.DepartmentID;
                newStudentHistoryEntry.Semester = studentModel.Semester;
                newStudentHistoryEntry.DateOfJoining = studentModel.DateOfJoining;
                newStudentHistoryEntry.StudentID = studentModel.StudentID;

                _context.tblStudentHistory.Add(newStudentHistoryEntry);
            }
            
            _context.SaveChanges();

            return true;
        }

        public bool DeleteStudent(string emailAddress)
        {
            //tblStudent student = _context.tblStudent.Find(EmailAddress);
            tblStudent removedStudent = (from student in _context.tblStudent
                                        where student.EmailAddress == emailAddress
                                        select student).FirstOrDefault();

            var removedStudentHistory = from studentHistory in _context.tblStudentHistory
                                                      where studentHistory.StudentID == removedStudent.StudentID
                                                      select studentHistory;

            tblUser removedUser = (from user in _context.tblUser
                                   where user.EmailAddress == removedStudent.EmailAddress
                                   select user).FirstOrDefault();

            //tblStudent removedStudent = getStudent;

            //Removing Student
            _context.Entry(removedStudent).State = System.Data.Entity.EntityState.Deleted;

            //Removing Student's History
            foreach (var entry in removedStudentHistory)
            {
                _context.Entry(entry).State = System.Data.Entity.EntityState.Deleted;
            }

            //Removing the user for that student
            _context.Entry(removedUser).State = System.Data.Entity.EntityState.Deleted;

            //_context.tblStudentHistory.Remove(removedStudentHistory);
            //_context.tblStudent.Remove(removedStudent);
            //_context.tblUser.Remove(removedUser); 
            
            _context.SaveChanges();

            //if ()
            return true;
            //else
            //    return false;
        }

        public List<StudentHistoryModel> GetStudentHistory(Guid studentId)
        {
            var getStudentsHistoryQuery = (from sHistoty in _context.tblStudentHistory
                                            where sHistoty.StudentID == studentId
                                            orderby sHistoty.Semester descending
                                            select sHistoty).ToList();

            List<StudentHistoryModel> lstStudentHistory = new List<StudentHistoryModel>();

            foreach (var entry in getStudentsHistoryQuery)
            {
                var getCollege = (from college in _context.tblCollege
                                 where college.CollegeID == entry.CollegeID
                                 select college.CollegeName).FirstOrDefault();

                var getStudentEmail = (from sName in _context.tblStudent
                                     where sName.StudentID == studentId
                                     select sName.EmailAddress).FirstOrDefault();

                var getDepartment = (from dept in _context.tblDepartment
                                    where dept.DepartmentID == entry.DepartmentID
                                    select dept).FirstOrDefault();

                StudentHistoryModel newEntry = new StudentHistoryModel()
                {
                    StudentID = studentId,
                    EmailAddress = getStudentEmail,
                    CollegeName = getCollege,
                    DepartmentName = getDepartment.DepartmentName,
                    Semester = entry.Semester,
                    DateOfJoining = entry.DateOfJoining,
                };
                lstStudentHistory.Add(newEntry);
            }
            return lstStudentHistory;
        }

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}
    }
}
