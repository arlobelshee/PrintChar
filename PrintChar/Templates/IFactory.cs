using JetBrains.Annotations;

namespace PrintChar.Templates
{
	public interface IFactory<out TResult>
	{
		[NotNull]
		TResult StatsFor([NotNull] Character data);

		[NotNull]
		TResult CardFor([NotNull] Power data);
	}
}
