using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;
using SystemCenter;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Implement.AdoNet
{
    public class AuthorityImplement : IAuthorityInterface
    {

                private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public AuthorityImplement(string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                this._connectionString = connectionString;
            }
        }
        public AUTHORITY GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AUTHORITY> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AUTHORITY> UserAuthorization(string UserId)
        {
            List<AUTHORITY> authorities = new List<AUTHORITY>();

            string sqlStatement = "SELECT SID,FUN_ID,USR_ID,GRP_ID,CDATE,CUSER,MDATE,MUSER FROM AUTHORITY";

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            using (SqlCommand command = new SqlCommand(sqlStatement, conn))
            {
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AUTHORITY item = new AUTHORITY();

                        item.SID = int.Parse(reader["SID"].ToString());
                        item.USR_ID = reader["USR_ID "].ToString();
                        item.PRG_ID = reader["PRG_ID "].ToString();
                        item.CDATE = DateTime.Parse(reader["CDATE"].ToString());
                        item.CUSER = reader["CUSER"].ToString();
                        item.MDATE = DateTime.Parse(reader["MDATE"].ToString());
                        item.MUSER = reader["MUSER"].ToString();

                        //for (int i = 0; i < reader.FieldCount; i++)
                        //{
                        //    PropertyInfo property = item.GetType().GetProperty(reader.GetName(i));

                        //    if (property != null && !reader.GetValue(i).Equals(DBNull.Value))
                        //    {
                        //        ReflectionHelper.SetValue(property.Name, reader.GetValue(i), item);
                        //    }
                        //}
                        authorities.Add(item);
                    }
                }
            }

            return authorities;
        }

        public IEnumerable<AUTHORITY> UserAuthorization(string UserId, string SystemId)
        {
            throw new NotImplementedException();
        }

        public List<FUNCTION> GetMenu()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 建構功能選單
        /// </summary>
        /// <param name="functions"></param>
        /// <param name="parentId"></param>
        public List<FUNCTION> ConstructMenu(IEnumerable<FUNCTION> functions, string parentId)
        {
            var subitems = from items in functions
                           where items.PARENT_ID == parentId
                           select items;
            if (subitems.Count() > 0)
            {
                foreach (var item in subitems)
                {
                    item.ChildMenuItems = ConstructMenu(functions, item.FUN_ID);
                }
            }
            return subitems.ToList();
        }

    }
}
