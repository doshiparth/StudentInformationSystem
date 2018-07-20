using System;
using System.Collections.Generic;

using StudentInformationSystem.Entities.ViewModels;

namespace StudentInformationSystem.Data
{
    internal interface IUserRepository
    {
        //Checks the login credentials of ths user and assigns roles based on it
        int Login(string emailAddress, string password);

    }
}
