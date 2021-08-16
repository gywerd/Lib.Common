// Public Domain.See License.txt
using System;
using System.IO;
using System.Text;

namespace Lib.Common.Disc
{
	///<summary>Logic for simple disc access</summary>
	public class DiscAccess
	{
		#region Fields
		///<remarks />
		public const string CsvPath=@"C:\CSV\";

		///<remarks />
		public const string DataSetsPath = @"C:\Datasets\";

		///<remarks />
		public const string LogsPath = @"C:\Logs\";

		///<remarks />
		public const string ResourcesPath = @"C:\Resources\";

		///<remarks />
		public static string WorkingDirectoryPath = Directory.GetCurrentDirectory();


		#endregion

		#region Constructors
		///<remarks />
		public DiscAccess() { }

		#endregion

		#region Methods

		/// <summary>
		/// Checks wether a folder exists on disk
		/// </summary>
		public static void CheckFoldersExist()
		{
			if (!Directory.Exists(CsvPath)) Directory.CreateDirectory(CsvPath);
			if (!Directory.Exists(DataSetsPath)) Directory.CreateDirectory(DataSetsPath);
			if (!Directory.Exists(LogsPath)) Directory.CreateDirectory(LogsPath);
			if (!Directory.Exists(ResourcesPath)) Directory.CreateDirectory(ResourcesPath);
		}

		/// <returns><see cref="string"/> containing the requested amount of dashes</returns>
		/// <param name="dashes">input int</param>
		private static string CreateDashString(int dashes)
		{
			string result=string.Empty;

			for (int i=0; i < dashes; i++)
			{
				result += "-";
			}

			return result;
		}

		/// <summary>
		/// Checks wether a file exists on disk
		/// </summary>
		/// <param name="filePath"><see cref="string"/></param>
		/// <returns>Result as <see cref="bool"/></returns>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static bool FileExist(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath))
				throw new ArgumentNullOrWhiteSpaceException(nameof(filePath), nameof(filePath)+Error.CantBeNullWhSp);

			if (File.Exists(filePath))
				return true;
			else
				return false;
		}

		#region Read
		/// <returns>Requested <see cref="string"/>[] from <paramref name="filePath"/></returns>
		/// <param name="filePath">string</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static string[] ReadStringArrayFromFile(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullOrWhiteSpaceException(nameof(filePath), nameof(filePath)+Error.CantBeNullWhSp);

			return File.ReadAllLines(filePath);
	}

		/// <returns>Content of <paramref name="filePath"/> as <see cref="string"/></returns>
		/// <param name="filePath"></param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static string ReadStringFromFile(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullOrWhiteSpaceException(nameof(filePath), nameof(filePath)+Error.CantBeNullWhSp);
			
			return File.ReadAllText(filePath);

		}

		#endregion

		#region Retrieve
		/// <returns><paramref name="tempString"/> extended with dashes intended for <see cref="Console"/></returns>
		/// <param name="tempString">string</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static string RetrieveDashedStringConsole(string tempString)
		{
			if (string.IsNullOrWhiteSpace(tempString)) throw new ArgumentNullOrWhiteSpaceException(nameof(tempString),tempString, nameof(tempString)+Error.CantBeNullWhSp);

			return CreateDashString((70-tempString.Length)/2)+tempString+CreateDashString((70-tempString.Length)-((70-tempString.Length)/2));

		}

		/// <returns><paramref name="tempString"/> extended with dashes intended for log file</returns>
		/// <param name="tempString">string</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static string RetrieveDashedStringLog(string tempString)
		{
			if (string.IsNullOrWhiteSpace(tempString)) throw new ArgumentNullOrWhiteSpaceException(nameof(tempString),tempString, nameof(tempString)+Error.CantBeNullWhSp);

			return CreateDashString((240-tempString.Length)/2)+tempString+CreateDashString((240-tempString.Length)-((240-tempString.Length)/2));

		}

		/// <returns>Log file path for <paramref name="caller"/> as <see cref="string"/></returns>
		/// <param name="caller"><see cref="string"/> name of application</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static string RetrieveLogFilePath(string caller)
		{
			if (string.IsNullOrWhiteSpace(caller)) throw new ArgumentNullOrWhiteSpaceException(nameof(caller), nameof(caller)+Error.CantBeNullWhSp);

			return LogsPath+"Log_"+caller+"_"+DateTime.Now.ToString("s").Replace(":", ".")+@".txt";

		}

		#endregion

		#region Write
		/// <summary>
		/// Writes the <paramref name="fileContent"/> to <paramref name="filePath"/>.
		/// </summary>
		/// <param name="filePath"><see cref="string"/> with file name</param>
		/// <param name="fileContent"><see cref="string"/> with file content</param>
		/// <param name="encoding"><see cref="Encoding"/></param>
		/// <returns><see cref="bool"/></returns>
		public static bool WriteStringToFile(string filePath, string fileContent, Encoding encoding = null)
		{
			if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentOutOfRangeException(nameof(filePath), "The file path can neither be empty, nor consist only of white-space characters.");
			if (fileContent==null) throw new ArgumentNullException(nameof(fileContent), "The file content cannot be null.");
			if (encoding==null) encoding=Encoding.UTF8;

			bool result =true; //Assume success until proven otherwise

			File.WriteAllText(filePath, fileContent+Environment.NewLine, encoding);

			return result;

		}

		/// <summary>
		/// Adds <paramref name="lineContent"/> to specified <paramref name="filePath"/>
		/// </summary>
		/// <param name="filePath"><see cref="string"/> with file name</param>
		/// <param name="lineContent"><see cref="string"/> with line content</param>
		/// <param name="encoding"><see cref="Encoding"/></param>
		/// <returns>result as <see cref="bool"/></returns>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static bool WriteStringLineToFile(string filePath, string lineContent, Encoding encoding = null)
		{
			if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullOrWhiteSpaceException(nameof(filePath),filePath,nameof(filePath)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(lineContent)) throw new ArgumentOutOfRangeException(nameof(lineContent), lineContent, nameof(lineContent)+Error.CantBeNullWhSp);
			if (encoding==null) encoding=Encoding.UTF8;

			File.AppendAllText(filePath, lineContent+Environment.NewLine, encoding);

			return true;

		}

		#endregion

		#endregion
	}
}
