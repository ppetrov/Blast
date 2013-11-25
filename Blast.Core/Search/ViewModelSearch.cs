using System.Collections.Generic;

namespace Blast.Core.Search
{
	public abstract class ViewModelSearch<T> where T : ViewModel
	{
		public TextSearch<T> TextSearch { get; private set; }
		public OptionSearch<T> OptionSearch { get; private set; }

		protected ViewModelSearch(TextSearch<T> textSearch = null, OptionSearch<T> optionSearch = null)
		{
			this.TextSearch = textSearch;
			this.OptionSearch = optionSearch;
		}

		public List<T> Search(List<T> items, string value = null, SearchOption<T> option = null)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			var current = items;
			var textSearch = this.TextSearch;
			if (textSearch != null)
			{
				current = textSearch.FindAll(items, value ?? this.GetCurrentValue());
			}
			var optionSearch = this.OptionSearch;
			if (optionSearch != null)
			{
				current = optionSearch.FindAll(current, option ?? this.GetCurrentOption());
			}
			return current;
		}

		private string GetCurrentValue()
		{
			var current = string.Empty;

			var textSearch = this.TextSearch;
			if (textSearch != null)
			{
				current = textSearch.Value;
			}

			return current;
		}

		private SearchOption<T> GetCurrentOption()
		{
			SearchOption<T> current = null;

			var optionSearch = this.OptionSearch;
			if (optionSearch != null)
			{
				current = optionSearch.Current;
			}

			return current;
		}
	}
}