using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 1/15/2022 at 12:36 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: lTradeTypeLookup_List
	/// </summary>
	public partial class lTradeTypeLookup_List_Result
	{
		public int lTradeTypeID { get; set; }
		public string lTrade { get; set; }

	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to lTradeTypeLookup_List stored procedure
		/// </summary>
		public static List<lTradeTypeLookup_List_Result> lTradeTypeLookup_List(this ComplexEntity ctx )
		{
			List<lTradeTypeLookup_List_Result> x = new List<lTradeTypeLookup_List_Result>();
			x = ctx.ExecProc<lTradeTypeLookup_List_Result>("lTradeTypeLookup_List", new List<SqlParameter>());
			
			return x;
			
		}
	}
}
