using System.Collections.ObjectModel;

namespace Blast.Core.Setup
{
	public interface ICollectionSetup<T> where T : ViewModel
	{
		ObservableCollection<T> Setup(ObservableCollection<T> current, ObservableCollection<T> items);
	}
}