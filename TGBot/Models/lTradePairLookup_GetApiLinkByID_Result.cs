using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 1/16/2022 at 6:50 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: lTradePairLookup_GetApiLinkByID
	/// </summary>
	public partial class lTradePairLookup_GetApiLinkByID_Result
	{
		public string lApiLink { get; set; }


		/// <summary>
		/// lTradePairLookup_GetApiLinkByID.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? lTradePairLookupID { get; set; }
			
			/// <summary>
			/// Call to create new lTradePairLookup_GetApiLinkByID_Result.Parameters from a json string
			/// </summary>
			public static lTradePairLookup_GetApiLinkByID_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<lTradePairLookup_GetApiLinkByID_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to lTradePairLookup_GetApiLinkByID stored procedure
		/// </summary>
		/// <param name="lTradePairLookupID">int?</param>
		public static List<lTradePairLookup_GetApiLinkByID_Result> lTradePairLookup_GetApiLinkByID(this ComplexEntity ctx, int? lTradePairLookupID)
		{
			List<lTradePairLookup_GetApiLinkByID_Result> x = new List<lTradePairLookup_GetApiLinkByID_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (lTradePairLookupID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@lTradePairLookupID", lTradePairLookupID));
			}
			
			x = ctx.ExecProc<lTradePairLookup_GetApiLinkByID_Result>("lTradePairLookup_GetApiLinkByID", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to lTradePairLookup_GetApiLinkByID stored procedure
		/// </summary>
		/// <param name="pars">lTradePairLookup_GetApiLinkByID.Parameters</param>
		public static List<lTradePairLookup_GetApiLinkByID_Result> lTradePairLookup_GetApiLinkByID(this ComplexEntity ctx, lTradePairLookup_GetApiLinkByID_Result.Parameters pars)
		{
			return ctx.lTradePairLookup_GetApiLinkByID(lTradePairLookupID: pars.lTradePairLookupID );
		}
	}
}
