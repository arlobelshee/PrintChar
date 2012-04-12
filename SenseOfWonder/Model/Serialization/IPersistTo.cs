namespace SenseOfWonder.Model.Impl
{
	public interface IPersistTo<T>
	{
		T PersistableData { get; }
		void UpdateFrom(T wonderCharData);
	}
}