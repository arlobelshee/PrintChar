using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;
using Plugin.Dnd4e;

namespace PrintChar.Tests
{
	[TestFixture]
	public class TransformChain
	{
		[SetUp]
		public void SetUp()
		{
			_testSubject = new CharacterTransformer<string>(_ParseByJustCreatingCharWithFilenameAsName, _GenerateBySelectingNameSeveralTimes);
		}

		[Test]
		public void ShouldApplyTransformationsInOrder()
		{
			_testSubject.Add(pc => pc.Name = pc.Name.Replace("hen", "lal"));
			_testSubject.Add(pc => pc.Name = pc.Name.Replace("al", "goo"));
			var result = _testSubject.Transform(_PcsNamed("Chen", "Tahen"));
			Assert.That(result.Select(pc => pc.Name).ToList(), Is.EqualTo(new[] {"Clgoo", "Tgoogoo"}));
		}

		[Test]
		public void TransformingWithNoTransformsShouldStillProduceCharacters()
		{
			var result = _testSubject.Transform(_PcsNamed("Chen"));
			Assert.That(result.Select(pc => pc.Name).ToList(), Is.EqualTo(new[] {"Chen"}));
		}

		[Test]
		public void ShouldUseSuppliedParserToLoadCharacters()
		{
			var result = _testSubject.Parse(_FilesNamed("Chen", "Wen"));
			Assert.That(result.Select(pc => pc.Name).ToList(), Is.EqualTo(new[] {"Chen", "Wen"}));
		}

		[Test]
		public void ShouldUseSuppliedGeneratorToCreateResults()
		{
			var result = _testSubject.Generate(_PcsNamed("Al", "Ben"));
			Assert.That(result.ToList(), Is.EqualTo(new[] {"Al", "Al", "Ben", "Ben", "Ben"}));
		}

		[Test]
		public void CompileShouldDoEverything()
		{
			_testSubject.Add(pc => pc.Name = pc.Name.Replace("enen", "en"));
			var result = _testSubject.Compile(_FilesNamed("Al", "Benen"));
			Assert.That(result.ToList(), Is.EqualTo(new[] {"Al", "Al", "Ben", "Ben", "Ben"}));
		}

		[NotNull]
		private static IEnumerable<FileInfo> _FilesNamed([NotNull] params string[] fileNames)
		{
			return fileNames.Select(fn => new FileInfo(fn));
		}

		[NotNull]
		private static IEnumerable<string> _GenerateBySelectingNameSeveralTimes([NotNull] CharacterDnd4E ch)
		{
			return Enumerable.Repeat(ch.Name, ch.Name.Length);
		}

		private static CharacterDnd4E _ParseByJustCreatingCharWithFilenameAsName([NotNull] FileInfo fileName)
		{
			return new CharacterDnd4E
			{
				Name = fileName.Name
			};
		}

		[NotNull]
		private static IEnumerable<CharacterDnd4E> _PcsNamed([NotNull] params string[] names)
		{
			return names.Select(name => new CharacterDnd4E
			{
				Name = name
			});
		}

		private CharacterTransformer<string> _testSubject;
	}
}
