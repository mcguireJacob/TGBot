using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGBot.Helper;

namespace TGBot.ViewModels
{
    public class OpenTradesVM
    {


        public List<TradeInfo_List_Result> OpenTradeList {get; set;}

        public List<lTradeTypeLookup_List_Result> TradeTypeLookupList { get; set; }

        public List<lTradePairLookup_List_Result> TradePairLookupList { get; set; }

        private readonly ComplexEntity _Database;
        public OpenTradesVM(ComplexEntity Database)
        {
            _Database = Database;


            OpenTradeList = _Database.TradeInfo_List();
            TradeTypeLookupList = _Database.lTradeTypeLookup_List();
            TradePairLookupList = _Database.lTradePairLookup_List();
        }

    }
}
