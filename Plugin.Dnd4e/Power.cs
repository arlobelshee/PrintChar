using System;
using JetBrains.Annotations;
using WotcOnlineDataRepository;

namespace PrintChar
{
	public class Power
	{
		public enum Usage
		{
			AtWill,
			Encounter,
			Daily,
			Once,
			HealingSurge,
		}

		[NotNull] private string _name = string.Empty;
		private WotcOnlineDataRepository.Power _onlineData;

		public Power()
		{
			Refresh = Usage.Once;
		}

		[NotNull]
		public string Name
		{
			get { return _name; }
			set
			{
				if (value == null)
					throw new ArgumentException("Power name is not allowed to be null. Use string.Empty instead.", "Name");
				_name = value;
			}
		}

		public bool Hidden { get; set; }
		public Usage Refresh { get; set; }
		public TargetType Target { get; set; }
		public ActionType Action { get; set; }

		[CanBeNull]
		public int? PowerId { get; set; }

		[CanBeNull]
		public WotcOnlineDataRepository.Power OnlineData
		{
			get { return _onlineData; }
			set
			{
				_onlineData = value;
				if (OnlineDataArrived != null)
				{
					OnlineDataArrived();
				}
			}
		}

		public event Action OnlineDataArrived;

		public override string ToString()
		{
			return string.Format("Power({0}, {1}, {2}, {3}, {4})", _name, Refresh, Target, Action, PowerId);
		}

		public bool Equals(Power other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other._name, _name) && Equals(other.Refresh, Refresh) && Equals(other.Target, Target) && Equals(other.Action, Action)
				&& Equals(other.PowerId, PowerId);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Power);
		}

		public override int GetHashCode()
		{
			var result = _name.GetHashCode();
			result = (result*397) ^ Refresh.GetHashCode();
			result = (result*397) ^ Target.GetHashCode();
			result = (result*397) ^ Action.GetHashCode();
			result = (result*397) ^ (PowerId ?? -1).GetHashCode();
			return result;
		}
	}
}
