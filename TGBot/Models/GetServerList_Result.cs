using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/26/2022 at 3:51 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: GetServerList
	/// </summary>
	public partial class GetServerList_Result
	{
		public int sID { get; set; }
		public string sServerName { get; set; }
		public string sServerMetaTraderValue { get; set; }
		public string sUniqueChar { get; set; }

	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to GetServerList stored procedure
		/// </summary>
		public static List<GetServerList_Result> GetServerList(this ComplexEntity ctx )
		{
			List<GetServerList_Result> x = new List<GetServerList_Result>();
			x = ctx.ExecProc<GetServerList_Result>("GetServerList", new List<SqlParameter>());
			
			return x;
			
		}
	}
}
