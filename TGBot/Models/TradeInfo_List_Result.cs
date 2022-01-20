using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 1/19/2022 at 10:04 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: TradeInfo_List
	/// </summary>
	public partial class TradeInfo_List_Result
	{
		public int tID { get; set; }
		public int? tTradeType { get; set; }
		public int? tTradingPair { get; set; }
		public decimal? tCurrentPrice { get; set; }
		public decimal? tLimitOne { get; set; }
		public decimal? tLimitTwo { get; set; }
		public decimal? tSL { get; set; }
		public decimal? tTp { get; set; }
		public bool tHitSl { get; set; }
		public bool tHitTp { get; set; }
		public DateTime? tTimePlaced { get; set; }
		public DateTime? tTradeClosed { get; set; }
		public bool tManuallyClosed { get; set; }
		public int tTelegramMessageID { get; set; }

	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to TradeInfo_List stored procedure
		/// </summary>
		public static List<TradeInfo_List_Result> TradeInfo_List(this ComplexEntity ctx )
		{
			List<TradeInfo_List_Result> x = new List<TradeInfo_List_Result>();
			x = ctx.ExecProc<TradeInfo_List_Result>("TradeInfo_List", new List<SqlParameter>());
			
			return x;
			
		}
	}
}
