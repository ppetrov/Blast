using System.Collections.ObjectModel;

namespace Blast.Core
{
	public abstract class ViewManager<T> : View<T> where T : ViewModel
	{
		public virtual void Insert(T item)
		{
			if (item == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Item);

			this.InsertAtSortedIndex(this.AllViewModels, item);

			if (this.IsMatchingSearch(item))
			{
				this.InsertAtSortedIndex(this.ViewModels, item);
			}

			this.UpdateCount();
		}

		public virtual void Update(T item)
		{
			if (item == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Item);

			if (this.ModelSort != null)
			{
				this.UpdateOldNewIndexes(this.AllViewModels, item);
			}
			if (this.IsMatchingSearch(item))
			{
				if (this.ModelSort != null)
				{
					this.UpdateOldNewIndexes(this.ViewModels, item);
				}
			}
			else
			{
				this.ViewModels.Remove(item);
			}

			this.UpdateCount();
		}

		public virtual void Delete(T item)
		{
			if (item == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Item);

			this.AllViewModels.Remove(item);
			this.ViewModels.Remove(item);

			this.UpdateCount();
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

		private bool IsMatchingSearch(T item)
		{
			if (this.ModelSearch == null)
			{
				return true;
			}
			return this.ModelSearch.Search(new ObservableCollection<T> { item }).Count > 0;
		}

		private void InsertAtSortedIndex(ObservableCollection<T> items, T item)
		{
			var index = items.Count;
			if (this.ModelSort == null)
			{
				index = this.FindSortIndex(items, item);
			}
			items.Insert(index, item);
		}

		private void UpdateOldNewIndexes(ObservableCollection<T> items, T item)
		{
			var index = this.FindSortIndex(items, item);
			var oldIndex = items.IndexOf(item);
			if (index != oldIndex)
			{
				this.Swap(items, oldIndex, index);
			}
		}

		private int FindSortIndex(ObservableCollection<T> items, T item)
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

		private void Swap(ObservableCollection<T> items, int oldIndex, int newIndex)
		{
			var tmp = items[oldIndex];
			items[oldIndex] = items[newIndex];
			items[newIndex] = tmp;
		}
	}
}