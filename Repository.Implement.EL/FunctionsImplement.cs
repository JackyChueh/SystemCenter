using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCenter;
using Repository.Interface;
using System.Data;
using System.Data.Common;

namespace Repository.Implement.EL
{
    public class FunctionsImplement : BaseRepository, IFunctionsInterface
    {
        public FunctionsImplement(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public FUNCTION GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FUNCTION> GetAll()
        {
            throw new NotImplementedException();
        }

   

    }
}
