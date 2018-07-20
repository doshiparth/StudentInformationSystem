using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Cryptography;
using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Entities;
using StudentInformationSystem.Common;

namespace StudentInformationSystem.Data
{
    public class UserRepository : IUserRepository
    {
        private StudentInformationSystemEntities _context;

        //Public constructor of the Repository
        public UserRepository(StudentInformationSystemEntities context)
        {
            this._context = context;
        }


        public int Login(string emailAddress, string password)
        {
            var getUser = (from user in _context.tblUser
                           where user.EmailAddress == emailAddress
                           select user).FirstOrDefault();

            if (getUser != null)
            {
                MD5 md5Hash = MD5.Create();
                if (Encryptor.VerifyMd5Hash(md5Hash, password, getUser.Password))
                {
                    return getUser.RoleID;
                }
                else
                    return 0;
            }
            return 0;
        }
    }
}
