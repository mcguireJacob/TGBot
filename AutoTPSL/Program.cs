using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using JSON = Newtonsoft.Json.JsonConvert;

namespace AutoTPSL
{
    class Program
    {
        static void Main(string[] args)
        {
            var tID = "";
            if( args.Length != 0 )
            {
                 tID = args[0];
            }


            tID = "30";



            
            CheckIfHitTakeProfitOrStopLoss(tID);




        }



        public async static void CheckIfHitTakeProfitOrStopLoss(string tID)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("tID", tID));

            var data = hitTheDB<TradeInfo_GetByID_Result>("TradeInfo_GetByID", parameters).FirstOrDefault();
            parameters.Clear();

            var currentPrice = data.tCurrentPrice;


            var priceOfAssetCurrently = data.tLimitTwo;

            


            while (!data.tManuallyClosed)
            {
                parameters.Add(new SqlParameter("tID", tID));
                data = hitTheDB<TradeInfo_GetByID_Result>("TradeInfo_GetByID", parameters).FirstOrDefault();
                parameters.Clear();
                Console.WriteLine("Current price: " + currentPrice);
                Console.WriteLine("Price Of Asseet : " + priceOfAssetCurrently);

                parameters.Add(new SqlParameter("tID", tID));
                priceOfAssetCurrently = hitTheDB<TradeInfo_GetByID_Result>("TradeInfo_GetByID", parameters).FirstOrDefault().tLimitTwo;
                parameters.Clear();
                System.Threading.Thread.Sleep(800);



                switch (data.tTradeType)
                {
                    case 1:
                        if(priceOfAssetCurrently > data.tTp)
                        {
                            parameters.Add(new SqlParameter("tID", tID));
                            var sauce = hitTheDB<TID>("TradeHitTP", parameters);
                            await SendReplyToMessage(tID, 1);
                            Console.WriteLine("Sauce");
                            
                        }
                        if(priceOfAssetCurrently < data.tSL)
                        {
                            parameters.Add(new SqlParameter("tID", tID));
                            var sauce = hitTheDB<TID>("TradeHitSL", parameters);
                            await SendReplyToMessage(tID, 2);
                            Console.WriteLine("SL");
                            
                        }
                        break;
                    case 2:
                        if (priceOfAssetCurrently < data.tTp)
                        {
                            parameters.Add(new SqlParameter("tID", tID));
                            var sauce = hitTheDB<TID>("TradeHitTP", parameters);
                            Console.WriteLine("Sauce");
                            
                        }
                        if (priceOfAssetCurrently > data.tSL)
                        {
                            parameters.Add(new SqlParameter("tID", tID));
                            var sauce = hitTheDB<TID>("TradeHitSL", parameters);
                            Console.WriteLine("SL");
                            
                        }
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }

            }
        }


        public static async Task SendReplyToMessage(string id, int TPSL)
        {
            var messageTPSL = "";
            //1 for TP 2 for SL
            if(TPSL == 1)
            {
                messageTPSL = "TP HIT";
            }
            else
            {
                messageTPSL = "SL HIT";
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("tID", id));
            int api = hitTheDB<TradeInfo_GetByID_Result>("TradeInfo_GetByID",parameters).FirstOrDefault().tTelegramMessageID;
            string apiLink = "https://api.telegram.org/bot5074478768:AAGgm7gcHeySMXo13qhw3fwwYHxx1F7S6eg/sendMessage?chat_id=@ShitTradinBot&reply_to_message_id=" + api + "&text=" + messageTPSL;

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiLink);
            request.Headers.Clear();

            
            await client.SendAsync(request);
            
           
            


           

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

    public partial class TID
    {
        public int tID { get; set; }
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
