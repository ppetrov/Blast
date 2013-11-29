using System;
using System.Collections.ObjectModel;

namespace Blast.Core.Setup
{
	public sealed class OverwriteItemsSetupCollection<T> : ISetupCollection<T>
		where T : ViewModel
	{
		public ObservableCollection<T> Setup(ObservableCollection<T> current, ObservableCollection<T> items)
		{
			var inputCount = items.Count;
			var viewModels = current;
			var common = Math.Min(viewModels.Count, inputCount);
			for (var i = 0; i < common; i++)
			{
				viewModels[i] = items[i];
			}
			while (viewModels.Count > inputCount)
			{
				viewModels.RemoveAt(viewModels.Count - 1);
			}
			while (viewModels.Count < inputCount)
			{
				var index = viewModels.Count;
				viewModels.Insert(index, items[index]);
			}

			return current;
		}
	}
}