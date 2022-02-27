using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/26/2022 at 5:51 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: GetAccount_ByEmail
	/// </summary>
	public partial class GetAccount_ByEmail_Result
	{
		public int aID { get; set; }
		public int? aAccountLogin { get; set; }
		public string aAccountPassword { get; set; }
		public string aAccountServer { get; set; }
		public decimal? aRiskPct { get; set; }
		public decimal? aFixedLotSize { get; set; }
		public string aAccountEmail { get; set; }


		/// <summary>
		/// GetAccount_ByEmail.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public string aAccountEmail { get; set; }
			
			/// <summary>
			/// Call to create new GetAccount_ByEmail_Result.Parameters from a json string
			/// </summary>
			public static GetAccount_ByEmail_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<GetAccount_ByEmail_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to GetAccount_ByEmail stored procedure
		/// </summary>
		/// <param name="aAccountEmail">string</param>
		public static List<GetAccount_ByEmail_Result> GetAccount_ByEmail(this ComplexEntity ctx, string aAccountEmail)
		{
			List<GetAccount_ByEmail_Result> x = new List<GetAccount_ByEmail_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (!string.IsNullOrEmpty(aAccountEmail))
			{
				sqlParams.Add(new SqlParameter("@aAccountEmail", aAccountEmail));
			}
			
			x = ctx.ExecProc<GetAccount_ByEmail_Result>("GetAccount_ByEmail", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to GetAccount_ByEmail stored procedure
		/// </summary>
		/// <param name="pars">GetAccount_ByEmail.Parameters</param>
		public static List<GetAccount_ByEmail_Result> GetAccount_ByEmail(this ComplexEntity ctx, GetAccount_ByEmail_Result.Parameters pars)
		{
			return ctx.GetAccount_ByEmail(aAccountEmail: pars.aAccountEmail );
		}
	}
}
