using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace PluginApi.Model
{
	public class EqualityFingerprint : IEquatable<EqualityFingerprint>
	{
		[NotNull] private object[] _fields;
		[NotNull] private IEnumerable<object>[] _sequenceFields = new IEnumerable<object>[] {};

		public EqualityFingerprint([NotNull] params object[] fields)
		{
			_fields = fields;
		}

		public void Add([NotNull] params object[] fields)
		{
			_fields = _fields.Concat(fields).ToArray();
		}

		public void AddSequences(params IEnumerable<object>[] fields)
		{
			_sequenceFields = _sequenceFields.Concat(fields).ToArray();
		}

		public bool Equals(EqualityFingerprint other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return _fields.SequenceEqual(other._fields)
				&& _sequenceFields.Zip(other._sequenceFields,
					(l, r) => l.SequenceEqual(r))
					.All(t => t);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as EqualityFingerprint);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var result = _fields.GetHashCode();
				return _sequenceFields.Aggregate(result,
					(current, f) => (current*397) ^ f.GetHashCode());
			}
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
