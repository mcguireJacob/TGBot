using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        

        
        private ComplexEntity Database;

        public HomeController(ComplexEntity Database)
        {
            
            this.Database = Database;
        }

        public IActionResult Index()
        {
            List<TradeInfo_List_Result> ok  = Database.TradeInfo_List();
            return View();
        }
       
        public async Task<IActionResult> SaveTheTrade(TradeInfo_Set_Result.Parameters passData)
        {
            if(passData.tID == null)
            {
                passData.tID = 0;
            }


            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  THIS NEEDS TO CHANGE FOR GOLD AND US30
            switch (passData.tTradeType)
            {
                case 1:
                    passData.tTPPips = (int)Math.Round(((decimal)(passData.tTp - passData.tCurrentPrice) * 10000));
                    passData.tSlPips = (int)Math.Round(((decimal)(passData.tCurrentPrice - passData.tSL) * 10000));
                    passData.tRiskRewardRadio = passData.tTPPips / passData.tSlPips;
                    break;
                case 2:
                    passData.tTPPips = (int)Math.Round(((decimal)(passData.tCurrentPrice - passData.tTp) * 10000));
                    passData.tSlPips = (int)Math.Round(((decimal)(passData.tSL - passData.tCurrentPrice) * 10000));
                    passData.tRiskRewardRadio = passData.tTPPips / passData.tSlPips;
                    break;
                case 3:
                    passData.tCurrentPrice = await GetPriceOfSelected((int)passData.tTradingPair);
                    passData.tTPPips = (int)Math.Round(((decimal)(passData.tTp - passData.tLimitOne) * 10000));
                    passData.tSlPips = (int)Math.Round(((decimal)(passData.tLimitOne - passData.tSL) * 10000));
                    passData.tRiskRewardRadio = passData.tTPPips / passData.tSlPips;
                    break;
                case 4:
                    passData.tCurrentPrice = await GetPriceOfSelected((int)passData.tTradingPair);
                    passData.tTPPips = (int)Math.Round(((decimal)(passData.tLimitOne - passData.tTp) * 10000));
                    passData.tSlPips = (int)Math.Round(((decimal)(passData.tSL - passData.tLimitOne) * 10000));
                    passData.tRiskRewardRadio = passData.tTPPips / passData.tSlPips;
                    break;
            }

            



                
                Database.TradeInfo_Set(passData);


            return Json(true);
            
        }


        


        public async Task<decimal> GetPriceOfSelected(int id)
        {

            lTradePairLookup_GetApiLinkByID_Result api  = Database.lTradePairLookup_GetApiLinkByID(id).FirstOrDefault();

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, api.lApiLink);
            request.Headers.Clear();
            request.Headers.Add("x-api-key", "6CxtVp2Ng73DYJsDMlwQi7e7TMo9LTjB5QXTlmG7");
            var yeet = await client.SendAsync(request);
            var ok = yeet.Content;
            var oka = await yeet.Content.ReadAsStringAsync();
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
