using System.Collections.Generic;
using JetBrains.Annotations;

namespace SenseOfWonder.Model.Serialization
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
