using System;
using System.ComponentModel;

namespace StudentInformationSystem.Common
{
    public class GetError
    {
        public static string GetErrorFromEnum(ErrorEnums demoEnum)
        {
            if (demoEnum == ErrorEnums.Successful)
                return "Added Successfully";
            else if (demoEnum == ErrorEnums.Failed)
                return "Error in adding the student. The Student Code, Email Address and the Contact Number should be unique";
            else if (demoEnum == ErrorEnums.EmailAddress)
                return "Error in adding the student. The Student Email Address should be unique";
            else if (demoEnum == ErrorEnums.PhoneNumber)
                return "Error in adding the student. The Student Contact Number should be unique";
            else if (demoEnum == ErrorEnums.Code)
                return "Error in adding the student. The Student Code should be unique";
            else
                return "Error occured during the operation. The Administrator has been notifed";
        }
    }

    public enum ErrorEnums
    {
        Failed,
        Successful,
        EmailAddress,
        PhoneNumber,
        Code
    }
}
