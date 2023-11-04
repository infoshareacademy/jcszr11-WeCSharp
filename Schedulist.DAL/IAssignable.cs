using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public interface IAssignable
    {
        void Assign(User user);
        //void AssignUserById (int id);
    }
}
