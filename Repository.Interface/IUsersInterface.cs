using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCenter;

namespace Repository.Interface
{
    public interface IUsersInterface
    {
        USERS GetOne(int id);
        IEnumerable<USERS> GetAll();
        void Insert(USERS users);
        void Update(USERS users);
        void Delete(USERS users);
        bool CheckPassword(string USR_ID, string PWD);
    }
}
