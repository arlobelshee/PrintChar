using System.Collections.Generic;

namespace SenseOfWonder.Model
{
	public class WonderRulesData
	{
		public WonderRulesData()
		{
			Cards = new List<WonderCard>();
		}

		public List<WonderCard> Cards { get; set; }

		public static WonderRulesData From(WonderRulesCharacter who)
		{
			return new WonderRulesData
			{
				Cards = who.CardData
			};
		}
	}
}
