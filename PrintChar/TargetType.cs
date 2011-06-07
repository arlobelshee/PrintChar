using System;
using JetBrains.Annotations;

namespace PrintChar
{
	public class TargetType : IEquatable<TargetType>
	{
		private const string _ICON_BLAST = "target_close_blast";
		private const string _ICON_BURST = "target_close_burst";
		private const string _ICON_RANGED = "target_ranged";
		private const string _ICON_MELEE = "target_melee";
		private const string _ICON_ANY_WEAPON = "target_any";
		private const string _ICON_PERSONAL = "target_personal";
		private const string _ICON_TOUCH = "target_touch";
		private const string _ICON_AREA = "target_area";
		private readonly int _area;
		[NotNull] private readonly string _format;
		private readonly int _range;

		private TargetType([NotNull] string format, [NotNull] string iconCode, int range, int area)
		{
			IconCode = iconCode;
			_format = format;
			_range = range;
			_area = area;
		}

		public bool Equals(TargetType other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return other._area == _area && Equals(other._format, _format) && other._range == _range;
		}

		[NotNull]
		public string IconCode { get; private set; }

		[NotNull]
		public static TargetType MeleeWeapon
		{
			get { return new TargetType("Melee Weapon", _ICON_MELEE, 0, 0); }
		}

		[NotNull]
		public static TargetType RangedWeapon
		{
			get { return new TargetType("Ranged Weapon", _ICON_RANGED, 0, 0); }
		}

		[NotNull]
		public static TargetType AnyWeapon
		{
			get { return new TargetType("Any Weapon", _ICON_ANY_WEAPON, 0, 0); }
		}

		[NotNull]
		public static TargetType Touch
		{
			get { return new TargetType("Touch", _ICON_TOUCH, 0, 0); }
		}

		[NotNull]
		public static TargetType Personal
		{
			get { return new TargetType("Personal", _ICON_PERSONAL, 0, 0); }
		}

		public override string ToString()
		{
			return string.Format(_format, _range, _area);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TargetType);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var result = _area;
				result = (result*397) ^ _format.GetHashCode();
				result = (result*397) ^ _range;
				return result;
			}
		}

		public static bool operator ==(TargetType left, TargetType right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(TargetType left, TargetType right)
		{
			return !Equals(left, right);
		}

		[NotNull]
		public static TargetType For([NotNull] string unparsedTargetValue)
		{
			unparsedTargetValue = unparsedTargetValue.ToLower();
			if (unparsedTargetValue == "personal")
				return Personal;
			if (unparsedTargetValue == "touch")
				return Touch;
			int range;
			int area;
			var actionParts = unparsedTargetValue.Split(new[] {' '}, 2);
			if (actionParts[0] == "area")
			{
				var details = actionParts[1].Split(new[] {' '});
				if (details.Length == 4 && details[0] == "burst" && details[2] == "within")
				{
					if (int.TryParse(details[1], out area) && int.TryParse(details[3], out range))
					{
						return Area(area, range);
					}
				}
			}
			if (actionParts[1] == "weapon")
			{
				if (actionParts[0] == "melee")
					return MeleeWeapon;
				if (actionParts[0] == "ranged")
					return RangedWeapon;
				if (actionParts[0] == "any")
					return AnyWeapon;
			}
			if (int.TryParse(actionParts[1], out range))
			{
				if (actionParts[0] == "melee")
					return Melee(range);
				if (actionParts[0] == "ranged")
					return Ranged(range);
			}
			if (actionParts[0] == "close")
			{
				var details = actionParts[1].Split(new[] {' '}, 2);
				if (int.TryParse(details[1], out area))
				{
					if (details[0] == "burst")
						return CloseBurst(area);
					if (details[0] == "blast")
						return CloseBlast(area);
				}
			}
			throw new NotImplementedException(string.Format("I do not know how to parse the target descriptor '{0}'.", unparsedTargetValue));
		}

		[NotNull]
		public static TargetType Melee(int range)
		{
			return new TargetType("Melee {0}", _ICON_MELEE, range, 0);
		}

		[NotNull]
		public static TargetType CloseBurst(int area)
		{
			return new TargetType("Close Burst {1}", _ICON_BURST, 0, area);
		}

		[NotNull]
		public static TargetType CloseBlast(int area)
		{
			return new TargetType("Close Blast {1}", _ICON_BLAST, 0, area);
		}

		[NotNull]
		public static TargetType Ranged(int range)
		{
			return new TargetType("Ranged {0}", _ICON_RANGED, range, 0);
		}

		[NotNull]
		public static TargetType Area(int area, int range)
		{
			return new TargetType("Burst {1} in {0}", _ICON_AREA, range, area);
		}
	}
}
