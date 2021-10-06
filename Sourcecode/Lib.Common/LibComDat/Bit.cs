// Public Domain.See License.txt
using System;
using System.Globalization;

namespace Lib.Common.DataTypes
{
	/// <Summary>DataType</Summary>
	public class bit : IFormattable, IComparable
	{
		#region Fields
		private byte m_value;

		#endregion

		#region Constructors
		/// <remarks />
		public bit() { m_value=0; }

		/// <remarks />
		/// <param name="i">0 or 1</param>
		private bit(byte i) { this.m_value=Convert.ToByte(Convert.ToBoolean(i)); }

		/// <remarks />
		/// <param name="s">'true' or 'false'</param>
		private bit(string s) { this.m_value=Convert.ToByte(Convert.ToBoolean(s)); }

		/// <remarks />
		/// <param name="b" />
		private bit(bool b) => this.m_value=Convert.ToByte(b);

		/// <remarks />
		/// <param name="obj" />
		private bit(object obj) { if (byte.TryParse(obj.ToString(), out byte i)) { m_value=i; Validate(); } else if (bool.TryParse(obj.ToString(), out bool b)) { m_value=Convert.ToByte(b); Validate(); } else if (obj.ToString().ToLower().Equals("true")||obj.ToString().ToLower().Equals("true")) { m_value=Convert.ToByte(Convert.ToBoolean(obj.ToString())); Validate(); } else m_value=0;}

		#endregion

		#region Boolean Operators

		/// <returns>Result as bool</returns>
		public static bool operator ==(bit a, bit b) => a.Equals(b);

		/// <returns>Result as bool</returns>
		public static bool operator !=(bit a, bit b) => !a.Equals(b);

		/// <returns>Result as bool</returns>
		public static bool operator <=(bit a, bit b) { if (a>b) return false; else return true; }

		/// <returns>Result as bool</returns>
		public static bool operator >=(bit a, bit b) { if (a<b) return false; else return true; }

		/// <returns>Result as bool</returns>
		public static bool operator <(bit a, bit b) { if (a.CompareTo(b)>-1) return true; else return false; }

		/// <returns>Result as bool</returns>
		public static bool operator >(bit a, bit b) { if (a.CompareTo(b)<1) return true; else return false; }

		#endregion

		#region Conversion Operators

		/// <returns><paramref name="b"/> as Bit</returns>
		public static explicit operator bit(bool b) => new(b);

		/// <returns><paramref name="s"/> as Bit</returns>
		public static explicit operator bit(int s) => new((byte)s);

		/// <returns><paramref name="s"/> as Bit</returns>
		public static explicit operator bit(string s) => new(s);

		/// <returns><paramref name="b"/> as a bool</returns>
		public static explicit operator bool(bit b) => b.ToBoolean();

		/// <returns><paramref name="b"/> as a bool</returns>
		public static explicit operator int(bit b) => b.ToInt32();

		/// <returns><paramref name="b"/> as a string</returns>
		public static explicit operator string(bit b) => b.ToString();

		/// <returns><paramref name="b"/> as Bit</returns>
		public static implicit operator bit(byte b) => new(b);

		/// <returns><paramref name="b"/> as integer</returns>
		public static implicit operator byte(bit b) => b.m_value;

		#endregion

		#region Methods

		/// <remarks />
		public int CompareTo(object obj) { Validate(); return this.ToBoolean().CompareTo((bool)obj); }

		#region Equals

		/// <summary>Compares this Bit to <paramref name="b"/></summary><param name="b" /><returns>Result as bool</returns>
		private bool Equals(bit b) { Validate(); b.Validate(); return this.m_value.Equals(b.m_value); }

		/// <summary>Compares this Bit to <paramref name="obj"/></summary><param name="obj" /><returns>Result as bool</returns>
		public override bool Equals(object obj) { Validate(); return this.ToBoolean().Equals((bool)obj); }

		#endregion

		/// <remarks />
		public override int GetHashCode() => this.m_value.GetHashCode();

		#region To something

		/// <returns>This bit as bool</returns>
		private bool ToBoolean() => Convert.ToBoolean(m_value);

		/// <returns>This bit as int</returns>
		private int ToInt32() => Convert.ToInt32(m_value);

		/// <returns>This bit as string</returns>
		public override string ToString() => ToString("g", CultureInfo.CurrentCulture);

		/// <returns>This bit as string with requested <paramref name="format"/></returns>
		/// <param name="format">e.g. 'g", or 'G'</param>
		public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

		/// <returns>This bit as string with requested <paramref name="format"/> and <paramref name="provider"/></returns>
		/// <param name="format">e.g. 'g", or 'G'</param>
		/// <param name="provider">e.g. 'da-DK' for danish</param>
		public string ToString(string format, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(format)) format="G";
			if (provider==null) provider=CultureInfo.CurrentCulture;

			switch (format)
			{
				case "g": return this.m_value.ToString();
				case "G": if (m_value.Equals(1)) return "true"; else return "false";
				default: throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

		#endregion

		/// <remarks />
		public void Validate() { if (m_value>1) m_value=1; else if (m_value<0) m_value=0; }
		 

		#endregion

	}
}
