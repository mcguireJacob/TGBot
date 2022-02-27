using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/26/2022 at 5:58 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: SetOrUpdateAccount
	/// </summary>
	public partial class SetOrUpdateAccount_Result
	{

		/// <summary>
		/// SetOrUpdateAccount.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? aID { get; set; }
			public string aAccountLogin { get; set; } 
			public string aAccountPassword { get; set; }
			public string aAccountServer { get; set; }
			public string aRiskPct { get; set; }
			public string aFixedLot { get; set; }
			public string aAccountEmail { get; set; }
			
			/// <summary>
			/// Call to create new SetOrUpdateAccount_Result.Parameters from a json string
			/// </summary>
			public static SetOrUpdateAccount_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<SetOrUpdateAccount_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to SetOrUpdateAccount stored procedure
		/// </summary>
		/// <param name="aID">int?</param>
		/// <param name="aAccountLogin">string</param>
		/// <param name="aAccountPassword">string</param>
		/// <param name="aAccountServer">string</param>
		/// <param name="aRiskPct">string</param>
		/// <param name="aFixedLot">string</param>
		/// <param name="aAccountEmail">string</param>
		public static List<SetOrUpdateAccount_Result> SetOrUpdateAccount(this ComplexEntity ctx, int? aID, string aAccountLogin, string aAccountPassword, string aAccountServer, string aRiskPct, string aFixedLot, string aAccountEmail)
		{
			List<SetOrUpdateAccount_Result> x = new List<SetOrUpdateAccount_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (aID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@aID", aID));
			}
			
			if (!string.IsNullOrEmpty(aAccountLogin))
			{
				sqlParams.Add(new SqlParameter("@aAccountLogin", aAccountLogin));
			}
			
			if (!string.IsNullOrEmpty(aAccountPassword))
			{
				sqlParams.Add(new SqlParameter("@aAccountPassword", aAccountPassword));
			}
			
			if (!string.IsNullOrEmpty(aAccountServer))
			{
				sqlParams.Add(new SqlParameter("@aAccountServer", aAccountServer));
			}
			
			if (!string.IsNullOrEmpty(aRiskPct))
			{
				sqlParams.Add(new SqlParameter("@aRiskPct", aRiskPct));
			}
			
			if (!string.IsNullOrEmpty(aFixedLot))
			{
				sqlParams.Add(new SqlParameter("@aFixedLot", aFixedLot));
			}
			
			if (!string.IsNullOrEmpty(aAccountEmail))
			{
				sqlParams.Add(new SqlParameter("@aAccountEmail", aAccountEmail));
			}
			
			x = ctx.ExecProc<SetOrUpdateAccount_Result>("SetOrUpdateAccount", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to SetOrUpdateAccount stored procedure
		/// </summary>
		/// <param name="pars">SetOrUpdateAccount.Parameters</param>
		public static List<SetOrUpdateAccount_Result> SetOrUpdateAccount(this ComplexEntity ctx, SetOrUpdateAccount_Result.Parameters pars)
		{
			return ctx.SetOrUpdateAccount(aID: pars.aID , aAccountLogin: pars.aAccountLogin , aAccountPassword: pars.aAccountPassword , aAccountServer: pars.aAccountServer , aRiskPct: pars.aRiskPct , aFixedLot: pars.aFixedLot , aAccountEmail: pars.aAccountEmail );
		}
	}
}
