using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/27/2022 at 3:33 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: GetPriceOfSelected
	/// </summary>
	public partial class GetPriceOfSelected_Result
	{
		public int pID { get; set; }
		public decimal? pPriceOfSelected { get; set; }
		public string pSelectedTradePair { get; set; }

	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to GetPriceOfSelected stored procedure
		/// </summary>
		public static List<GetPriceOfSelected_Result> GetPriceOfSelected(this ComplexEntity ctx )
		{
			List<GetPriceOfSelected_Result> x = new List<GetPriceOfSelected_Result>();
			x = ctx.ExecProc<GetPriceOfSelected_Result>("GetPriceOfSelected", new List<SqlParameter>());
			
			return x;
			
		}
	}
}
