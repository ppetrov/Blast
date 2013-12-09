using System.Collections.ObjectModel;
using System.Linq;

namespace Blast.Core
{
	public abstract class ViewManager<T> : View<T> where T : ViewModel
	{
		public virtual void Insert(T item)
		{
			if (item == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Item);

			this.Insert(this.AllViewModels, item);
			this.Insert(this.ViewModels, item);

			this.UpdateCount();
		}

		public virtual void Update(T item)
		{
			if (item == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Item);

			this.Update(this.AllViewModels, item);
			this.Update(this.ViewModels, item);

			this.UpdateCount();
		}

		public virtual void Delete(T item)
		{
			if (item == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Item);

			this.Delete(this.AllViewModels, item);
			this.Delete(this.ViewModels, item);

			this.UpdateCount();
		}

		private void Insert(ObservableCollection<T> items, T item)
		{
			var index = this.FindNewIndex(items, item);
			if (index >= 0)
			{
				items.Insert(index, item);
			}
		}

		private void Update(ObservableCollection<T> items, T item)
		{
			var newIndex = this.FindNewIndex(items, item);
			if (newIndex >= 0)
			{
				var oldIndex = items.IndexOf(item);
				if (oldIndex != newIndex)
				{
					var tmp = items[oldIndex];
					items[oldIndex] = items[newIndex];
					items[newIndex] = tmp;
				}
			}
		}

		private void Delete(ObservableCollection<T> items, T item)
		{
			items.Remove(item);
		}

		private void UpdateCount()
		{
			var modelSearch = this.ModelSearch;
			if (modelSearch != null)
			{
				var optionSearch = modelSearch.OptionSearch;
				if (optionSearch != null)
				{
					optionSearch.SetupCount(this.ViewModels);
				}
			}
		}

		private int FindNewIndex(ObservableCollection<T> items, T item)
		{
			var index = items.Count;

			var modelSearch = this.ModelSearch;
			if (modelSearch != null && !modelSearch.Search(new ObservableCollection<T> { item }).Any())
			{
				index = -1;
			}
			if (index >= 0)
			{
				if (this.ModelSort != null)
				{
					var comparison = this.ModelSort.OptionSort.Current.Comparison;
					for (var i = 0; i < items.Count; i++)
					{
						if (comparison(items[i], item) > 0)
						{
							index = i;
							break;
						}
					}
				}
			}
			return index;
		}
	}
}