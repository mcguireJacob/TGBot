using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGBot.Helper;

namespace TGBot.ViewModels
{
    public class BotUserVM
    {
        private ComplexEntity Database;
        private readonly IHttpContextAccessor context;
        public GetAccount_ByEmail_Result Account_ByEmail_Result { get; set; }

        public List<GetServerList_Result> ServerList { get
            {
                return Database.GetServerList();
            } 
        }

        public BotUserVM(ComplexEntity Database, IHttpContextAccessor context)
        {
            this.Database = Database;
            this.context = context;
            Account_ByEmail_Result = Database.GetAccount_ByEmail(context.HttpContext.Session.GetString("userEmail")).FirstOrDefault() ?? new GetAccount_ByEmail_Result();
        }


    }
}
