using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TGBot.Helper;
using TGBot.Models;
using JSON = Newtonsoft.Json.JsonConvert;

namespace TGBot.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        private readonly IWebHostEnvironment webHostEnvironment;
        private ComplexEntity Database;

        public HomeController(ComplexEntity Database, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.Database = Database;
        }

        public IActionResult Index()
        {
            List<lTradePairLookup_List_Result> TradePairsModel  = Database.lTradePairLookup_List();
            return View(TradePairsModel);
        }
       
        public async Task<IActionResult> SaveTheTrade(TradeInfo_Set_Result.Parameters passData)
        {
            if(passData.tID == null)
            {
                passData.tID = 0;
            }

            var divideByNumber = 10000;
            if (passData.tTradingPair == 13)
            {
                divideByNumber = 10;
            }
            //IF its not gold
            
                switch (passData.tTradeType)
                {
                    case 1:
                        passData.tTPPips = (int)Math.Round(((decimal)(passData.tTp - passData.tCurrentPrice) * divideByNumber));
                        passData.tSlPips = (int)Math.Round(((decimal)(passData.tCurrentPrice - passData.tSL) * divideByNumber));
                        passData.tRiskRewardRadio = (decimal)passData.tTPPips / (decimal)passData.tSlPips;
                        break;
                    case 2:
                        passData.tTPPips = (int)Math.Round(((decimal)(passData.tCurrentPrice - passData.tTp) * divideByNumber));
                        passData.tSlPips = (int)Math.Round(((decimal)(passData.tSL - passData.tCurrentPrice) * divideByNumber));
                        passData.tRiskRewardRadio = (decimal)passData.tTPPips / (decimal)passData.tSlPips;
                        break;
                    case 3:
                        passData.tCurrentPrice = await GetPriceOfSelected((int)passData.tTradingPair);
                        passData.tTPPips = (int)Math.Round(((decimal)(passData.tTp - passData.tLimitOne) * divideByNumber));
                        passData.tSlPips = (int)Math.Round(((decimal)(passData.tLimitOne - passData.tSL) * divideByNumber));
                        passData.tRiskRewardRadio = (decimal)passData.tTPPips / (decimal)passData.tSlPips;
                        break;
                    case 4:
                        passData.tCurrentPrice = await GetPriceOfSelected((int)passData.tTradingPair);
                        passData.tTPPips = (int)Math.Round(((decimal)(passData.tLimitOne - passData.tTp) * divideByNumber));
                        passData.tSlPips = (int)Math.Round(((decimal)(passData.tSL - passData.tLimitOne) * divideByNumber));
                        passData.tRiskRewardRadio = (decimal)passData.tTPPips / (decimal)passData.tSlPips;
                        break;
                }
            
            
            


            



                
            var setData =  Database.TradeInfo_Set(passData);
            var configuration = GetConfiguration();
            var filename = configuration.GetSection("dirPath").Value;

            Process buyNow = Process.Start(new ProcessStartInfo()
            {

                FileName = Path.Combine(filename, "AutoTPSL.exe"),
                Arguments = setData.FirstOrDefault().tID.ToString(),

            });


         


            ProcessStartInfo pyArgs = new ProcessStartInfo();

            //DEV
            pyArgs.FileName = configuration.GetSection("python.exePath").Value;
            pyArgs.Arguments = string.Format("{0}", configuration.GetSection("pythonScript").Value);

            pyArgs.UseShellExecute = false;
            
            Process p = Process.Start(pyArgs);
                









            var path = Path.GetFullPath("TGBotConsole");
            return Json(path);
            
        }


        public IConfigurationRoot GetConfiguration()
        {
            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return build.Build();
        }


        public async Task<decimal> GetPriceOfSelected(int id)
        {

            lTradePairLookup_GetApiLinkByID_Result api  = Database.lTradePairLookup_GetApiLinkByID(id).FirstOrDefault();
            HttpResponseMessage yeet;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, api.lApiLink);
            request.Headers.Clear();
            request.Headers.Add("x-api-key", "6CxtVp2Ng73DYJsDMlwQi7e7TMo9LTjB5QXTlmG7");
            
            yeet = await client.SendAsync(request);
          
            
            
            
            var ok = yeet.Content;
            var oka = await yeet.Content.ReadAsStringAsync();
            if(oka.Contains("Limit Exceeded"))
            {
                client = new HttpClient();
                request = new HttpRequestMessage(HttpMethod.Get, api.lApiLink);
                request.Headers.Clear();
                request.Headers.Add("x-api-key", "wDgA1rmIJV2CJ635gIyZv54Rs4cOeyIU4rP5Kfsb");
                yeet = await client.SendAsync(request);
                ok = yeet.Content;
                oka = await yeet.Content.ReadAsStringAsync();
            }


            dynamic l = JSON.DeserializeObject(oka);

            
            decimal p = l.quoteResponse.result[0].regularMarketPrice;



            return p;


        }

  
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
