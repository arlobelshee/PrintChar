using System.ComponentModel.Composition;
using FluentAssertions;
using NUnit.Framework;
using PluginApi.Model;
using SenseOfWonder.Model;

namespace SenseOfWonder.Tests.GameSystemShowsUpInCoreApp
{
	[TestFixture]
	public class DllCanBeLoadedAsAPlugin
	{
		[Test]
		public void ShouldExportTheGameSystem()
		{
			var exportAsGameSystem = new ExportAttribute(typeof (GameSystem));
			typeof (SenseOfWonderSystem).GetCustomAttributes(typeof (ExportAttribute), false)
				.Should().Equal(exportAsGameSystem);
		}
	}
}
