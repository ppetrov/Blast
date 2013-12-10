using System.Collections.ObjectModel;

namespace Blast.Core.Search
{
	public abstract class ViewModelSearch<T> where T : ViewModel
	{
		public TextSearch<T> TextSearch { get; private set; }
		public OptionSearch<T> OptionSearch { get; private set; }

		protected ViewModelSearch(TextSearch<T> input)
		{
			if (input == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Input);

			this.TextSearch = input;
		}

		protected ViewModelSearch(OptionSearch<T> input)
		{
			if (input == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Input);

			this.OptionSearch = input;
		}

		protected ViewModelSearch(TextSearch<T> x, OptionSearch<T> y)
		{
			if (x == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.X);
			if (y == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Y);

			this.TextSearch = x;
			this.OptionSearch = y;
		}

		public ObservableCollection<T> Search(ObservableCollection<T> items, string value = null, SearchOption<T> option = null)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			if (this.TextSearch == null && this.OptionSearch == null)
			{
				return new ObservableCollection<T>(items);
			}
			var current = items;
			var textSearch = this.TextSearch;
			if (textSearch != null)
			{
				current = textSearch.Search(current, value ?? textSearch.Current);
			}
			var optionSearch = this.OptionSearch;
			if (optionSearch != null)
			{
				current = optionSearch.Search(current, option ?? optionSearch.Current);
			}
			return current;
		}
	}
}