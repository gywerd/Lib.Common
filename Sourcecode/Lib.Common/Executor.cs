// Public Domain. See License.txt
using System;
using System.Collections.Generic;
using System.Data;

namespace Lib.Common.Sql
{
	/// <remarks/>
	public class Executor : DbConn
	{
		#region Methods

		/// <returns>List{strings} from <paramref name="table"/> in database</returns>
		/// <param name="conn">Connection string</param>
		/// <param name="table">Name of database table</param> />
		/// <param name="id" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static List<string> ReadListFromDataBase(string conn, string table, int id=-1)
		{
			if (string.IsNullOrWhiteSpace(conn)) throw new ArgumentNullOrWhiteSpaceException(nameof(conn), conn, nameof(conn)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(table)) throw new ArgumentNullOrWhiteSpaceException(nameof(table), table, nameof(table)+Error.CantBeNullWhSp);

			List<string> listRes=new();

			using DataTable dm = GetListDataTable(conn,table,id);

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

		/// <returns>List{strings} from <paramref name="storedProcedure"/> in database</returns>
		/// <param name="conn">Connection string</param>
		/// <param name="proc">Name of Stored procedure</param>
		/// <param name="args">e.g. @InstitutionIdentifier, @OrganizationStructureIdentifier or @OrganizationIdentifier</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static List<string> ReadListFromDataBaseFromStoredProcedure(string conn, string proc, string[] args=null)
		{
			if (string.IsNullOrWhiteSpace(conn)) throw new ArgumentNullOrWhiteSpaceException(nameof(conn), conn, nameof(conn)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(proc)) throw new ArgumentNullOrWhiteSpaceException(nameof(proc), proc, nameof(proc)+Error.CantBeNullWhSp);

			List<string> listRes=new();

			using DataTable dm = DbReturnDataTableFromStoredProcedure(conn,proc,args);

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

		/// <summary>Sends <paramref name="query"/> to database</summary>
		/// <param name="conn">Connection string</param>
		/// <param name="query">SQL-query</param>
		/// <returns>Result as <see cref="bool"/></returns>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static bool WriteToDataBase(string conn, string query)
		{
			if (string.IsNullOrWhiteSpace(conn)) throw new ArgumentNullOrWhiteSpaceException(nameof(conn), conn, nameof(conn)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(query)) throw new ArgumentNullOrWhiteSpaceException(nameof(query), query, nameof(query)+Error.CantBeNullWhSp);

			return FunctionExecuteNonQuery(conn, query);

		}

		/// <returns>List from <paramref name="table"/> as a <see cref="DataTable"/></returns>
		/// <param name="conn">Connection string</param>
		/// <param name="table">Database table name</param>
		/// <param name="id">if id>=0 selects specific entry in table; else selects all entries</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		private static DataTable GetListDataTable(string conn, string table, int id=-1)
		{
			if (string.IsNullOrWhiteSpace(conn)) throw new ArgumentNullOrWhiteSpaceException(nameof(conn), conn, nameof(conn)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(table)) throw new ArgumentNullOrWhiteSpaceException(nameof(table), table, nameof(table)+Error.CantBeNullWhSp);

			if (id>=0) return DbReturnDataTable(conn,@"SELECT * FROM ["+table+"] WHERE [Id]="+id);
			else return DbReturnDataTable(conn,"SELECT * FROM ["+table+"]");

		}

		#endregion

	}
}
