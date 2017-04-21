using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCenter;
using Repository.Interface;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Repository.Implement.AdoNet
{

    public class FunctionImplement : IFunctionsInterface
    {
        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public FunctionImplement(string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                this._connectionString = connectionString;
            }
        }

        public FUNCTION GetOne(int id)
        {
            string sqlStatement = "SELECT SID,FUN_ID,FUN_NAME,FUN_DESC,FUN_STATUS,FUN_TYPE,FUN_PATH,FUN_FILE,PARENT_ID,SHOW_INDEX,CDATE,CUSER,MDATE,MUSER FROM FUNCTION WHERE SID = @SID";

            FUNCTION item = new FUNCTION();

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            using (SqlCommand comm = new SqlCommand(sqlStatement, conn))
            {
                comm.Parameters.Add(new SqlParameter("SID", id));

                if (conn.State != ConnectionState.Open) conn.Open();

                using (IDataReader reader = comm.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item.SID = int.Parse(reader["SID"].ToString());
                        item.FUN_ID = reader["FUN_ID"].ToString();
                        item.FUN_NAME = reader["FUN_NAME"].ToString();
                        item.FUN_DESC = reader["FUN_DESC"].ToString();
                        item.FUN_STATUS = reader["FUN_STATUS"].ToString();
                        item.FUN_TYPE = reader["FUN_TYPE"].ToString();
                        item.FUN_PATH = reader["FUN_PATH"].ToString();
                        item.FUN_FILE = reader["FUN_FILE"].ToString();
                        item.PARENT_ID = reader["PARENT_ID"].ToString();
                        item.SHOW_INDEX = int.Parse(reader["SHOW_INDEX"].ToString());
                        item.CDATE = DateTime.Parse(reader["CDATE"].ToString());
                        item.CUSER = reader["CUSER"].ToString();
                        item.MDATE = DateTime.Parse(reader["MDATE"].ToString());
                        item.MUSER = reader["MUSER"].ToString();
                        //for (int i = 0; i < reader.FieldCount; i++)
                        //{
                        //    PropertyInfo property = item.GetType().GetProperty(reader.GetName(i));

                        //    if (property != null && !reader.GetValue(i).Equals(DBNull.Value))
                        //    {
                        //        //ReflectionHelper.SetValue(property.Name, reader.GetValue(i), item);
                        //        //SetPropertyValue((object)item, property.Name, reader.GetValue(i));
                        //    }
                        //}
                    }
                }
            }
            return item;
        }

        public IEnumerable<FUNCTION> GetAll()
        {
            List<FUNCTION> programs = new List<FUNCTION>();

            string sqlStatement = "SELECT SID,FUN_ID,FUN_NAME,FUN_DESC,FUN_STATUS,FUN_TYPE,FUN_PATH,FUN_FILE,PARENT_ID,SHOW_INDEX,CDATE,CUSER,MDATE,MUSER FROM FUNCTION";

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
                        FUNCTION item = new FUNCTION();

                        item.SID = int.Parse(reader["SID"].ToString());
                        item.FUN_ID = reader["FUN_ID"].ToString();
                        item.FUN_NAME = reader["FUN_NAME"].ToString();
                        item.FUN_DESC = reader["FUN_DESC"].ToString();
                        item.FUN_STATUS = reader["FUN_STATUS"].ToString();
                        item.FUN_TYPE = reader["FUN_TYPE"].ToString();
                        item.FUN_PATH = reader["FUN_PATH"].ToString();
                        item.FUN_FILE = reader["FUN_FILE"].ToString();
                        item.PARENT_ID = reader["PARENT_ID"].ToString();
                        item.SHOW_INDEX = int.Parse(reader["SHOW_INDEX"].ToString());
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
                        programs.Add(item);
                    }
                }
            }

            return programs;
        }

        #region SetPropertyValue
        /// <summary>
        /// SetPropertyValue.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="val">The val.</param>
        /// <param name="instance">The instance.</param>
        public void SetPropertyValue(object instance, string propertyName, string val)
        {
            if (null == instance) return;

            Type type = instance.GetType();
            //PropertyInfo info = GetPropertyInfo(type, propertyName);
            PropertyInfo info = type.GetProperty(propertyName);

            if (null == info) return;

            try
            {
                if (info.PropertyType.Equals(typeof(string)))
                {
                    info.SetValue(instance, val, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(Boolean)))
                {
                    bool value = false;
                    value = val.ToLower().StartsWith("true");
                    info.SetValue(instance, value, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(int)))
                {
                    int value = String.IsNullOrEmpty(val) ? 0 : int.Parse(val);
                    info.SetValue(instance, value, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(double)))
                {
                    double value = 0.0d;
                    if (!String.IsNullOrEmpty(val))
                    {
                        value = Convert.ToDouble(val);
                    }
                    info.SetValue(instance, value, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(DateTime)))
                {
                    DateTime value = String.IsNullOrEmpty(val)
                        ? DateTime.MinValue
                        : DateTime.Parse(val);
                    info.SetValue(instance, value, new object[0]);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
