using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blast.Core
{
	public abstract class ViewManager<T> : View<T> where T : ViewModel
	{
		public virtual void Insert(T item)
		{
			if (item == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Item);

			this.Insert(this.AllViewModels, item, true);
			this.Insert(this.ViewModels, item, false);

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

		private void Insert(ObservableCollection<T> items, T item, bool skipSearch)
		{
			var index = this.FindInsertIndex(items, item, skipSearch);
			if (index >= 0)
			{
				items.Insert(index, item);
			}
		}

		private void Update(ObservableCollection<T> items, T item)
		{
			var index = this.FindInsertIndex(items, item, false);
			if (index >= 0)
			{
				index = Math.Max(0, Math.Min(index, items.Count - 1));

				var oldIndex = items.IndexOf(item);
				if (oldIndex < 0)
				{
					items.Insert(index, item);
				}
				else
				{
					if (oldIndex != index)
					{
						var tmp = items[oldIndex];
						items[oldIndex] = items[index];
						items[index] = tmp;
					}
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

		private int FindInsertIndex(ObservableCollection<T> items, T item, bool skipSearch)
		{
			if (!skipSearch)
			{
				var modelSearch = this.ModelSearch;
				if (modelSearch != null && !modelSearch.Search(new ObservableCollection<T> { item }).Any())
				{
					return -1;
				}
			}
			var sortIndex = this.FindSortIndex(items, item);
			if (sortIndex >= 0)
			{
				return sortIndex;
			}
			return items.Count;
		}

		private int FindSortIndex(ObservableCollection<T> items, T item)
		{
			if (this.ModelSort != null)
			{
				var index = 0;
				var comparison = this.ModelSort.OptionSort.Current.Comparison;
				for (var i = 0; i < items.Count; i++)
				{
					if (comparison(items[i], item) > 0)
					{
						index = i;
						break;
					}
				}
				return index;
			}

			return -1;
		}
	}
}