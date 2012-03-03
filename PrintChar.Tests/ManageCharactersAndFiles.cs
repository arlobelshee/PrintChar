using System.IO;
using FluentAssertions;
using NUnit.Framework;
using PluginApi;
using PluginApi.Model;

namespace PrintChar.Tests
{
	[TestFixture]
	public class ManageCharactersAndFiles
	{
		[Test]
		public void ShouldUseLabelAndExtensionCorrectlyToInitializeOpenDialog()
		{
			_testSubject.CreateOpenDialog().ShouldHave().SharedProperties().EqualTo(new
			{
				Filter = "Trivial file (*.simple)|*.simple",
				DefaultExt = "simple",
				CheckFileExists = true,
				Multiselect = false,
				InitialDirectory = string.Empty
			});
		}

		[Test]
		public void IfHaveAlreadyOpenedCharacterShouldSetOpenDialogToStartInThatLocation()
		{
			_testSubject.CurrentCharacter = _arbitraryCharacter;
			_testSubject.CreateOpenDialog().InitialDirectory.Should().Be(_arbitraryCharacter.File.Location.DirectoryName);
		}

		[Test]
		public void CancellingTheOpenDialogShouldResultInNoChangeInOpenCharacter()
		{
			_testSubject.CurrentCharacter = _arbitraryCharacter;
			_testSubject.LoadCharacter(null);
			_testSubject.Character.Should().BeSameAs(_arbitraryCharacter);
		}

		[Test]
		public void OpeningANewCharacterShouldUpdateTheCharacterFile()
		{
			string tempFile = Path.GetTempFileName();
			using (Undo.Step(() => File.Delete(tempFile)))
			{
				_testSubject.CurrentCharacter = _arbitraryCharacter;
				_testSubject.LoadCharacter(tempFile);
				_testSubject.Character.File.Location.FullName.Should().Be(tempFile);
			}
		}

		private _AllGameSystemsViewModel _testSubject;
		private SillyCharacter _arbitraryCharacter;

		[SetUp]
		public void Setup()
		{
			_testSubject = new _AllGameSystemsViewModel(new _SimplisticGameSystem());
			_arbitraryCharacter = new SillyCharacter(Data.EmptyAt(new FileInfo(@"R:\arbitrary\path\ee.dnd4e")));
		}

		public class SillyCharacter : Character
		{
			public SillyCharacter(IDataFile data)
			{
				File = data;
			}
		}

		internal class _AllGameSystemsViewModel : AllGameSystemsViewModel
		{
			public _AllGameSystemsViewModel(params GameSystem[] gameSystems) : base(gameSystems) {}

			public Character CurrentCharacter
			{
				set { Character = value; }
			}
		}

		internal class _SimplisticGameSystem : GameSystem
		{
			public _SimplisticGameSystem()
				: base("Trivial", "simple") {}

			protected override Character Parse(IDataFile characterData)
			{
				return new SillyCharacter(characterData);
			}

			public SillyCharacter CurrentCharacter
			{
				set { Character = value; }
			}
		}
	}
}
