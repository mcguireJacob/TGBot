using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TGBot.Helper;
using TGBot.Models;

namespace TGBot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private db db = new db();
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

        //public IActionResult SaveTheTrade(TradeInfo_Set_Result ti)
        //{
        //    Database.TradeInfo_Set(ti);
        //}

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
