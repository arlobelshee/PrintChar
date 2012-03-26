using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using PluginApi.Model;

namespace PrintChar.Tests.zzTestSupportData
{
	internal class _TracingGameSystem : GameSystem
	{
		private List<IDataFile> _ParsedFiles { get; set; }
		private List<IDataFile> _CreatedFiles { get; set; }

		public _TracingGameSystem() : base("Tracing", "tracechar")
		{
			_ParsedFiles = new List<IDataFile>();
			_CreatedFiles = new List<IDataFile>();
		}

		public override Character Parse(IDataFile characterData)
		{
			_ParsedFiles.Add(characterData);
			return new _SillyCharacter(characterData, this);
		}

		public override Character CreateIn(IDataFile characterData)
		{
			_CreatedFiles.Add(characterData);
			return new _SillyCharacter(characterData, this);
		}

		protected override IDataFile LocateFile(FileInfo location)
		{
			return Data.EmptyAt(location);
		}

		public void ShouldHaveCreatedNothing()
		{
			ShouldHaveCreated();
		}

		public void ShouldHaveLoadedNothing()
		{
			ShouldHaveLoaded();
		}

		public void ShouldHaveCreated(params string[] expected)
		{
			_CreatedFiles.Select(f => f.Location.FullName).Should().Equal(expected);
		}

		public void ShouldHaveLoaded(params string[] expected)
		{
			_ParsedFiles.Select(f => f.Location.FullName).Should().Equal(expected);
		}
	}
}