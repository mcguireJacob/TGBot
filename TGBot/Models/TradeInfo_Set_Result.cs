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
	/// Complex Data Entity: TradeInfo_Set
	/// </summary>
	public partial class TradeInfo_Set_Result
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


		/// <summary>
		/// TradeInfo_Set.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? tID { get; set; }
			public int? tTradeType { get; set; }
			public int? tTradingPair { get; set; }
			public decimal? tCurrentPrice { get; set; }
			public decimal? tLimitOne { get; set; }
			public decimal? tLimitTwo { get; set; }
			public decimal? tSL { get; set; }
			public decimal? tTp { get; set; }
			public bool? tHitSl { get; set; }
			public bool? tHitTp { get; set; }
			public DateTime? tTimePlaced { get; set; }
			public DateTime? tTradeClosed { get; set; }
			public bool? tManuallyClosed { get; set; }
			public int? tTelegramMessageID { get; set; }
			
			/// <summary>
			/// Call to create new TradeInfo_Set_Result.Parameters from a json string
			/// </summary>
			public static TradeInfo_Set_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<TradeInfo_Set_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to TradeInfo_Set stored procedure
		/// </summary>
		/// <param name="tID">int?</param>
		/// <param name="tTradeType">int?</param>
		/// <param name="tTradingPair">int?</param>
		/// <param name="tCurrentPrice">decimal?</param>
		/// <param name="tLimitOne">decimal?</param>
		/// <param name="tLimitTwo">decimal?</param>
		/// <param name="tSL">decimal?</param>
		/// <param name="tTp">decimal?</param>
		/// <param name="tHitSl">bool?</param>
		/// <param name="tHitTp">bool?</param>
		/// <param name="tTimePlaced">DateTime?</param>
		/// <param name="tTradeClosed">DateTime?</param>
		/// <param name="tManuallyClosed">bool?</param>
		/// <param name="tTelegramMessageID">int?</param>
		public static List<TradeInfo_Set_Result> TradeInfo_Set(this ComplexEntity ctx, int? tID, int? tTradeType, int? tTradingPair, decimal? tCurrentPrice, decimal? tLimitOne, decimal? tLimitTwo, decimal? tSL, decimal? tTp, bool? tHitSl, bool? tHitTp, DateTime? tTimePlaced, DateTime? tTradeClosed, bool? tManuallyClosed, int? tTelegramMessageID)
		{
			List<TradeInfo_Set_Result> x = new List<TradeInfo_Set_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (tID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tID", tID));
			}
			
			if (tTradeType.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tTradeType", tTradeType));
			}
			
			if (tTradingPair.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tTradingPair", tTradingPair));
			}
			
			if (tCurrentPrice.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tCurrentPrice", tCurrentPrice));
			}
			
			if (tLimitOne.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tLimitOne", tLimitOne));
			}
			
			if (tLimitTwo.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tLimitTwo", tLimitTwo));
			}
			
			if (tSL.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tSL", tSL));
			}
			
			if (tTp.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tTp", tTp));
			}
			
			if (tHitSl.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tHitSl", tHitSl));
			}
			
			if (tHitTp.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tHitTp", tHitTp));
			}
			
			if (tTimePlaced.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tTimePlaced", tTimePlaced));
			}
			
			if (tTradeClosed.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tTradeClosed", tTradeClosed));
			}
			
			if (tManuallyClosed.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tManuallyClosed", tManuallyClosed));
			}
			
			if (tTelegramMessageID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tTelegramMessageID", tTelegramMessageID));
			}
			
			x = ctx.ExecProc<TradeInfo_Set_Result>("TradeInfo_Set", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to TradeInfo_Set stored procedure
		/// </summary>
		/// <param name="pars">TradeInfo_Set.Parameters</param>
		public static List<TradeInfo_Set_Result> TradeInfo_Set(this ComplexEntity ctx, TradeInfo_Set_Result.Parameters pars)
		{
			return ctx.TradeInfo_Set(tID: pars.tID , tTradeType: pars.tTradeType , tTradingPair: pars.tTradingPair , tCurrentPrice: pars.tCurrentPrice , tLimitOne: pars.tLimitOne , tLimitTwo: pars.tLimitTwo , tSL: pars.tSL , tTp: pars.tTp , tHitSl: pars.tHitSl , tHitTp: pars.tHitTp , tTimePlaced: pars.tTimePlaced , tTradeClosed: pars.tTradeClosed , tManuallyClosed: pars.tManuallyClosed , tTelegramMessageID: pars.tTelegramMessageID );
		}
	}
}
