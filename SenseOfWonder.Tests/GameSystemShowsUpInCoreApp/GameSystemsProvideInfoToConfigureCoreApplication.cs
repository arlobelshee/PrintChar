using System;
using FluentAssertions;
using NUnit.Framework;
using SenseOfWonder.Model;
using SenseOfWonder.Views;

namespace SenseOfWonder.Tests.GameSystemShowsUpInCoreApp
{
	[TestFixture]
	public class GameSystemsProvideInfoToConfigureCoreApplication
	{
		[Test]
		public void ShouldAllowCharacterCreationAndEditing()
		{
			new SenseOfWonderSystem().IsReadOnly.Should().BeFalse();
		}

		[Test]
		public void ShouldAllowCardEditingButNotCreation()
		{
			new SenseOfWonderCards().IsReadOnly.Should().BeTrue();
		}

		[Test]
		public void ShouldProvideFileFilterInfo()
		{
			new SenseOfWonderSystem().ShouldHave()
				.Properties(w => w.FileExtension, w => w.FilePattern, w => w.Name).EqualTo(new
				{
					FileExtension = "wonder",
					Name = "Sense of Wonder",
					FilePattern = "*.wonder"
				});
		}

		[Test]
		public void ShouldProvideFileFilterInfoForCardFile()
		{
			new SenseOfWonderCards().ShouldHave()
				.Properties(w => w.FileExtension, w => w.FilePattern, w => w.Name).EqualTo(new
				{
					FileExtension = "wonderrules",
					Name = "Sense of Wonder Rule System",
					FilePattern = "*.wonderrules"
				});
		}

		[Test, Explicit, STAThread]
		public void ShouldCreateEditorControlWhenAsked()
		{
			new SenseOfWonderSystem().EditorPages.Should().HaveCount(1).And.ContainItemsAssignableTo<EditCharacter>();
		}
	}
}
