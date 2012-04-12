namespace SenseOfWonder.Model.Serialization
{
	public interface IPersistTo<T>
	{
		T PersistableData { get; }
		void UpdateFrom(T characterData);
	}
}