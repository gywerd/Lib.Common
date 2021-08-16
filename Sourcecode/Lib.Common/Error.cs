// Public Domain. See License.txt
using System;

namespace Lib.Common
{
	/// <Summary>Common Error messages</Summary>
	public static class Error
	{
		#region Properties
		/// <remarks />
		public const string CantBeEmpty = @" cannot be empty.";

		/// <remarks />
		public const string CantBeNull = @" cannot be NULL.";

		/// <remarks />
		public const string CantBeNullEmpty = @" cannot be NULL or empty.";

		/// <remarks />
		public const string CantBeNullWhSp = @" cannot be null, empty, or consists only of whitespace characters.";

		/// <remarks />
		public const string CantBeWhSp = @" cannot consist only of whitespace characters.";

		/// <remarks />
		public const string CantContWhSp = @" cannot contain whitespace characters.";

		/// <remarks />
		public const string ErrReadingFile = @"- Error while reading file from disc:\r\n";

		/// <remarks />
		public const string ErrReadFileCont = @"- Error while reading content of";

		/// <remarks />
		public const string ErrWriteFile = @"- Error while writing file to disc:\r\n";

		/// <remarks />
		public const string ErrWriteStrLine = @"- Error while adding string line to file:\r\n";

		/// <remarks />
		public const string InvBit = @" is not recognized as a valid bit as neither 'true'/'false', nor repesenting an integer within the range [0;1].";

		/// <remarks />
		public const string InvDate = @" is not recognized as a valid day with the format 'yyyy-MM-dd'.";

		/// <remarks />
		public const string InvDay = @" is not recognized as a valid day representing an integer within the range [1;31].";

		/// <remarks />
		public const string InvHour = @" is not recognized as a valid hour representing an integer within the range [0;23].";

		/// <remarks />
		public const string InvInt = @"  is not recognized as a valid integer.";

		/// <remarks />
		public const string InvLogFilePath = @" must contain the application name.";

		/// <remarks />
		public const string InvMin = @" is not recognized as a valid minute representing an integer within the range [0;59].";

		/// <remarks />
		public const string InvMon = @" is not recognized as a valid month representing an integer within the range [1;12].";

		/// <remarks />
		public const string InvSec = @" is not recognized as a valid second representing an integer within the range [0;59].";

		/// <remarks />
		public const string InvTime = " is not recognized as a valid time.";

		/// <remarks />
		public const string InvTypeParam = " is not recognised as a valid type parameter.";

		/// <remarks />
		public const string InvYear = " is not recognized as a valid second representing an integer within the range [1900;9999].";

		/// <remarks />
		public const string NotDeseriz = " could not be deserialized.";

		/// <remarks />
		public const string NotSeriz = " could not be serialized.";

		/// <remarks />
		public const string UnkArg = " is not recognised as a valid argument.";

		/// <remarks />
		public const string UnkArgs = " are not recognised as valid arguments.";

		/// <remarks />
		public const string UnkAPI = " is not recognised as a valid API.";

		/// <remarks />
		public const string UnkAPIs = " are not recognised as valid APIs.";

		/// <remarks />
		public const string UnkParam = " is not recognized as a valid parameter.";

		/// <remarks />
		public const string UnkParams = " are not recognized as valid parameters.";

		/// <remarks />
		public const string UnkTarget = @" is not recognized as a valid target.";

		/// <remarks />
		public const string UnkTargets = @" are not recognized as valid targets.";

		#endregion

	}

}

