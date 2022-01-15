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
	/// Complex Data Entity: TradeInfo_DeleteByID
	/// </summary>
	public partial class TradeInfo_DeleteByID_Result
	{

		/// <summary>
		/// TradeInfo_DeleteByID.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? tID { get; set; }
			
			/// <summary>
			/// Call to create new TradeInfo_DeleteByID_Result.Parameters from a json string
			/// </summary>
			public static TradeInfo_DeleteByID_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<TradeInfo_DeleteByID_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to TradeInfo_DeleteByID stored procedure
		/// </summary>
		/// <param name="tID">int?</param>
		public static List<TradeInfo_DeleteByID_Result> TradeInfo_DeleteByID(this ComplexEntity ctx, int? tID)
		{
			List<TradeInfo_DeleteByID_Result> x = new List<TradeInfo_DeleteByID_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (tID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tID", tID));
			}
			
			x = ctx.ExecProc<TradeInfo_DeleteByID_Result>("TradeInfo_DeleteByID", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to TradeInfo_DeleteByID stored procedure
		/// </summary>
		/// <param name="pars">TradeInfo_DeleteByID.Parameters</param>
		public static List<TradeInfo_DeleteByID_Result> TradeInfo_DeleteByID(this ComplexEntity ctx, TradeInfo_DeleteByID_Result.Parameters pars)
		{
			return ctx.TradeInfo_DeleteByID(tID: pars.tID );
		}
	}
}
