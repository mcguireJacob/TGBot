using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/5/2022 at 1:01 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: GetProcessID_ByTicketID
	/// </summary>
	public partial class GetProcessID_ByTicketID_Result
	{
		public int pProcessID { get; set; }


		/// <summary>
		/// GetProcessID_ByTicketID.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? pTradeID { get; set; }
			
			/// <summary>
			/// Call to create new GetProcessID_ByTicketID_Result.Parameters from a json string
			/// </summary>
			public static GetProcessID_ByTicketID_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<GetProcessID_ByTicketID_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to GetProcessID_ByTicketID stored procedure
		/// </summary>
		/// <param name="pTradeID">int?</param>
		public static List<GetProcessID_ByTicketID_Result> GetProcessID_ByTicketID(this ComplexEntity ctx, int? pTradeID)
		{
			List<GetProcessID_ByTicketID_Result> x = new List<GetProcessID_ByTicketID_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (pTradeID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@pTradeID", pTradeID));
			}
			
			x = ctx.ExecProc<GetProcessID_ByTicketID_Result>("GetProcessID_ByTicketID", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to GetProcessID_ByTicketID stored procedure
		/// </summary>
		/// <param name="pars">GetProcessID_ByTicketID.Parameters</param>
		public static List<GetProcessID_ByTicketID_Result> GetProcessID_ByTicketID(this ComplexEntity ctx, GetProcessID_ByTicketID_Result.Parameters pars)
		{
			return ctx.GetProcessID_ByTicketID(pTradeID: pars.pTradeID );
		}
	}
}
