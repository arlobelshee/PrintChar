using JetBrains.Annotations;

namespace SenseOfWonder.Model.Impl
{
	public class WonderCharData
	{
		[CanBeNull]
		public string Gender { get; set; }

		[CanBeNull]
		public string Name { get; set; }
	}
}
