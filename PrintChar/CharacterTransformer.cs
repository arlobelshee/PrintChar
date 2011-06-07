using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace PrintChar
{
	public class CharacterTransformer<TResult>
	{
		[NotNull] private readonly List<Action<Character>> _charTransforms = new List<Action<Character>>();
		[NotNull] private readonly Func<Character, IEnumerable<TResult>> _generator;
		[NotNull] private readonly Func<FileInfo, Character> _parser;

		public CharacterTransformer([NotNull] Func<FileInfo, Character> parser,
			[NotNull] Func<Character, IEnumerable<TResult>> generator)
		{
			_parser = parser;
			_generator = generator;
		}

		public void Add(Action<Character> transformation)
		{
			_charTransforms.Add(transformation);
		}

		[NotNull]
		public IEnumerable<Character> Transform([NotNull] IEnumerable<Character> pcs)
		{
			return pcs.Select(pc =>
			{
				foreach (var transform in _charTransforms)
				{
					transform(pc);
				}
				return pc;
			});
		}

		[NotNull]
		public IEnumerable<Character> Parse([NotNull] IEnumerable<FileInfo> charFiles)
		{
			return charFiles.Select(_parser);
		}

		[NotNull]
		public IEnumerable<TResult> Generate([NotNull] IEnumerable<Character> pcs)
		{
			return pcs.SelectMany(_generator);
		}

		[NotNull]
		public IEnumerable<TResult> Compile([NotNull] IEnumerable<FileInfo> characterFiles)
		{
			return Generate(Transform(Parse(characterFiles)));
		}
	}
}
