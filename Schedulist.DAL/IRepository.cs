using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T item);
    }
}
