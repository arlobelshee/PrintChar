namespace SenseOfWonder.Model.Impl
{
	public class WonderCharData
	{
		public static WonderCharData From(WonderCharacter who)
		{
			return new WonderCharData()
			{
				Name = who.Name,
				Gender = who.Gender
			};
		}

		public string Gender { get; set; }
		public string Name { get; set; }
	}
}