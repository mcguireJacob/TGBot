using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 1/22/2022 at 5:27 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: ManuallyCloseTrade_ByID
	/// </summary>
	public partial class ManuallyCloseTrade_ByID_Result
	{

		/// <summary>
		/// ManuallyCloseTrade_ByID.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? tID { get; set; }
			public int? tManuallyClosedPips { get; set; }
			
			/// <summary>
			/// Call to create new ManuallyCloseTrade_ByID_Result.Parameters from a json string
			/// </summary>
			public static ManuallyCloseTrade_ByID_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<ManuallyCloseTrade_ByID_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to ManuallyCloseTrade_ByID stored procedure
		/// </summary>
		/// <param name="tID">int?</param>
		/// <param name="tManuallyClosedPips">int?</param>
		public static List<ManuallyCloseTrade_ByID_Result> ManuallyCloseTrade_ByID(this ComplexEntity ctx, int? tID, int? tManuallyClosedPips)
		{
			List<ManuallyCloseTrade_ByID_Result> x = new List<ManuallyCloseTrade_ByID_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (tID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tID", tID));
			}
			
			if (tManuallyClosedPips.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tManuallyClosedPips", tManuallyClosedPips));
			}
			
			x = ctx.ExecProc<ManuallyCloseTrade_ByID_Result>("ManuallyCloseTrade_ByID", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to ManuallyCloseTrade_ByID stored procedure
		/// </summary>
		/// <param name="pars">ManuallyCloseTrade_ByID.Parameters</param>
		public static List<ManuallyCloseTrade_ByID_Result> ManuallyCloseTrade_ByID(this ComplexEntity ctx, ManuallyCloseTrade_ByID_Result.Parameters pars)
		{
			return ctx.ManuallyCloseTrade_ByID(tID: pars.tID , tManuallyClosedPips: pars.tManuallyClosedPips );
		}
	}
}
