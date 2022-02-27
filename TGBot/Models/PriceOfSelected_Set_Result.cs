using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/27/2022 at 3:05 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: PriceOfSelected_Set
	/// </summary>
	public partial class PriceOfSelected_Set_Result
	{

		/// <summary>
		/// PriceOfSelected_Set.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public string pTradePair { get; set; }
			
			/// <summary>
			/// Call to create new PriceOfSelected_Set_Result.Parameters from a json string
			/// </summary>
			public static PriceOfSelected_Set_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<PriceOfSelected_Set_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to PriceOfSelected_Set stored procedure
		/// </summary>
		/// <param name="pTradePair">string</param>
		public static List<PriceOfSelected_Set_Result> PriceOfSelected_Set(this ComplexEntity ctx, string pTradePair)
		{
			List<PriceOfSelected_Set_Result> x = new List<PriceOfSelected_Set_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (!string.IsNullOrEmpty(pTradePair))
			{
				sqlParams.Add(new SqlParameter("@pTradePair", pTradePair));
			}
			
			x = ctx.ExecProc<PriceOfSelected_Set_Result>("PriceOfSelected_Set", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to PriceOfSelected_Set stored procedure
		/// </summary>
		/// <param name="pars">PriceOfSelected_Set.Parameters</param>
		public static List<PriceOfSelected_Set_Result> PriceOfSelected_Set(this ComplexEntity ctx, PriceOfSelected_Set_Result.Parameters pars)
		{
			return ctx.PriceOfSelected_Set(pTradePair: pars.pTradePair );
		}
	}
}
