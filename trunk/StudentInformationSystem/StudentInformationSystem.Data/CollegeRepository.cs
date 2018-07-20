using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace StudentInformationSystem.Data
{
    public class CollegeRepository : ICollegeRepository
    {
        private StudentInformationSystemEntities _context;

        //Public constructor of the Repository
        public CollegeRepository(StudentInformationSystemEntities context)
        {
            this._context = context;
        }

        //Operations Start
        public tblCollege GetCollegeByID(Guid collegeID)
        {
            return _context.tblCollege.Find(collegeID);
        }

        public IEnumerable<tblCollege> GetColleges()
        {
            return _context.tblCollege.ToList();
        }

        public void InsertCollege(tblCollege college)
        {
            _context.tblCollege.Add(college);
        }

        public void UpdateCollege(tblCollege college)
        {
            _context.Entry(college).State = EntityState.Modified;
        }

        public void DeleteCollege(Guid collegeID)
        {
            tblCollege college = _context.tblCollege.Find(collegeID);
            _context.tblCollege.Remove(college);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
