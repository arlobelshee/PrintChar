using System.ComponentModel.Composition;
using FluentAssertions;
using NUnit.Framework;
using PluginApi.Model;

namespace SenseOfWonder.Tests
{
	[TestFixture]
	public class PluginInterfaces
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
