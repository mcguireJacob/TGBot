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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        
        private ComplexEntity Database;

        public HomeController(ILogger<HomeController> logger, ComplexEntity Database)
        {
            _logger = logger;
            this.Database = Database;
        }

        public IActionResult Index()
        {
            List<TradeInfo_List_Result> ok  = Database.TradeInfo_List();
            return View();
        }
       
        public IActionResult SaveTheTrade(TradeInfo_Set_Result.Parameters passData)
        {
            if(passData.tID == null)
            {
                passData.tID = 0;
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


        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
