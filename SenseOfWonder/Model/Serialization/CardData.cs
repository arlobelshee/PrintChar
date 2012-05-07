using JetBrains.Annotations;

namespace SenseOfWonder.Model.Serialization
{
	public class CardData
	{
		[CanBeNull]
		public string Name { get; set; }
	}

	public class CardDataDesignData : CardData
	{
		public CardDataDesignData()
		{
			Name = "Right Here, Right Now";
		}
	}
}
