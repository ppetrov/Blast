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

		protected ViewModelSearch(TextSearch<T> a, OptionSearch<T> b)
		{
			if (a == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.A);
			if (b == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.B);

			this.TextSearch = a;
			this.OptionSearch = b;
		}

		public ObservableCollection<T> Search(ObservableCollection<T> items, string value = null, SearchOption<T> option = null)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			var textSearch = this.TextSearch;
			var optionSearch = this.OptionSearch;
			if (textSearch != null || optionSearch != null)
			{
				var current = items;
				if (textSearch != null)
				{
					current = textSearch.Search(current, value ?? textSearch.Current);
				}
				if (optionSearch != null)
				{
					current = optionSearch.Search(current, option ?? optionSearch.Current);
				}
				return current;
			}

			return new ObservableCollection<T>(items);
		}
	}
}