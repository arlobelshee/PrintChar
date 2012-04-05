namespace PrintChar.Tests.zzTestSupportData
{
	internal class _ReadOnlyGameSystem : _TestDummyGameSystem
	{
		internal const string Extension = "read";

		public _ReadOnlyGameSystem()
			: base("Read Only Characters", Extension)
		{
			IsReadOnly = true;
		}
	}
}
