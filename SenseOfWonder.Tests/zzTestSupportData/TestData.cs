using PluginApi.Model;
using SenseOfWonder.Model;

namespace SenseOfWonder.Tests.zzTestSupportData
{
	internal static class _TestData
	{
		public static WonderCharacter Character(string name, string gender, IDataFile backingStore)
		{
			WonderCharacter testSubject = DefaultCharacter(backingStore);
			testSubject.Name = name;
			testSubject.Gender = gender;
			return testSubject;
		}

		public static WonderCharacter Character(string name, string gender)
		{
			return Character(name, gender, Data.Anything());
		}

		public static WonderCharacter DefaultCharacter(IDataFile backingStore)
		{
			return WonderCharacter.Create(new SenseOfWonderSystem(), backingStore);
		}

		public static WonderCharacter DefaultCharacter()
		{
			return DefaultCharacter(Data.Anything());
		}
	}
}
