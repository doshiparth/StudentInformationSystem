using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

using StudentInformationSystem.Data;
using StudentInformationSystem.Common;

namespace StudentInformationSystem.Business.Services
{
    public static class UserServiceTest
    {
        private static UnitOfWork _unitOfWork = new UnitOfWork();

        public static int Login(string emailAddress, string password)
        {
            try
            {
                tblUser user = new tblUser();
                user = _unitOfWork.UserRepository.Get(filter: prop => prop.EmailAddress == emailAddress).FirstOrDefault();

                if (user != null)
                {
                    MD5 md5Hash = MD5.Create();
                    if (Encryptor.VerifyMd5Hash(md5Hash, password, user.Password))
                    {
                        return user.RoleID;
                    }
                    else
                        return 0;
                }
                return 0;
            }
            catch (Exception e)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                ErrorLogger.LogException(e, currentFile);
                return 0;
            }
        }
    }
}
