using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/5/2022 at 1:00 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: TradeInfo_GetByID
	/// </summary>
	public partial class TradeInfo_GetByID_Result
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
		public decimal? tRiskRewardRadio { get; set; }
		public int? tManuallyClosedPips { get; set; }
		public bool? tLimitOrderHit { get; set; }


		/// <summary>
		/// TradeInfo_GetByID.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? tID { get; set; }
			
			/// <summary>
			/// Call to create new TradeInfo_GetByID_Result.Parameters from a json string
			/// </summary>
			public static TradeInfo_GetByID_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<TradeInfo_GetByID_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to TradeInfo_GetByID stored procedure
		/// </summary>
		/// <param name="tID">int?</param>
		public static List<TradeInfo_GetByID_Result> TradeInfo_GetByID(this ComplexEntity ctx, int? tID)
		{
			List<TradeInfo_GetByID_Result> x = new List<TradeInfo_GetByID_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (tID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tID", tID));
			}
			
			x = ctx.ExecProc<TradeInfo_GetByID_Result>("TradeInfo_GetByID", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to TradeInfo_GetByID stored procedure
		/// </summary>
		/// <param name="pars">TradeInfo_GetByID.Parameters</param>
		public static List<TradeInfo_GetByID_Result> TradeInfo_GetByID(this ComplexEntity ctx, TradeInfo_GetByID_Result.Parameters pars)
		{
			return ctx.TradeInfo_GetByID(tID: pars.tID );
		}
	}
}
