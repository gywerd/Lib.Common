// Public Domain.See License.txt
using System;
using System.Globalization;

namespace Lib.Common.DataTypes
{
	/// <Summary>DataType</Summary>
	public class Bit : IFormattable
	{
		#region Fields
		private byte value;

		#endregion

		#region Constructors
		/// <remarks />
		public Bit() { value=0; }

		/// <remarks />
		/// <param name="bit">0 or 1</param>
		/// <exception cref="ArgumentInvalidException" />
		private Bit(int bit)
		{
			if (bit<0||bit>1) throw new ArgumentInvalidException(nameof(bit), bit, nameof(bit)+Error.InvBit);

			this.value=(byte)bit;

		}

		/// <remarks />
		/// <param name="bit">'true' or 'false'</param>
		/// <exception cref="ArgumentInvalidException" />
		/// <exception cref="ArgumentNullOrWhiteSpaceException" />
		private Bit(string bit)
		{
			if (!string.IsNullOrWhiteSpace(bit)) throw new ArgumentNullOrWhiteSpaceException(nameof(bit), bit, nameof(bit)+Error.CantBeNullWhSp);
			if (!bit.ToLower().Equals("true")&&!bit.ToLower().Equals("false")) throw new ArgumentInvalidException(nameof(bit), bit, Error.InvBit);

			if (bit.ToLower().Equals("true")) this.value=1;
			else this.value=0;

		}

		/// <remarks />
		/// <param name="bit" />
		private Bit(bool bit) => this.value=Convert.ToByte(bit);

		#endregion

		#region Operators

		/// <summary>Sets <see cref="Bit"/> using data from an <see cref="int"/></summary>
		/// <param name="bit">An integer within the range of [0;1]</param>
		public static implicit operator Bit(int bit) => new Bit(bit);

		/// <summary>Sets <see cref="Bit"/> using data from a <see cref="string"/></summary>
		public static implicit operator Bit(string bit) => new Bit(bit);

		/// <summary>Sets <see cref="Bit"/> using data from a <see cref="string"/></summary>
		public static implicit operator Bit(bool bit) => new Bit(bit);

		/// <returns>Value of <see cref="Bit"/> as a <see cref="int"/></returns>
		public static implicit operator int(Bit bit) => bit.ToInt32();

		/// <returns> Value of <see cref="Bit"/> as a <see cref="string"/></returns>
		public static implicit operator string(Bit bit) => bit.ToString();

		/// <returns> Value of <see cref="Bit"/> as a <see cref="string"/></returns>
		public static implicit operator bool(Bit bit) => bit.ToBoolean();

		#endregion

		#region Properties
		/// <remarks />
		public int Value { get => value; set => this.value = (byte)value; }

		#endregion

		#region Methods
		/// <returns>This <see cref="Bit"/> as an <see cref="bool"/></returns>
		public bool ToBoolean()
		{
			if (this==null) throw new NullReferenceException();

			return Convert.ToBoolean(value);

		}

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
