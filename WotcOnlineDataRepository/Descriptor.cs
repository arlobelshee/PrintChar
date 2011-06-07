using System;
using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	public class Descriptor : IEquatable<Descriptor>
	{
		public Descriptor([NotNull] string label, [NotNull] string details)
		{
			Label = label;
			Details = details;
		}

		public bool Equals(Descriptor other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other.Label, Label) && Equals(other.Details, Details);
		}

		[NotNull]
		public string Label { get; private set; }

		[NotNull]
		public string Details { get; private set; }

		public override string ToString()
		{
			return string.Format("<b>{0}</b>: {1}", Label, Details);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Descriptor);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Label.GetHashCode()*397) ^ Details.GetHashCode();
			}
		}

		public static bool operator ==(Descriptor left, Descriptor right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Descriptor left, Descriptor right)
		{
			return !Equals(left, right);
		}
	}
}