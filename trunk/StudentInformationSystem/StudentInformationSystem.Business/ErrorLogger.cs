using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentInformationSystem.Data;

namespace StudentInformationSystem.Business
{
    public class ErrorLogger
    {
        //static private StudentInformationSystemEntities _context = new StudentInformationSystemEntities();
        private static UnitOfWork _unitOfWork = new UnitOfWork();

        public static void LogException(Exception ex, string fileName)
        {
            Guid logId = Guid.NewGuid();
            tblException exception = new tblException()
            {
                LogID = logId,
                ExceptionMsg = ex.Message,
                ExceptionDate = DateTime.Now,
                ExceptionStackTrace = ex.StackTrace + ex.InnerException,
                ExceptionErrorCode = ex.HResult,
                FileName = fileName
            };

            _unitOfWork.ExceptionRepository.Insert(exception);
            _unitOfWork.Save();
        }

    }
}
