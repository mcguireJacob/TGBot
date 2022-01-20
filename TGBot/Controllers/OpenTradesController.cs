using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult CloseTradeByID(int tID)
        {
            _Database.ManuallyCloseTrade_ByID(tID);
            return RedirectToAction("Index");


        }
    }
}
