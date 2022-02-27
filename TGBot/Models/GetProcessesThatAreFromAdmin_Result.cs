using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/27/2022 at 3:12 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: GetProcessesThatAreFromAdmin
	/// </summary>
	public partial class GetProcessesThatAreFromAdmin_Result
	{
		public int pID { get; set; }
		public int pTradeID { get; set; }
		public int pProcessID { get; set; }
		public bool pProcessAdminGetPrice { get; set; }

	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to GetProcessesThatAreFromAdmin stored procedure
		/// </summary>
		public static List<GetProcessesThatAreFromAdmin_Result> GetProcessesThatAreFromAdmin(this ComplexEntity ctx )
		{
			List<GetProcessesThatAreFromAdmin_Result> x = new List<GetProcessesThatAreFromAdmin_Result>();
			x = ctx.ExecProc<GetProcessesThatAreFromAdmin_Result>("GetProcessesThatAreFromAdmin", new List<SqlParameter>());
			
			return x;
			
		}
	}
}
