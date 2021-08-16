// Public Domain.See License.txt
using System;

namespace Lib.Common
{
	/// <Summary>Common Error messages</Summary>
	public static class Error
	{
		#region Properties
		/// <remarks />
		public const string CantBeEmpty = " cannot be empty.";

		/// <remarks />
		public const string CantBeNull = " cannot be NULL.";

		/// <remarks />
		public const string CantBeNullEmpty = " cannot be NULL or empty.";

		/// <remarks />
		public const string CantBeNullWhSp = " cannot be null, empty, or consists only of whitespace characters.";

		/// <remarks />
		public const string CantBeWhSp = " cannot consist only of whitespace characters.";

		/// <remarks />
		public const string CantContWhSp = " cannot contain whitespace characters.";

		/// <remarks />
		public const string ErrReadingFile = "- Error while reading file from disc:\r\n";

		/// <remarks />
		public const string ErrReadFileCont = "- Error while reading content of";

		/// <remarks />
		public const string ErrWriteFile = "- Error while writing file to disc:\r\n";

		/// <remarks />
		public const string ErrWriteStrLine = "- Error while adding string line to file:\r\n";

		/// <remarks />
		public const string InvAPI = " is not a valid API. Valid SD APIs contain the substring 'get', but does not contain whitespace characters.";

		/// <remarks />
		public const string InvArg = " is not recognised as a valid argument.";

		/// <remarks />
		public const string InvBitInt = "A valid bit an integer within the range [0;1].";

		/// <remarks />
		public const string InvBitStr = "A valid bit is either 'true' og 'false'.";

		/// <remarks />
		public const string InvDate = " is not a valid date.";

		/// <remarks />
		public const string InvDateStr = " is not a valid date. A date string must have the format 'yyyy-MM-dd''.";

		/// <remarks />
		public const string InvDayInt = "A day must be an integer within the range [1;31].";

		/// <remarks />
		public const string InvDayStr = "A day must represent an integer within the range [1;31].";

		/// <remarks />
		public const string InvHourInt = "An hour must be an integer within the range [0;23].";

		/// <remarks />
		public const string InvHourStr = "An hour must represent an integer within the range [0;23].";

		/// <remarks />
		public const string InvInt = " is an invalid Integer.";

		/// <remarks />
		public const string InvLogFilePath = " must contain the application name.";

		/// <remarks />
		public const string InvMinInt = "A minute must be an integer within the range [0;59].";

		/// <remarks />
		public const string InvMinStr = "A minute must represent an integer within the range [0;59].";

		/// <remarks />
		public const string InvMonInt = "A month must be an integer within the range [1;12].";

		/// <remarks />
		public const string InvMonStr = "A month must represent an integer within the range [1;12].";

		/// <remarks />
		public const string InvSecInt = "A second must be an integer within the range [0;59].";

		/// <remarks />
		public const string InvSecStr = "A second must represent an integer within the range [0;59].";

		/// <remarks />
		public const string InvTargDayMon = " must be 'day' or 'month'.";

		/// <remarks />
		public const string InvTime = " is not a valid time.";

		/// <remarks />
		public const string InvTypeParam = " is not recognised as a valid type parameter.";

		/// <remarks />
		public const string InvYearInt = "A year must be an integer within the range [1900;9999].";

		/// <remarks />
		public const string InvYearStr = "A year must represent an integer within the range [1900;9999].";

		/// <remarks />
		public const string NotDeseriz = " could not be deserialized.";

		/// <remarks />
		public const string NotSeriz = " could not be serialized.";

		/// <remarks />
		public const string UnkParam = " is an Unknown parameter.";

		/// <remarks />
		public const string UnkParams = " are unknown parameters.";

		/// <remarks />
		public const string UnkAPI = " is an unknown API.";

		/// <remarks />
		public const string UnkAPIs = " are unknown APIs.";

		#endregion

	}

}

