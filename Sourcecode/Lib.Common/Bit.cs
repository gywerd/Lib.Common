// Public Domain.See License.txt
using System;
using System.Globalization;

namespace Lib.Common.DataTypes
{
	/// <Summary>DataType</Summary>
	public class Bit : IFormattable
	{
		#region Fields
		private int value;

		#endregion

		#region Constructors
		/// <remarks />
		public Bit() { value=0; }

		/// <remarks />
		/// <param name="i">0 or 1</param>
		public Bit(int i) => this.value=Convert.ToInt32(Convert.ToBoolean(i));

		/// <remarks />
		/// <param name="s">'true' or 'false'</param>
		public Bit(string s) => this.value=Convert.ToInt32(Convert.ToBoolean(s));

		/// <remarks />
		/// <param name="bit" />
		public Bit(bool bit) => this.value=Convert.ToInt32(bit);

		#endregion

		#region Operators

		/// <returns><paramref name="i"/> as Bit</returns>
		public static implicit operator Bit(int i) => new Bit(i);

		/// <returns><paramref name="s"/> as Bit</returns>
		public static implicit operator Bit(string s) => new Bit(s);

		/// <returns><paramref name="b"/> as Bit</returns>
		public static implicit operator Bit(bool b) => new Bit(b);

		/// <returns><paramref name="bit"/> as integer</returns>
		public static implicit operator int(Bit bit) => bit.ToInt32();

		/// <returns><paramref name="bit"/> as a string</returns>
		public static implicit operator string(Bit bit) => bit.ToString();

		/// <returns><paramref name="bit"/> as a bool</returns>
		public static implicit operator bool(Bit bit) => bit.ToBoolean();

		#endregion

		#region Properties
		/// <remarks />
		public int Value { get => value; set => this.value = Convert.ToInt32(Convert.ToBoolean(value)); }

		#endregion

		#region Methods
		/// <returns>This <see cref="Bit"/> as an <see cref="bool"/></returns>
		public bool ToBoolean() => Convert.ToBoolean(value);

		/// <returns>This <see cref="Bit"/> as an <see cref="int"/></returns>
		public int ToInt32() => value;

		/// <returns>This <see cref="Bit"/> as <see cref="string"/></returns>
		public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

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
				case "g": return this.Value.ToString();
				case "G":
					if (value.Equals(1))
					{
						return "true";
					}
					else return "false";
				default: throw new FormatException(string.Format("The {0} format string is not supported.", format));
			}

		}

		#endregion

	}
}
