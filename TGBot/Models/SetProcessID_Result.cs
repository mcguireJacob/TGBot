using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/27/2022 at 2:42 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: SetProcessID
	/// </summary>
	public partial class SetProcessID_Result
	{

		/// <summary>
		/// SetProcessID.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? pTradeID { get; set; }
			public int? pProcessID { get; set; }
			public bool? pProcessAdminGetPrice { get; set; }
			
			/// <summary>
			/// Call to create new SetProcessID_Result.Parameters from a json string
			/// </summary>
			public static SetProcessID_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<SetProcessID_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to SetProcessID stored procedure
		/// </summary>
		/// <param name="pTradeID">int?</param>
		/// <param name="pProcessID">int?</param>
		/// <param name="pProcessAdminGetPrice">bool?</param>
		public static List<SetProcessID_Result> SetProcessID(this ComplexEntity ctx, int? pTradeID, int? pProcessID, bool? pProcessAdminGetPrice)
		{
			List<SetProcessID_Result> x = new List<SetProcessID_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (pTradeID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@pTradeID", pTradeID));
			}
			
			if (pProcessID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@pProcessID", pProcessID));
			}
			
			if (pProcessAdminGetPrice.HasValue)
			{
				sqlParams.Add(new SqlParameter("@pProcessAdminGetPrice", pProcessAdminGetPrice));
			}
			
			x = ctx.ExecProc<SetProcessID_Result>("SetProcessID", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to SetProcessID stored procedure
		/// </summary>
		/// <param name="pars">SetProcessID.Parameters</param>
		public static List<SetProcessID_Result> SetProcessID(this ComplexEntity ctx, SetProcessID_Result.Parameters pars)
		{
			return ctx.SetProcessID(pTradeID: pars.pTradeID , pProcessID: pars.pProcessID , pProcessAdminGetPrice: pars.pProcessAdminGetPrice );
		}
	}
}
