// Public Domain. See License.txt
using System;
using System.Collections.Generic;
using System.Data;

namespace Lib.Common.Sql
{
	/// <remarks/>
	public class Executor : DbConn
	{
		#region Constructors
		/// <summary>
		/// Initiates an empty instance of <see cref="Executor"/>.
		/// </summary>
		public Executor() { }

		#endregion

		#region Methods

		/// <returns>List from <paramref name="table"/> as a <see cref="DataTable"/></returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="table">string</param>
		/// <param name="id">int</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		private static DataTable GetListDataTable(string connectionString, string table, int id=-1)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(table)) throw new ArgumentNullOrWhiteSpaceException(nameof(table), table, nameof(table)+Error.CantBeNullWhSp);

			if (id>=0) return DbReturnDataTable(connectionString,@"SELECT * FROM ["+table+"] WHERE [Id]="+id);
			else return DbReturnDataTable(connectionString,"SELECT * FROM "+table);

		}

		/// <returns><see cref="List{T}"/> of <see cref="string"/>s from <paramref name="table"/> in Db</returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="table">string</param>
		/// <param name="id">int</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static List<string> ReadListFromDataBase(string connectionString, string table, int id=-1)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(table)) throw new ArgumentNullOrWhiteSpaceException(nameof(table), table, nameof(table)+Error.CantBeNullWhSp);

			List<string> listRes=new();

			using DataTable dm = GetListDataTable(connectionString,table,id);

			foreach (DataRow row in dm.Rows)
			{
				string rowString = string.Empty;

				for (int i = 0; i<row.Table.Columns.Count; i++)
				{
					rowString+=row[i]+";";
				}

				rowString=rowString.Remove(rowString.Length-1);
				listRes.Add(rowString);

			}

			return listRes;

		}

		/// <returns><see cref="List{T}"/> of <see cref="string"/>s from <paramref name="storedProcedure"/> in Db</returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="storedProcedure">name of Stored procedure as a <see cref="string"/></param>
		/// <param name="args"><see cref="string"/>: @InstitutionIdentifier, @OrganizationStructureIdentifier or @OrganizationIdentifier</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static List<string> ReadListFromDataBaseFromStoredProcedure(string connectionString, string storedProcedure, string[] args=null)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(storedProcedure)) throw new ArgumentNullOrWhiteSpaceException(nameof(storedProcedure), storedProcedure, nameof(storedProcedure)+Error.CantBeNullWhSp);

			List<string> listRes=new();

			using DataTable dm = DbReturnDataTableFromStoredProcedure(connectionString,storedProcedure,args);

			foreach (DataRow row in dm.Rows)
			{
				string rowString = string.Empty;

				for (int i = 0; i<row.Table.Columns.Count; i++)
				{
					rowString+=row[i]+";";
				}

				rowString=rowString.Remove(rowString.Length-1);
				listRes.Add(rowString);

			}

			return listRes;

		}

		/// <summary>
		/// Sends <paramref name="sqlQuery"/> to Db
		/// </summary>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="sqlQuery"><see cref="string"/></param>
		/// <returns>Result as <see cref="bool"/></returns>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static bool WriteToDataBase(string connectionString, string sqlQuery)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(sqlQuery)) throw new ArgumentNullOrWhiteSpaceException(nameof(sqlQuery), sqlQuery, nameof(sqlQuery)+Error.CantBeNullWhSp);

			return FunctionExecuteNonQuery(connectionString, sqlQuery);

		}

		#endregion

	}
}
