using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Data;

namespace TweeterBook.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext _repoContext;
        private IEmployeeRepository _employee;       

        public IEmployeeRepository Employees
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeRepository(_repoContext);
                }

                return _employee;
            }
        }       

        public RepositoryWrapper(DataContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
