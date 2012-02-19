using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace PluginApi.Model
{
	public class CharacterTransformer<TCharacter, TResult>
	{
		[NotNull] private readonly List<Action<TCharacter>> _charTransforms = new List<Action<TCharacter>>();
		[NotNull] private readonly Func<TCharacter, IEnumerable<TResult>> _generator;
		[NotNull] private readonly Func<IDataFile, TCharacter> _parser;

		public CharacterTransformer([NotNull] Func<IDataFile, TCharacter> parser,
			[NotNull] Func<TCharacter, IEnumerable<TResult>> generator)
		{
			_parser = parser;
			_generator = generator;
		}

		public void Add(Action<TCharacter> transformation)
		{
			_charTransforms.Add(transformation);
		}

		[NotNull]
		public IEnumerable<TCharacter> Transform([NotNull] IEnumerable<TCharacter> pcs)
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
		public IEnumerable<TCharacter> Parse([NotNull] IEnumerable<IDataFile> charFiles)
		{
			return charFiles.Select(_parser);
		}

		[NotNull]
		public IEnumerable<TResult> Generate([NotNull] IEnumerable<TCharacter> pcs)
		{
			return pcs.SelectMany(_generator);
		}

		[NotNull]
		public IEnumerable<TResult> Compile([NotNull] IEnumerable<IDataFile> characterFiles)
		{
			return Generate(Transform(Parse(characterFiles)));
		}
	}
}
