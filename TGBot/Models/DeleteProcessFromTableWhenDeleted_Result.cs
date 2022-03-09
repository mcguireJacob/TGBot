using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using JSON = Newtonsoft.Json.JsonConvert;
using TGBot.Helper;

//AUTO-GENERATED 2/28/2022 at 6:46 PM
//DO NOT ALTER THIS FILE
//EXTEND THE CLASS IN A NEW PARTIAL CLASS INSTEAD
namespace TGBot
{
	/// <summary>
	/// Complex Data Entity: DeleteProcessFromTableWhenDeleted
	/// </summary>
	public partial class DeleteProcessFromTableWhenDeleted_Result
	{

		/// <summary>
		/// DeleteProcessFromTableWhenDeleted.Parameters object
		/// </summary>
		public partial class Parameters
		{
			public int? pProcessID { get; set; }
			
			/// <summary>
			/// Call to create new DeleteProcessFromTableWhenDeleted_Result.Parameters from a json string
			/// </summary>
			public static DeleteProcessFromTableWhenDeleted_Result.Parameters Create(string json)
			{
				return JSON.DeserializeObject<DeleteProcessFromTableWhenDeleted_Result.Parameters>(json);
			}
		}
	}

	public static partial class Extensions
	{
		/// <summary>
		/// Call to DeleteProcessFromTableWhenDeleted stored procedure
		/// </summary>
		/// <param name="pProcessID">int?</param>
		public static List<DeleteProcessFromTableWhenDeleted_Result> DeleteProcessFromTableWhenDeleted(this ComplexEntity ctx, int? pProcessID)
		{
			List<DeleteProcessFromTableWhenDeleted_Result> x = new List<DeleteProcessFromTableWhenDeleted_Result>();
			 List<SqlParameter> sqlParams = new List<SqlParameter>();
			
			if (pProcessID.HasValue)
			{
				sqlParams.Add(new SqlParameter("@pProcessID", pProcessID));
			}
			
			x = ctx.ExecProc<DeleteProcessFromTableWhenDeleted_Result>("DeleteProcessFromTableWhenDeleted", sqlParams);
			
			return x;
			
		}

		/// <summary>
		/// Call to DeleteProcessFromTableWhenDeleted stored procedure
		/// </summary>
		/// <param name="pars">DeleteProcessFromTableWhenDeleted.Parameters</param>
		public static List<DeleteProcessFromTableWhenDeleted_Result> DeleteProcessFromTableWhenDeleted(this ComplexEntity ctx, DeleteProcessFromTableWhenDeleted_Result.Parameters pars)
		{
			return ctx.DeleteProcessFromTableWhenDeleted(pProcessID: pars.pProcessID );
		}
	}
}
