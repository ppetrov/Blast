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

			var hasSort = (this.ModelSort != null);
			if (hasSort)
			{
				this.UpdateIndex(this.AllViewModels, item);
			}
			if (this.IsMatchingSearch(item))
			{
				if (hasSort)
				{
					this.UpdateIndex(this.ViewModels, item);
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
			if (this.ModelSearch == null)
			{
				return;
			}
			var optionSearch = this.ModelSearch.OptionSearch;
			if (optionSearch != null)
			{
				optionSearch.SetupCount(this.ViewModels);
			}
		}

		private bool IsMatchingSearch(T item)
		{
			return this.ModelSearch == null || this.ModelSearch.Search(new ObservableCollection<T> { item }).Count > 0;
		}

		private void InsertAtSortedIndex(ObservableCollection<T> items, T item)
		{
			var index = items.Count;
			if (this.ModelSort != null)
			{
				index = this.FindSortIndex(items, item);
			}
			items.Insert(index, item);
		}

		private void UpdateIndex(ObservableCollection<T> items, T item)
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
			var comparison = this.ModelSort.OptionSort.Current.Comparison;
			for (var i = 0; i < items.Count; i++)
			{
				if (comparison(items[i], item) > 0)
				{
					return i;
				}
			}
			return 0;
		}

		private void Swap(ObservableCollection<T> items, int i, int j)
		{
			var tmp = items[i];
			items[i] = items[j];
			items[j] = tmp;
		}
	}
}