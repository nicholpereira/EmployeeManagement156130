using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Data
{
    public interface AbstractRepository<T> where T : class
    {
        List<T> GetAll();

        T Get(int id);

        T Update(T obj);

        List<T> Find(Func<T, bool> predicate);

        bool Add(T obj);

        bool Delete(int id);
    }
}
