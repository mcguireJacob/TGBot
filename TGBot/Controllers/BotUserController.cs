using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGBot.Helper;
using TGBot.ViewModels;

namespace TGBot.Controllers
{
    public class BotUserController : Controller
    {
        private ComplexEntity Database;
        private readonly IHttpContextAccessor context;

        public BotUserController(ComplexEntity Database, IHttpContextAccessor context)
        {
            this.Database = Database;
            this.context = context;
        }
        public IActionResult Index()
        {
            BotUserVM VM = new BotUserVM(Database, context);
            
            return View(VM);
        }


        public void SaveAccountProfile(SetOrUpdateAccount_Result.Parameters passData)
        {
            if(passData.aID == null)
            {
                passData.aID = 0;
            }
            passData.aFixedLot = Decimal.Parse(passData.aFixedLot.ToString());
           
            passData.aAccountEmail = context.HttpContext.Session.GetString("userEmail");
            Database.SetOrUpdateAccount(passData);


        }
    }
}
