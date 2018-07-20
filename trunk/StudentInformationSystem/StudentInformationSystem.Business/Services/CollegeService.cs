using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Data;

namespace StudentInformationSystem.Business.Services
{
    public class CollegeService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public List<CollegeDetailsModel> GetColleges()
        {
            try
            {
                IEnumerable<tblCollege> lstColleges = _unitOfWork.CollegeRepository.Get();
                List<CollegeDetailsModel> lstModels = new List<CollegeDetailsModel>();
                foreach (var entry in lstColleges)
                {
                    CollegeDetailsModel model = new CollegeDetailsModel()
                    {
                        CollegeID = entry.CollegeID,
                        CollegeCode = entry.CollegeCode,
                        CollegeName = entry.CollegeName
                    };
                    if (entry.IsDeleted)
                    {
                        //Don't add the college in the list
                    }
                    else
                        lstModels.Add(model);
                }
                return lstModels;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                List<CollegeDetailsModel> collegesList = new List<CollegeDetailsModel>();
                return collegesList;
            }
        }

        public CollegeDetailsModel GetCollege(Guid ID)
        {
            try
            {
                tblCollege entity = new tblCollege();
                entity = _unitOfWork.CollegeRepository.GetByID(ID);
                CollegeDetailsModel model = new CollegeDetailsModel()
                {
                    CollegeID = entity.CollegeID,
                    CollegeCode = entity.CollegeCode,
                    CollegeName = entity.CollegeName
                };
                if (entity.IsDeleted)
                {
                    return new CollegeDetailsModel();
                }
                else
                    return model;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                CollegeDetailsModel model = new CollegeDetailsModel();
                return model;
            }
        }

        public bool InsertCollege(CollegeDetailsModel collegeDetailsModel)
        {
            try
            {
                tblCollege entity = new tblCollege();

                entity = _unitOfWork.CollegeRepository.Get(filter: prop => prop.CollegeCode.Equals(collegeDetailsModel.CollegeCode)).FirstOrDefault();

                if (entity == null)
                {
                    Guid collegeId = Guid.NewGuid();
                    tblCollege newCollege = new tblCollege()
                    {
                        IsDeleted = false,
                        CollegeID = collegeId,
                        CollegeCode = collegeDetailsModel.CollegeCode,
                        CollegeName = collegeDetailsModel.CollegeName
                    };
                    _unitOfWork.CollegeRepository.Insert(newCollege);
                    _unitOfWork.Save();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                return false;
            }
        }

        public bool UpdateCollege(CollegeDetailsModel collegeDetailsModel)
        {
            try
            {
                tblCollege entityToUpdate = new tblCollege();
                entityToUpdate = _unitOfWork.CollegeRepository.GetByID(collegeDetailsModel.CollegeID);
                if (entityToUpdate == null)
                {
                    return false;
                }

                List<tblCollege> getAllCollegeCodes = _unitOfWork.CollegeRepository.Get().ToList();
                foreach (var entry in getAllCollegeCodes)
                {
                    //CollegeCode already exists
                    if (entry.CollegeCode == entityToUpdate.CollegeCode)
                        break;
                    if (entry.CollegeCode == collegeDetailsModel.CollegeCode)
                        return false;
                }

                entityToUpdate.CollegeCode = collegeDetailsModel.CollegeCode;
                entityToUpdate.CollegeName = collegeDetailsModel.CollegeName;

                _unitOfWork.CollegeRepository.Update(entityToUpdate);
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

        public bool DeleteCollege(Guid ID)
        {
            try
            {
                tblCollege entityToDelete = _unitOfWork.CollegeRepository.GetByID(ID);
                if (entityToDelete == null)
                {
                    return false;
                }
                entityToDelete.IsDeleted = true;
                _unitOfWork.CollegeRepository.Update(entityToDelete);
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

    }
}

#region Extra code
//IEnumerable<tblDepartment> deptsToDelete = _unitOfWork.DepartmentRepository.Get(filter: prop => prop.CollegeId.Equals(entityToDelete.CollegeID)).ToList();

//IEnumerable<tblStudentHistory> studentHistoryToDelete = _unitOfWork.StudentHistoryRepository.Get(filter: prop => prop.CollegeID.Equals(entityToDelete.CollegeID)).ToList();

//_unitOfWork.StudentHistoryRepository.DeleteRange(studentHistoryToDelete);
//_unitOfWork.DepartmentRepository.DeleteRange(deptsToDelete);
//_unitOfWork.CollegeRepository.Delete(entityToDelete);
#endregion