using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using JSON = Newtonsoft.Json.JsonConvert;

namespace AutoTPSL
{
    class Program
    {
        static void Main(string[] args)
        {
            var tID = "";
            if(args[0] != null)
            {
                 tID = args[0];
            }
            



            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("tID", tID));

            var data = hitTheDB<TradeInfo_GetByID_Result>("TradeInfo_GetByID", parameters);




        }



        public static List<T> hitTheDB<T>(string procName, List<SqlParameter> parameters)
        {


            List<T> retList = new List<T>();

            List<IDictionary<string, object>> temp = new List<IDictionary<string, object>>();
            SqlConnection SqlConnection = new SqlConnection();
            SqlCommand SqlCommand = null;
            SqlDataReader SqlDataReader = null;
            try
            {


                SqlConnection.ConnectionString = "Server=localhost\\SQLEXPRESS;Initial Catalog=Trades;Trusted_Connection=True;";





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

    public partial class TradeInfo_GetByID_Result
    {
        public int tID { get; set; }
        public int? tTradeType { get; set; }
        public int? tTradingPair { get; set; }
        public decimal? tCurrentPrice { get; set; }
        public decimal? tLimitOne { get; set; }
        public decimal? tLimitTwo { get; set; }
        public decimal? tSL { get; set; }
        public decimal? tTp { get; set; }
        public bool tHitSl { get; set; }
        public bool tHitTp { get; set; }
        public DateTime? tTimePlaced { get; set; }
        public DateTime? tTradeClosed { get; set; }
        public bool tManuallyClosed { get; set; }
        public int tTelegramMessageID { get; set; }
        public decimal? tRiskRewardRadio { get; set; }
        public int? tManuallyClosedPips { get; set; }
    }
}
