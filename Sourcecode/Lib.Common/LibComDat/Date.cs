// Public Domain. See License.txt
using System;
using System.Globalization;

namespace Lib.Common.DataTypes
{
	#nullable enable
	/// <Summary>DataType</Summary>
	public class Date : IFormattable, IComparable, IConvertible
	{
		#region Constructors

		/// <remarks />
		public Date() { this.Value="2010-01-01"; }

		/// <param name="date" />
		public Date(Date date) => this.Value=date.Value;

		/// <param name="dt" />
		public Date(DateTime dt) => this.Value=dt.ToString("yyyy-MM-dd");

		/// <remarks /><param name="year" /><param name="month" /><param name="day" /><exception cref="ArgumentInvalidException" />
		public Date(int year, int month, int day) => this.Value=new DateTime(year, month, day).ToString("yyyy-MM-dd");

		/// <param name="year" /><param name="month" /><param name="day" />
		public Date(string year, string month, string day) => this.Value=new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day)).ToString("yyyy-MM-dd");

		/// <exception cref="ArgumentNullOrWhiteSpaceException" /><exception cref="ArgumentInvalidException" />
		public Date(string date) => this.Value=Convert.ToDateTime(date).ToString("yyyy-MM-dd");

		#endregion

		#region Boolean Operators

		/// <returns>Result as bool</returns>
		public static bool operator ==(Date a, Date b) { return a.CompareTo(b) switch { 0 => true, _ => false }; }

		/// <returns>Result as bool</returns>
		public static bool operator !=(Date a, Date b) { return a.CompareTo(b) switch { 0 => false, _ => true }; }

		/// <returns>Result as bool</returns>
		public static bool operator >=(Date a, Date b) { return a.CompareTo(b) switch {>=0 => true, _ => false }; }

		/// <returns>Result as bool</returns>
		public static bool operator <=(Date a, Date b) { return a.CompareTo(b) switch {<=0 => true, _ => false }; }

		/// <returns>Result as bool</returns>
		public static bool operator >(Date a, Date b) { return a.CompareTo(b) switch {>0 => true, _ => false }; }

		/// <returns>Result as bool</returns>
		public static bool operator <(Date a, Date b) { return a.CompareTo(b) switch {<0 => true, _ => false }; }

		#endregion

		#region Implicit Operators

		/// <returns><paramref name="dt"/> as Date</returns><param name="dt" />
		public static explicit operator Date(DateTime dt) => new(dt);

		/// <returns><paramref name="date"/> as DateTime</returns><param name="date" />
		public static explicit operator DateTime(Date date) => date.ToDateTime();

		/// <returns><paramref name="date"/> as Date</returns><param name="date" />
		public static implicit operator Date(string date) => new(date);

		/// <returns><paramref name="date"/> as string</returns><param name="date" />
		public static implicit operator string(Date date) => date.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public string Value { get; private set; } = "2010-01-01";

		#endregion

		#region Static Methods

		/// <returns>Identical copy of <paramref name="date"/></returns><param name="date" />
		public static Date Clone(Date date) => new Date(date);

		/// <returns>Result as bool</returns><param name="date1" /><param name="date2" />
		public static bool Equals(Date date1, Date date2) => date1.Value.Equals(date2.Value);

		/// <returns>Result as bool</returns><param name="date" />
		public static bool IsEmpty(Date date) => date.Value.Equals("2010-01-01");

		#region Is something

		/// <remarks /><param name="year">Four digit integer within the range [1900;9999]</param>
		public static bool IsLeapYear(string year) => IsLeapYear(Convert.ToDecimal(year));

		/// <returns>Result as bool</returns><param name="year">Four digit integer within the range [1900;9999]</param><exception cref="ArgumentInvalidException" />
		public static bool IsLeapYear(decimal year)
		{
			if (!year.ToString().Length.Equals(4)||year<1900m||year>9999m) throw new ArgumentInvalidException(nameof(year), year, Error.InvYear);
			else if (decimal.Remainder(year, 4m).Equals(0m)&decimal.Remainder(year, 100m).Equals(0m)&&decimal.Remainder(year, 400m).Equals(0m)) return true;
			else if (decimal.Remainder(year, 4m).Equals(0m)&!decimal.Remainder(year, 100m).Equals(0m)) return true;
			else return false;
		}

		/// <remarks />
		public static bool IsValid(Date date) => IsValid(date.Value);

		/// <remarks />
		public static bool IsValid(string date) => DateTime.TryParse(date.ToString(), out DateTime dateTime);

		#endregion

		#region To something

		/// <remarks />
		public static string ToString(Date date) => date.ToString();

		/// <remarks />
		private static string ToFourCharString(int year) => ToFourCharString(year.ToString());

		/// <returns><paramref name="s"/> as four character string</returns><param name="s" /><exception cref="ArgumentInvalidException" />
		private static string ToFourCharString(string s)
		{
			if (Convert.ToInt32(s)<0) throw new ArgumentInvalidException(nameof(s), s, nameof(s)+Error.CantBeNeg);
			else return s.Length switch { <=0 => "2000", 1 => "200"+s.ToString(), 2 => Convert.ToInt32(s) switch {>21 => "19"+s, _ => "20"+s }, 3 => Convert.ToInt32(s) switch {>=900 => "1"+s, _ => "2"+s }, 4 => s, _ => s.Remove(0, s.Length-4), };
		}

		/// <remarks />
		private static string ToTwoCharString(int i) => ToTwoCharString(Convert.ToString(i));

		/// <returns><paramref name="s"/> as two character string</returns><param name="s" />
		private static string ToTwoCharString(string s) => s.Length switch {<=0 => "00", 1 => "0"+s, 2 => s, _ => s.Remove(0, s.Length-2), };

		#endregion

		#endregion

		#region Non-Static Methods

		#region Add

		/// <remarks />
		public void AddDays(int days) => Value=Convert.ToDateTime(Value).AddDays(days).ToString("yyyy-MM-dd");

		/// <remarks />
		public void AddDays(string days) => AddDays(Convert.ToInt32(days));

		/// <remarks />
		public void AddMonths(int months) => Value=Convert.ToDateTime(Value).AddMonths(months).ToString("yyyy-MM-dd");

		/// <remarks />
		public void AddMonths(string months) => AddMonths(Convert.ToInt32(months));

		/// <remarks />
		public void AddYears(int years) => Value=Convert.ToDateTime(Value).AddYears(years).ToString("yyyy-MM-dd");

		/// <remarks />
		public void AddYears(string years) => AddYears(Convert.ToInt32(years));

		#endregion

		/// <returns>Identical copy of this <see cref="Date"/></returns>
		public Date Clone() => new(this);

		/// <remarks /><param name="obj" />
		public int CompareTo(object obj) => this.ToDateTime().CompareTo(Convert.ToDateTime(obj));

		#region Equals

		/// <returns>Result as bool</returns><param name="date" />
		public bool Equals(Date date) => this.Value.Equals(date.Value);

		/// <returns>Result as bool</returns><param name="obj">Date</param>
		public override bool Equals(object obj) => this.ToDateTime().Equals(Convert.ToDateTime(obj));

		#endregion

		#region Get

		/// <remarks />
		public override int GetHashCode() => ToDateTime().GetHashCode();

		/// <remarks />
		public TypeCode GetTypeCode() => TypeCode.DateTime;

		#endregion

		#region Is something

		/// <returns>'true' if this Date is equal to '2010-01-01' - else 'false'</returns>
		public bool IsEmpty() { if (!this.Value.Equals("2010-01-01")) return false; else return true; }

		/// <returns>Result as bool</returns>
		public bool IsLeapYear()
		{
			decimal year = Convert.ToDecimal(Value.Remove(4));

			if (year<1900m||year>9999m) throw new ArgumentInvalidException(nameof(year), year, Error.InvYear);
			else if (decimal.Remainder(year, 4m).Equals(0m)&decimal.Remainder(year, 100m).Equals(0m)&&decimal.Remainder(year, 400m).Equals(0m)) return true;
			else if (decimal.Remainder(year, 4m).Equals(0m)&!decimal.Remainder(year, 100m).Equals(0m)) return true;
			else return false;
		}

		/// <remarks />
		public bool IsValid() => DateTime.TryParse(this.Value, out DateTime dateTime);

		#endregion

		#region To something 

		/// <returns>This Date as bool</returns>
		/// <param name="provider" />
		public bool ToBoolean(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToBoolean(provider);

		/// <returns>This Date as byte</returns>
		/// <param name="provider" />
		public byte ToByte(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToByte(provider);

		/// <returns>This Date as char</returns>
		/// <param name="provider" />
		public char ToChar(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToChar(provider);

		/// <returns>This Date as DateTime</returns>
		public DateTime ToDateTime() => Convert.ToDateTime(Value);

		/// <returns>This Date as DateTime</returns><param name="provider" />
		public DateTime ToDateTime(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToDateTime(provider);

		/// <returns>This Date as DateTime</returns><param name="provider" />
		public decimal ToDecimal(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToDecimal(provider);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public double ToDouble(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToDouble(provider);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public short ToInt16(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToInt16(provider);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public int ToInt32(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToInt32(provider);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public long ToInt64(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToInt64(provider);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public sbyte ToSByte(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToSByte(provider);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public float ToSingle(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToSingle(provider);

		/// <returns>This Date as string</returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public string ToString(IFormatProvider provider) => ToString("g",provider);

		/// <returns>This Date as string with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This Date as string with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		public string ToString(string format, IFormatProvider provider) { return format switch { "d" => ((DateTime)this).ToString("d", provider), "D" => ((DateTime)this).ToString("D", provider), "g" => ((DateTime)this).ToString("yyyy-MM-dd"), _ => throw new FormatException(string.Format("The {0} format string is not supported.", format)), }; }

		/// <returns>This Date as DateTime</returns>
		/// <param name="conversionType" />
		/// <param name="provider" />
		public object ToType(Type conversionType, IFormatProvider provider) => ((IConvertible)ToDateTime()).ToType(conversionType, provider);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public ushort ToUInt16(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToUInt16(provider);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public uint ToUInt32(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToUInt32(provider);

		/// <returns>This Date as DateTime</returns>
		/// <param name="provider" />
		public ulong ToUInt64(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToUInt64(provider);

		#endregion

		#endregion

	}

}
