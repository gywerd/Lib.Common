// Public Domain.See License.txt
using System;

namespace Lib.Common
{
	/// <Summary>Common Exception</Summary>
	public class ArgumentInvalidException : ExpressionException
	{
		#region Constructors
		/// <remarks />
		public ArgumentInvalidException() { }

		/// <remarks />
		public ArgumentInvalidException(string? message) : base(message) { }

		/// <remarks />
		public ArgumentInvalidException(string? message, Exception? innerException) : base(message, innerException) { }

		/// <remarks />
		public ArgumentInvalidException(string? paramName, object? actualValue, string? message) : base(paramName, actualValue, message) { }

		/// <remarks />
		public ArgumentInvalidException(string? paramName, string? message) : base(paramName, message ) { }

		#endregion

		#region Properties
		/// <remarks />
		public string ParameterName { get => ExpressionName; }

		#endregion

		#region Methods
		/// <remarks />
		public override string ToErrorString()
		{
			if (this==null) throw new NullReferenceException();

			if (this.ExpressionName!=null&&this.ActualValue!=null) return Environment.NewLine+"- An ArgumentInvalidException occurred ("+this.ExpressionName+": "+this.ActualValue.ToString()+"):"+Environment.NewLine+this+Environment.NewLine;
			else if (this.ExpressionName!=null&&this.ActualValue==null) return Environment.NewLine+"- An ArgumentInvalidException occurred ("+this.ExpressionName+"):"+Environment.NewLine+this+Environment.NewLine;
			else return Environment.NewLine+"- An ArgumentInvalidException error occurred:"+Environment.NewLine+this+Environment.NewLine;

		}

		#endregion

	}

	/// <Summary>Common Exception</Summary>
	public class ArgumentNullOrWhiteSpaceException : ExpressionException
	{
		#region Constructors
		/// <remarks />
		public ArgumentNullOrWhiteSpaceException() { }

		/// <remarks />
		public ArgumentNullOrWhiteSpaceException(string? message) : base(message) { }

		/// <remarks />
		public ArgumentNullOrWhiteSpaceException(string? message, Exception? innerException) : base(message, innerException) { }

		/// <remarks />
		public ArgumentNullOrWhiteSpaceException(string? paramName, object? actualValue, string? message) : base(paramName, actualValue, message) { }

		/// <remarks />
		public ArgumentNullOrWhiteSpaceException(string? paramName, string? message) : base(paramName, message ) { }

		#endregion

		#region Properties
		/// <remarks />
		public string ParameterName { get => ExpressionName; }

		#endregion

		#region Methods
		/// <remarks />
		public override string ToErrorString()
		{
			if (this==null) throw new NullReferenceException();

			if (this.ExpressionName!=null&&this.ActualValue!=null) return Environment.NewLine+"- An ArgumentNullOrWhiteSpaceException occurred ("+this.ExpressionName+": "+this.ActualValue.ToString()+"):"+Environment.NewLine+this+Environment.NewLine;
			else if (this.ExpressionName!=null&&this.ActualValue==null) return Environment.NewLine+"- An ArgumentNullOrWhiteSpaceException occurred ("+this.ExpressionName+"):"+Environment.NewLine+this+Environment.NewLine;
			else return Environment.NewLine+"- An ArgumentNullOrWhiteSpaceException error occurred:"+Environment.NewLine+this+Environment.NewLine;

		}

		#endregion

	}

	/// <Summary>Common Exception</Summary>
	public class ExpressionException : Exception
	{
		#region Constructors
		/// <remarks />
		public ExpressionException() { }

		/// <remarks />
		public ExpressionException(string? message) : base(message) { }

		/// <remarks />
		public ExpressionException(string? message, Exception? innerException) : base(message, innerException) { }

		/// <remarks />
		public ExpressionException(string? exprName, object? actualValue, string? message) : base(message)
		{
			if (!string.IsNullOrWhiteSpace(exprName))
			{
				this.ExpressionName=exprName;
			}

			if (actualValue == null)
			{
				this.ActualValue=actualValue;
			}

		}

		/// <remarks />
		public ExpressionException(string? exprName, string? message) : base(message) 
			{
			if (!string.IsNullOrWhiteSpace(exprName))
			{
				this.ExpressionName=exprName;
			}

		}

		#endregion

		#region Properties
		/// <remarks />
		public object ActualValue { get; init; }

		/// <remarks />
		protected string ExpressionName { get; init; }

		#endregion

		#region Methods
		/// <remarks />
		public static string ToErrorString(ExpressionException ex)
		{
			if (ex.ExpressionName!=null&&ex.ActualValue!=null)
			{
				return Environment.NewLine+"- An ExpressionException occurred ("+ex.ExpressionName+": "+ex.ActualValue.ToString()+"):"+Environment.NewLine+ex+Environment.NewLine;
			}
			else if (ex.ExpressionName!=null&&ex.ActualValue==null)
			{
				return Environment.NewLine+"- An ExpressionException occurred ("+ex.ExpressionName+"):"+Environment.NewLine+ex+Environment.NewLine;
			}
			else
			{
				return Environment.NewLine+"- An ExpressionException error occurred:"+Environment.NewLine+ex+Environment.NewLine;

			}
		}

		/// <remarks />
		public static string ToErrorString(Exception ex)
		{
			return Environment.NewLine+"- An "+ex.GetType().ToString()+" error occurred:"+Environment.NewLine+ex+Environment.NewLine;
		}

		/// <remarks />
		public virtual string ToErrorString()
		{
			if (this==null) throw new NullReferenceException();

			if (this.ExpressionName!=null&&this.ActualValue!=null) return Environment.NewLine+"- An ExpressionException occurred ("+this.ExpressionName+": "+this.ActualValue.ToString()+"):"+Environment.NewLine+this+Environment.NewLine;
			else if (this.ExpressionName!=null&&this.ActualValue==null) return Environment.NewLine+"- An ExpressionException occurred ("+this.ExpressionName+"):"+Environment.NewLine+this+Environment.NewLine;
			else return Environment.NewLine+"- An ExpressionException error occurred:"+Environment.NewLine+this+Environment.NewLine;

		}

		#endregion

	}

	/// <Summary>Common Exception</Summary>
	public class InvalidRefException : ExpressionException
	{
		#region Constructors
		/// <remarks />
		public InvalidRefException() { }

		/// <remarks />
		public InvalidRefException(string? message) : base(message) { }

		/// <remarks />
		public InvalidRefException(string? message, Exception? innerException) : base(message, innerException) { }

		/// <remarks />
		public InvalidRefException(string? refName, object? actualValue, string? message) : base(refName, actualValue, message) { }

		/// <remarks />
		public InvalidRefException(string? refName, string? message) : base(refName, message) { }

		#endregion

		#region Properties
		/// <remarks />
		public string ReferenceName { get => ExpressionName; }

		#endregion

		#region Methods
		/// <remarks />
		public override string ToErrorString()
		{
			if (this==null) throw new NullReferenceException();

			if (this.ExpressionName!=null&&this.ActualValue!=null) return Environment.NewLine+"- An InvalidRefException occurred ("+this.ExpressionName+": "+this.ActualValue.ToString()+"):"+Environment.NewLine+this+Environment.NewLine;
			else if (this.ExpressionName!=null&&this.ActualValue==null) return Environment.NewLine+"- An InvalidRefException occurred ("+this.ExpressionName+"):"+Environment.NewLine+this+Environment.NewLine;
			else return Environment.NewLine+"- An InvalidRefException error occurred:"+Environment.NewLine+this+Environment.NewLine;

		}

		#endregion

	}

	/// <Summary>Common Exception</Summary>
	public class NullRefException : ExpressionException
	{
		#region Constructors
		/// <remarks />
		public NullRefException() { }

		/// <remarks />
		public NullRefException(string? message) : base(message) { }

		/// <remarks />
		public NullRefException(string? message, Exception? innerException) : base(message, innerException) { }

		/// <remarks />
		public NullRefException(string? refName, string? message) : base(refName, message) { }

		#endregion

		#region Properties
		/// <remarks />
		public string ReferenceName { get => ExpressionName; }

		#endregion

		#region Methods
		/// <remarks />
		public override string ToErrorString()
		{
			if (this==null) throw new NullReferenceException();

			if(this.ExpressionName!=null) return Environment.NewLine+"- A NullReferenceException occurred ("+this.ExpressionName+"):"+Environment.NewLine+this+Environment.NewLine;
			else return Environment.NewLine+"- A NullReferenceException error occurred:"+Environment.NewLine+this+Environment.NewLine;

		}

		#endregion

	}

	/// <Summary>Common Exception</Summary>
	public class NullOrWhiteSpaceRefException : ExpressionException
	{
		#region Constructors
		/// <remarks />
		public NullOrWhiteSpaceRefException() { }

		/// <remarks />
		public NullOrWhiteSpaceRefException(string? message) : base(message) { }

		/// <remarks />
		public NullOrWhiteSpaceRefException(string? message, Exception? innerException) : base(message, innerException) { }

		/// <remarks />
		public NullOrWhiteSpaceRefException(string? refName, object? actualValue, string? message) : base(refName, actualValue, message) { }

		/// <remarks />
		public NullOrWhiteSpaceRefException(string? refName, string? message) : base(refName, message) { }

		#endregion

		#region Properties
		/// <remarks />
		public string ReferenceName { get => ExpressionName; }

		#endregion

		#region Methods
		/// <remarks />
		public override string ToErrorString()
		{
			if (this==null) throw new NullReferenceException();

			if (this.ExpressionName!=null&&this.ActualValue!=null) return Environment.NewLine+"- A NullOrWhiteSpaceRefException occurred ("+this.ExpressionName+": "+this.ActualValue.ToString()+"):"+Environment.NewLine+this+Environment.NewLine;
			else if (this.ExpressionName!=null&&this.ActualValue==null) return Environment.NewLine+"- A NullOrWhiteSpaceRefException occurred ("+this.ExpressionName+"):"+Environment.NewLine+this+Environment.NewLine;
			else return Environment.NewLine+"- A NullOrWhiteSpaceRefException error occurred:"+Environment.NewLine+this+Environment.NewLine;

		}

		#endregion

	}

	/// <Summary>Common Exception</Summary>
	public class SyntaxRefException : ExpressionException
	{
		#region Constructors
		/// <remarks />
		public SyntaxRefException() { }

		/// <remarks />
		public SyntaxRefException(string? message) : base(message) { }

		/// <remarks />
		public SyntaxRefException(string? message, Exception? innerException) : base(message, innerException) { }

		/// <remarks />
		public SyntaxRefException(string? refName, object? actualValue, string? message) : base(refName, actualValue, message) { }

		/// <remarks />
		public SyntaxRefException(string? exprName, string? message) : base(exprName, message) { }

		#endregion

		#region Properties
		/// <remarks />
		public string ReferenceName { get => ExpressionName; }

		#endregion

		#region Methods
		/// <remarks />
		public override string ToErrorString()
		{
			if (this==null) throw new NullReferenceException();

			if (this.ExpressionName!=null&&this.ActualValue!=null) return Environment.NewLine+"- A SyntaxRefException occurred ("+this.ExpressionName+": "+this.ActualValue.ToString()+"):"+Environment.NewLine+this+Environment.NewLine;
			else if (this.ExpressionName!=null&&this.ActualValue==null) return Environment.NewLine+"- A SyntaxRefException occurred ("+this.ExpressionName+"):"+Environment.NewLine+this+Environment.NewLine;
			else return Environment.NewLine+"- A SyntaxRefException error occurred:"+Environment.NewLine+this+Environment.NewLine;

		}

		#endregion

	}

	/// <remarks />
	public class UnknownRefException : ExpressionException
	{
		#region Constructors
		/// <remarks />
		public UnknownRefException() { }

		/// <remarks />
		public UnknownRefException(string? message) : base(message) { }

		/// <remarks />
		public UnknownRefException(string? message, Exception? innerException) : base(message, innerException) { }

		/// <remarks />
		public UnknownRefException(string? refName, string? message) : base(refName, message) { }

		#endregion

		#region Properties
		/// <remarks />
		public string ReferenceName { get => ExpressionName; }

		#endregion

		#region Methods
		/// <remarks />
		public override string ToErrorString()
		{
			if (this==null) throw new NullReferenceException();

			if (this.ExpressionName!=null&&this.ActualValue!=null) return Environment.NewLine+"- An UnknownRefException occurred ("+this.ExpressionName+": "+this.ActualValue.ToString()+"):"+Environment.NewLine+this+Environment.NewLine;
			else if (this.ExpressionName!=null&&this.ActualValue==null) return Environment.NewLine+"- An UnknownRefException occurred ("+this.ExpressionName+"):"+Environment.NewLine+this+Environment.NewLine;
			else return Environment.NewLine+"- An UnknownRefException error occurred:"+Environment.NewLine+this+Environment.NewLine;

		}

		#endregion

	}

}

