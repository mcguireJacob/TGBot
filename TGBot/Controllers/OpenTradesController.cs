using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TGBot.Helper;
using TGBot.Models;
using TGBot.ViewModels;

namespace TGBot.Controllers
{
    public class OpenTradesController : Controller
    {
        
        private ComplexEntity _Database;
        public OpenTradesController(ComplexEntity Database)
        {
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
            

            var controller = new HomeController(_Database);

            decimal priceNowDuringClose = await controller.GetPriceOfSelected((int)getInfoByID.tTradingPair);

            int closingPrice = 0;

           


            //TODO get the pip win or lost -
            switch (getInfoByID.tTradeType)
            {
                case 1:
                    closingPrice = (int)Math.Round((priceNowDuringClose - (decimal)getInfoByID.tCurrentPrice) * 10000);
                    break;
                case 2:
                    closingPrice = (int)Math.Round(((decimal)getInfoByID.tCurrentPrice - priceNowDuringClose) * 10000);
                    break;
            }



           

            _Database.ManuallyCloseTrade_ByID(tID, closingPrice);



            await SendReplyToMessage(tID);
            



            


        }

        



        public async Task SendReplyToMessage(int id)
        {
            int api = _Database.TradeInfo_GetByID(id).FirstOrDefault().tTelegramMessageID;
            string apiLink = "https://api.telegram.org/bot5074478768:AAGgm7gcHeySMXo13qhw3fwwYHxx1F7S6eg/sendMessage?chat_id=@ShitTradinBot&reply_to_message_id="+ api + "&text=Close Trade NOW";

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiLink);
            request.Headers.Clear();
            
            var yeet = await client.SendAsync(request);

            
        }
    }
}
