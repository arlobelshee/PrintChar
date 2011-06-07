using System;
using System.Linq;
using JetBrains.Annotations;

namespace PrintChar
{
	public class EqualityFingerprint : IEquatable<EqualityFingerprint>
	{
		[NotNull] private readonly object[] _fields;

		public EqualityFingerprint([NotNull] params object[] fields)
		{
			_fields = fields;
		}

		public bool Equals(EqualityFingerprint other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return _fields.SequenceEqual(other._fields);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as EqualityFingerprint);
		}

		public override int GetHashCode()
		{
			return _fields.GetHashCode();
		}

		public static bool operator ==(EqualityFingerprint left, EqualityFingerprint right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(EqualityFingerprint left, EqualityFingerprint right)
		{
			return !Equals(left, right);
		}
	}
}