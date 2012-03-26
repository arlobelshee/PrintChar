using System;
using FluentAssertions;
using NUnit.Framework;
using SenseOfWonder.Views;

namespace SenseOfWonder.Tests.GameSystemShowsUpInCoreApp
{
	[TestFixture]
	public class GameSystemPrividesInfoToConfigureCoreApplication
	{
		[Test]
		public void ShouldAllowCharacterCreationAndEditing()
		{
			new SenseOfWonderSystem().IsReadOnly.Should().BeFalse();
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

		[Test, Explicit, STAThread]
		public void ShouldCreateEditorControlWhenAsked()
		{
			new SenseOfWonderSystem().EditorPages.Should().HaveCount(1).And.ContainItemsAssignableTo<Editor>();
		}
	}
}
