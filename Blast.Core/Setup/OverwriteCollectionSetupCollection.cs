using System.Collections.ObjectModel;

namespace Blast.Core.Setup
{
	public sealed class OverwriteCollectionSetupCollection<T> : ISetupCollection<T>
		where T : ViewModel
	{
		public ObservableCollection<T> Setup(ObservableCollection<T> current, ObservableCollection<T> items)
		{
			return items;
		}
	}
}