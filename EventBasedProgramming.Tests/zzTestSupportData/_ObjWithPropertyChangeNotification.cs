using System.ComponentModel;
using EventBasedProgramming.Binding;

namespace EventBasedProgramming.Tests.zzTestSupportData
{
	internal class _ObjWithPropertyChangeNotification : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public string Description { get; set; }

		public void FireDescriptionChangedBecauseTestSaidTo()
		{
			PropertyChanged.Raise(this, () => Description);
		}
	}
}