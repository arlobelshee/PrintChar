using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Plugin.Dnd4e;

namespace PrintChar
{
	public class CharacterTransformer<TResult>
	{
		[NotNull] private readonly List<Action<CharacterDnd4E>> _charTransforms = new List<Action<CharacterDnd4E>>();
		[NotNull] private readonly Func<CharacterDnd4E, IEnumerable<TResult>> _generator;
		[NotNull] private readonly Func<FileInfo, CharacterDnd4E> _parser;

		public CharacterTransformer([NotNull] Func<FileInfo, CharacterDnd4E> parser,
			[NotNull] Func<CharacterDnd4E, IEnumerable<TResult>> generator)
		{
			_parser = parser;
			_generator = generator;
		}

		public void Add(Action<CharacterDnd4E> transformation)
		{
			_charTransforms.Add(transformation);
		}

		[NotNull]
		public IEnumerable<CharacterDnd4E> Transform([NotNull] IEnumerable<CharacterDnd4E> pcs)
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
		public IEnumerable<CharacterDnd4E> Parse([NotNull] IEnumerable<FileInfo> charFiles)
		{
			return charFiles.Select(_parser);
		}

		[NotNull]
		public IEnumerable<TResult> Generate([NotNull] IEnumerable<CharacterDnd4E> pcs)
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
