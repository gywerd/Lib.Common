// Public Domain. See License.txt
using System;
using System.IO;
using System.Text;

namespace Lib.Common.Disc
{
	///<summary>Logic for simple disc access</summary>
	public class DiscAccess
	{
		#region Methods

		#region Create
		/// <summary>Creates a file on disk</summary>
		/// <param name="path">File path</param>
		/// <returns>Result as bool</returns>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static void CreateFile(string path) => System.IO.File.WriteAllText(path, "", System.Text.Encoding.UTF8);

		/// <summary>Creates a folder on disk</summary>
		/// <param name="path">Folder path</param>
		/// <returns>Result as bool</returns>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static void CreateFolder(string path) => System.IO.Directory.CreateDirectory(path);

		/// <returns><see cref="string"/> containing the requested amount of dashes</returns>
		/// <param name="dashes">input int</param>
		private static string CreateDashString(int dashes)
		{
			string result = string.Empty;

			for (int i = 0; i<dashes; i++) result+="-";

			return result;

		}

		#endregion

		#region Exist
		/// <summary>Checks wether a file exists on disk</summary>
		/// <param name="path">File path</param>
		/// <returns>Result as bool</returns>
		public static bool FileExist(string path) => System.IO.File.Exists(path);

		/// <summary>Checks wether a folder exists on disk</summary>
		/// <param name="path">Folder path</param>
		/// <returns>Result as bool</returns>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static bool FolderExist(string path) => System.IO.Directory.Exists(path);

		#endregion

		#region Read
		/// <returns>Content of <paramref name="path"/> as string[]</returns>
		/// <param name="path">File path</param>
		public static string[] ReadStringArrayFromFile(string path) => System.IO.File.ReadAllLines(path);

		/// <returns>Content of <paramref name="path"/> as <see cref="string"/></returns>
		/// <param name="path">File path</param>
		public static string ReadStringFromFile(string path) => System.IO.File.ReadAllText(path);

		#endregion

		#region Retrieve
		/// <returns><paramref name="s"/> extended with dashes intended for Console as string</returns>
		/// <param name="s" />
		/// <exception cref="ArgumentNullException" />
		public static string RetrieveDashedStringConsole(string s)
		{
			if (s==null) throw new ArgumentNullException(nameof(s), nameof(s)+Error.CantBeNull);

			if (s.Length.Equals(0)) return CreateDashString(70);
			if (s.Length>70) return s.Remove(70);
			else return CreateDashString((70-s.Length)/2)+s+CreateDashString((70-s.Length)-((70-s.Length)/2));

		}

		/// <returns><paramref name="s"/> extended with dashes intended for log file as string</returns>
		/// <param name="s" />
		/// <exception cref="ArgumentNullException" />
		public static string RetrieveDashedStringLog(string s)
		{
			if (s==null) throw new ArgumentNullException(nameof(s), nameof(s)+Error.CantBeNull);

			if (s.Length.Equals(0)) return CreateDashString(240);
			if (s.Length>240) return s.Remove(240);
			else return CreateDashString((240-s.Length)/2)+s+CreateDashString((240-s.Length)-((240-s.Length)/2));

		}

		#endregion

		#region Write
		/// <summary>Writes the <paramref name="content"/> to <paramref name="path"/></summary><param name="path">File pame</param><param name="content">File content</param><param name="encoding" />
		public static void WriteStringToFile(string path, string content, Encoding? encoding=null) { if(encoding==null) encoding=Encoding.UTF8; File.WriteAllText(path, content+Environment.NewLine, encoding); }

		/// <summary>Adds <paramref name="content"/> to specified <paramref name="path"/></summary><param name="path">File path</param><param name="content">Line content</param><param name="encoding" />
		public static void WriteStringLineToFile(string path, string content, Encoding? encoding=null) { if(encoding==null) encoding=Encoding.UTF8; File.AppendAllText(path, content+Environment.NewLine, encoding); }

		#endregion

		#endregion
	}
}
