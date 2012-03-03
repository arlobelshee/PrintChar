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
			_testSubject.Name.Should().Be("Trivial");
		}

		[Test]
		public void ShouldDescribeFileFilters()
		{
			_testSubject.FileExtension.Should().Be("simple");
			_testSubject.FilePattern.Should().Be("*.simple");
		}

		[Test]
		public void ShouldLoadCharactersFromFilesOnCommand()
		{
			string tempFile = Path.GetTempFileName();
			using (Undo.Step(() => File.Delete(tempFile)))
			{
				_testSubject.LoadCharacter(tempFile).File.Location.FullName.Should().Be(tempFile);
			}
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

		private _SimplisticGameSystem _testSubject;

		[SetUp]
		public void Setup()
		{
			_testSubject = new _SimplisticGameSystem();
		}

		public class SillyCharacter : Character
		{
			public SillyCharacter(IDataFile data, GameSystem system) : base(system)
			{
				File = data;
			}
		}
	}
}
