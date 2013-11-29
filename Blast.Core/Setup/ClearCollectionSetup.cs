using System.Collections.ObjectModel;

namespace Blast.Core.Setup
{
	public sealed class ClearCollectionSetup<T> : ICollectionSetup<T>
		where T : ViewModel
	{
		public ObservableCollection<T> Setup(ObservableCollection<T> current, ObservableCollection<T> items)
		{
			current.Clear();
			foreach (var item in items)
			{
				current.Add(item);
			}
			return current;
		}
	}
}