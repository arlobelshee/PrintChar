using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Plugin.Dnd4e;

namespace Plugin.Dnd4e.Templates
{
	public class GenerateCardsWithFactory<TResult>
	{
		[NotNull] private readonly IFactory<TResult> _factory;

		public GenerateCardsWithFactory([NotNull] IFactory<TResult> factory)
		{
			_factory = factory;
		}

		[NotNull]
		public IEnumerable<TResult> Generate([NotNull] CharacterDnd4E pc)
		{
			return new[] {_factory.StatsFor(pc)}.Concat(pc.Powers.Where(pow => !pow.Hidden).Select(_factory.CardFor));
		}

		[NotNull]
		public static Func<CharacterDnd4E, IEnumerable<TResult>> Using([NotNull] IFactory<TResult> factory)
		{
			return new GenerateCardsWithFactory<TResult>(factory).Generate;
		}
	}
}
