namespace PrintChar.Tests.zzTestSupportData
{
	internal class _WritableGameSystem : _TestDummyGameSystem
	{
		internal const string Extension = "write";

		public _WritableGameSystem()
			: base("Writable Characters", Extension) {}

		public _WritableGameSystem(string extensionOverride)
			: base("Writable Characters", extensionOverride) {}
	}
}
