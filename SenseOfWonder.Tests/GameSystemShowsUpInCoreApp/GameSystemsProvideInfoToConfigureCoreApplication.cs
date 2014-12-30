using System;
using System.Linq;
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
			new Model.SenseOfWonder().IsReadOnly.Should().BeFalse();
		}

		[Test]
		public void ShouldAllowCardEditingButNotCreation()
		{
			new RulesEditingSystem().IsReadOnly.Should().BeTrue();
		}

		[Test]
		public void ShouldProvideFileFilterInfo()
		{
			new Model.SenseOfWonder().ShouldBeEquivalentTo(new
			{
				FileExtension = "wonder",
				Name = "Sense of Wonder",
				FilePattern = "*.wonder"
			}, options => options.ExcludingMissingProperties());
		}

		[Test]
		public void ShouldProvideFileFilterInfoForCardFile()
		{
			new RulesEditingSystem().ShouldBeEquivalentTo(new
				{
					FileExtension = "wonderrules",
					Name = "Sense of Wonder Rule System",
					FilePattern = "*.wonderrules"
				}, options => options.ExcludingMissingProperties());
		}

		[Test, Explicit, STAThread]
		public void ShouldCreateEditorControlWhenAsked()
		{
			var editorPages = new Model.SenseOfWonder().EditorPages;
			editorPages.Should().HaveCount(1);
			editorPages.Single().Content.Should().BeAssignableTo<EditCharacter>();
		}
	}
}
