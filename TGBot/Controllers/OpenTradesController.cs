using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TGBot.Helper;
using TGBot.Models;
using TGBot.ViewModels;

namespace TGBot.Controllers
{
    [Authorize(Roles = "TraderAdmin")]
    public class OpenTradesController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private ComplexEntity _Database;
        public OpenTradesController(ComplexEntity Database, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            _Database = Database;
        }
        public IActionResult Index()
        {
            OpenTradesVM VM = new OpenTradesVM(_Database);
            return View(VM);
        }
        [HttpPost]
        public async void CloseTradeByID(int tID)
        {
            var getInfoByID = _Database.TradeInfo_GetByID(tID).FirstOrDefault();
            

            var controller = new HomeController(_Database, webHostEnvironment);
            decimal priceNowDuringClose = (decimal)getInfoByID.tCurrentPrice;
            if (getInfoByID.tLimitTwo != null)
            {
                priceNowDuringClose = (decimal)getInfoByID.tLimitTwo;
            }
            
            
            

            int closingPrice = 0;

            var multiplyByNumber = 100;
            if (getInfoByID.tTradingPair == 13)
            {
                multiplyByNumber = 10;
            }

            //TODO get the pip win or lost -
            switch (getInfoByID.tTradeType)
            {
                case 1:
                    closingPrice = (int)Math.Round((priceNowDuringClose - (decimal)getInfoByID.tCurrentPrice) * multiplyByNumber);
                    break;
                case 2:
                    closingPrice = (int)Math.Round(((decimal)getInfoByID.tCurrentPrice - priceNowDuringClose) * multiplyByNumber);
                    break;
                case 3:
                case 4:
                    var processToClose = _Database.GetProcessID_ByTicketID(getInfoByID.tID);
                    closingPrice = 0;
                    if(processToClose.Count > 0)
                    {
                        Process[] p = Process.GetProcesses();
                        foreach (var process in p)
                        {
                            if (process.Id == processToClose.FirstOrDefault().pProcessID)
                            {
                                process.Kill();
                                break;
                            }
                        }

                        if (getInfoByID.tLimitOrderHit != null && getInfoByID.tTradeType == 3)
                        {
                            closingPrice = (int)Math.Round((priceNowDuringClose - (decimal)getInfoByID.tLimitOne) * multiplyByNumber);
                        }

                        if (getInfoByID.tLimitOrderHit != null && getInfoByID.tTradeType == 4)
                        {
                            closingPrice = (int)Math.Round(((decimal)getInfoByID.tLimitOne - priceNowDuringClose) * multiplyByNumber);
                        }
                    }
                    
                    break;
            }

            _Database.ManuallyCloseTrade_ByID(tID, closingPrice);


            ProcessStartInfo pyArgs = new ProcessStartInfo();
            var configuration = GetConfiguration();
            //DEV
            pyArgs.FileName = configuration.GetSection("pythonexePath").Value;
            pyArgs.Arguments = string.Format("{0} {1}", configuration.GetSection("pythonScript").Value, tID);

            pyArgs.UseShellExecute = false;

            Process py = Process.Start(pyArgs);



            await SendReplyToMessage(tID);
           

        }
        public IConfigurationRoot GetConfiguration()
        {
            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return build.Build();
        }





        public async Task SendReplyToMessage(int id)
        {
            int api = _Database.TradeInfo_GetByID(id).FirstOrDefault().tTelegramMessageID;
            string apiLink = "https://api.telegram.org/bot5074478768:AAGgm7gcHeySMXo13qhw3fwwYHxx1F7S6eg/sendMessage?chat_id=@ShitTradinBot&reply_to_message_id="+ api + "&text=Close Trade";

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiLink);
            request.Headers.Clear();
            
            var yeet = await client.SendAsync(request);

            
        }
    }
}
