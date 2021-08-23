# Lib.Common
Common extension library for C#

Lib.Common
- Error - common error messages as constant strings
- ArgumentInvalidException : ExpressionException
- ArgumentNullOrEmptyException : ExpressionException
- ArgumentNullOrWhiteSpaceException : ExpressionException
- EmptyRefException : ExpressionException
- ExpressionException : System.Exception
- InvalidRefException : ExpressionException
- NullRefException : ExpressionException
- NullOrEmptyRefException : ExpressionException
- NullOrWhiteSpaceRefException : ExpressionException
- SyntaxRefException : ExpressionException
- UnknownRefException : ExpressionException

Lib.Common.DataTypes:
- Bit - equivalent of System.Boolean with primary value equal to 0 or 1 - common to SQL
- Date - equivalent to System.DateTime.Date with primary value within {1900-01-01;9999-12-31} - common to SQL
- Day - equivalent of System.String with primary value within {01;31} - element of Date
- Month - equivalent of System.String with primary value within {01;12} - element of Date
- Year - equivalent of System.String with primary value within {1900;9999} - element of Date
- Time - equivalent to System.DateTime.Time with primary value within {00:00:00;23:59:59} - common to SQL
- Hour - equivalent of bool with primary value within {00;23} - element of Time
- Minute - equivalent of bool with primary value within {00;59} - element of Time
- Second - equivalent of bool with primary value within {00;59} - element of Time

Lib.Common.Disc
- DiscAccess.CreateFile(string path) => System.IO.File.WriteAllText(path, "", Encoding.UTF8);
- DiscAccess.CreateFolder(string path) => System.IO.Directory.CreateDirectory(path).Exists;
- DiscAccess.FileExist(string path) => System.IO.File.Exists(path)
- DiscAccess.FolderExist(string path) => System.IO.Directory.Exists(path)
- DiscAccess.ReadStringArrayFromFile(string path) => System.IO.File.ReadAllLines(path) 
- DiscAccess.ReadStringFromFile(string path) => System.IO.File.ReadAllText(path)
- DiscAccess.RetrieveDashedStringConsole(string s) - Returns s extended with dashes intended for System.Console.WriteLine
- DiscAccess.RetrieveDashedStringLog(string s) - Returns s extended with dashes intended for log file
- DiscAccess.WriteStringToFile(string path, string content, System.Text.Encoding encoding=System.Text.Encoding.UTF8) => System.IO.File.WriteAllText(path, content+Environment.NewLine, encoding) 
- DiscAccess.WriteStringLineToFile(string path, string content, System.Text.Encoding encoding=System.Text.Encoding.UTF8) => System.IO.File.AppendAllText(path, content+Environment.NewLine, encoding)

Lib.Common.Sql
- Executor.ReadListFromDataBase(string connectionString, string table, int id=-1) - returns System.Collections.Generic.List{T} of strings from database table
- Executor.ReadListFromDataBaseFromStoredProcedure(string conn, string proc, string[] args=null) - returns System.Collections.Generic.List{T} of strings from stored procedure in database
- Executor.WriteToDataBase(string conn, string query) - Sends SQL-query to database and returns result as bool
