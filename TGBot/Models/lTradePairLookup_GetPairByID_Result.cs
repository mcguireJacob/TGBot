using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/27/2022 at 6:01 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: lTradePairLookup_GetPairByID
	/// </summary>
	public partial class lTradePairLookup_GetPairByID_Result
	{
		public string lTradePair { get; set; }


		/// <summary>
		/// lTradePairLookup_GetPairByID.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? lTradePairLookupID { get; set; }
			
			/// <summary>
			/// Call to create new lTradePairLookup_GetPairByID_Result.Parameters from a json string
			/// </summary>
			public static lTradePairLookup_GetPairByID_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<lTradePairLookup_GetPairByID_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to lTradePairLookup_GetPairByID stored procedure
		/// </summary>
		/// <param name="lTradePairLookupID">int?</param>
		public static List<lTradePairLookup_GetPairByID_Result> lTradePairLookup_GetPairByID(this ComplexEntity ctx, int? lTradePairLookupID)
		{
			List<lTradePairLookup_GetPairByID_Result> x = new List<lTradePairLookup_GetPairByID_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (lTradePairLookupID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@lTradePairLookupID", lTradePairLookupID));
			}
			
			x = ctx.ExecProc<lTradePairLookup_GetPairByID_Result>("lTradePairLookup_GetPairByID", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to lTradePairLookup_GetPairByID stored procedure
		/// </summary>
		/// <param name="pars">lTradePairLookup_GetPairByID.Parameters</param>
		public static List<lTradePairLookup_GetPairByID_Result> lTradePairLookup_GetPairByID(this ComplexEntity ctx, lTradePairLookup_GetPairByID_Result.Parameters pars)
		{
			return ctx.lTradePairLookup_GetPairByID(lTradePairLookupID: pars.lTradePairLookupID );
		}
	}
}
