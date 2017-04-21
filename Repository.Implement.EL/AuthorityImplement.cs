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
    public class AuthorityImplement : BaseRepository, IAuthorityInterface
    {
        public AuthorityImplement(string connectionStringName)
            : base(connectionStringName)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AUTHORITY GetOne(int id)
        {
            AUTHORITY item = null;

            string sql = "SELECT SID,FUN_ID,USR_ID,GRP_ID,CDATE,CUSER,MDATE,MUSER FROM AUTHORITY WHERE SID=@SID";
            using (DbCommand cmd = Db.GetSqlStringCommand(sql))
            {
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "SID";
                param.Value = id;
                cmd.Parameters.Add(param);
                
                using (IDataReader reader = this.Db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        IRowMapper<AUTHORITY> rowMapper = MapBuilder<AUTHORITY>.MapAllProperties()
                                                  //.MapByName(x => x.SID)
                                                  //.DoNotMap(x => x.ParentItemId)
                                                  .Build();
                        //IRowMapper<AUTHORITY> rowMapper = MapBuilder<AUTHORITY>.BuildAllProperties();
                        item = rowMapper.MapRow(reader);
                    }
                }
            }
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AUTHORITY> GetAll()
        {
            List<AUTHORITY> items = new List<AUTHORITY>();

            string sql = "SELECT SID,FUN_ID,USR_ID,GRP_ID,CDATE,CUSER,MDATE,MUSER FROM AUTHORITY";
            using (DbCommand cmd = Db.GetSqlStringCommand(sql))
            {
                using (IDataReader reader = this.Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        IRowMapper<AUTHORITY> rowMapper = MapBuilder<AUTHORITY>.MapAllProperties()
                            //.MapByName(x => x.SID)
                            //.DoNotMap(x => x.ParentItemId)
                                                  .Build();
                        //IRowMapper<AUTHORITY> rowMapper = MapBuilder<AUTHORITY>.BuildAllProperties();
                        AUTHORITY item = rowMapper.MapRow(reader);
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FUNCTION> GetMenu(string USR_ID)
        {
            List<FUNCTION> functions = new List<FUNCTION>();

            string sql = @"SELECT F.FUN_ID,F.FUN_NAME,F.FUN_DESC,F.FUN_STATUS,F.FUN_TYPE,F.FUN_PATH,F.FUN_FILE,F.PARENT_ID,F.SHOW_INDEX,A.USR_ID
	            FROM FUNCTIONS F LEFT JOIN AUTHORITY A ON F.FUN_ID=A.FUN_ID AND A.USR_ID=@USR_ID
                ORDER BY F.SHOW_INDEX";

            using (DbCommand cmd = Db.GetSqlStringCommand(sql))
            {
                Db.AddInParameter(cmd, "USR_ID", DbType.String, USR_ID);
                using (IDataReader reader = this.Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        FUNCTION item = new FUNCTION();
                        item.FUN_ID = reader["FUN_ID"].ToString();
                        item.FUN_NAME = reader["FUN_NAME"].ToString();
                        item.FUN_DESC = reader["FUN_DESC"].ToString();
                        item.FUN_STATUS = reader["FUN_STATUS"].ToString();
                        item.FUN_TYPE = reader["FUN_TYPE"].ToString();
                        item.FUN_PATH = reader["FUN_PATH"].ToString();
                        item.FUN_FILE = reader["FUN_FILE"].ToString();
                        item.PARENT_ID = reader["PARENT_ID"].ToString();
                        item.SHOW_INDEX = int.Parse(reader["SHOW_INDEX"].ToString());
                        item.USR_ID = reader["USR_ID"].ToString();

                        functions.Add(item);
                    }
                }
            }
            return ConstructMenu(functions, "ROOT", USR_ID);
        }

        /// <summary>
        /// 建構功能選單
        /// </summary>
        /// <param name="functions"></param>
        /// <param name="parentId"></param>
        private List<FUNCTION> ConstructMenu(IEnumerable<FUNCTION> functions, string parentId, string USR_ID)
        {
            var currentItems = from items in functions
                               where items.PARENT_ID == parentId && items.FUN_STATUS=="Y" && (items.USR_ID == USR_ID || items.FUN_TYPE == "S" || items.FUN_TYPE == "F")
                               select items;
            List<FUNCTION> listItems = currentItems.ToList();

            if (listItems.Count() > 0)
            {
                foreach (var subitem in currentItems)
                {
                    subitem.ChildMenuItems = ConstructMenu(functions, subitem.FUN_ID, USR_ID);
                    if (subitem.ChildMenuItems.Count() == 0 && subitem.FUN_TYPE != "P")
                    {
                        listItems.Remove(subitem);
                    }
                }
            }

            return listItems;
        }

    }
}
