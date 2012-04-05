using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using PluginApi.Model;
using SenseOfWonder.Model;

namespace SenseOfWonder.Tests.CharactersPersistInFiles
{
	[TestFixture, UseReporter(typeof(NUnitReporter))]
	public class WriteCharacterToFile
	{
		[Test]
		public void ResultingFileShouldContainAllCharacterDataForSimpleCharacter()
		{
			var backingStore = Data.EmptyAt(@"arbitrary.wonder");
			var testSubject = new WonderCharacter(new SenseOfWonderSystem(), backingStore)
			{
				Name = "Bob",
				Gender = "Male"
			};
			Approvals.Verify(backingStore.Contents + "\r\n");
		}
	}
}
