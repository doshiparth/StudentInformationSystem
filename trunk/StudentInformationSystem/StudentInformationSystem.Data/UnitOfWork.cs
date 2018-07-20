using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Data
{
    public class UnitOfWork : IDisposable
    {
        private StudentInformationSystemEntities _context = new StudentInformationSystemEntities();

        private GenericRepository<tblStudent> studentRepository;
        private GenericRepository<tblStudentHistory> studentHistoryRepository;
        private GenericRepository<tblCollege> collegeRepository;
        private GenericRepository<tblDepartment> departmentRepository;
        private GenericRepository<tblCity> cityRepository;
        private GenericRepository<tblState> stateRepository;
        private GenericRepository<tblUser> userRepository;
        private GenericRepository<tblException> exceptionRepository;
        //private GenericRepository<tblRole> roleRepository;


        public GenericRepository<tblStudent> StudentRepository
        {
            get
            {
                if (this.studentRepository == null)
                    this.studentRepository = new GenericRepository<tblStudent>(_context);
                return studentRepository;
            }
        }

        public GenericRepository<tblCollege> CollegeRepository
        {
            get
            {
                if (this.collegeRepository == null)
                    this.collegeRepository = new GenericRepository<tblCollege>(_context);
                return collegeRepository;
            }
        }

        public GenericRepository<tblStudentHistory> StudentHistoryRepository
        {
            get
            {
                if (this.studentHistoryRepository == null)
                    this.studentHistoryRepository = new GenericRepository<tblStudentHistory>(_context);
                return studentHistoryRepository;
            }
        }

        public GenericRepository<tblUser> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new GenericRepository<tblUser>(_context);
                return userRepository;
            }
        }

        public GenericRepository<tblCity> CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                    this.cityRepository = new GenericRepository<tblCity>(_context);
                return cityRepository;
            }
        }

        public GenericRepository<tblState> StateRepository
        {
            get
            {
                if (this.stateRepository == null)
                    this.stateRepository = new GenericRepository<tblState>(_context);
                return stateRepository;
            }
        }
        public GenericRepository<tblDepartment> DepartmentRepository
        {
            get
            {
                if (this.departmentRepository == null)
                    this.departmentRepository = new GenericRepository<tblDepartment>(_context);
                return departmentRepository;
            }
        }

        public GenericRepository<tblException> ExceptionRepository
        {
            get
            {
                if (this.exceptionRepository == null)
                    this.exceptionRepository = new GenericRepository<tblException>(_context);
                return exceptionRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    _context.Dispose();
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
