using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
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




            try
            {
                CheckIfHitTakeProfitOrStopLoss(tID);
            }
            catch(Exception e)
            {
                if (File.Exists("LogFileForConsole.txt"))
                {
                    File.WriteAllText("LogFileForConsole.txt", e.Message);
                }
                else
                {
                    File.Create("FILEOfFIlining.txt");
                }
                
            }
            




        }



        public static void CheckIfHitTakeProfitOrStopLoss(string tID)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("tID", tID));

            var data = hitTheDB<TradeInfo_GetByID_Result>("TradeInfo_GetByID", parameters).FirstOrDefault();
            parameters.Clear();

            

            parameters.Add(new SqlParameter("lTradePairLookupID", data.tTradingPair));
            var PairType = hitTheDB<TradingPair>("lTradePairLookup_GetPairByID", parameters).FirstOrDefault().lTradePair;

            try
            {
                UpdateCurrentPrice(PairType, tID);
            }
            catch (Exception e)
            {
                if (File.Exists("LogFileForConsole.txt"))
                {
                    File.WriteAllText("LogFileForConsole.txt", e.Message);
                }

            }
            


            parameters.Clear();


            var priceOfAssetCurrently = data.tLimitTwo;


          
            


            while (!data.tManuallyClosed)
            {
                parameters.Add(new SqlParameter("tID", tID));
                data = hitTheDB<TradeInfo_GetByID_Result>("TradeInfo_GetByID", parameters).FirstOrDefault();
                parameters.Clear();
                
                Console.WriteLine("Price Of Asseet : " + priceOfAssetCurrently);

                


                parameters.Add(new SqlParameter("tID", tID));
                priceOfAssetCurrently = hitTheDB<TradeInfo_GetByID_Result>("TradeInfo_GetByID", parameters).FirstOrDefault().tLimitTwo;
                parameters.Clear();
                System.Threading.Thread.Sleep(800);



                switch (data.tTradeType)
                {
                    case 1:
                        if(priceOfAssetCurrently >= data.tTp)
                        {
                            TPSL(tID, 1);
                        }
                        if(priceOfAssetCurrently <= data.tSL)
                        {

                            TPSL(tID, 2);
                        }
                        break;
                    case 2:
                        if (priceOfAssetCurrently <= data.tTp)
                        {
                            TPSL(tID, 1);

                        }
                        if (priceOfAssetCurrently >= data.tSL)
                        {
                            TPSL(tID, 2);

                        }
                        break;
                    case 3:
                        if(priceOfAssetCurrently <= data.tLimitOne && data.tLimitOrderHit == null)
                        {
                            TPSL(tID, 3);
                        }
                        if (priceOfAssetCurrently >= data.tTp && data.tLimitOrderHit != null)
                        {
                            TPSL(tID, 1);
                        }
                        if (priceOfAssetCurrently <= data.tSL && data.tLimitOrderHit != null)
                        {
                            TPSL(tID, 2);
                        }

                        break;
                    case 4:

                        if (priceOfAssetCurrently >= data.tLimitOne && data.tLimitOrderHit == null)
                        {
                            TPSL(tID, 4);
                        }
                        if (priceOfAssetCurrently <= data.tTp && data.tLimitOrderHit != null)
                        {
                            TPSL(tID, 1);
                        }
                        if (priceOfAssetCurrently >= data.tSL && data.tLimitOrderHit != null)
                        {
                            TPSL(tID, 2);
                        }
                        break;
                }

            }
            killPythonScript(tID);


        }

        public static void TPSL(string tID, int winLoss)
        {
            //Local Testing
            //var hardcodedPathToConsoleAPP = "C:\\Users\\Jacob\\Documents\\repo\\TGBotConsole\\AutoTPSL.exe";


            //Deployment
            var hardcodedPathToConsoleAPP = "C:\\inetpub\\wwwroot\\TGBotConsole\\AutoTPSL.exe";



            Console.WriteLine(winLoss);

            List<SqlParameter> parameters = new List<SqlParameter>();
            switch (winLoss)
            {
                case 1:
                    parameters.Add(new SqlParameter("tID", tID));
                    hitTheDB<TID>("TradeHitTP", parameters);
                    SendReplyToMessage(tID, winLoss).Wait();
                    killPythonScript(tID);
                    
                    break;
                case 2:
                    parameters.Add(new SqlParameter("tID", tID));
                    hitTheDB<TID>("TradeHitSL", parameters);
                    SendReplyToMessage(tID, winLoss).Wait();
                    killPythonScript(tID);
                    
                    break;
                case 3:
                    parameters.Add(new SqlParameter("tID", tID));
                    hitTheDB<TID>("TradeHitLimit", parameters);
                    SendReplyToMessage(tID, winLoss).Wait();
                    
                    Process buyNow = Process.Start(new ProcessStartInfo()
                    {

                        FileName = Path.Combine(hardcodedPathToConsoleAPP),
                        Arguments = tID,

                    });
                    parameters.Clear();
                    parameters.Add(new SqlParameter("pTradeID", tID));
                    parameters.Add(new SqlParameter("pProcessID", buyNow.Id));
                    hitTheDB<SetProcessID_Result>("SetProcessID", parameters);
                    killPythonScript(tID, buyNow.Id);
                    break;
                case 4:
                    parameters.Add(new SqlParameter("tID", tID));
                    hitTheDB<TID>("TradeHitLimit", parameters);
                    SendReplyToMessage(tID, winLoss).Wait();
                    
                    Process SellNow = Process.Start(new ProcessStartInfo()
                    {

                        FileName = Path.Combine(hardcodedPathToConsoleAPP),
                        Arguments = tID,

                    });
                    parameters.Clear();
                    parameters.Add(new SqlParameter("pTradeID", tID));
                    parameters.Add(new SqlParameter("pProcessID", SellNow.Id));
                    hitTheDB<SetProcessID_Result>("SetProcessID", parameters);
                    killPythonScript(tID, SellNow.Id);
                    break;
            }
        }


        public static async Task<bool> SendReplyToMessage(string id, int TPSL)
        {
            var messageTPSL = "";
            //1 for TP 2 for SL
            switch (TPSL)
            {
                case 1:
                    messageTPSL = "TP HIT";
                    break;
                case 2:
                    messageTPSL = "SL HIT";
                    break;
                case 3:
                case 4:
                    messageTPSL = "Limit Order Filled";
                    break;
            }
                
                
            
            
            
                
            

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("tID", id));
            int api = hitTheDB<TradeInfo_GetByID_Result>("TradeInfo_GetByID",parameters).FirstOrDefault().tTelegramMessageID;
            string apiLink = "https://api.telegram.org/bot5074478768:AAGgm7gcHeySMXo13qhw3fwwYHxx1F7S6eg/sendMessage?chat_id=@ShitTradinBot&reply_to_message_id=" + api + "&text=" + messageTPSL;

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiLink);
            request.Headers.Clear();

            
            var ok = await client.SendAsync(request);

            



            return true;

           

        }




        public static void UpdateCurrentPrice(string pair, string tID )
        {
            pair = pair.Insert(3, "/");


            ProcessStartInfo pyArgs = new ProcessStartInfo();

            //DEV
            //pyArgs.FileName = "C:\\Users\\Jacob\\AppData\\Local\\Programs\\Python\\Python310\\python.exe";
            //pyArgs.Arguments = string.Format("{0} {1} {2}", "C:\\Users\\Jacob\\Documents\\repo\\TGBot\\GetPrices.py", pair, tID);

            //PROD

            pyArgs.FileName = "C:\\Python\\python.exe";
            pyArgs.Arguments = string.Format("{0} {1} {2}", "C:\\inetpub\\wwwroot\\GetPrices.py", pair, tID);




            pyArgs.UseShellExecute = false;

            try
            {
                Process p = Process.Start(pyArgs);
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("pTradeID", tID));
                parameters.Add(new SqlParameter("pProcessID", p.Id));
                hitTheDB<SetProcessID_Result>("SetProcessID", parameters);
            }
            catch (Exception e)
            {
                if (File.Exists("LogFileForConsole.txt"))
                {
                    File.WriteAllText("LogFileForConsole.txt", e.Message);
                }

            }
            

            

        }

        public static void killPythonScript(string tID, int limitProcessID = -1)
        {

            if (File.Exists("LogFileForConsole.txt"))
            {
                File.WriteAllText("LogFileForConsole.txt", "Killing All Proccesses TID : " + tID);
            }


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("pTradeID", tID));
            
            var processToClose = hitTheDB<SetProcessID_Result>("GetProcessID_ByTicketID", parameters);
            List<Process> listOfProcessIDs = new List<Process>();
            if (processToClose.Count > 0)
            {
                Process[] p = Process.GetProcesses();
                foreach (var proc in processToClose)
                {
                    foreach(var process in p)
                    {
                        if (process.Id == proc.pProcessID)
                        {
                            listOfProcessIDs.Add(process);
                        }
                    }
                    
                }

                foreach(var procc in listOfProcessIDs)
                {
                    if(procc.Id != limitProcessID)
                    {
                        procc.Kill();
                    }
                    
                }
                Environment.Exit(0);
            }
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


                SqlConnection.ConnectionString = "Server=TGBOT\\SQLEXPRESS;Initial Catalog=Trades;User Id=sa;Password=sa";
                //SqlConnection.ConnectionString = "Server=localhost\\SQLEXPRESS;Initial Catalog=Trades;Trusted_Connection=True;";





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
        public bool? tLimitOrderHit { get; set; }
    }


    public partial class TradingPair
    {
        public string lTradePair { get; set; }
    }


    public partial class SetProcessID_Result
    {
        public int? pTradeID { get; set; }
        public int? pProcessID { get; set; }

    }

    
}
