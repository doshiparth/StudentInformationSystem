using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentInformationSystem.Data;
using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Entities;
using StudentInformationSystem.Common;

using System.Security.Cryptography;

namespace StudentInformationSystem.Business.Services
{
    public class StudentServiceTest
    {
        private static UnitOfWork _unitOfWork = new UnitOfWork();

        public static List<StudentInfoModel> GetStudents()
        {
            try
            {
                IEnumerable<tblStudent> lstStudents = _unitOfWork.StudentRepository.Get();

                List<StudentInfoModel> studentsList = new List<StudentInfoModel>();

                foreach (var singleStudent in lstStudents)
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
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName(); 
                ErrorLogger.LogException(e, currentFile);
                List<StudentInfoModel> studentsList = new List<StudentInfoModel>();
                return studentsList;
            }
        }

        public static StudentModel GetStudent(Guid ID)
        {
            try
            {
                //throw new IndexOutOfRangeException();
                tblStudent studentEntity = _unitOfWork.StudentRepository.GetByID(ID);
                
                tblStudentHistory historyEntity = _unitOfWork.StudentHistoryRepository.Get(filter: prop => prop.StudentID == studentEntity.StudentID,
                    orderBy: prop => prop.OrderByDescending(d => d.Semester)).FirstOrDefault();

                if (historyEntity == null)
                {
                    return new StudentModel();
                }
                tblCity cityEntity = _unitOfWork.CityRepository.Get(filter: prop => prop.CityID == studentEntity.CityID).FirstOrDefault();
                tblState stateEntity = _unitOfWork.StateRepository.Get(filter: prop => prop.StateID == cityEntity.StateID).FirstOrDefault();
                tblCollege collegeEntity = _unitOfWork.CollegeRepository.Get(filter: prop => prop.CollegeID == historyEntity.CollegeID).FirstOrDefault();
                tblDepartment departmentEntity = _unitOfWork.DepartmentRepository.Get(filter: prop => prop.DepartmentID == historyEntity.DepartmentID).FirstOrDefault();
                StudentModel studentModel = new StudentModel()
                {
                    StudentCode = studentEntity.StudentCode,
                    StudentID = studentEntity.StudentID,
                    FirstName = studentEntity.FirstName,
                    LastName = studentEntity.LastName,
                    ContactNumber = studentEntity.ContactNumber,
                    EmailAddress = studentEntity.EmailAddress,
                    DateOfBirth = studentEntity.DateOfBirth,
                    Address = studentEntity.Address,
                    DateOfJoining = historyEntity.DateOfJoining,
                    Semester = historyEntity.Semester,
                    CollegeID = collegeEntity.CollegeID,
                    CollegeName = collegeEntity.CollegeName,
                    DepartmentID = departmentEntity.DepartmentID,
                    DepartmentName = departmentEntity.DepartmentName,
                    CityID = cityEntity.CityID,
                    CityName = cityEntity.CityName,
                    StateName = stateEntity.StateName
                };
                return studentModel;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                StudentModel student = new StudentModel();
                return student;
            }
        }

        public static ErrorEnums InsertStudent(StudentModel studentModel)
        {
            try
            {
                tblStudent studentEntity = new tblStudent();
                studentEntity = _unitOfWork.StudentRepository.Get(filter: prop => prop.StudentCode == studentModel.StudentCode).FirstOrDefault();
                if (studentEntity == null)
                {
                    studentEntity = _unitOfWork.StudentRepository.Get(filter: prop => prop.ContactNumber == studentModel.ContactNumber).FirstOrDefault();
                    if (studentEntity == null)
                    {
                        studentEntity = _unitOfWork.StudentRepository.Get(filter: prop => prop.EmailAddress == studentModel.EmailAddress).FirstOrDefault();
                        if (studentEntity == null)
                        {
                            Guid newStudentId = Guid.NewGuid();
                            tblStudent newStudent = new tblStudent();
                            newStudent.StudentID = newStudentId;
                            newStudent.StudentCode = studentModel.StudentCode;
                            newStudent.FirstName = studentModel.FirstName;
                            newStudent.LastName = studentModel.LastName;
                            newStudent.ContactNumber = studentModel.ContactNumber;
                            newStudent.EmailAddress = studentModel.EmailAddress;
                            newStudent.DateOfBirth = studentModel.DateOfBirth;
                            newStudent.Address = studentModel.Address;
                            newStudent.CityID = studentModel.CityID;

                            Guid newStudentHistoryId = Guid.NewGuid();
                            tblStudentHistory studentHistoryEntity = new tblStudentHistory()
                            {
                                StudentHistoryID = newStudentHistoryId,
                                DepartmentID = studentModel.DepartmentID,
                                CollegeID = studentModel.CollegeID,
                                Semester = studentModel.Semester,
                                DateOfJoining = studentModel.DateOfJoining,
                                StudentID = newStudentId
                            };

                            Guid newUserID = Guid.NewGuid();

                            //Generating a new MD5 encrypted password
                            MD5 md5Hash = MD5.Create();
                            string newPassword = "student";
                            string newEncryptedPassword = Encryptor.GetMd5Hash(md5Hash, newPassword);

                            tblUser studentUserEntity = new tblUser()
                            {
                                UserID = newUserID,
                                EmailAddress = studentModel.EmailAddress,
                                Password = newEncryptedPassword,
                                RoleID = 3
                            };
                            _unitOfWork.StudentRepository.Insert(newStudent);
                            _unitOfWork.StudentHistoryRepository.Insert(studentHistoryEntity);
                            _unitOfWork.UserRepository.Insert(studentUserEntity);

                            _unitOfWork.Save();
                            return ErrorEnums.Successful;
                        }
                        else
                            //Email address exists
                            return ErrorEnums.EmailAddress;
                    }
                    else
                        //Contact number exists
                        return ErrorEnums.PhoneNumber;
                }
                else
                    //Student code exists
                    return ErrorEnums.Code;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                return ErrorEnums.Failed;
            }
        }

        public static bool UpdateStudent(StudentModel studentModel)
        {
            try
            {
                tblStudent studentEntityToUpdate = new tblStudent();
                studentEntityToUpdate = _unitOfWork.StudentRepository.GetByID(studentModel.StudentID);

                if (studentEntityToUpdate.Equals(null))
                    return false;
                else
                {
                    studentEntityToUpdate.FirstName = studentModel.FirstName;
                    studentEntityToUpdate.LastName = studentModel.LastName;
                    studentEntityToUpdate.ContactNumber = studentModel.ContactNumber;
                    studentEntityToUpdate.EmailAddress = studentModel.EmailAddress;
                    studentEntityToUpdate.DateOfBirth = studentModel.DateOfBirth;
                    studentEntityToUpdate.Address = studentModel.Address;
                    studentEntityToUpdate.CityID = studentModel.CityID;

                    IEnumerable<tblStudentHistory> historyEntityList = new List<tblStudentHistory>();
                    historyEntityList = _unitOfWork.StudentHistoryRepository.Get(filter: prop => prop.StudentID == studentModel.StudentID);

                    bool entryNotExists = true;
                    foreach (var entry in historyEntityList)
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

                        _unitOfWork.StudentHistoryRepository.Insert(newStudentHistoryEntry);
                    }
                }
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                return false;
            }
        }

        public static bool DeleteStudent(Guid id)
        {
            try
            {
                tblStudent studentEntityToDelete = new tblStudent();
                IEnumerable<tblStudentHistory> historyEntityToDeleteList = new List<tblStudentHistory>();
                tblUser userEntityToDelete = new tblUser();
                studentEntityToDelete = _unitOfWork.StudentRepository.GetByID(id);
                historyEntityToDeleteList = _unitOfWork.StudentHistoryRepository.Get(filter: prop => prop.StudentID == studentEntityToDelete.StudentID);
                userEntityToDelete = _unitOfWork.UserRepository.Get(filter: prop => prop.EmailAddress == studentEntityToDelete.EmailAddress).FirstOrDefault();

                _unitOfWork.StudentHistoryRepository.DeleteRange(historyEntityToDeleteList);
                _unitOfWork.StudentRepository.Delete(studentEntityToDelete);
                _unitOfWork.UserRepository.Delete(userEntityToDelete);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                return false;
            }
        }

        public static List<StudentHistoryModel> GetStudentHistory(Guid id)
        {
            try
            {
                List<StudentHistoryModel> modelList = new List<StudentHistoryModel>();
                IEnumerable<tblStudentHistory> historyEntityList = new List<tblStudentHistory>();
                historyEntityList = from ordered in _unitOfWork.StudentHistoryRepository.Get(filter: prop => prop.StudentID == id)
                                    orderby ordered.Semester descending
                                    select ordered;
                foreach (var history in historyEntityList)
                {
                    tblCollege collegeEntity = new tblCollege();
                    collegeEntity = _unitOfWork.CollegeRepository.Get(filter: prop => prop.CollegeID == history.CollegeID).FirstOrDefault();
                    tblDepartment departmentEntity = new tblDepartment();
                    departmentEntity = _unitOfWork.DepartmentRepository.Get(filter: prop => prop.DepartmentID == history.DepartmentID).FirstOrDefault();
                    StudentHistoryModel newHistory = new StudentHistoryModel()
                    {
                        StudentID = history.StudentID,
                        CollegeName = collegeEntity.CollegeName,
                        DepartmentName = departmentEntity.DepartmentName,
                        Semester = history.Semester,
                        DateOfJoining = history.DateOfJoining
                    };
                    modelList.Add(newHistory);
                }
                return modelList;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                List<StudentHistoryModel> modelList = new List<StudentHistoryModel>();
                return modelList;
            }
        }

        public static StudentModel GetStudentByEmail(string email)
        {
            try
            {
                tblStudent studentEntity = _unitOfWork.StudentRepository.Get(filter: prop => prop.EmailAddress == email).FirstOrDefault();
                tblStudentHistory historyEntity = _unitOfWork.StudentHistoryRepository.Get(filter: prop => prop.StudentID == studentEntity.StudentID).FirstOrDefault();
                tblCollege collegeEntity = _unitOfWork.CollegeRepository.Get(filter: prop => prop.CollegeID == historyEntity.CollegeID).FirstOrDefault();
                tblDepartment departmentEntity = _unitOfWork.DepartmentRepository.Get(filter: prop => prop.DepartmentID == historyEntity.DepartmentID).FirstOrDefault();
                tblCity cityEntity = _unitOfWork.CityRepository.Get(filter: prop => prop.CityID == studentEntity.CityID).FirstOrDefault();
                tblState stateEntity = _unitOfWork.StateRepository.Get(filter: prop => prop.StateID == cityEntity.StateID).FirstOrDefault();
                StudentModel studentModel = new StudentModel()
                {
                    StudentCode = studentEntity.StudentCode,
                    StudentID = studentEntity.StudentID,
                    FirstName = studentEntity.FirstName,
                    LastName = studentEntity.LastName,
                    ContactNumber = studentEntity.ContactNumber,
                    EmailAddress = studentEntity.EmailAddress,
                    DateOfBirth = studentEntity.DateOfBirth,
                    Address = studentEntity.Address,
                    DateOfJoining = historyEntity.DateOfJoining,
                    Semester = historyEntity.Semester,
                    CollegeID = collegeEntity.CollegeID,
                    CollegeName = collegeEntity.CollegeName,
                    DepartmentID = departmentEntity.DepartmentID,
                    DepartmentName = departmentEntity.DepartmentName,
                    CityID = cityEntity.CityID,
                    CityName = cityEntity.CityName,
                    StateName = stateEntity.StateName
                };
                return studentModel;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                StudentModel studentModel = new StudentModel();
                return studentModel;
            }
        }
    }
}
