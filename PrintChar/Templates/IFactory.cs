using JetBrains.Annotations;
using Plugin.Dnd4e;

namespace PrintChar.Templates
{
	public interface IFactory<out TResult>
	{
		[NotNull]
		TResult StatsFor([NotNull] CharacterDnd4E data);

		[NotNull]
		TResult CardFor([NotNull] Power data);
	}
}
