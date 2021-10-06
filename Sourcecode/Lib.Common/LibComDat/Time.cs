// Public Domain.See License.txt
using System;
using System.Globalization;

namespace Lib.Common.DataTypes
{
	#nullable enable
	/// <Summary>DataType</Summary>
	public class Time : IFormattable,IComparable,IConvertible
	{
		#region Constructors

		/// <remarks />
		public Time() => this.Value="00:00:00";

		/// <remarks />
		public Time(DateTime dateTime) => this.Value=dateTime.Hour.ToString("HH:mm:ss");

		/// <remarks />
		public Time(int hour, int minute, int second) => this.Value=new DateTime(hour, minute, second).ToString("HH:mm:ss");

		/// <remarks />
		public Time(string hour, string minute, string second) => this.Value=new DateTime(Convert.ToInt32(hour), Convert.ToInt32(minute), Convert.ToInt32(second)).ToString("HH:mm:ss");

		/// <remarks /><exception cref="ArgumentNullException" />
		public Time(string time) => this.Value=Convert.ToDateTime(time).ToString("HH:mm:ss");

		/// <remarks /><exception cref="ArgumentNullException" />
		public Time(Time time) => this.Value=time.Value;

		#endregion

		#region Boolean Operators

		/// <returns>Result as bool</returns>
		public static bool operator ==(Time a, Time b) => a.CompareTo(b) switch { 0 => true, _ => false  };

		/// <returns>Result as bool</returns>
		public static bool operator !=(Time a, Time b) => a.CompareTo(b) switch { 0 => false, _ => true  };

		/// <returns>Result as bool</returns>
		public static bool operator >=(Time a, Time b) => a.CompareTo(b) switch { >=0 => true, _ => false  };

		/// <returns>Result as bool</returns>
		public static bool operator <=(Time a, Time b) => a.CompareTo(b) switch { <=0 => true, _ => false  };

		/// <returns>Result as bool</returns>
		public static bool operator >(Time a, Time b) => a.CompareTo(b) switch { >0 => true, _ => false  };

		/// <returns>Result as bool</returns>
		public static bool operator <(Time a, Time b) => a.CompareTo(b) switch { <0 => true, _ => false  };

		#endregion

		#region Implicit Operators

		/// <returns><paramref name="dt"/> as Time</returns><param name="dt" />
		public static explicit operator Time(DateTime dt) => new Time(dt);

		/// <returns><paramref name="time"/> as DateTime</returns><param name="time" />
		public static explicit operator DateTime(Time time) => time.ToDateTime();

		/// <returns><paramref name="time"/> as Date</returns><param name="time" />
		public static implicit operator Time(string time) => new(time);

		/// <returns><paramref name="time"/> as string</returns><param name="time" />
		public static implicit operator string(Time time) => time.ToString();

		#endregion

		#region Properties
		/// <remarks />
		public string Value { get; private set; }

		#endregion

		#region Static Methods

		/// <returns>Copy of <paramref name="time"/></returns><param name="time" />
		public static Time Clone(Time time) => new(time);

		/// <returns>Result as bool</returns><param name="time1" /><param name="time2" />
		public static bool Equals(Time time1, Time time2) => time1.ToDateTime().Equals(time2.ToDateTime());

		/// <returns>Result as bool</returns><exception cref="ArgumentNullException" />
		public static bool IsEmpty(Time time) => time.Value.Equals("00:00:00");

		/// <returns>Result as bool</returns><param name="time">Time in the format 'HH:mm:ss'</param><exception cref="ArgumentNullException" />
		public static bool IsValid(string time) => DateTime.TryParse(time, out DateTime dateTime);

		/// <returns><paramref name="time"/> as string</returns><param name="time" />
		public static string ToString(Time time) => time.ToString();

		/// <returns><paramref name="s"/> as two character string</returns>
		private static string ToTwoCharString(string s) => s.Length switch { <=0 => "00", 1 => "0"+s, 2 => s, _ => s.Remove(0, s.Length-2), };

		/// <remarks />
		private static string ToTwoCharString(int minute) => ToTwoCharString(Convert.ToString(minute));

		#endregion

		#region Methods

		#region Add

		/// <remarks />
		public void AddHours(int hours) => this.Value=this.ToDateTime().AddHours(hours).ToString("HH:mm:ss");

		/// <remarks />
		public void AddHours(string hours) => AddHours(Convert.ToInt32(hours));

		/// <remarks />
		public void AddMinutes(int minutes) => this.Value=this.ToDateTime().AddMinutes(minutes).ToString("HH:mm:ss");

		/// <remarks />
		public void AddMinutes(string minutes) => AddMinutes(Convert.ToInt32(minutes));

		/// <remarks />
		public void AddSeconds(int seconds) => this.Value=this.ToDateTime().AddSeconds(seconds).ToString("HH:mm:ss");

		/// <remarks />
		public void AddSeconds(string seconds) => AddSeconds(Convert.ToInt32(seconds));

		#endregion

		/// <returns>Identical copy of this <see cref="Time"/></returns>
		public Time Clone() => new Time(this);

		/// <remarks />
		public int CompareTo(object obj) => this.ToDateTime().CompareTo(Convert.ToDateTime(obj));

		#region Equals

		/// <returns>Result as bool</returns><param name="time" />
		public bool Equals(Time time) => ToDateTime().Equals(time.ToDateTime());

		/// <returns>Result as bool</returns><param name="obj" />
		public override bool Equals(object obj) => ToDateTime().Equals(Convert.ToDateTime(obj));

		#endregion

		#region Get

		/// <remarks />
		public override int GetHashCode() => ToDateTime().GetHashCode();

		/// <remarks />
		public TypeCode GetTypeCode() => TypeCode.DateTime;

		#endregion

		#region Is something
		/// <returns>Result as bool</returns>
		public bool IsEmpty() { if (!this.Value.Equals("00:00:00")) return false; else return true; }

		#endregion

		#region To something

		/// <remarks />
		public bool ToBoolean(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToBoolean(provider);

		/// <remarks />
		public byte ToByte(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToByte(provider);

		/// <remarks />
		public char ToChar(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToChar(provider);


		/// <returns>Result as DateTime</returns>
		public DateTime ToDateTime() => Convert.ToDateTime(this.Value);

		/// <remarks />
		public DateTime ToDateTime(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToDateTime(provider);
		/// <remarks />
		public decimal ToDecimal(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToDecimal(provider);

		/// <remarks />
		public double ToDouble(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToDouble(provider);

		/// <remarks />
		public short ToInt16(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToInt16(provider);

		/// <remarks />
		public int ToInt32(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToInt32(provider);

		/// <remarks />
		public long ToInt64(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToInt64(provider);

		/// <remarks />
		public sbyte ToSByte(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToSByte(provider);

		/// <remarks />
		public float ToSingle(IFormatProvider provider) => ((IConvertible)ToDateTime()).ToSingle(provider);

		/// <returns>This Time as string</returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This Time as string</returns>
		public string ToString(IFormatProvider provider) => ToString("g",provider);

		/// <returns>This Time as string with requested <paramref name="format"/></returns><param name="format">'d", 'D', or 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This Time as string with requested <paramref name="format"/> and <paramref name="provider"/></returns><param name="format">E.g. 'd", 'D', or 'g'</param><param name="provider">E.g. 'da-DK' for danish</param>
		/// <exception cref="FormatException" />
		public string ToString(string format, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(format)) format="g"; if (provider==null) provider=CultureInfo.CurrentCulture;
			return format switch { "g" => Convert.ToDateTime(this.Value).ToString("HH:mm:ss"), "t" => Convert.ToDateTime(this.Value).ToString("t", provider), "T" => Convert.ToDateTime(this.Value).ToString("T", provider), _ => throw new FormatException(string.Format("The {0} format string is not supported.", format)), };
		}

		/// <remarks />
		public object ToType(Type conversionType, IFormatProvider provider) => ((IConvertible)this.Value).ToType(conversionType, provider);

		/// <remarks />
		public ushort ToUInt16(IFormatProvider provider) => ((IConvertible)this.Value).ToUInt16(provider);

		/// <remarks />
		public uint ToUInt32(IFormatProvider provider) => ((IConvertible)this.Value).ToUInt32(provider);

		/// <remarks />
		public ulong ToUInt64(IFormatProvider provider) => ((IConvertible)this.Value).ToUInt64(provider);

		#endregion

		#endregion

	}

}
