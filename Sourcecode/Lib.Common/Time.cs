// Public Domain.See License.txt
using System;
using System.Globalization;

namespace Lib.Common.DataTypes
{

	/// <Summary>DataType</Summary>
	public class Time : IFormattable
	{
		#region Fields
		private Hour hour;
		private Minute minute;
		private Second second;
		private const int minutesPerHour = 60, secondsPerMinute = 60;


		#endregion

		#region Constructors

		/// <remarks />
		public Time()
		{
			this.hour="00";
			this.minute="00";
			this.second="00";
		}

		/// <remarks />
		public Time(DateTime dateTime)
		{
			this.hour=dateTime.Hour.ToString();
			this.minute=dateTime.Minute.ToString();
			this.second=dateTime.Second.ToString();
		}

		/// <remarks />
		public Time(Time time)
		{
			if (time==null) throw new NullReferenceException();

			this.hour=time.hour;
			this.minute=time.minute;
			this.second=time.second;

		}

		/// <remarks />
		public Time(int hour, int minute, int second)
		{
			if (hour<0||hour>23) throw new ArgumentOutOfRangeException(nameof(hour), Error.InvHour);
			if (minute<0||minute>59) throw new ArgumentOutOfRangeException(nameof(minute), Error.InvMin);
			if (second<0||second>59) throw new ArgumentOutOfRangeException(nameof(second), Error.InvSec);

			string time = hour.ToString()+"-"+minute.ToString()+"-"+second.ToString();

			if (!IsValid(time)) throw new InvalidRefException(nameof(time), time, time+Error.InvTime);

			this.hour=hour;
			this.minute=minute;
			this.second=second;

		}

		/// <remarks />
		public Time(string hour, string minute, string second)
		{
			if (string.IsNullOrWhiteSpace(hour)) throw new ArgumentNullOrWhiteSpaceException(nameof(hour), hour, nameof(hour)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(minute)) throw new ArgumentNullOrWhiteSpaceException(nameof(minute), minute, nameof(minute)+Error.CantBeNullWhSp);
			if (string.IsNullOrWhiteSpace(second)) throw new ArgumentNullOrWhiteSpaceException(nameof(second), second, nameof(second)+Error.CantBeNullWhSp);
			if (Convert.ToInt32(hour)<0||Convert.ToInt32(hour)>23) throw new ArgumentOutOfRangeException(nameof(hour), hour, Error.InvHour);
			if (Convert.ToInt32(minute)<0||Convert.ToInt32(minute)>59) throw new ArgumentOutOfRangeException(nameof(minute), minute, Error.InvMin);
			if (Convert.ToInt32(second)<0||Convert.ToInt32(second)>59) throw new ArgumentOutOfRangeException(nameof(second), second, Error.InvSec);

			string date = hour.ToString()+":"+minute.ToString()+"-"+second.ToString();

			if (!IsValid(date)) throw new InvalidRefException(nameof(date), date+Error.InvDate);

			this.hour=hour;
			this.minute=minute;
			this.second=second;

		}

		/// <remarks />
		public Time(string time)
		{
			if (string.IsNullOrWhiteSpace(time)) throw new ArgumentNullOrWhiteSpaceException(nameof(time), time, nameof(time)+Error.CantBeNullWhSp);
			if (!IsValid(time)) throw new InvalidRefException(nameof(time), time, time+Error.InvDate);

			DateTime dateTime = Convert.ToDateTime(time);

			this.hour=dateTime.Hour.ToString();
			this.minute=dateTime.Minute.ToString();
			this.second=dateTime.Second.ToString();

		}

		#endregion

		#region Operators

		/// <summary>Sets <see cref="Time"/> using data from a <see cref="DateTime"/></summary>
		public static implicit operator Time(DateTime dateTime) => new Time(dateTime);

		/// <summary>Sets <see cref="Time"/> using data from <see cref="DateTime"/></summary>
		public static implicit operator Time(string time)
		{
			if (time==null) return null;

			return new Time(time);

		}

		/// <returns>Value of <see cref="Time"/> as a <see cref="DateTime"/></returns>
		public static implicit operator DateTime(Time time) => time.ToDateTime();

		/// <returns> Value of <see cref="Time"/> as a <see cref="string"/></returns>
		public static implicit operator string(Time time) => time.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public Hour Hour { get => hour; }

		/// <remarks />
		public Minute Minute { get => minute; }

		/// <remarks />
		public Second Second { get => second; }

		/// <remarks />
		public string Value { get => ToString(); }

		#endregion

		#region Static Methods

		/// <param name="time"><see cref="Time"/> to be cloned</param>
		/// <returns>Identical copy of <paramref name="time"/></returns>
		public static Time Clone(Time time)
		{
			if (time==null) return null;

			return new Time(time);

		}

		/// <returns>'true' if <paramref name="time1"/> is identical to <paramref name="time2"/> - else 'false'</returns>
		/// <param name="time1">1st <see cref="Time"/> to compare</param>
		/// <param name="time2">2nd <see cref="Time"/> to compare</param>
		public static bool Equals(Time time1, Time time2)
		{
			if (time1==null&&time2==null) return true;
			if (time1!=null&&time2==null) return false;
			if (time1==null&&time2!=null) return false;
			if (time1.Value.Length!=time2.Value.Length) return false;

			return time1.Value.Equals(time2.Value);

		}

		/// <returns>'true' if time equals '00:00:00'</returns>
		/// <exception cref="ArgumentNullException" />
		public static bool IsEmpty(Time time)
		{
			if (time==null) throw new ArgumentNullException(nameof(time), nameof(time)+Error.CantBeNull);

			return time.Equals("00:00:00");

		}

		/// <returns>'true' if <paramref name="time"/> is valid - else 'false'</returns>
		/// <param name="time">Time in the format 'HH:mm:ss' as <see cref="string"/></param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static bool IsValid(string time)
		{
			if (string.IsNullOrWhiteSpace(time)) throw new ArgumentNullOrWhiteSpaceException(nameof(time), time, nameof(time)+Error.CantBeNullWhSp);

			return DateTime.TryParse(time, out DateTime dateTime);

		}

		/// <returns><paramref name="time"/> converted to <see cref="string"/></returns>
		/// <param name="time"><see cref="Date"/> to convert</param>
		/// <exception cref="NullReferenceException" />
		public static string ToString(Time time)
		{
			if (time==null) throw new NullReferenceException();

			return time.ToString();

		}

		#endregion

		#region Methods

		/// <summary>Adds number of <paramref name="hours"/> to this <see cref="Time"/> (or substracts if <paramref name="hours"/> is neagative)</summary>
		/// <param name="hours">Number of hours to add as <see cref="int"/></param>
		/// <exception cref="NullReferenceException" />
		public void AddHours(int hours)
		{
			if (hour.ToInt32()+hours>24)
			{
				Math.DivRem(hours+hour.ToInt32(), 24, out int remHours);

				this.hour=this.hour.ToInt32()+remHours;
			}
			else if(this.hour.ToInt32()+hours<0)
			{
				Math.DivRem(hours-this.hour.ToInt32(), 24, out int remHours);

				this.hour=this.hour.ToInt32()+remHours;
			}
			else this.hour=this.hour.ToInt32()+hours;

		}

		/// <summary>Adds number of <paramref name="hours"/> to this <see cref="Time"/> (or substracts if <paramref name="hours"/> is neagative)</summary>
		/// <param name="hours">Number of hours to add as <see cref="string"/></param>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public void AddHours(string hours) => AddHours(Convert.ToInt32(hours));

		/// <summary>Adds number of <paramref name="minutes"/> to this <see cref="Time"/> (or substracts if <paramref name="minutes"/> is neagative)</summary>
		/// <param name="minutes">Number of months to add as <see cref="int"/></param>
		/// <exception cref="NullReferenceException" />
		public void AddMinutes(int minutes)
		{
			if (this.minute.ToInt32()+minutes>60)
			{
				RetrieveHoursMinutes(minutes, out int remMinutes, out int remHours);

				if (!remHours.Equals(0)) AddHours(remHours);

				this.minute=this.minute.ToInt32()+remMinutes;

			}
			else if (minute.ToInt32()+minutes<0)
			{
				RetrieveNegHoursMinutes(minutes, out int remMinutes, out int remHours);

				if (!remHours.Equals(0)) AddHours(remHours);

				this.minute=this.minute.ToInt32()+remMinutes;

			}
			else this.minute=this.minute.ToInt32()+minutes;
		}

		/// <summary>Adds number of <paramref name="minutes"/> to this <see cref="Time"/> (or substracts if <paramref name="minutes"/> is neagative)</summary>
		/// <param name="minutes">Number of minutes to add as <see cref="string"/></param>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public void AddMinutes(string minutes) => AddMinutes(Convert.ToInt32(minutes));

		/// <summary>Adds number of <paramref name="seconds"/> to this <see cref="Time"/> (or substracts if <paramref name="seconds"/> is neagative)</summary>
		/// <param name="seconds">Number of seconds to add as <see cref="int"/></param>
		/// <exception cref="NullReferenceException" />
		public void AddSeconds(int seconds)
		{

			if (this.second.ToInt32()+seconds>60)
			{
				RetrieveHoursMinutesSeconds(seconds, out int remSeconds, out int remMinutes, out int remHours);

				if (!remHours.Equals(0)) AddHours(remHours);
				if (remMinutes.Equals(0)) AddMinutes(remMinutes);

				this.second=this.second.ToInt32()+remSeconds;
			}
			else if (this.second.ToInt32()+seconds>60)
			{
				RetrieveNegHoursMinutesSeconds(seconds, out int remSeconds, out int remMinutes, out int remHours);

				if (!remHours.Equals(0)) AddHours(remHours);
				if (!remMinutes.Equals(0)) AddMinutes(remMinutes);

				this.second=this.second.ToInt32()+remSeconds;

			}
			else this.second=this.second.ToInt32()+seconds;

		}

		/// <summary>Adds number of <paramref name="seconds"/> to this <see cref="Time"/> (or substracts if <paramref name="seconds"/> is neagative)</summary>
		/// <param name="seconds">Number of seconds to add as <see cref="string"/></param>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public void AddSeconds(string seconds) => AddSeconds(Convert.ToInt32(seconds));

		/// <returns>Identical copy of this <see cref="Time"/></returns>
		public Time Clone()
		{
			return new Time(this);
		}

		/// <returns>'true' if this <see cref="Time"/> is identical to <paramref name="time"/> - else 'false'</returns>
		/// <param name="time">Date to compare as <see cref="string"/> in format 'yyyy-DD-mm'</param>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public bool Equals(string time)
		{
			if (this==null) throw new NullReferenceException(); //this is necessary to guard against reverse-pinvokes and other callers who do not use the callvirt instruction
			if (time==null) return false;

			return this.Value.Equals(time);

		}

		/// <returns>'true' if this <see cref="Time"/> is identical to <paramref name="time"/> - else 'false'</returns>
		/// <param name="time"></param>
		/// <exception cref="NullReferenceException" />
		public bool Equals(Time time)
		{
			if (this==null) throw new NullReferenceException();  //this is necessary to guard against reverse-pinvokes andother callers who do not use the callvirt instruction
			if (time==null) return false;
			if (ReferenceEquals(this, time)) return true;

			return Equals(time.Value);

		}

		/// <returns>'true' if this <see cref="Time"/> is identical to <paramref name="dateTime"/>.Time - else 'false'</returns>
		/// <param name="dateTime">Month or day</param>
		public bool Equals(DateTime dateTime)
		{
			if (this==null) throw new NullReferenceException();  //this is necessary to guard against reverse-pinvokes andother callers who do not use the callvirt instruction

			return Equals(dateTime.ToString("HH:mm:ss"));

		}

		/// <returns>'true' if this <see cref="Time"/> is equal to '00:00:00' - else 'false'</returns>
		public bool IsEmpty()
		{
			if (this==null) throw new NullReferenceException();
			if (!this.hour.Equals("00")||!this.minute.Equals("00")||!this.second.Equals("00")) return false;

			return true;

		}

		/// <remarks />
		private void RetrieveNegHoursMinutes(int minutes, out int remMinutes, out int remHours)
		{
			remHours=0;
			remMinutes=0;

			bool cont = false;

			if (minutes<0-this.minute.ToInt32())
			{
				int tempMinutes = minutes+60;
				int tempHours = -1;

				if (tempMinutes<-60)
				{
					cont=true;

					while (cont)
					{
						tempMinutes+=60;
						tempHours-=1;

						if (tempMinutes>-60) cont=false;

					}

					remHours=tempHours;
					remMinutes=tempMinutes;

				}
				else if (tempMinutes.Equals(-60)) remHours=tempHours-1;
				else
				{
					remHours=tempHours;
					remMinutes=tempMinutes;
				}

			}
			else if (minutes.Equals(0-this.minute.ToInt32()))
			{
				remHours-=1;
				remMinutes=59-this.minute.ToInt32();
			}
			else remMinutes=minutes;

		}

		/// <remarks />
		private void RetrieveNegHoursMinutes(string minutes, out int remMinutes, out int remHours) => RetrieveNegHoursMinutes(Convert.ToInt32(minutes), out remMinutes, out remHours);

		/// <remarks />
		private void RetrieveNegHoursMinutesSeconds(int seconds, out int remSeconds, out int remMinutes, out int remHours)
		{
			remHours=0;
			remMinutes=0;
			remSeconds=0;

			bool cont = false;

			if (seconds<0-this.second.ToInt32())
			{
				int tempSeconds = seconds+60;
				int tempMinutes = -1;

				if (tempSeconds<-60)
				{
					cont=true;

					while (cont)
					{
						tempSeconds+=60;
						tempMinutes-=1;

						if (tempSeconds>-60)
							cont=false;

					}

					if (tempMinutes<-60)
					{
						RetrieveNegHoursMinutes(tempMinutes, out remMinutes, out remHours);
						remSeconds=tempSeconds;
					}
					else if (tempMinutes.Equals(-60))
					{
						remHours-=1;
						remSeconds=tempSeconds;
					}
					else
					{
						remMinutes=tempMinutes;
						remSeconds=tempSeconds;
					}


				}
				else if (tempSeconds.Equals(-60))
				{
					remMinutes=tempMinutes-1;
					remSeconds=59-this.second.ToInt32();
				}
				else
				{
					remMinutes=tempMinutes;
					remSeconds=tempSeconds;
				}

			}
			else if (seconds.Equals(0-this.second.ToInt32()))
			{
				remMinutes-=1;
				remSeconds=59-this.second.ToInt32();

			}
			else remSeconds=seconds;

		}

		/// <remarks />
		private void RetrieveNegHoursMinutesSeconds(string minutes, out int remSeconds, out int remMinutes, out int remHours) => RetrieveNegHoursMinutesSeconds(Convert.ToInt32(minutes), out remSeconds, out remMinutes, out remHours);

		/// <remarks />
		private void RetrieveHoursMinutes(int minutes, out int remMinutes, out int remHours)
		{
			remHours=0;
			remMinutes=0;

			bool cont = false;

			if (minutes>this.minute.ToInt32())
			{
				int tempMinutes = minutes-this.minute.ToInt32();
				int tempHours = 1;

				if (tempMinutes>60)
				{
					cont=true;

					while (cont)
					{
						tempMinutes-=60;
						tempHours+=1;

						if (tempMinutes<60) cont=false;

					}

					remHours=tempHours;
					remMinutes=tempMinutes;

				}
				else if (tempMinutes.Equals(60)) remHours=tempHours+1;
				else
				{
					remHours=tempHours;
					remMinutes=tempMinutes;
				}

			}
			else if (minutes.Equals(this.minute.ToInt32()))
			{
				remHours-=1;
				remMinutes=59-this.minute.ToInt32();
			}
			else remMinutes=minutes;

		}

		/// <remarks />
		private void RetrieveHoursMinutes(string minutes, out int remMinutes, out int remHours) => RetrieveHoursMinutes(Convert.ToInt32(minutes), out remMinutes, out remHours);

		/// <remarks />
		private void RetrieveHoursMinutesSeconds(int seconds, out int remSeconds, out int remMinutes, out int remHours)
		{
			remHours=0;
			remMinutes=0;
			remSeconds=0;

			bool cont = false;

			if (seconds>this.second.ToInt32())
			{
				int tempSeconds = seconds-this.second.ToInt32();
				int tempMinutes = +1;

				if (tempSeconds>60)
				{
					cont=true;

					while (cont)
					{
						tempSeconds-=60;
						tempMinutes+=1;

						if (tempSeconds<60) cont=false;

					}

					if (tempMinutes>60)
					{
						RetrieveNegHoursMinutes(tempMinutes, out remMinutes, out remHours);
						remSeconds=tempSeconds;
					}
					else if (tempMinutes.Equals(60))
					{
						remHours+=1;
						remSeconds=tempSeconds;
					}
					else
					{
						remMinutes=tempMinutes;
						remSeconds=tempSeconds;
					}


				}
				else if (tempSeconds.Equals(60))
				{
					remMinutes=tempMinutes+1;
				}
				else
				{
					remMinutes=tempMinutes;
					remSeconds=tempSeconds;
				}

			}
			else if (seconds.Equals(0-this.second.ToInt32()))
			{
				remMinutes-=1;
				remSeconds=59-this.second.ToInt32();

			}
			else remSeconds=seconds;

		}

		/// <remarks />
		private void RetrieveHoursMinutesSeconds(string minutes, out int remSeconds, out int remMinutes, out int remHours) => RetrieveHoursMinutesSeconds(Convert.ToInt32(minutes), out remSeconds, out remMinutes, out remHours);

		/// <returns>This <see cref="Time"/> as <see cref="DateTime"/></returns>
		/// <exception cref="NullReferenceException" />
		/// <exception cref="NullOrWhiteSpaceRefException" />
		public DateTime ToDateTime()
		{
			if (this==null) throw new NullReferenceException();
			if (this.Value==null) throw new NullOrWhiteSpaceRefException(nameof(this.Value), this.Value, nameof(this.Value)+Error.CantBeNullWhSp);

			return Convert.ToDateTime(this.Value);

		}

		/// <returns>This <see cref="Time"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Time"/> as <see cref="string"/> with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Time"/> as <see cref="string"/> with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		/// <exception cref="FormatException" />
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="G";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			switch (format)
			{
				case "g": return Convert.ToDateTime(this.Value).ToString("HH:mm:ss");
				case "t": return Convert.ToDateTime(this.Value).ToString("t", provider);
				case "T": return Convert.ToDateTime(this.Value).ToString("T", provider);
				default: throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

		#endregion

	}

	/// <Summary>DataType</Summary>
	public class Minute : IFormattable
	{
		#region Fields
		private string value;

		#endregion

		#region Constructors
		/// <remarks />
		public Minute() { value="00"; }

		/// <remarks />
		private Minute(int minute)
		{
			if (minute<0||minute>59) throw new ArgumentInvalidException(nameof(minute), minute, Error.InvMin);

			value=ToTwoCharString(minute);

		}

		/// <remarks />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		/// <exception cref="ArgumentInvalidException" />
		private Minute(string minute)
		{
			if (!string.IsNullOrWhiteSpace(minute)) throw new ArgumentNullOrWhiteSpaceException(nameof(minute), minute, nameof(minute)+Error.CantBeNullWhSp);
			if (Convert.ToInt32(minute)<0||Convert.ToInt32(minute)>59) throw new ArgumentInvalidException(nameof(minute), minute, Error.InvMin);

			value=ToTwoCharString(minute);

		}

		#endregion

		#region Operators

		/// <summary>Sets <see cref="Minute"/> using data from an <see cref="int"/></summary>
		/// <param name="minute">An within the range of [0;59] as <see cref="int"/></param>
		/// <exception cref="ArgumentInvalidException" />
		public static implicit operator Minute(int minute) => new Minute(minute);

		/// <summary>Sets <see cref="Minute"/> using data from a <see cref="string"/></summary>
		/// <param name="minute">An integer within the range of [0;59] as <see cref="string"/></param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static implicit operator Minute(string minute) => new Minute(minute);

		/// <returns>Value of <see cref="Minute"/> as a <see cref="int"/></returns>
		public static implicit operator int(Minute minute) => minute.ToInt32();

		/// <returns> Value of <see cref="Minute"/> as a <see cref="string"/></returns>
		public static implicit operator string(Minute minute) => minute.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public string Value { get => value; set => value=ToTwoCharString(value); }
		#endregion

		#region Methods
		/// <returns><paramref name="minute"/> converted two character <see cref="string"/></returns>
		/// <exception cref="NullOrWhiteSpaceRefException" />
		private static string ToTwoCharString(string minute)
		{
			if (string.IsNullOrWhiteSpace(minute)) throw new NullOrWhiteSpaceRefException(nameof(minute), minute, nameof(minute)+Error.CantBeNullWhSp);

			switch (minute.Length)
			{
				case<=0:
					return "00";
				case 1:
					return "0"+minute;
				case 2:
					return minute;
				default:
					return minute.Remove(0, minute.Length-2);
			}

		}

		/// <returns><paramref name="minute"/> converted into a two character string</returns>
		private static string ToTwoCharString(int minute) => ToTwoCharString(Convert.ToString(minute));

		/// <returns>This <see cref="Minute"/> as an <see cref="int"/></returns>
		public int ToInt32() => Convert.ToInt32(value);

		/// <returns>This <see cref="Minute"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Minute"/> as <see cref="string"/> with requested <paramref name="format"/></returns>
		/// <param name="format">Second format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Minute"/> as <see cref="string"/> with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Second format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		/// <exception cref="FormatException" />
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="g";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			switch (format)
			{
				case "g":
					return ToTwoCharString(this.value);
				case "m":
					return Convert.ToInt32(this.Value).ToString();
				case "mm":
					return ToTwoCharString(this.value);
				default: throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

		#endregion

	}

	/// <Summary>DataType</Summary>
	public class Hour : IFormattable
	{
		#region Fields
		private string value;

		#endregion

		#region Constructors
		/// <remarks />
		public Hour() { value="00"; }

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		private Hour(int hour)
		{
			if (hour<0||hour>23) throw new ArgumentInvalidException(nameof(hour), hour, Error.InvHour);

			value=ToTwoCharString(hour);

		}

		/// <remarks />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		/// <exception cref="ArgumentInvalidException" />
		private Hour(string hour)
		{
			if (!string.IsNullOrWhiteSpace(hour)) throw new ArgumentNullOrWhiteSpaceException(nameof(hour), hour, nameof(hour)+Error.CantBeNullWhSp);
			if (Convert.ToInt32(hour)<0||Convert.ToInt32(hour)>59) throw new ArgumentInvalidException(nameof(hour), hour, Error.InvMin);

			value=ToTwoCharString(hour);

		}

		#endregion

		#region Operators

		/// <summary>Sets <see cref="Hour"/> using data from an <see cref="int"/></summary>
		/// <param name="hour">An within the range of [0;59] as <see cref="int"/></param>
		/// <exception cref="ArgumentInvalidException" />
		public static implicit operator Hour(int hour) => new Hour(hour);

		/// <summary>Sets <see cref="Hour"/> using data from a <see cref="string"/></summary>
		/// <param name="hour">An integer within the range of [0;59] as <see cref="string"/></param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static implicit operator Hour(string hour) => new Hour(hour);

		/// <returns>Value of <see cref="Hour"/> as a <see cref="int"/></returns>
		public static implicit operator int(Hour hour) => hour.ToInt32();

		/// <returns> Value of <see cref="Hour"/> as a <see cref="string"/></returns>
		public static implicit operator string(Hour hour) => hour.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public string Value { get => value; set => value=ToTwoCharString(value); }
		#endregion

		#region Methods
		/// <returns><paramref name="hour"/> converted into a two character string</returns>
		/// <exception cref="ArgumentInvalidException" />
		private static string ToTwoCharString(int hour)
		{
			if (hour<0) throw new ArgumentInvalidException(nameof(hour), hour, Error.InvHour);

			switch (hour.ToString().Length)
			{
				case 1: return "0"+hour.ToString();
				case 2: return hour.ToString();
				default: return hour.ToString().Remove(0, hour.ToString().Length-2);
			}

		}

		/// <returns><paramref name="hour"/> converted two character <see cref="string"/></returns>
		/// <exception cref="NullOrWhiteSpaceRefException" />
		private static string ToTwoCharString(string hour) 
		{
			if (string.IsNullOrWhiteSpace(hour)) throw new ArgumentNullOrWhiteSpaceException(nameof(hour), hour, nameof(hour)+Error.CantBeNullWhSp);

			return ToTwoCharString(Convert.ToInt32(hour));

		}

		/// <returns>This <see cref="Hour"/> as an <see cref="int"/></returns>
		public int ToInt32() => Convert.ToInt32(value);

		/// <returns>This <see cref="Hour"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Hour"/> as <see cref="string"/> with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Hour"/> as <see cref="string"/> with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		/// <exception cref="FormatException" />
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="g";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			switch (format)
			{
				case "g":
					return ToTwoCharString(this.value);
				case "m":
					return Convert.ToInt32(this.Value).ToString();
				case "mm":
					return ToTwoCharString(this.value);
				default:
					throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

		#endregion

	}

	/// <Summary>DataType</Summary>
	public class Second : IFormattable
	{
		#region Fields
		private string value;

		#endregion

		#region Constructors
		/// <remarks />
		public Second() { value="00"; }

		/// <remarks />
		/// <exception cref="ArgumentInvalidException" />
		private Second(int second)
		{
			if (second<0||second>59) throw new ArgumentInvalidException(nameof(second), second, Error.InvSec);

			value=ToTwoCharString(second);

		}

		/// <remarks />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		/// <exception cref="ArgumentInvalidException" />
		private Second(string second)
		{
			if (!string.IsNullOrWhiteSpace(second)) throw new ArgumentNullOrWhiteSpaceException(nameof(second), second, nameof(second)+Error.CantBeNullWhSp);
			if (Convert.ToInt32(second)<0||Convert.ToInt32(second)>59) throw new ArgumentInvalidException(nameof(second), second, Error.InvMin);

			value=ToTwoCharString(second);

		}

		#endregion

		#region Operators

		/// <summary>Sets <see cref="Value"/> using data from an <see cref="int"/></summary>
		/// <param name="second">An within the range of [0;59] as <see cref="int"/></param>
		/// <exception cref="ArgumentInvalidException" />
		public static implicit operator Second(int second) => new Second(second);

		/// <summary>Sets <see cref="Value"/> using data from a <see cref="string"/></summary>
		/// <param name="second">An integer within the range of [0;59] as <see cref="string"/></param>
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		public static implicit operator Second(string second) => new Second(second);

		/// <returns>Value of <see cref="Value"/> as a <see cref="int"/></returns>
		public static implicit operator int(Second second) => second.ToInt32();

		/// <returns> Value of <see cref="Value"/> as a <see cref="string"/></returns>
		public static implicit operator string(Second second) => second.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public string Value { get => value; set => value=ToTwoCharString(value); }
		#endregion

		#region Methods
		/// <returns><paramref name="second"/> converted two character <see cref="string"/></returns>
		/// <exception cref="NullOrWhiteSpaceRefException" />
		private static string ToTwoCharString(string second)
		{
			if (string.IsNullOrWhiteSpace(second)) throw new NullOrWhiteSpaceRefException(nameof(second), second, nameof(second)+Error.CantBeNullWhSp);

			switch (second.Length)
			{
				case<=0:
					return "00";
				case 1:
					return "0"+second;
				case 2:
					return second;
				default:
					return second.Remove(0, second.Length-2);
			}

		}

		/// <returns><paramref name="second"/> converted into a two character string</returns>
		private static string ToTwoCharString(int second) => ToTwoCharString(Convert.ToString(second));

		/// <returns>This <see cref="Second"/> as an <see cref="int"/></returns>
		public int ToInt32() => Convert.ToInt32(value);

		/// <returns>This <see cref="Second"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Second"/> as <see cref="string"/> with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Second"/> as <see cref="string"/> with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		/// <exception cref="FormatException" />
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="g";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			switch (format)
			{
				case "g":
					return ToTwoCharString(this.value);
				case "m":
					return Convert.ToInt32(this.Value).ToString();
				case "mm":
					return ToTwoCharString(this.value);
				default:
					throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

		#endregion

	}

}
