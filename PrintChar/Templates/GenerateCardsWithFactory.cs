using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace PrintChar.Templates
{
	public class GenerateCardsWithFactory<TResult>
	{
		[NotNull] private readonly IFactory<TResult> _factory;

		public GenerateCardsWithFactory([NotNull] IFactory<TResult> factory)
		{
			_factory = factory;
		}

		[NotNull]
		public IEnumerable<TResult> Generate([NotNull] Character pc)
		{
			return new[] {_factory.StatsFor(pc)}.Concat(pc.Powers.Where(pow => !pow.Hidden).Select(_factory.CardFor));
		}

		[NotNull]
		public static Func<Character, IEnumerable<TResult>> Using([NotNull] IFactory<TResult> factory)
		{
			return new GenerateCardsWithFactory<TResult>(factory).Generate;
		}
	}
}
