using System.Collections.Generic;
using JetBrains.Annotations;

namespace SenseOfWonder.Model
{
	public class RulesData
	{
		public RulesData()
		{
			Cards = new List<CardData>();
		}

		[NotNull]
		public List<CardData> Cards { get; set; }
	}
}
