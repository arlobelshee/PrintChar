using JetBrains.Annotations;

namespace SenseOfWonder.Model.Impl
{
	public class WonderCharData
	{
		public static WonderCharData From(WonderCharacter who)
		{
			return new WonderCharData
			{
				Name = who.Name,
				Gender = who.Gender
			};
		}

		[CanBeNull]
		public string Gender { get; set; }

		[CanBeNull]
		public string Name { get; set; }

		public void UpdateCharacter(WonderCharacter who)
		{
			who.Name = Name ?? string.Empty;
			who.Gender = Gender ?? string.Empty;
		}
	}
}
