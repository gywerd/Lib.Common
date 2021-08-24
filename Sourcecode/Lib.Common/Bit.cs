// Public Domain.See License.txt
using System;
using System.Globalization;

namespace Lib.Common.DataTypes
{
	/// <Summary>DataType</Summary>
	public class Bit : IFormattable, IComparable, IComparable<Bit>, IComparable<bool>, IComparable<int>, IComparable<string>
	{
		#region Fields
		private int value;

		#endregion

		#region Constructors
		/// <remarks />
		public Bit() { value=0; }

		/// <remarks />
		/// <param name="i">0 or 1</param>
		private Bit(int i) => this.value=Convert.ToInt32(Convert.ToBoolean(i));

		/// <remarks />
		/// <param name="s">'true' or 'false'</param>
		private Bit(string s) => this.value=Convert.ToInt32(Convert.ToBoolean(s));

		/// <remarks />
		/// <param name="b" />
		private Bit(bool b) => this.value=Convert.ToInt32(b);

		#endregion

		#region Operators

		/// <returns>Result as bool</returns>
		public static bool operator == (Bit a, Bit b) => a.Equals(b);

		/// <returns>Result as bool</returns>
		public static bool operator != (Bit a, Bit b) => !a.Equals(b);

		/// <returns><paramref name="b"/> as Bit</returns>
		public static implicit operator Bit(bool b) => new(b);

		/// <returns><paramref name="i"/> as Bit</returns>
		public static implicit operator Bit(int i) => new(i);

		/// <returns><paramref name="s"/> as Bit</returns>
		public static implicit operator Bit(string s) => new(s);

		/// <returns><paramref name="bit"/> as a bool</returns>
		public static implicit operator bool(Bit bit) => bit.ToBoolean();

		/// <returns><paramref name="bit"/> as integer</returns>
		public static implicit operator int(Bit bit) => bit.value;

		/// <returns><paramref name="bit"/> as a string</returns>
		public static implicit operator string(Bit bit) => bit.ToString();

		#endregion

		#region Methods

		#region Compare To

		/// <remarks />
		public int CompareTo(Bit other) => this.ToBoolean().CompareTo(other.ToBoolean());

		/// <remarks />
		public int CompareTo(bool other) => this.ToBoolean().CompareTo(other);

		/// <remarks />
		public int CompareTo(int other) => this.ToBoolean().CompareTo(Convert.ToBoolean(other));

		/// <remarks />
		public int CompareTo(object obj) => this.ToBoolean().CompareTo(Convert.ToBoolean(obj));

		/// <remarks />
		public int CompareTo(string other)=> this.ToBoolean().CompareTo(Convert.ToBoolean(other));

		#endregion

		#region Equals

		/// <summary>Compares this Bit to <paramref name="bit"/></summary>
		/// <param name="bit" />
		/// <returns>Result as bool</returns>
		private bool Equals(Bit bit)
		{
			if(this==null&&bit==null) return true;
			else if(this!=null&&bit==null) return false;
			else if(this==null&&bit!=null) return false;
			else return this.value.Equals(bit.value);
		}

		/// <summary>Compares this Bit to <paramref name="obj"/></summary>
		/// <param name="obj" />
		/// <returns>Result as bool</returns>
		public override bool Equals(object obj) => this.Equals((Bit)obj);

		#endregion

		#region To something

		/// <remarks />
		public override int GetHashCode() => value.GetHashCode();

		/// <returns>This <see cref="Bit"/> as an <see cref="bool"/></returns>
		private bool ToBoolean() => Convert.ToBoolean(value);

		/// <returns>This <see cref="Bit"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Bit"/> as <see cref="string"/> with requested <paramref name="format"/></returns>
		/// <param name="format">Date format as a <see cref="string"/> - e.g. 'd", 'D', and 'g'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This <see cref="Bit"/> as <see cref="string"/> with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">Day format as a <see cref="string"/> - e.g. 'g", and 'G'</param>
		/// <param name="provider">Format provider as a <see cref="string"/> - e.g. 'da-DK' for danish</param>
		public string ToString(string format, IFormatProvider provider)
		{
			if (this==null) return "null";
			if (string.IsNullOrEmpty(format)) format="G";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			switch (format)
			{
				case "g": return this.value.ToString();
				case "G": if (value.Equals(1)) return "true"; else return "false";
				default: throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

		#endregion

		#endregion

	}
}
