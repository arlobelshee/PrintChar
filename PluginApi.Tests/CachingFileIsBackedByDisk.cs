using System.IO;
using JetBrains.Annotations;
using NUnit.Framework;
using PluginApi.Model;

namespace PluginApi.Tests
{
	[TestFixture]
	public class CachingFileIsBackedByDisk
	{
		[SetUp]
		public void SetUp()
		{
			_backingFile = new FileInfo("CachingFileTests.txt");
		}

		[TearDown]
		public void TearDown()
		{
			if (_backingFile.Exists)
				_backingFile.Delete();
		}

		[Test]
		public void ChangesShouldBeWrittenToBackingFile()
		{
			const string valueWritten = "the test value";
			_WriteToTestSubject(valueWritten);
			Assert.That(_GetBackingFileContents(), Is.EqualTo(valueWritten));
		}

		[Test]
		public void CacheStartsWithCurrentValueInFile()
		{
			const string originalValue = "original value";
			_WriteToBackingFile(originalValue);
			Assert.That(_ReadFromTestSubject(), Is.EqualTo(originalValue));
		}

		[Test]
		public void ChangingValueOverwritesFile()
		{
			const string originalValue = "original value";
			const string valueWritten = "the test value";

			_WriteToBackingFile(originalValue);
			_WriteToTestSubject(valueWritten);
			Assert.That(_GetBackingFileContents(), Is.EqualTo(valueWritten));
		}

		private string _GetBackingFileContents()
		{
			using (var data = _backingFile.OpenText())
			{
				return data.ReadToEnd();
			}
		}

		private void _WriteToBackingFile(string contents)
		{
			using (var data = new StreamWriter(_backingFile.OpenWrite()))
			{
				data.Write(contents);
			}
		}

		private void _WriteToTestSubject(string valueWritten)
		{
			using (var testSubject = new CachedFile(_backingFile, requireToExistAlready: false)
				)
			{
				testSubject.Contents = valueWritten;
			}
		}

		private string _ReadFromTestSubject()
		{
			using (var testSubject = new CachedFile(_backingFile))
			{
				return testSubject.Contents;
			}
		}

		[NotNull] private FileInfo _backingFile;
	}
}
