using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using TGBot.Models;
using JSON = Newtonsoft.Json.JsonConvert;

namespace TGBot.Helper
{
    public class ComplexEntity
    {
        /// <summary>
        /// Dependencies
        /// </summary>
        private readonly db AppSettings;
        

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="AppSettings">Application settings</param>
        /// <param name="Logger">Logger</param>
        public ComplexEntity()
        {
            AppSettings = new db();
            
        }

        /// <summary>
        /// Executes a stored procedure with the given parameters and returns a typed list 
        /// </summary>
        /// <typeparam name="T">Output type. Note, this is a singular object but the function will return a List&lt;T&gt;.</typeparam>
        /// <param name="procName">The name of the procedure to execute</param>
        /// <param name="parameters">List&lt;SqlParameter&gt; to execute the procedure with</param>
        /// <param name="databaseName">Optional. In not sent, the function will use the appsetting "DefaultConnection". Otherwise it will search appsettings for the setting name you've passed in and use that value.</param>
        /// <returns></returns>
        public List<T> ExecProc<T>(string procName, List<SqlParameter> parameters, string databaseName = null)
        {
            List<T> retList = new List<T>();

            List<IDictionary<string, object>> temp = new List<IDictionary<string, object>>();
            SqlConnection SqlConnection = null;
            SqlCommand SqlCommand = null;
            SqlDataReader SqlDataReader = null;
            try
            {
                if (string.IsNullOrEmpty(databaseName))
                {
                    SqlConnection = AppSettings.ConnectionStrings;
                }
                

                SqlCommand = new SqlCommand(procName, SqlConnection) { CommandType = System.Data.CommandType.StoredProcedure };
                SqlConnection.Open();

                if (parameters != null)
                {
                    SqlCommand.Parameters.AddRange(parameters.ToArray());
                }

                SqlDataReader = SqlCommand.ExecuteReader();
                if (SqlDataReader.HasRows)
                {
                    while (SqlDataReader.Read())
                    {
                        int c = 0;
                        IDictionary<string, object> data = new ExpandoObject();

                        while (c < SqlDataReader.VisibleFieldCount)
                        {
                            data[SqlDataReader.GetName(c)] = SqlDataReader.GetValue(c) is DBNull ? null : SqlDataReader.GetValue(c);

                            c++;
                        }
                        temp.Add(data);
                    }
                }
            }
            catch (SqlException sx)
            {
                
                throw;
            }
            catch (Exception x)
            {
                
                throw;
            }
            finally
            {
                if (SqlDataReader != null)
                {
                    SqlDataReader.Dispose();
                }

                if (SqlCommand != null)
                {
                    SqlCommand.Dispose();
                }

                if (SqlConnection != null)
                {
                    SqlConnection.Dispose();
                }
            }

            temp.ForEach(x =>
            {
                retList.Add(x.ToType<T>());
            });

            return retList;
        }

        /// <summary>
        /// Retrieves a string value from appsettings baring the key name that is passed in
        /// </summary>
        /// <param name="name">Connection string name</param>
        /// <returns>Connections string as string</returns>
        
    }

    public static class IDictExtensions
    {
        /// <summary>
        /// Does a conversion to a typed model
        /// </summary>
        /// <typeparam name="T">Output type</typeparam>
        /// <param name="me">IDictionay to convert</param>
        /// <returns>T</returns>
        public static T ToType<T>(this IDictionary<string, object> me)
        {
            return JSON.DeserializeObject<T>(JSON.SerializeObject(me));
        }
    }
}
