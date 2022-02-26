using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/24/2022 at 8:35 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: GetUserRole_ByUserID
	/// </summary>
	public partial class GetUserRole_ByUserID_Result
	{
		public string Name { get; set; }


		/// <summary>
		/// GetUserRole_ByUserID.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public string id { get; set; }
			
			/// <summary>
			/// Call to create new GetUserRole_ByUserID_Result.Parameters from a json string
			/// </summary>
			public static GetUserRole_ByUserID_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<GetUserRole_ByUserID_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to GetUserRole_ByUserID stored procedure
		/// </summary>
		/// <param name="id">string</param>
		public static List<GetUserRole_ByUserID_Result> GetUserRole_ByUserID(this ComplexEntity ctx, string id)
		{
			List<GetUserRole_ByUserID_Result> x = new List<GetUserRole_ByUserID_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (!string.IsNullOrEmpty(id))
			{
				sqlParams.Add(new SqlParameter("@id", id));
			}
			
			x = ctx.ExecProc<GetUserRole_ByUserID_Result>("GetUserRole_ByUserID", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to GetUserRole_ByUserID stored procedure
		/// </summary>
		/// <param name="pars">GetUserRole_ByUserID.Parameters</param>
		public static List<GetUserRole_ByUserID_Result> GetUserRole_ByUserID(this ComplexEntity ctx, GetUserRole_ByUserID_Result.Parameters pars)
		{
			return ctx.GetUserRole_ByUserID(id: pars.id );
		}
	}
}
