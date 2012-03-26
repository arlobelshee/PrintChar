using System.IO;
using JetBrains.Annotations;
using PluginApi.Model;
using FluentAssertions;

namespace PluginApi.Tests
{
	internal class _SimplisticGameSystem : GameSystem
	{
		public _SimplisticGameSystem()
			: base("Trivial", "simple") {}

		public bool FileShouldAlreadyExist { get; set; }

		[NotNull]
		public override Character Parse([NotNull] IDataFile characterData)
		{
			return new DescribeAGameSystem.SillyCharacter(characterData, this);
		}

		public override Character CreateIn(IDataFile characterData)
		{
			return new DescribeAGameSystem.SillyCharacter(characterData, this);
		}

		protected override IDataFile LocateFile(FileInfo location, bool requireToExistAlready)
		{
			requireToExistAlready.Should().Be(FileShouldAlreadyExist);
			return Data.EmptyAt(location);
		}
	}
}