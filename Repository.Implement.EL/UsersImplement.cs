using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCenter;
using Repository.Interface;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Repository.Implement.EL
{
    public class UsersImplement: BaseRepository, IUsersInterface
    {
        public UsersImplement(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public USERS GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<USERS> GetAll()
        {
            List<USERS> items = new List<USERS>();

            string sql = "SELECT SID,USR_ID,USR_NAME,PWD,EMAIL,USR_STATUS,MEMO,CDATE,CUSER,MDATE,MUSER FROM USERS";
            using (DbCommand cmd = Db.GetSqlStringCommand(sql))
            {
                using (IDataReader reader = this.Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        IRowMapper<USERS> rowMapper = MapBuilder<USERS>.MapAllProperties()
                            //.MapByName(x => x.SID)
                            //.DoNotMap(x => x.ParentItemId)
                            .Build();
                            //IRowMapper<USERS> rowMapper = MapBuilder<USERS>.BuildAllProperties();
                        USERS item = rowMapper.MapRow(reader);
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public void Insert(USERS users)
        {
            string sql = @"INSERT INTO USER (USR_ID,USR_NAME,PWD,EMAIL,USR_STATUS,MEMO,MUSER,MDATE,CUSER,CDATE) 
                VALUES (@USR_ID,@USR_NAME,@PWD,@EMAIL,@USR_STATUS,@MEMO,@MUSER,GETDATE(),@CUSER,GETDATE());";
            using (DbCommand cmd = Db.GetSqlStringCommand(sql))
            {
                Db.AddInParameter(cmd, "USR_ID", DbType.String, users.USR_ID);
                Db.AddInParameter(cmd, "USR_NAME", DbType.String, users.USR_NAME);
                Db.AddInParameter(cmd, "PWD", DbType.String, users.PWD);
                Db.AddInParameter(cmd, "EMAIL", DbType.String, users.EMAIL);
                Db.AddInParameter(cmd, "USR_STATUS", DbType.String, users.USR_STATUS);
                Db.AddInParameter(cmd, "MEMO", DbType.String, users.MEMO);
                Db.AddInParameter(cmd, "MUSER", DbType.String, users.MUSER);
                Db.AddInParameter(cmd, "CUSER", DbType.String, users.CUSER);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(USERS users)
        {
            throw new NotImplementedException();
        }

        public void Delete(USERS users)
        {
            throw new NotImplementedException();
        }

        public bool CheckPassword(string USR_ID, string PWD)
        {
            bool pass = false;

            string sql = "SELECT PWD FROM USERS WHERE USR_ID=@USR_ID ";
            using (DbCommand cmd = Db.GetSqlStringCommand(sql))
            {
                //DbParameter param = cmd.CreateParameter();
                //param.ParameterName = "@USR_ID";
                //param.Value = USR_ID;
                //cmd.Parameters.Add(param);

                Db.AddInParameter(cmd, "USR_ID", DbType.String, USR_ID);
                
                using (IDataReader reader = this.Db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        if (reader["PWD"].ToString() == PWD)
                        {
                            pass = true;
                        }
                    }
                }
            }

            return pass;
        }
   

    }
}
