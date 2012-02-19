using System.IO;
using FluentAssertions;
using NUnit.Framework;
using PluginApi.Model;

namespace PluginApi.Tests
{
	[TestFixture]
	public class DescribeAGameSystem
	{
		[Test]
		public void ShouldHaveAName()
		{
			_testSubject.Name.Should().Be("4th Edition D&D");
		}

		[Test]
		public void ShouldDescribeFileFilters()
		{
			_testSubject.FileExtension.Should().Be("dnd4e");
			_testSubject.FilePattern.Should().Be("*.dnd4e");
		}

		[Test]
		public void ShouldUseLabelAndExtensionCorrectlyToInitializeOpenDialog()
		{
			_testSubject.CreateOpenDialog().ShouldHave().SharedProperties().EqualTo(new
			{
				Filter = "4th Edition D&D file (*.dnd4e)|*.dnd4e",
				DefaultExt = "dnd4e",
				CheckFileExists = true,
				Multiselect = false,
				InitialDirectory = string.Empty
			});
		}

		[Test]
		public void IfHaveAlreadyOpenedCharacterShouldSetOpenDialogToStartInThatLocation()
		{
			_testSubject.CharacterFile = new CachedFile(new FileInfo(Path.GetTempPath() + @"ee.dnd4e"), false);
			_testSubject.CreateOpenDialog().InitialDirectory.Should().Be(Path.GetTempPath().TrimEnd('\\'));
		}

		[Test]
		public void ShouldAllowCreatingNewCharactersByDefault()
		{
			_testSubject.IsReadOnly.Should().BeFalse();
		}

		[Test]
		public void ShouldAllowGameSystemsWhichCannotCreateCharacters()
		{
			_testSubject.IsReadOnly = true;
			_testSubject.IsReadOnly.Should().BeTrue();
		}

		private GameSystem _testSubject;

		[SetUp]
		private void Setup()
		{
			_testSubject = new GameSystem("4th Edition D&D", "dnd4e");
		}
	}
}
