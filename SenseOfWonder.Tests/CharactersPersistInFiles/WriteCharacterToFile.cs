using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using PluginApi.Model;
using SenseOfWonder.Model;
using SenseOfWonder.Tests.zzTestSupportData;

namespace SenseOfWonder.Tests.CharactersPersistInFiles
{
	[TestFixture, UseReporter(typeof(DiffReporter))]
	public class WriteCharacterToFile
	{
		[Test]
		public void ResultingFileShouldContainAllCharacterDataForSimpleCharacter()
		{
			var dataFile = Data.EmptyAt(@"arbitrary.wonder");
			var testSubject = _TestData.DefaultCharacter(dataFile);
			testSubject.Name = "Bob";
			testSubject.Gender = "Male";
			Approvals.Verify(dataFile.Contents);
		}
	}
}
