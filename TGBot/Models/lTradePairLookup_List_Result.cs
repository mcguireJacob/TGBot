using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 1/16/2022 at 6:43 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: lTradePairLookup_List
	/// </summary>
	public partial class lTradePairLookup_List_Result
	{
		public int? lTradePairLookupID { get; set; }
		public string lTradePair { get; set; }
		public string lApiLink { get; set; }

	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to lTradePairLookup_List stored procedure
		/// </summary>
		public static List<lTradePairLookup_List_Result> lTradePairLookup_List(this ComplexEntity ctx )
		{
			List<lTradePairLookup_List_Result> x = new List<lTradePairLookup_List_Result>();
			x = ctx.ExecProc<lTradePairLookup_List_Result>("lTradePairLookup_List", new List<SqlParameter>());
			
			return x;
			
		}
	}
}
