// Public Domain. See License.txt
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lib.Common.DataTypes
{

	/// <Summary>DataType</Summary>
	public class Date : IFormattable
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
		public Date() { this.year="2010"; this.month="01"; this.day="01"; CheckValidDate(); }

		/// <remarks />
		/// <exception cref="ArgumentNullException" />
		public Date(Date date)
		{
			if (date==null) throw new ArgumentNullException(nameof(date),nameof(date)+Error.CantBeNull);

			this.day=date.day;
			this.month=date.month;
			this.year=date.year;

			CheckValidDate();

		}

		/// <remarks />
		public Date(DateTime dateTime)
		{
			this.day=dateTime.Day.ToString();
			this.month=dateTime.Month.ToString();
			this.year=dateTime.Year.ToString();

			CheckValidDate();

		}

		/// <remarks />
		/// <exception cref="ArgumentOutOfRangeException" />
		/// <exception cref="InvalidRefException" />
		public Date(int year, int month, int day)
		{
			if (year<1900||year>9999) throw new ArgumentOutOfRangeException(nameof(year), Error.InvYear);
			if (month<1||month>9999) throw new ArgumentOutOfRangeException(nameof(month), Error.InvMon);
			if (day<1||year>9999) throw new ArgumentOutOfRangeException(nameof(day), Error.InvDay);

			string date = year.ToString()+"-"+month.ToString()+"-"+day.ToString();

			if (!IsValid(date)) throw new InvalidRefException(nameof(date), date, date+Error.InvDate);

			this.year=year.ToString();
			this.month=month.ToString();
			this.day=day.ToString();


			CheckValidDate();

		}

		/// <remarks />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		/// <exception cref="ArgumentInvalidException" />
		public Date(string date)
		{
			if (string.IsNullOrWhiteSpace(date)) throw new ArgumentNullOrWhiteSpaceException(nameof(date), date, nameof(date)+Error.CantBeNullWhSp);
			if (!IsValid(date)) throw new ArgumentInvalidException(nameof(date), date+Error.InvDate);

			DateTime dateTime = Convert.ToDateTime(date);

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
			if (Convert.ToInt32(month)<1||Convert.ToInt32(month)>9999) throw new ArgumentOutOfRangeException(nameof(month), month, Error.InvMon);
			if (Convert.ToInt32(day)<1||Convert.ToInt32(day)>9999) throw new ArgumentOutOfRangeException(nameof(day), day, Error.InvDay);

			string tempMonth;
			string tempDay;

			switch (month.ToString().Length)
			{
				case 1:
					tempMonth="0"+month;
					break;
				default:
					tempMonth=month;
					break;
			}

			switch (day.ToString().Length)
			{
				case 1:
					tempDay="0"+day;
					break;
				default:
					tempDay=day;
					break;
			}

			string date = year.ToString()+"-"+tempMonth+"-"+tempDay;

			if (!IsValid(date)) throw new InvalidRefException(nameof(date), date+Error.InvDate);

			this.year=year;
			this.month=tempMonth;
			this.day=tempDay;

			CheckValidDate();

		}

		#endregion

		#region Operators

		/// <summary>Sets <see cref="Date"/> using data from a <see cref="DateTime"/></summary>
		/// <param name="dateTime"></param>
		public static implicit operator Date(DateTime dateTime) => new Date(dateTime);

		/// <summary>Sets <see cref="Date"/> using data from <see cref="DateTime"/></summary>
		/// <param name="date">Date as <paramref name="date"/> with the format 'yyyy-MM-dd'</param>
		/// <exception cref="ArgumentInvalidException" />
		public static implicit operator Date(string date)
		{
			if (date==null) return null;
			if (!IsValid(date)) throw new ArgumentInvalidException(nameof(date),date,nameof(date)+Error.InvDate);

			return new Date(date);

		}

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

		/// <remarks />
		public string Value { get => ToString("g"); }

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

		/// <summary>Adds number of <paramref name="days"/> to this <see cref="Date"/> (or substracts if <paramref name="days"/> is neagative)</summary>
		/// <param name="days">Number of days to add as <see cref="int"/></param>
		/// <exception cref="NullReferenceException" />
		public void AddDays(int days)
		{
			if (this==null) throw new NullReferenceException(); // This is necessary to safe guard the class

			CheckLeapYear();

			if (days+this.day.ToInt32()>Month.RetrieveDaysPerMonth(this.month, leapYear))
			{
				RetrieveYearsMonthsDays(days, out int remDays, out int remMonths, out int remYears);

				if (remMonths>0) AddMonths(remMonths);
				if (remYears>0) AddYears(remYears);

				this.day=day.ToInt32()+remDays;
			}
			else if (days+this.day.ToInt32()<0)
			{
				RetrieveNegYearsMonthsDays(days, out int remDays, out int remMonths, out int remYears);

				if (remMonths>0) AddMonths(remMonths);
				if (remYears>0) AddYears(remYears);

				this.day=day.ToInt32()+remDays;

			}
			else this.day=day.ToInt32()+days;

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

			if (this.month.ToInt32()+months>12)
			{
				RetrieveYearsMonths(months, out int remMonths, out int remYears);

				if (remYears>0) AddYears(remYears);

				this.month=this.month.ToInt32()+remMonths;

			}
			else if (this.month.ToInt32()+months<0)
			{
				RetrieveNegYearsMonths(months, out int remMonths, out int remYears);

				if (remYears>0) AddYears(remYears);

				this.month=this.month.ToInt32()+remMonths;

			}
			else this.month=this.month.ToInt32()+months;
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
		public void AddYears(int years)
		{
			if (this==null) throw new NullReferenceException(); // This is necessary to safe guard the class

			this.year=this.year.ToInt32()+years;

		}

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

		/// <summary>Checks wether <see cref="Year"/> is a leap year, and sets the <see cref="LeapYear"/> flag</summary>
		/// <exception cref="NullReferenceException" />
		private void CheckValidDate()
		{
			if (string.IsNullOrWhiteSpace(this.year)) throw new NullOrWhiteSpaceRefException(nameof(this.year), this.year, nameof(this.year)+Error.CantBeNullWhSp);

			CheckLeapYear();
			this.validDate=DateTime.TryParse(this.Value, out DateTime dt);

		}

		/// <summary>Checks wether <see cref="Year"/> is a leap year, and sets the <see cref="LeapYear"/> flag</summary>
		/// <exception cref="NullReferenceException" />
		private void CheckLeapYear()
		{
			if (string.IsNullOrWhiteSpace(this.year)) throw new NullOrWhiteSpaceRefException(nameof(this.year), this.year, nameof(this.year)+Error.CantBeNullWhSp);

			this.leapYear=Year.IsLeapYear(this.year.ToInt32());

		}

		/// <returns>Identical copy of this <see cref="Date"/></returns>
		public Date Clone()
		{
			if (this==null) return null;

			return new Date(this);
		}

		/// <returns>'true' if this <see cref="Date"/> is identical to <paramref name="date"/> - else 'false'</returns>
		/// <param name="date">Date to compare as <see cref="string"/> in format 'yyyy-DD-mm'</param>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public bool Equals(string date)
		{
			if (this==null) throw new NullReferenceException(); // This is necessary to safe guard the class
			if (!IsValid(date)) throw new ArgumentInvalidException(nameof(date), date, date+Error.InvDate);
			if (date==null) return false;

			return this.Value.Equals(date);

		}

		/// <returns>'true' if this <see cref="Date"/> is identical to <paramref name="date"/> - else 'false'</returns>
		/// <param name="date"></param>
		/// <exception cref="NullReferenceException" />
		public bool Equals(Date date)
		{
			if (this==null) throw new NullReferenceException();  //this is necessary to guard against reverse-pinvokes andother callers who do not use the callvirt instruction
			if (date==null) return false;
			if (ReferenceEquals(this, date)) return true;

			return Equals(date.Value);

		}

		/// <returns>'true' if this <see cref="Date"/> is identical to <paramref name="dateTime"/>.Date - else 'false'</returns>
		/// <param name="dateTime">Month or day</param>
		public bool Equals(DateTime dateTime)
		{
			if (this==null) throw new NullReferenceException();  //this is necessary to guard against reverse-pinvokes andother callers who do not use the callvirt instruction

			return Equals(dateTime.ToString("yyyy-MM-dd"));

		}

		/// <returns>'true' if this <see cref="Date"/> is equal to '2010-01-01' - else 'false'</returns>
		public bool IsEmpty()
		{
			if (this==null) throw new NullReferenceException();
			if (!this.year.Equals("2010")) return false;
			if (!this.month.Equals("01")||!this.day.Equals("01")) return false;

			return true;

		}

		/// <remarks />
		private void RetrieveNegYearsMonths(int months, out int remMonths, out int remYears)
		{
			remMonths=0;
			remYears=0;

			bool cont = false;

			if (months<0-this.month.ToInt32())
			{
				int tempMonths = months+12;
				int tempYears = -1;

				if (tempMonths<-12)
				{
					cont=true;

					while (cont)
					{
						tempMonths+=12;
						tempYears-=1;

						if (tempMonths>-12) cont=false;

					}

					remYears=tempYears;
					remMonths=tempMonths;
				}
				else if (tempMonths.Equals(-12))
				{
					remYears=tempYears-1;
					remMonths-=this.month.ToInt32();
				}
				else
				{
					remYears=tempYears;
					remMonths=tempMonths;
				}

			}
			else if (months.Equals(0-this.month.ToInt32()))
			{
				remYears-=1;
				remMonths=13-this.month.ToInt32();
			}
			else remMonths=months;

		}

		/// <remarks />
		private void RetrieveNegYearsMonths(string months, out int remMonths, out int remYears) => RetrieveNegYearsMonths(Convert.ToInt32(months), out remMonths, out remYears);

		private void RetrieveNegYearsMonthsDays(int days, out int remDays, out int remMonths, out int remYears)
		{
			CheckLeapYear();

			remDays=0;
			remMonths=0;
			remYears=0;
			// OBS Org Month!!!!!!!!!!!!!!!!!!
			bool cont = false;

			if (days<0-this.day.ToInt32())
			{
				int tempDays = days+60;
				int tempMonths = -1;

				int currentYear = this.year.ToInt32();
				int currentMonth = this.month.ToInt32()-1;

				if (currentMonth<1)
				{
					currentMonth=currentMonth+12;
					currentYear-=1;
				}

				int daysCurentMonth = Month.RetrieveDaysPerMonth(currentMonth, Year.IsLeapYear(currentYear));

				if (tempDays>0-daysCurentMonth)
				{
					cont=true;

					while (cont)
					{
						tempDays+=daysCurentMonth;
						currentMonth-=1;

						if (currentMonth<1)
						{
							currentMonth=currentMonth+12;
							currentYear-=1;
						}

						daysCurentMonth=Month.RetrieveDaysPerMonth(currentMonth, Year.IsLeapYear(currentYear));
						tempMonths-=1;

						if (tempDays>0-daysCurentMonth) cont=false;

					}

					if (tempMonths>12)
					{
						RetrieveNegYearsMonths(tempMonths, out remMonths, out remYears);
						remDays=tempDays;
					}
					else if (tempMonths.Equals(12))
					{
						remYears-=1;
						remDays=tempDays;
					}
					else
					{
						remMonths=tempMonths;
						remDays=tempDays;
					}


				}
				else if (tempDays==0-daysCurentMonth) remMonths=tempMonths-1;
				else
				{
					remMonths=tempMonths;
					remDays=tempDays;
				}

			}
			else if (days.Equals(0-this.day.ToInt32())) remMonths-=1;
			else remDays=days;

		}

		/// <remarks />
		private void RetrieveNegYearsMonthsDays(string days, out int remDays, out int remMonths, out int remYears) => RetrieveNegYearsMonthsDays(Convert.ToInt32(days), out remDays, out remMonths, out remYears);

		private void RetrieveYearsMonths(int months, out int remMonths, out int remYears)
		{
			remMonths=0;
			remYears=0;

			bool cont = false;

			if (months>12-this.month.ToInt32())
			{
				int tempMonths = months-12;
				int tempYears = 1;

				if (tempMonths>12)
				{
					cont=true;

					while (cont)
					{
						tempMonths-=12;
						tempYears+=1;

						if (tempMonths>12)
							cont=false;

					}

					remYears=tempYears;
					remMonths=tempMonths;
				}
				else if (tempMonths.Equals(12)) remYears=tempYears+1;
				else
				{
					remYears=tempYears;
					remMonths=tempMonths;
				}

			}
			else if (months.Equals(12-this.month.ToInt32())) remYears+=1;
			else remMonths=months;
		}

		/// <remarks />
		private void RetrieveYearsMonths(string months, out int remMonths, out int remYears) => RetrieveYearsMonths(Convert.ToInt32(months), out remMonths, out remYears);


		private void RetrieveYearsMonthsDays(int days, out int remDays, out int remMonths, out int remYears)
		{
			CheckLeapYear();

			remDays=0;
			remMonths=0;
			remYears=0;
			int daysOrgMonth = Month.RetrieveDaysPerMonth(this.month, leapYear);

			bool cont = false;

			if (days>daysOrgMonth-this.day.ToInt32())
			{
				int tempDays = days-daysOrgMonth+this.day.ToInt32();
				int tempMonths = 1;

				int currentYear = this.year.ToInt32();
				int currentMonth = this.month.ToInt32()+1;

				if (currentMonth>12)
				{
					currentMonth=currentMonth-12;
					currentYear+=1;
				}

				int daysCurentMonth = Month.RetrieveDaysPerMonth(currentMonth, Year.IsLeapYear(currentYear));

				if (tempDays>daysCurentMonth)
				{
					cont=true;

					while (cont)
					{
						tempDays-=daysCurentMonth;
						currentMonth+=1;

						if (currentMonth>12)
						{
							currentMonth=currentMonth-12;
							currentYear+=1;
						}

						daysCurentMonth=Month.RetrieveDaysPerMonth(currentMonth, Year.IsLeapYear(currentYear));
						tempMonths+=1;

						if (tempDays<daysCurentMonth) cont=false;

					}

					if (tempMonths>12)
					{
						RetrieveYearsMonths(tempMonths, out remMonths, out remYears);
						remDays=tempDays;
					}
					else if (tempMonths.Equals(12))
					{
						remYears=1;
						remDays=tempDays;
					}
					else
					{
						remMonths=tempMonths;
						remDays=tempDays;
					}


				}
				else if (tempDays==daysCurentMonth) remMonths=tempMonths+1;
				else
				{
					remMonths=tempMonths;
					remDays=tempDays;
				}

			}
			else if (days==daysOrgMonth-this.day.ToInt32()) remMonths+=1;
			else remDays=days;

		}

		/// <remarks />
		private void RetrieveYearsMonthsDays(string days, out int remDays, out int remMonths, out int remYears) => RetrieveYearsMonthsDays(Convert.ToInt32(days), out remDays, out remMonths, out remYears);

		/// <returns>This <see cref="Date"/> as <see cref="DateTime"/></returns>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="NullOrWhiteSpaceRefException" />
		public DateTime ToDateTime()
		{
			if (this==null) throw new NullReferenceException();
			if (this.Value==null) throw new NullOrWhiteSpaceRefException(nameof(this.Value), this.Value, nameof(this.Value)+Error.CantBeNullWhSp);

			return Convert.ToDateTime(this.Value);

		}

		/// <returns>This <see cref="Date"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Date"/> as <see cref="string"/> with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Date"/> as <see cref="string"/> with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="g";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			switch (format)
			{
				case "d": return Convert.ToDateTime(this.Value).ToString("d",provider);
				case "D": return Convert.ToDateTime(this.Value).ToString("D",provider);
				case "g": return Convert.ToDateTime(this.Value).ToString("yyyy-MM-dd");
				default: throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

		#endregion

	}

	/// <Summary>DataType</Summary>
	public class Day : IFormattable
	{
		#region Fields
		private string value;

		#endregion

		#region Constructors
		/// <remarks />
		public Day() { value="01"; }

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		private Day(int day)
		{
			if (day<1||day>31) throw new ArgumentInvalidException(nameof(day), day, Error.InvDay);

			value=ToTwoCharString(day);

		}

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		private Day(string day)
		{
			if (!string.IsNullOrWhiteSpace(day)) throw new ArgumentNullOrWhiteSpaceException(nameof(day), day, nameof(day)+Error.CantBeNullWhSp);
			if (Convert.ToInt32(day)<1||Convert.ToInt32(day)>31) throw new ArgumentInvalidException(nameof(day), day, Error.InvDay);

			value=ToTwoCharString(day);

		}

		#endregion

		#region Operators

		/// <summary>Sets <see cref="Day"/> using data from an <see cref="int"/></summary>
		/// <param name="day">An within the range of [1;31]</param>
		/// <exception cref="ArgumentInvalidException" />
		public static implicit operator Day(int day) => new Day(day);

		/// <summary>Sets <see cref="Day"/> using data from a <see cref="string"/></summary>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static implicit operator Day(string day) => new Day(day);

		/// <returns>Value of <see cref="Day"/> as a <see cref="int"/></returns>
		public static implicit operator int(Day day) => day.ToInt32();

		/// <returns> Value of <see cref="Day"/> as a <see cref="string"/></returns>
		public static implicit operator string(Day day) => day.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public string Value { get => value; set => value=ToTwoCharString(value); }
		#endregion

		#region Methods
		/// <returns><paramref name="day"/> converted two character <see cref="string"/></returns>
		/// <exception cref="NullOrWhiteSpaceRefException" />
		public static string ToTwoCharString(string day)
		{
			if (string.IsNullOrWhiteSpace(day)) throw new NullOrWhiteSpaceRefException(nameof(day), day, nameof(day)+Error.CantBeNullWhSp);

			switch (day.Length)
			{
				case<=0:
					return "00";
				case 1:
					return "0"+day;
				case 2:
					return day;
				default:
					return day.Remove(0, day.Length-2);
			}

		}

		/// <returns><paramref name="day"/> converted into a two character string</returns>
		protected static string ToTwoCharString(int day) => ToTwoCharString(Convert.ToString(day));

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
			if (this==null)
				return "null";
			if (string.IsNullOrEmpty(format)) format="G";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			switch (format)
			{
				case "g": return Convert.ToInt32(this.Value).ToString();
				case "G": return ToTwoCharString(this.value);
				default: throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

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

		/// <summary>Sets <see cref="Month"/> using data from an <see cref="int"/></summary>
		public static implicit operator Month(int month) => new Month(month);

		/// <summary>Sets <see cref="Month"/> using data from a <see cref="string"/></summary>
		public static implicit operator Month(string month) => new Month(month);

		/// <returns>Value of <see cref="Month"/> as a <see cref="int"/></returns>
		public static implicit operator int(Month day) => day.ToInt32();

		/// <returns> Value of <see cref="Month"/> as a <see cref="string"/></returns>
		public static implicit operator string(Month day) => day.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public string Value { get => value; set => value=ToTwoCharString(value); }

		#endregion

		#region Methods
		/// <returns><paramref name="month"/> converted two character <see cref="string"/></returns>
		/// <exception cref="NullOrWhiteSpaceRefException" />
		private static string ToTwoCharString(string month)
		{
			if (string.IsNullOrWhiteSpace(month)) throw new NullOrWhiteSpaceRefException(nameof(month), month, nameof(month)+Error.CantBeNullWhSp);

			switch (month.Length)
			{
				case<=0:
					return "00";
				case 1:
					return "0"+month;
				case 2:
					return month;
				default:
					return month.Remove(0, month.Length-2);
			}

		}

		/// <returns><paramref name="month"/> converted into a two character string</returns>
		protected static string ToTwoCharString(int month) => ToTwoCharString(Convert.ToString(month));

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

			switch (format)
			{
				case "g": return Convert.ToInt32(this.Value).ToString();
				case "G": return ToTwoCharString(this.value);
				default: throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

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

		/// <summary>Sets <see cref="Year"/> using data from an <see cref="int"/></summary>
		public static implicit operator Year(int month) => new Year(month);

		/// <summary>Sets <see cref="Year"/> using data from a <see cref="string"/></summary>
		public static implicit operator Year(string month) => new Year(month);

		/// <returns>Value of <see cref="Year"/> as a <see cref="int"/></returns>
		public static implicit operator int(Year day) => day.ToInt32();

		/// <returns> Value of <see cref="Year"/> as a <see cref="string"/></returns>
		public static implicit operator string(Year day) => day.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public string Value { get => value; set => value=ToFourCharString(value); }

		#endregion

		#region Methods
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

		/// <returns><paramref name="year"/> converted into a two character string</returns>
		/// <exception cref="ArgumentInvalidException" />
		private static string ToFourCharString(int year)
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

		/// <returns><paramref name="year"/> converted into four character <see cref="string"/></returns>
		private static string ToFourCharString(string year)
		{
			if (string.IsNullOrWhiteSpace(year)) throw new NullOrWhiteSpaceRefException(nameof(year), year, nameof(year)+Error.CantBeNullWhSp);

			return ToFourCharString(Convert.ToInt32(year));

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

			switch (format)
			{
				case "g": return Convert.ToInt32(this.Value).ToString();
				case "G": return ToFourCharString(this.value);
				default: throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

		#endregion

	}

}
