using System.IO;
using System.Windows;
using NUnit.Framework;

namespace Plugin.Dnd4e.Tests
{
	[TestFixture]
	public class DataFilesLoadProperly
	{
		[Test, RequiresSTA, Explicit]
		public void DesignDataShouldLoadShivra()
		{
			var testSubject = new DataFilesDesignData();
			var location = new FileInfo(testSubject.Path);
			Assert.That(location.Exists, Is.True);
			Assert.That(location.Name, Is.EqualTo("Shivra.dnd4e"));
			Assert.That(testSubject.ConfigData, Is.Empty);
			Assert.That(testSubject.CharFileName, Is.EqualTo("Shivra.dnd4e"));
			Assert.That(testSubject.IsValid, Is.EqualTo(Visibility.Visible));
		}

		[Test]
		public void NewObjectShouldHaveEmptyStringValues()
		{
			var testSubject = new DataFiles();
			Assert.That(testSubject.Path, Is.Empty);
			Assert.That(testSubject.ConfigData, Is.Empty);
			Assert.That(testSubject.CharFileName, Is.Empty);
			Assert.That(testSubject.IsValid, Is.EqualTo(Visibility.Collapsed));
		}
	}
}
