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
	/// Complex Data Entity: DeleteTrade_ByID
	/// </summary>
	public partial class DeleteTrade_ByID_Result
	{

		/// <summary>
		/// DeleteTrade_ByID.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? tID { get; set; }
			
			/// <summary>
			/// Call to create new DeleteTrade_ByID_Result.Parameters from a json string
			/// </summary>
			public static DeleteTrade_ByID_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<DeleteTrade_ByID_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to DeleteTrade_ByID stored procedure
		/// </summary>
		/// <param name="tID">int?</param>
		public static List<DeleteTrade_ByID_Result> DeleteTrade_ByID(this ComplexEntity ctx, int? tID)
		{
			List<DeleteTrade_ByID_Result> x = new List<DeleteTrade_ByID_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (tID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@tID", tID));
			}
			
			x = ctx.ExecProc<DeleteTrade_ByID_Result>("DeleteTrade_ByID", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to DeleteTrade_ByID stored procedure
		/// </summary>
		/// <param name="pars">DeleteTrade_ByID.Parameters</param>
		public static List<DeleteTrade_ByID_Result> DeleteTrade_ByID(this ComplexEntity ctx, DeleteTrade_ByID_Result.Parameters pars)
		{
			return ctx.DeleteTrade_ByID(tID: pars.tID );
		}
	}
}
