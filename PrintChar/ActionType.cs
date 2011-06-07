using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace PrintChar
{
	public class ActionType : IEquatable<ActionType>
	{
		private const string _ICON_STANDARD = "action_standard";
		private const string _ICON_MINOR = "action_minor";
		private const string _ICON_FREE = "action_free";
		private const string _ICON_IMMEDIATE = "action_immediate";
		private const string _ICON_NO_ACTION = "action_none";
		private const string _ICON_MOVE = "action_move";
		[NotNull] private readonly string _descriptor;

		private static readonly Dictionary<string, Func<ActionType>> _PARSE_TABLE = new Dictionary<string, Func<ActionType>>
		{
			{"standard action", Standard},
			{"move action", Move},
			{"minor action", Minor},
			{"free action", Free},
			{"immediate interrupt", Interrupt},
			{"immediate reaction", Reaction},
			{"opportunity action", Opportunity},
			{"no action", NoAction},
		};

		private ActionType([NotNull] string descriptor, [NotNull] string iconCode)
		{
			_descriptor = descriptor;
			IconCode = iconCode;
		}

		public bool Equals(ActionType other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other._descriptor, _descriptor) && Equals(other.IconCode, IconCode);
		}

		[NotNull]
		public string IconCode { get; private set; }

		[NotNull]
		public static ActionType Standard()
		{
			return new ActionType("Standard Action", _ICON_STANDARD);
		}

		[NotNull]
		public static ActionType Move()
		{
			return new ActionType("Move Action", _ICON_MOVE);
		}

		[NotNull]
		public static ActionType Minor()
		{
			return new ActionType("Minor Action", _ICON_MINOR);
		}

		[NotNull]
		public static ActionType Free()
		{
			return new ActionType("Free Action", _ICON_FREE);
		}

		[NotNull]
		public static ActionType Interrupt()
		{
			return new ActionType("Immediate Interrupt", _ICON_IMMEDIATE);
		}

		[NotNull]
		public static ActionType Reaction()
		{
			return new ActionType("Immediate Reaction", _ICON_IMMEDIATE);
		}

		[NotNull]
		public static ActionType Opportunity()
		{
			return new ActionType("Opportunity Action", _ICON_IMMEDIATE);
		}

		[NotNull]
		public static ActionType NoAction()
		{
			return new ActionType("No Action", _ICON_NO_ACTION);
		}

		[NotNull]
		public static ActionType For([NotNull] string unparsedActionValue)
		{
			Func<ActionType> builder;
			if (!_PARSE_TABLE.TryGetValue(unparsedActionValue.ToLower(), out builder))
				throw new NotImplementedException(string.Format("I do not know how to parse the action descriptor '{0}'.", unparsedActionValue));
			return builder();
		}

		public override string ToString()
		{
			return _descriptor;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as ActionType);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_descriptor.GetHashCode()*397) ^ IconCode.GetHashCode();
			}
		}

		public static bool operator ==(ActionType left, ActionType right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ActionType left, ActionType right)
		{
			return !Equals(left, right);
		}
	}
}
