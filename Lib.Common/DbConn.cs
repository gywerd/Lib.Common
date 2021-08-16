// Public Domain. See License.txt
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Lib.Common.Sql
{
	/// <remarks/>
	public class DbConn
	{
		#region Constructors
		/// <remarks/>
		public DbConn() { }

		#endregion

		#region Methods

		/// <returns>Response to <paramref name="sqlQuery"/> as an <see cref="ArrayList"/></returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="sqlQuery"><see cref="string"/></param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		protected static ArrayList DbReturnArrayListString(string connectionString, string sqlQuery)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(sqlQuery)) throw new ArgumentNullOrWhiteSpaceException(nameof(sqlQuery), sqlQuery, nameof(sqlQuery)+Error.CantBeNullWhSp);

			ArrayList arrayList = new();

			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();

				using var cmd = connection.CreateCommand();
				cmd.CommandText=sqlQuery;

				using var reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					for (int i = 0; i<reader.FieldCount; i++)
					{
						arrayList.Add(reader.GetValue(i).ToString());
					}
				}

			}

			return arrayList;

		}

		/// <returns>Response to SQL-query as a <see cref="DataTable"/></returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="sqlQuery"><see cref="string"/></param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		protected static DataTable DbReturnDataTable(string connectionString, string sqlQuery)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(sqlQuery)) throw new ArgumentNullOrWhiteSpaceException(nameof(sqlQuery), sqlQuery, nameof(sqlQuery)+Error.CantBeNullWhSp);

			DataTable dtRes = new();

			using (SqlConnection connection = new(connectionString))
			{
				// Open the SqlConnection.
				connection.Open();

				// This code uses an SqlCommand based on the SqlConnection.
				using SqlCommand command = new(sqlQuery, connection);
				using SqlDataAdapter adapter = new(command);

				adapter.Fill(dtRes);
			}

			return dtRes;

		}

		/// <returns>Response of <see cref="SqlCommand"/> and returns the result as a <see cref="DataTable"/></returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="cmd">SqlCommand</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		/// <exception cref="ArgumentNullException" />
		protected static DataTable DbReturnDataTable(string connectionString, SqlCommand cmd)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (cmd==null) throw new ArgumentNullException(nameof(cmd), nameof(cmd)+Error.CantBeNull);


			using (cmd.Connection)
			{
				// Open the SqlConnection.
				cmd.Connection.Open();

				// This code uses an SqlCommand based on the SqlConnection.
				using (cmd)
				{
					using SqlDataAdapter adapter = new(cmd);
					using DataTable dtRes = new();

					adapter.Fill(dtRes);
					return dtRes;
				}
			}
		}

		/// <returns>Response to <paramref name="storedProcedure"/> as a <see cref="DataTable"/></returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="storedProcedure">string with stored procedure name</param>
		/// <param name="args">string - @InstitutionIdentifier, @OrganizationStructureIdentifier or @OrganizationIdentifier</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		protected static DataTable DbReturnDataTableFromStoredProcedure(string connectionString, string storedProcedure, string[] args = null)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(storedProcedure)) throw new ArgumentNullOrWhiteSpaceException(nameof(storedProcedure), storedProcedure, nameof(storedProcedure)+Error.CantBeNullWhSp);

			DataTable dtRes = new();

			if (args!=null)
			{
				try
				{
					using SqlConnection connection = new(connectionString);
					// Open the SqlConnection.
					connection.Open();

					// This code uses an SqlCommand based on the SqlConnection.
					using SqlCommand command = new(storedProcedure, connection);
					command.CommandType=CommandType.StoredProcedure;

					switch (storedProcedure.ToLower())
					{
						case "selectget3in1organizations":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							break;
						case "selectget3in1organizationstructures":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							break;
						case "selectget3in1persons":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							break;
						case "selectgetdepartments":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							command.Parameters.AddWithValue("@Uuid", args[1]);
							command.Parameters.AddWithValue("@Active", args[2]);
							break;
						case "selectgetemployments":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							command.Parameters.AddWithValue("@Uuid", args[1]);
							command.Parameters.AddWithValue("@Active", args[2]);
							break;
						case "selectgetinstitutions":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							break;
						case "selectgetmochpersons":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							break;
						case "selectgetorganizations":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							command.Parameters.AddWithValue("@Active", args[2]);
							break;
						case "selectgetorganizationstructures":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							break;
						case "selectgetpersons":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							break;
						case "selectgetprofessions":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							command.Parameters.AddWithValue("@Active", args[2]);
							break;
						case "selectxmemployments":
							command.Parameters.AddWithValue("@InstitutionIdentifier", args[0]);
							break;
					}

					using SqlDataAdapter adapter = new(command);

					adapter.Fill(dtRes);
				}
				catch (SqlException)
				{
					throw;
				}
			}
			else
			{
				try
				{
					using SqlConnection connection = new(connectionString);
					// Open the SqlConnection.
					connection.Open();

					// This code uses an SqlCommand based on the SqlConnection.
					using SqlCommand command = new(storedProcedure, connection);

					command.CommandType=CommandType.StoredProcedure;

					using SqlDataAdapter adapter = new(command);

					adapter.Fill(dtRes);
				}
				catch (SqlException)
				{
					throw;
				}
			}

			return dtRes;

		}

		/// <returns>Response to <paramref name="sqlQuery"/> as a <see cref="List{T}"/> of <see cref="string"/>s</returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="sqlQuery">string with SQL-query</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		protected static List<string> DbReturnListString(string connectionString, string sqlQuery)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(sqlQuery)) throw new ArgumentNullOrWhiteSpaceException(nameof(sqlQuery), sqlQuery, nameof(sqlQuery)+Error.CantBeNullWhSp);

			List<string> listString = new();

			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();

				using SqlCommand cmd = connection.CreateCommand();
				cmd.CommandText=sqlQuery;

				using var reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					for (int i = 0; i<reader.FieldCount; i++)
					{
						listString.Add(reader.GetValue(i).ToString());
					}
				}

			}
			return listString;
		}

		/// <returns>Response to <paramref name="sqlQuery"/> as a <see cref="bool"/></returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="sqlQuery">string with SQL-query</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		protected static bool DbReturnBool(string connectionString, string sqlQuery)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(sqlQuery)) throw new ArgumentNullOrWhiteSpaceException(nameof(sqlQuery), sqlQuery, nameof(sqlQuery)+Error.CantBeNullWhSp);

			bool bolRes = false;

			using (SqlConnection connection = new(connectionString))
			{
				// Open the SqlConnection.
				connection.Open();

				// This code uses an SqlCommand based on the SqlConnection.
				using SqlCommand cmd = new(sqlQuery, connection);
				using SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
					bolRes=Convert.ToBoolean(reader.GetValue(0).ToString());

			}

			return bolRes;
		}

		/// <returns>Response to <paramref name="sqlQuery"/> as a <see cref="bool"/></returns>
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="sqlQuery"><see cref="string"/></param>
		/// <param name="args"><see cref="string"/>[]</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		/// <exception cref="ArgumentNullException" />
		protected static bool DbReturnBool(string connectionString, string sqlQuery, string[] args)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(sqlQuery)) throw new ArgumentNullOrWhiteSpaceException(nameof(sqlQuery), sqlQuery, nameof(sqlQuery)+Error.CantBeNullWhSp);
			if (args==null) throw new ArgumentNullException(nameof(sqlQuery), nameof(sqlQuery)+Error.CantBeNull);

			bool result = false;

			if (args!=null)
			{
				using SqlConnection connection = new(connectionString);
				using SqlCommand cmd = new(sqlQuery, connection);

				cmd.CommandType=CommandType.StoredProcedure;

				switch (sqlQuery)
				{
					case "usersAddUser":
						cmd.Parameters.Add("@pPerson", SqlDbType.Int).Value=Convert.ToInt32(args[0]);
						cmd.Parameters.Add("@pInitials", SqlDbType.NVarChar).Value=args[1];
						cmd.Parameters.Add("@pPassword", SqlDbType.NVarChar).Value=args[2];
						cmd.Parameters.Add("@pJobDescription", SqlDbType.Int).Value=Convert.ToInt32(args[3]);
						cmd.Parameters.Add("@pUserLevel", SqlDbType.Int).Value=Convert.ToInt32(args[4]);
						break;
					case "usersUpdatePassword":
						cmd.Parameters.Add("@pId", SqlDbType.Int).Value=Convert.ToInt32(args[0]);
						cmd.Parameters.Add("@pOldPassword", SqlDbType.NVarChar).Value=args[1];
						cmd.Parameters.Add("@pNewPassword", SqlDbType.NVarChar).Value=args[2];
						break;
					case "usersLogin":
						cmd.Parameters.Add("@pInitials", SqlDbType.NVarChar).Value=args[0];
						cmd.Parameters.Add("@pPassword", SqlDbType.NVarChar).Value=args[1];
						break;
				}

				connection.Open();

				using DataTable dtRes = DbReturnDataTable(connectionString,cmd);

				if (dtRes.Rows.Count>0)
				{
					foreach (DataRow row in dtRes.Rows)
					{
						result=Convert.ToBoolean(row[0].ToString());
						break;
					}

				}

			}

			return result;

		}

		/// <summary> Sends an SQL-query to Db</summary>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		/// <param name="connectionString"><see cref="string"/></param>
		/// <param name="sqlQuery"><see cref="string"/></param>
		/// <returns>Result as <see cref="bool"/></returns>
		protected static bool FunctionExecuteNonQuery(string connectionString, string sqlQuery)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullOrWhiteSpaceException(nameof(connectionString), connectionString, nameof(connectionString)+Error.CantBeNullWhSp);

			using SqlConnection connection = new(connectionString);

			// Open the SqlConnection.
			connection.Open();

			// This code uses an SqlCommand based on the SqlConnection.
			using SqlCommand cmd = new(sqlQuery, connection);

			cmd.CommandTimeout=60;
			cmd.ExecuteNonQuery();

			return true;

		}

		#endregion

	}

}
