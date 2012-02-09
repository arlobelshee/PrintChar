using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	public class TestPowers
	{
		private TestPowers(int id, [NotNull] string name)
		{
			Name = name;
			PowerId = id;
		}

		[NotNull]
		public string Name { get; private set; }

		public int PowerId { get; private set; }

		public Power LoadFrom(IDnd4ERepository repository)
		{
			return repository.PowerDetails(new[] {PowerId}).Result[Name];
		}

		public static TestPowers PowerSimple
		{
			get { return new TestPowers(7448, "Centered Flurry of Blows"); }
		}

		public static TestPowers PowerWithAugments
		{
			get { return new TestPowers(12946, "Unsteadying Rebuke"); }
		}

		public static TestPowers PowerWithSubpower
		{
			get { return new TestPowers(4020, "Hypnotic Pattern"); }
		}

		public static TestPowers Subpower
		{
			get { return new TestPowers(7343, "Hypnotic Pattern Attack"); }
		}

		public static implicit operator int([NotNull] TestPowers pow)
		{
			return pow.PowerId;
		}
	}
}
