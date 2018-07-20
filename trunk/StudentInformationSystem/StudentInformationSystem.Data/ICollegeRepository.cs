using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentInformationSystem.Entities.ViewModels;

namespace StudentInformationSystem.Data
{
    interface ICollegeRepository : IDisposable
    {
        tblCollege GetCollegeByID(Guid collegeID);

        IEnumerable<tblCollege> GetColleges();

        void InsertCollege(tblCollege college);
        
        void UpdateCollege(tblCollege college);

        void DeleteCollege(Guid collegeID);

        void Save();
    }
}
