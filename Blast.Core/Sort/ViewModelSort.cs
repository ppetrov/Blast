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

		public ObservableCollection<T> Sort(ObservableCollection<T> items, SortOption<T> option)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			var optionSort = this.OptionSort;
			optionSort.SetupCurrent(option);
			optionSort.SetupDicrection(option);

			return optionSort.Sort(items, option);
		}
	}
}