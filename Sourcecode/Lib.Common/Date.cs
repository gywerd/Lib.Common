// Public Domain. See License.txt
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lib.Common.DataTypes
{

	/// <Summary>DataType</Summary>
	public class Date : IFormattable,IComparable, IComparable<Date>, IComparable<string>
	{
		#region Fields
		private const int daysPerOrdinaryYear = 365, daysPerLeapYear = 366, februaryLeapYear = 29;

		private bool validDate;
		private bool leapYear;

		private Day day;
		private Month month;
		private Year year;

		#endregion

		#region Constructors

		/// <remarks />
		public Date() { this.year="2010"; this.month="01"; this.day="01"; }

		/// <remarks />
		/// <exception cref="ArgumentOutOfRangeException" />
		/// <exception cref="InvalidRefException" />
		public Date(int year, int month, int day)
		{
			if (year<1900||year>9999) throw new ArgumentOutOfRangeException(nameof(year), Error.InvYear);
			if (month<1||month>12) throw new ArgumentOutOfRangeException(nameof(month), Error.InvMon);
			if (day<1||day>31) throw new ArgumentOutOfRangeException(nameof(day), Error.InvDay);

			DateTime dateTime = Convert.ToDateTime(Year.ToFourCharString(year)+"-"+Month.ToTwoCharString(month)+"-"+Day.ToTwoCharString(day));

			this.month=dateTime.Month.ToString();
			this.year=dateTime.Year.ToString();
			this.day=dateTime.Day.ToString();

			CheckValidDate();

		}

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		/// <exception cref="ArgumentInvalidException" />
		public Date(string year, string month, string day)
		{
			if (string.IsNullOrWhiteSpace(year)) throw new ArgumentNullOrWhiteSpaceException(nameof(year), year, nameof(year)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(month)) throw new ArgumentNullOrWhiteSpaceException(nameof(month), month, nameof(month)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(day)) throw new ArgumentNullOrWhiteSpaceException(nameof(day), day, nameof(day)+Error.CantBeNullWhSp);
			if (Convert.ToInt32(year)<1900||Convert.ToInt32(year)>9999) throw new ArgumentOutOfRangeException(nameof(year), year, Error.InvYear);
			if (Convert.ToInt32(month)<1||Convert.ToInt32(month)>12) throw new ArgumentOutOfRangeException(nameof(month), month, Error.InvMon);
			if (Convert.ToInt32(day)<1||Convert.ToInt32(day)>31) throw new ArgumentOutOfRangeException(nameof(day), day, Error.InvDay);

			DateTime dateTime = Convert.ToDateTime(Year.ToFourCharString(year)+"-"+Month.ToTwoCharString(month)+"-"+Day.ToTwoCharString(day));

			this.month=dateTime.Month.ToString();
			this.year=dateTime.Year.ToString();
			this.day=dateTime.Day.ToString();

			CheckValidDate();

		}

		/// <remarks />
		/// <exception cref="ArgumentNullException" />
		private Date(Date date)
		{
			if (date==null) throw new ArgumentNullException(nameof(date),nameof(date)+Error.CantBeNull);

			this.day=date.day;
			this.month=date.month;
			this.year=date.year;

			CheckValidDate();

		}

		/// <remarks />
		private Date(DateTime dt)
		{
			this.day=dt.Day.ToString();
			this.month=dt.Month.ToString();
			this.year=dt.Year.ToString();

			CheckValidDate();

		}

		/// <remarks />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		/// <exception cref="ArgumentInvalidException" />
		private Date(string date)
		{
			if (string.IsNullOrWhiteSpace(date)) throw new ArgumentNullOrWhiteSpaceException(nameof(date), date, nameof(date)+Error.CantBeNullWhSp);
			if (!IsValid(date)) throw new ArgumentInvalidException(nameof(date), date+Error.InvDate);

			DateTime dateTime = Convert.ToDateTime(date);

			this.month=dateTime.Month.ToString();
			this.year=dateTime.Year.ToString();
			this.day=dateTime.Day.ToString();

			CheckValidDate();

		}

		#endregion

		#region Operators

		/// <returns>Result as bool</returns>
		public static bool operator == (Date a, Date b) => a.Equals(b);

		/// <returns>Result as bool</returns>
		public static bool operator != (Date a, Date b) => !a.Equals(b);

		/// <summary>Sets <see cref="Date"/> using data from a <see cref="DateTime"/></summary>
		/// <param name="dateTime"></param>
		public static implicit operator Date(DateTime dateTime) => new(dateTime);

		/// <summary>Sets <see cref="Date"/> using data from <see cref="DateTime"/></summary>
		/// <param name="date">Date as <paramref name="date"/> with the format 'yyyy-MM-dd'</param>
		/// <exception cref="ArgumentInvalidException" />
		public static implicit operator Date(string date) => new(date);

		/// <returns>Value of <see cref="Date"/> as a <see cref="DateTime"/></returns>
		/// <param name="date"></param>
		public static implicit operator DateTime(Date date) => date.ToDateTime();

		/// <returns> Value of <see cref="Date"/> as a <see cref="string"/></returns>
		/// <param name="date"></param>
		public static implicit operator string(Date date) => date.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public bool LeapYear { get => this.leapYear; }

		/// <remarks />
		public bool ValidDate { get => this.validDate; }

		/// <remarks />
		public Day Day { get => this.day; }

		/// <remarks />
		public Month Month { get => this.month; }

		/// <remarks />
		public Year Year { get => this.year; }

		#endregion

		#region Static Methods

		/// <returns>Identical copy of <paramref name="date"/></returns>
		/// <param name="date"><see cref="Date"/> to be cloned</param>
		public static Date Clone(Date date)
		{
			if (date==null) return null;

			return new Date(date);

		}

		/// <returns>'true' if <paramref name="date1"/> is identical to <paramref name="date2"/> - else 'false'</returns>
		/// <param name="date1">1st <see cref="Date"/> to compare</param>
		/// <param name="date2">2nd <see cref="Date"/> to compare</param>
		public static bool Equals(Date date1, Date date2)
		{
			if (date1==null&&date2==null) return true;
			if (date1!=null&&date2==null) return false;
			if (date1==null&&date2!=null) return false;

			if (!date1.ToString().Length.Equals(date2.ToString().Length)) return false;

			return date1.ToString().Equals(date2.ToString());

		}

		/// <returns>'true' if value of <paramref name="date"/> is '2010-01-01' - else false</returns>
		/// <param name="date"><see cref="Date"/> to check</param>
		/// <exception cref="ArgumentNullException" />
		public static bool IsEmpty(Date date)
		{
			if (date==null) throw new ArgumentNullException(nameof(date), nameof(date)+Error.CantBeNull);

			return date.Equals("2010-01-01");

		}

		/// <returns>'true' if <paramref name="date"/> is valid - else 'false'</returns>
		/// <param name="date">Date in the format 'yyyy-MM-dd' as <see cref="string"/></param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static bool IsValid(string date)
		{
			if (string.IsNullOrWhiteSpace(date)) throw new ArgumentNullOrWhiteSpaceException(nameof(date), date, nameof(date)+Error.CantBeNullWhSp);

			return DateTime.TryParse(date, out DateTime dateTime);

		}

		/// <returns><paramref name="date"/> converted to <see cref="string"/></returns>
		/// <param name="date"><see cref="Date"/> to convert</param>
		/// <exception cref="NullReferenceException" />
		public static string ToString(Date date)
		{
			if (date==null) throw new NullReferenceException();

			return date.ToString();

		}

		#endregion

		#region Non-Static Methods

		#region Add

		/// <summary>Adds number of <paramref name="days"/> to this <see cref="Date"/> (or substracts if <paramref name="days"/> is neagative)</summary>
		/// <param name="days">Number of days to add as <see cref="int"/></param>
		/// <exception cref="NullReferenceException" />
		public void AddDays(int days)
		{
			if (this==null) throw new NullReferenceException(); // This is necessary to safe guard the class
			DateTime dt = ((DateTime)this).AddDays(days);

			this.day=dt.Day;
			this.month=dt.Month;
			this.year=dt.Year;

		}

		/// <summary>Adds number of <paramref name="days"/> to this <see cref="Date"/> (or substracts if <paramref name="days"/> is neagative)</summary>
		/// <param name="days">Number of days to add as <see cref="string"/></param>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public void AddDays(string days)
		{
			if (this==null) throw new NullReferenceException(); // This is necessary to safe guard the class
			if (string.IsNullOrWhiteSpace(days)) throw new ArgumentNullOrWhiteSpaceException(nameof(days), days, nameof(days)+Error.CantBeNullWhSp);

			AddDays(Convert.ToInt32(days));

		}

		/// <summary>Adds number of <paramref name="months"/> to this <see cref="Date"/> (or substracts if <paramref name="months"/> is neagative)</summary>
		/// <param name="months">Number of months to add as <see cref="int"/></param>
		/// <exception cref="NullReferenceException" />
		public void AddMonths(int months)
		{
			if (this==null) throw new NullReferenceException(); // This is necessary to safe guard the class
			DateTime dt = ((DateTime)this).AddMonths(months);

			this.month=dt.Month;
			this.year=dt.Year;

		}

		/// <summary>Adds number of <paramref name="months"/> to this <see cref="Date"/> (or substracts if <paramref name="months"/> is neagative)</summary>
		/// <param name="months">Number of months to add as <see cref="string"/></param>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public void AddMonths(string months)
		{
			if (this==null) throw new NullReferenceException(); // This is necessary to safe guard the class
			if (string.IsNullOrWhiteSpace(months)) throw new ArgumentNullOrWhiteSpaceException(nameof(months), months, nameof(months)+Error.CantBeNullWhSp);

			AddMonths(Convert.ToInt32(months));

		}

		/// <summary>Adds number of <paramref name="years"/> to this <see cref="Date"/> (or substracts if <paramref name="years"/> is neagative)</summary>
		/// <param name="years">Number of years to add as <see cref="int"/></param>
		/// <exception cref="NullReferenceException" />
		public void AddYears(int years) =>this.year=((DateTime)this).AddYears(years).Year;

		/// <summary>Adds number of <paramref name="years"/> to this <see cref="Date"/> (or substracts if <paramref name="years"/> is neagative)</summary>
		/// <param name="years">Number of years to add as <see cref="string"/></param>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public void AddYears(string years)
		{
			if (this==null) throw new NullReferenceException(); // This is necessary to safe guard the class
			if (string.IsNullOrWhiteSpace(years)) throw new ArgumentNullOrWhiteSpaceException(nameof(years), years, nameof(years)+Error.CantBeNullWhSp);

			AddYears(Convert.ToInt32(years));

		}

		#endregion

		#region Check

		/// <summary>Checks wether <see cref="Year"/> is a leap year, and sets the <see cref="LeapYear"/> flag</summary>
		/// <exception cref="NullReferenceException" />
		private void CheckValidDate()
		{
			if (string.IsNullOrWhiteSpace(this.year)) throw new NullOrWhiteSpaceRefException(nameof(this.year), this.year, nameof(this.year)+Error.CantBeNullWhSp);

			this.leapYear=Year.IsLeapYear(this.year.ToInt32());
			this.validDate=DateTime.TryParse(ToString(), out DateTime dt);

		}

		/// <summary>Checks wether <see cref="Year"/> is a leap year, and sets the <see cref="LeapYear"/> flag</summary>
		/// <exception cref="NullReferenceException" />
		private void CheckLeapYear()
		{
			if (string.IsNullOrWhiteSpace(this.year)) throw new NullOrWhiteSpaceRefException(nameof(this.year), this.year, nameof(this.year)+Error.CantBeNullWhSp);

			this.leapYear=Year.IsLeapYear(this.year.ToInt32());

		}

		#endregion

		#region Compare To
		/// <remarks />
		public int CompareTo(Date other) => this.ToDateTime().CompareTo(other.ToDateTime());


		/// <remarks />
		public int CompareTo(object obj) => this.ToDateTime().CompareTo(Convert.ToDateTime(obj));

		/// <remarks />
		public int CompareTo(string other)=> this.ToDateTime().CompareTo(Convert.ToDateTime(other));

		#endregion

		/// <returns>Identical copy of this <see cref="Date"/></returns>
		public Date Clone()
		{
			if (this==null) return null;

			return new Date(this);
		}

		#region Equals

		/// <summary>Compares this Date to <paramref name="date"/></summary>
		/// <param name="date"></param>
		/// <returns>Result as bool</returns>
		public bool Equals(Date date) => ((DateTime)this).Equals(date);

		/// <summary>Compares this Date to <paramref name="dt"/></summary>
		/// <param name="dt">Month or day</param>
		/// <returns>Result as bool</returns>
		public bool Equals(DateTime dt) => ((DateTime)this).Equals(dt);

		/// <summary>Compares this Bit to <paramref name="obj"/></summary>
		/// <param name="obj" />
		/// <returns>Result as bool</returns>
		public override bool Equals(object obj) => this.Equals((Bit)obj);

		/// <returns>'true' if this <see cref="Date"/> is identical to <paramref name="date"/> - else 'false'</returns>
		/// <param name="date">Date to compare as <see cref="string"/> in format 'yyyy-DD-mm'</param>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public bool Equals(string date) =>  ((DateTime)this).Equals(Convert.ToDateTime(date));

		#endregion

		/// <remarks />
		public override int GetHashCode() => ToString().GetHashCode();

		/// <returns>'true' if this <see cref="Date"/> is equal to '2010-01-01' - else 'false'</returns>
		public bool IsEmpty()
		{
			if (this==null) throw new NullReferenceException();
			else if (!this.year.Equals("2010")) return false;
			else if (!this.month.Equals("01")||!this.day.Equals("01")) return false;
			else return true;
		}

		#region To something 
		/// <returns>This Date as <see cref="DateTime"/></returns>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="NullOrWhiteSpaceRefException" />
		public DateTime ToDateTime() => Convert.ToDateTime(ToString());

		/// <returns>This <see cref="Date"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This Date as string with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This Date as string with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="g";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			return format switch
			{
				"d" => ((DateTime)this).ToString("d", provider),
				"D" => ((DateTime)this).ToString("D", provider),
				"g" => ((DateTime)this).ToString("yyyy-MM-dd"),
				_ => throw new FormatException(string.Format("The {0} format string is not supported.", format)),
			};
		}

		#endregion

		#endregion

	}

	/// <Summary>DataType</Summary>
	public class Day : IFormattable, IComparable, IComparable<Day>, IComparable<int>, IComparable<string>
	{
		#region Fields
		private string value;

		#endregion

		#region Constructors
		/// <remarks />
		public Day() { value="01"; }

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		public Day(int day)
		{
			if (day<1||day>31) throw new ArgumentInvalidException(nameof(day), day, Error.InvDay);

			value=ToTwoCharString(day);

		}

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public Day(string day)
		{
			if (!string.IsNullOrWhiteSpace(day)) throw new ArgumentNullOrWhiteSpaceException(nameof(day), day, nameof(day)+Error.CantBeNullWhSp);
			if (Convert.ToInt32(day)<1||Convert.ToInt32(day)>31) throw new ArgumentInvalidException(nameof(day), day, Error.InvDay);

			value=ToTwoCharString(day);

		}

		#endregion

		#region Operators

		/// <returns>Result as bool</returns>
		public static bool operator == (Day a, Day b) => a.Equals(b);

		/// <returns>Result as bool</returns>
		public static bool operator != (Day a, Day b) => !a.Equals(b);

		/// <returns><paramref name="i"/> as Day</returns>
		/// <param name="i">Integer within the range of [1;31]</param>
		public static implicit operator Day(int i) => new(i);

		/// <returns><paramref name="i"/> as Day</returns>
		/// <param name="i">Integer within the range of [1;31]</param>
		public static implicit operator Day(string s) => new(s);

		/// <returns><paramref name="day"/> as int</returns>
		/// <param name="day" />
		public static implicit operator int(Day day) => day.ToInt32();

		/// <returns><paramref name="day"/> as a string</returns>
		/// <param name="day" />
		public static implicit operator string(Day day) => day.value;

		#endregion

		#region Methods

		#region Compare To

		/// <remarks />
		public int CompareTo(Day other) => this.ToInt32().CompareTo(other.ToInt32());

		/// <remarks />
		public int CompareTo(int other) => this.ToInt32().CompareTo(other);

		/// <remarks />
		public int CompareTo(object obj) => this.CompareTo((Day)obj);

		/// <remarks />
		public int CompareTo(string other)=> this.ToInt32().CompareTo(Convert.ToInt32(other));

		#endregion

		/// <summary>Compares this Day to <paramref name="obj"/></summary>
		/// <param name="obj" />
		/// <returns>Result as bool</returns>
		public override bool Equals(object obj) => this.value.Equals(ToTwoCharString(obj.ToString()));

		/// <remarks />
		public override int GetHashCode() => this.value.GetHashCode();

		#region To something
		/// <returns><paramref name="day"/> converted two character <see cref="string"/></returns>
		/// <exception cref="NullOrWhiteSpaceRefException" />
		public static string ToTwoCharString(string day)
		{
			if (string.IsNullOrWhiteSpace(day)) throw new NullOrWhiteSpaceRefException(nameof(day), day, nameof(day)+Error.CantBeNullWhSp);

			return day.Length switch
			{
				<=0 => "00",
				1 => "0"+day,
				2 => day,
				_ => day.Remove(0, day.Length-2),
			};
		}

		/// <returns><paramref name="day"/> converted into a two character string</returns>
		public static string ToTwoCharString(int day) => ToTwoCharString(Convert.ToString(day));

		/// <returns>This <see cref="Day"/> as an <see cref="int"/></returns>
		public int ToInt32() => Convert.ToInt32(value);

		/// <returns>This <see cref="Day"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Day"/> as <see cref="string"/> with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Day"/> as <see cref="string"/> with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Day format as a <see cref="string"/> - e.g. 'g", and 'G'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="G";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			return format switch
			{
				"g" => Convert.ToInt32(this.value).ToString(),
				"G" => ToTwoCharString(this.value),
				_ => throw new FormatException(string.Format("The {0} format string is not supported.", format)),
			};
		}

		#endregion

		#endregion

	}

	/// <Summary>DataType</Summary>
	public class Month : IFormattable
	{
		#region Fields
		private string value;

		private static Dictionary<int, int> daysPerMonth = new() { { 1, 31 }, { 2, 28 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
		private static Dictionary<int, string> monthNames = new() { { 1, "January" }, { 2, "February" }, { 3, "March" }, { 4, "April" }, { 5, "May" }, { 6, "June" }, { 7, "July" }, { 8, "August" }, { 9, "September" }, { 10, "October" }, { 11, "November" }, { 12, "December" } };

		#endregion

		#region Constructors
		/// <remarks />
		public Month() { value="01"; }

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		private Month(int month)
		{
			if (month<1||month>12) throw new ArgumentInvalidException(nameof(month), month, Error.InvMon);

			value=ToTwoCharString(month);

		}

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		private Month(string month)
		{
			if (!string.IsNullOrWhiteSpace(month)) throw new ArgumentNullOrWhiteSpaceException(nameof(month), month, nameof(month)+Error.CantBeNullWhSp);
			if (Convert.ToInt32(month)<1||Convert.ToInt32(month)>12) throw new ArgumentInvalidException(nameof(month), month, Error.InvMon);

			value=ToTwoCharString(month);

		}

		#endregion

		#region Operators

		/// <returns>Result as bool</returns>
		public static bool operator == (Month a, Month b) => a.value.Equals(b.value);

		/// <returns>Result as bool</returns>
		public static bool operator != (Month a, Month b) => !a.value.Equals(b.value);

		/// <summary>Sets <see cref="Month"/> using data from an <see cref="int"/></summary>
		public static implicit operator Month(int month) => new(month);

		/// <summary>Sets <see cref="Month"/> using data from a <see cref="string"/></summary>
		public static implicit operator Month(string month) => new(month);

		/// <returns>Value of <see cref="Month"/> as a <see cref="int"/></returns>
		public static implicit operator int(Month month) => month.ToInt32();

		/// <returns> Value of <see cref="Month"/> as a <see cref="string"/></returns>
		public static implicit operator string(Month month) => month.value;

		#endregion

		#region Methods

		#region Compare To

		/// <remarks />
		public int CompareTo(Day other) => this.ToInt32().CompareTo(other.ToInt32());

		/// <remarks />
		public int CompareTo(int other) => this.ToInt32().CompareTo(other);

		/// <remarks />
		public int CompareTo(object obj) => this.CompareTo((Day)obj);

		/// <remarks />
		public int CompareTo(string other)=> this.ToInt32().CompareTo(Convert.ToInt32(other));

		#endregion

		/// <summary>Compares this Month to <paramref name="obj"/></summary>
		/// <param name="obj" />
		/// <returns>Result as bool</returns>
		public override bool Equals(object obj) => this.value.Equals(obj.ToString());

		/// <remarks />
		public override int GetHashCode() => this.value.GetHashCode();

		#region To something

		/// <returns><paramref name="month"/> converted two character <see cref="string"/></returns>
		/// <exception cref="NullOrWhiteSpaceRefException" />
		public static string ToTwoCharString(string month)
		{
			if (string.IsNullOrWhiteSpace(month)) throw new NullOrWhiteSpaceRefException(nameof(month), month, nameof(month)+Error.CantBeNullWhSp);

			return month.Length switch
			{
				<=0 => "00",
				1 => "0"+month,
				2 => month,
				_ => month.Remove(0, month.Length-2),
			};
		}

		/// <returns><paramref name="month"/> converted into a two character string</returns>
		public static string ToTwoCharString(int month) => ToTwoCharString(Convert.ToString(month));

		/// <returns>This <see cref="Month"/> as an <see cref="int"/></returns>
		public int ToInt32() => Convert.ToInt32(this.value);

		/// <returns>This <see cref="Month"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Month"/> as <see cref="string"/> with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Month"/> as <see cref="string"/> with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Month format as a <see cref="string"/> - e.g. 'g", and 'G'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="G";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			return format switch
			{
				"g" => Convert.ToInt32(this.value).ToString(),
				"G" => ToTwoCharString(this.value),
				_ => throw new FormatException(string.Format("The {0} format string is not supported.", format)),
			};
		}

		/// <returns>days per month as <see cref="int"/></returns>
		/// <exception cref="ArgumentInvalidException" />
		public static int RetrieveDaysPerMonth(int month, bool leapYear)
		{
			if (month<1||month>12) throw new ArgumentInvalidException(nameof(month),month,nameof(month)+Error.InvMon);

			if (leapYear&&month.Equals(2)) return 29;
			else return daysPerMonth[month];
		}

		/// <returns>Name of month as <see cref="string"/></returns>
		/// <exception cref="ArgumentInvalidException" />
		public static string RetrieveMonthName(int month)
		{
			if (month<1||month>12) throw new ArgumentInvalidException(nameof(month), month, nameof(month)+Error.InvMon);

			return monthNames[month];

		}

		#endregion

		#endregion

	}

	/// <Summary>DataType</Summary>
	public class Year : IFormattable
	{

		#region Fields
		private string value;

		#endregion

		#region Constructors
		/// <remarks />
		public Year() { value="2010"; }

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		private Year(int year)
		{
			if (year<1||year>12) throw new ArgumentInvalidException(nameof(year), year, Error.InvMon);

			value=ToFourCharString(year);

		}

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		private Year(string year)
		{
			if (!string.IsNullOrWhiteSpace(year)) throw new ArgumentNullOrWhiteSpaceException(nameof(year), year, nameof(year)+Error.CantBeNullWhSp);
			if (Convert.ToInt32(year)<1||Convert.ToInt32(year)>12) throw new ArgumentInvalidException(nameof(year), year, Error.InvMon);

			value=ToFourCharString(year);

		}

		#endregion

		#region Operators

		/// <returns>Result as bool</returns>
		public static bool operator == (Year a, Year b) => a.Value.Equals(b.Value);

		/// <returns>Result as bool</returns>
		public static bool operator != (Year a, Year b) => !a.Value.Equals(b.Value);

		/// <returns><paramref name="i"/> as Year</returns>
		public static implicit operator Year(int i) => new(i);

		/// <returns><paramref name="s"/> as Year</returns>
		public static implicit operator Year(string s) => new(s);

		/// <returns><paramref name="year"/>  as int</returns>
		public static implicit operator int(Year year) => year.ToInt32();

		/// <returns><paramref name="year"/> as string</returns>
		public static implicit operator string(Year year) => year.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public string Value { get => value; set => value=ToFourCharString(value); }

		#endregion

		#region Methods

		/// <summary>Compares this Year to <paramref name="obj"/></summary>
		/// <param name="obj" />
		/// <returns>Result as bool</returns>
		public override bool Equals(object obj) => this.Value.Equals(obj.ToString());

		/// <remarks />
		public override int GetHashCode() => Value.GetHashCode();

		#region Is something
		/// <returns>'true' if <paramref name="year"/> is a leap year - else 'false'</returns>
		/// <param name="year">Four digit integer within the range [1900;9999]</param>
		/// <exception cref="ArgumentOutOfRangeException" />
		public static bool IsLeapYear(int year)
		{
			if (!year.ToString().Length.Equals(4)||Convert.ToInt32(year)<1900||Convert.ToInt32(year)<9999) throw new ArgumentOutOfRangeException(nameof(year), year, Error.InvYear);

			return IsLeapYear(year.ToString());
		}

		/// <returns>'true' if <paramref name="year"/> is a leap year - else 'false'</returns>
		/// <param name="year">Four digit integer within the range [1900;9999] as <see cref="string"/> with the format 'yyyy'</param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		/// <exception cref="ArgumentOutOfRangeException" />
		public static bool IsLeapYear(string year)
		{

			if (string.IsNullOrWhiteSpace(year)) throw new ArgumentNullOrWhiteSpaceException(nameof(year), year, nameof(year)+Error.CantBeNullWhSp);
			if (!year.Length.Equals(4)||Convert.ToInt32(year)<1900||Convert.ToInt32(year)<9999) throw new ArgumentOutOfRangeException(nameof(year), year, Error.InvYear);


			if (decimal.Remainder(Convert.ToDecimal(year), 4m).Equals(0m))
			{
				if (decimal.Remainder(Convert.ToDecimal(year), 100m).Equals(0m))
				{
					if (decimal.Remainder(Convert.ToDecimal(year), 400m).Equals(0m))
						return true;
					else
						return false;
				}

				return true;

			}
			else
				return false;

		}

		#endregion

		#region To something

		/// <returns><paramref name="year"/> converted into four character <see cref="string"/></returns>
		public static string ToFourCharString(string year)
		{
			if (string.IsNullOrWhiteSpace(year)) throw new NullOrWhiteSpaceRefException(nameof(year), year, nameof(year)+Error.CantBeNullWhSp);

			return ToFourCharString(Convert.ToInt32(year));

		}

		/// <returns><paramref name="year"/> converted into a two character string</returns>
		/// <exception cref="ArgumentInvalidException" />
		public static string ToFourCharString(int year)
		{
			if (year<0) throw new ArgumentInvalidException(nameof(year), year, nameof(year)+Error.InvYear);

			int thisYear = Convert.ToInt32(DateTime.Today.Year.ToString().Remove(0, 2));

			switch (year.ToString().Length)
			{
				case 1: return "200"+year.ToString();
				case 2: if (year<=thisYear) return "20"+year.ToString(); else return "19"+year.ToString();
				case 3: if (year>=900&&year<=999) return "1"+year; else return "2"+year.ToString();
				case 4: return year.ToString();
				default: return year.ToString().Remove(0, year.ToString().Length-4);
			}
		}

		/// <returns>This <see cref="Year"/> as an <see cref="int"/></returns>
		public int ToInt32() => Convert.ToInt32(value);

		/// <returns>This <see cref="Year"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Year"/> as <see cref="string"/> with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Year"/> as <see cref="string"/> with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Year format as a <see cref="string"/> - e.g. 'g', and 'G'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		/// <exception cref="FormatException" />
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="G";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			return format switch
			{
				"g" => Convert.ToInt32(this.Value).ToString(),
				"G" => ToFourCharString(this.value),
				_ => throw new FormatException(string.Format("The {0} format string is not supported.", format)),
			};
		}

		#endregion

		#endregion

	}

}
