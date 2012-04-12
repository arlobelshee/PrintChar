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
			WonderCharacter testSubject = DefaultCharacter();
			testSubject.Name = name;
			testSubject.Gender = gender;
			return testSubject;
		}

		public static WonderCharacter DefaultCharacter(IDataFile backingStore)
		{
			return WonderCharacter.Create(new Model.SenseOfWonder(), backingStore);
		}

		public static WonderCharacter DefaultCharacter()
		{
			return WonderCharacter.CreateWithoutBackingDataStore(new Model.SenseOfWonder(), Data.Anything().Location);
		}

		public static WonderRulesCharacter EmptyRulesetCharacter(IDataFile backingStore)
		{
			return WonderRulesCharacter.Create(new RulesEditingSystem(), backingStore);
		}

		public static WonderRulesCharacter EmptyRulesetCharacter()
		{
			return WonderRulesCharacter.CreateWithoutBackingDataStore(new RulesEditingSystem(), Data.Anything().Location);
		}
	}
}
