using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweeterBook.Repository
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository Employees { get; }

        void Save();

    }
}
