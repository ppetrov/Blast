using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blast.Core.Sort
{
	public abstract class ViewModelSort<T> where T : ViewModel
	{
		public OptionSort<T> OptionSort { get; private set; }

		protected ViewModelSort(OptionSort<T> option)
		{
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.OptionSort = option;
		}

		public void Sort(List<T> items, SortOption<T> option = null)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			this.OptionSort.Sort(items, option ?? this.OptionSort.Current);
		}

		public ObservableCollection<T> Sort(ObservableCollection<T> items, SortOption<T> option = null)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			return this.OptionSort.Sort(items, option ?? this.OptionSort.Current);
		}
	}
}