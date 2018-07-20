using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Data;

namespace StudentInformationSystem.Business.Services
{
    public class CityTestService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();


        public List<CityModel> GetCities(Guid stateID)
        {
            try
            {

                List<tblCity> entityList = new List<tblCity>();
                entityList = _unitOfWork.CityRepository.Get(filter: prop => prop.StateID == stateID).ToList();
                List<CityModel> newList = new List<CityModel>();

                foreach (var entry in entityList)
                {
                    CityModel cityModel = new CityModel()
                    {
                        CityID = entry.CityID,
                        CityName = entry.CityName,
                        StateID = entry.StateID
                    };
                    if (entry.IsDeleted)
                    {
                    }
                    else
                        newList.Add(cityModel);
                }

                //return (from city in _unitOfWork.CityRepository.Get()
                //        where city.StateID == stateID
                //        select new CityModel()
                //        {
                //            StateID = city.StateID,
                //            CityName = city.CityName,
                //            CityID = city.CityID
                //        }).ToList();
                return newList;
            }

            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                List<CityModel> citiesList = new List<CityModel>();
                return citiesList;
            }
        }

        public  List<StateModel> GetStates()
        {
            try
            {
                IEnumerable<tblState> lstState = _unitOfWork.StateRepository.Get();
                List<StateModel> stateList = new List<StateModel>();
                foreach (var singleState in lstState)
                {
                    StateModel state = new StateModel()
                    {
                        StateID = singleState.StateID,
                        StateName = singleState.StateName
                    };
                    stateList.Add(state);
                }
                return stateList;
            }

            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                List<StateModel> statesList = new List<StateModel>();
                return statesList;
            }
        }

        public bool InsertCity(CityModel cityModel)
        {
            try
            {
                tblCity entity = new tblCity();
                entity = _unitOfWork.CityRepository.Get(filter: prop => prop.CityName.Equals(cityModel.CityName)).FirstOrDefault();
                Guid newGuid = Guid.NewGuid();
                if (entity == null)
                {
                    tblCity city = new tblCity()
                    {
                        CityID = newGuid,
                        CityName = cityModel.CityName,
                        StateID = cityModel.StateID

                    };
                    _unitOfWork.CityRepository.Insert(city);
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

        public bool DeleteCity(Guid CityID)
        {
            try
            {
                tblCity city = _unitOfWork.CityRepository.GetByID(CityID);
                //_unitOfWork.CityRepository.Delete(city);
                city.IsDeleted = true;
                _unitOfWork.CityRepository.Update(city);
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

        public CityModel GetCity(Guid CityID)
        {
            try
            {
                tblCity entity = new tblCity();
                entity = _unitOfWork.CityRepository.GetByID(CityID);
               
                CityModel cityModel = new CityModel()
                 {
                     CityID = entity.CityID,
                     StateID = entity.StateID,
                     CityName = entity.CityName
                 };
                return cityModel;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                CityModel city = new CityModel();
                return city;
               
            }
        }

        public bool UpdateCity(CityModel cityModel)
        {
            try
            {
                var entity = _unitOfWork.CityRepository.GetByID(cityModel.CityID);

                entity.CityID = cityModel.CityID;
                entity.CityName = cityModel.CityName;
                entity.StateID = cityModel.StateID;

                _unitOfWork.CityRepository.Update(entity);
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

        public List<CityModel> GetAllCities()
        {
             try
            {
                IEnumerable<tblCity> lstCities = _unitOfWork.CityRepository.Get();

                List<CityModel> citiesList = new List<CityModel>();

                foreach (var singleCity in lstCities)
                {
                    CityModel city = new CityModel()
                    {
                        CityID = singleCity.CityID,
                        CityName = singleCity.CityName,
                        StateID = singleCity.StateID
                    };
                    citiesList.Add(city);


                }
                return citiesList;
            }
            catch (Exception e)
            {

                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                List<CityModel> citiesList = new List<CityModel>();
                return citiesList;
            }
              
        }
    }




}
