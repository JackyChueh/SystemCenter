using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCenter;

namespace Repository.Interface
{
    public interface IFunctionsInterface
    {
        FUNCTION GetOne(int id);
        IEnumerable<FUNCTION> GetAll();
    }
}
