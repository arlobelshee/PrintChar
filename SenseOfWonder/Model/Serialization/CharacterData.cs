using JetBrains.Annotations;

namespace SenseOfWonder.Model.Serialization
{
	public class CharacterData
	{
		[CanBeNull]
		public string Gender { get; set; }

		[CanBeNull]
		public string Name { get; set; }
	}
}
