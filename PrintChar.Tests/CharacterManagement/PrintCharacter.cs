using NUnit.Framework;
using PrintChar.Tests.zzTestSupportData;
using FluentAssertions;

namespace PrintChar.Tests.CharacterManagement
{
	[TestFixture]
	public class PrintCharacter
	{
		[Test]
		public void ShouldDisablePrintingIfNoCharacterIsSelected()
		{
			new AllGameSystemsViewModel(new _ReadOnlyGameSystem()).IsValid.Should().BeFalse();
		}

		[Test]
		public void ShouldAllowPrintingIfCharacterIsSelected()
		{
			new DesignTimeGameSystems().IsValid.Should().BeTrue();
		}
	}
}
