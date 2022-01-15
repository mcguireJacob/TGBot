using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 1/14/2022 at 10:39 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: usp_TradeInfoUpdate
	/// </summary>
	public partial class usp_TradeInfoUpdate_Result
	{
		public int tID { get; set; }
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


		/// <summary>
		/// usp_TradeInfoUpdate.Parameters object
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
			
			/// <summary>
			/// Call to create new usp_TradeInfoUpdate_Result.Parameters from a json string
			/// </summary>
			public static usp_TradeInfoUpdate_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<usp_TradeInfoUpdate_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to usp_TradeInfoUpdate stored procedure
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
		public static List<usp_TradeInfoUpdate_Result> usp_TradeInfoUpdate(this ComplexEntity ctx, int? tID, int? tTradeType, int? tTradingPair, decimal? tCurrentPrice, decimal? tLimitOne, decimal? tLimitTwo, decimal? tSL, decimal? tTp, bool? tHitSl, bool? tHitTp, DateTime? tTimePlaced, DateTime? tTradeClosed, bool? tManuallyClosed)
		{
			List<usp_TradeInfoUpdate_Result> x = new List<usp_TradeInfoUpdate_Result>();
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
			
			x = ctx.ExecProc<usp_TradeInfoUpdate_Result>("usp_TradeInfoUpdate", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to usp_TradeInfoUpdate stored procedure
		/// </summary>
		/// <param name="pars">usp_TradeInfoUpdate.Parameters</param>
		public static List<usp_TradeInfoUpdate_Result> usp_TradeInfoUpdate(this ComplexEntity ctx, usp_TradeInfoUpdate_Result.Parameters pars)
		{
			return ctx.usp_TradeInfoUpdate(tID: pars.tID , tTradeType: pars.tTradeType , tTradingPair: pars.tTradingPair , tCurrentPrice: pars.tCurrentPrice , tLimitOne: pars.tLimitOne , tLimitTwo: pars.tLimitTwo , tSL: pars.tSL , tTp: pars.tTp , tHitSl: pars.tHitSl , tHitTp: pars.tHitTp , tTimePlaced: pars.tTimePlaced , tTradeClosed: pars.tTradeClosed , tManuallyClosed: pars.tManuallyClosed );
		}
	}
}
