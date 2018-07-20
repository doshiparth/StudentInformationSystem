using System;

using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Data;

namespace StudentInformationSystem.Business.Services
{
    public static class UserService
    {
        static private StudentInformationSystemEntities _context = new StudentInformationSystemEntities();
        static UserRepository userRepository = new UserRepository(_context);

        public static int Login(string emailAddress, string password)
        {
            return userRepository.Login(emailAddress, password);
        }
    }
}
