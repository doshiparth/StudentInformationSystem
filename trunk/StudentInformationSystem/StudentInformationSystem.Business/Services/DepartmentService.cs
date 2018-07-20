using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Data;


namespace StudentInformationSystem.Business.Services
{
    public class DepartmentService
    {
         static private StudentInformationSystemEntities _context = new StudentInformationSystemEntities();
        //static private CollegeRepository collegeRepository = new CollegeRepository(_context);

        private UnitOfWork _unitOfWork = new UnitOfWork();


        public List<CollegeModel> GetAllDepartment(Guid collegeId)
        {
            try
            {
                IEnumerable<tblDepartment> tblDept = _unitOfWork.DepartmentRepository.Get(filter: prop => prop.CollegeId == collegeId);
                List<CollegeModel> deptList = new List<CollegeModel>();

                foreach (var d in tblDept)
                {
                    CollegeModel model = new CollegeModel()
                    {   
                        CollegeID=d.CollegeId,
                        DepartmentID = d.DepartmentID,
                        DepartmentCode = d.DepartmentCode,
                        DepartmentName = d.DepartmentName,
                        TotalSem = d.TotalSem
                    };
                    if (d.IsDeleted)
                    {
                    }
                    else
                        deptList.Add(model);
                }
                return deptList;
            }
            catch(Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                List<CollegeModel> deptList = new List<CollegeModel>();
                return deptList;
            }
        }


        public bool InsertDepartment(CollegeModel collegeModel)
        {
            try
            {
                tblDepartment check = new tblDepartment();
                check = _unitOfWork.DepartmentRepository.Get(prop => prop.DepartmentCode == collegeModel.DepartmentCode).FirstOrDefault();
                Guid newGuid = Guid.NewGuid();
                if (check == null)
                {
                    tblDepartment entity = new tblDepartment()
                    {
                        DepartmentID = newGuid,
                        DepartmentCode = collegeModel.DepartmentCode,
                        DepartmentName = collegeModel.DepartmentName,
                        CollegeId = collegeModel.CollegeID,
                        TotalSem = collegeModel.TotalSem,
                        IsDeleted = false
                    };
                    _unitOfWork.DepartmentRepository.Insert(entity);
                    _unitOfWork.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                CollegeModel college = new CollegeModel();
                return false;
            }
            
        }


        public CollegeModel GetDepartment(Guid id)
        {
            try 
            {
            CollegeModel collegeModel = new CollegeModel();
            tblDepartment entity = new tblDepartment();
            entity = _unitOfWork.DepartmentRepository.GetByID(id);
            collegeModel.DepartmentID = entity.DepartmentID;
            collegeModel.CollegeID=entity.CollegeId;
            collegeModel.DepartmentCode = entity.DepartmentCode;
            collegeModel.DepartmentName = entity.DepartmentName;
            collegeModel.TotalSem = entity.TotalSem;
            return collegeModel;
            }
            catch(Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                CollegeModel collegeModel = new CollegeModel();
                return collegeModel;
            }
        }


        public bool EditDepartment(CollegeModel collegeModel)
        {
            try 
            {
            tblDepartment entityToUpdate = new tblDepartment()
            {
                DepartmentID=collegeModel.DepartmentID,
                CollegeId = collegeModel.CollegeID,
                DepartmentCode = collegeModel.DepartmentCode,
                DepartmentName = collegeModel.DepartmentName,
                TotalSem = collegeModel.TotalSem
            };
            _unitOfWork.DepartmentRepository.Update(entityToUpdate);
            _unitOfWork.Save();
            return true;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                CollegeModel college = new CollegeModel();
                return false;
            }


        }
        public bool DeleteDepartment(Guid ID)
        {
            try
            {
            tblDepartment entityToDelete = new tblDepartment();
            entityToDelete = _unitOfWork.DepartmentRepository.GetByID(ID);
            //_unitOfWork.DepartmentRepository.Delete(ID);
            entityToDelete.IsDeleted = true;
            _unitOfWork.DepartmentRepository.Update(entityToDelete);
            _unitOfWork.Save();
            return true;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                CollegeModel college = new CollegeModel();
                return false;
            }
        }


        
    }
}
