using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 1/30/2022 at 3:06 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: TradeInfo_SearchByDate
	/// </summary>
	public partial class TradeInfo_SearchByDate_Result
	{
		public string lTradePair { get; set; }
		public int? tSlPips { get; set; }
		public int? tTPPips { get; set; }
		public bool tHitSl { get; set; }
		public bool tHitTp { get; set; }
		public bool tManuallyClosed { get; set; }
		public int? tManuallyClosedPips { get; set; }
		public int? WinRatio { get; set; }


		/// <summary>
		/// TradeInfo_SearchByDate.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public DateTime? from { get; set; }
			public DateTime? to { get; set; }
			
			/// <summary>
			/// Call to create new TradeInfo_SearchByDate_Result.Parameters from a json string
			/// </summary>
			public static TradeInfo_SearchByDate_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<TradeInfo_SearchByDate_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to TradeInfo_SearchByDate stored procedure
		/// </summary>
		/// <param name="from">DateTime?</param>
		/// <param name="to">DateTime?</param>
		public static List<TradeInfo_SearchByDate_Result> TradeInfo_SearchByDate(this ComplexEntity ctx, DateTime? from, DateTime? to)
		{
			List<TradeInfo_SearchByDate_Result> x = new List<TradeInfo_SearchByDate_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (from.HasValue)
			{
				sqlParams.Add(new SqlParameter("@from", from));
			}
			
			if (to.HasValue)
			{
				sqlParams.Add(new SqlParameter("@to", to));
			}
			
			x = ctx.ExecProc<TradeInfo_SearchByDate_Result>("TradeInfo_SearchByDate", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to TradeInfo_SearchByDate stored procedure
		/// </summary>
		/// <param name="pars">TradeInfo_SearchByDate.Parameters</param>
		public static List<TradeInfo_SearchByDate_Result> TradeInfo_SearchByDate(this ComplexEntity ctx, TradeInfo_SearchByDate_Result.Parameters pars)
		{
			return ctx.TradeInfo_SearchByDate(from: pars.from , to: pars.to );
		}
	}
}
