using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blast.Core.Search
{
	public abstract class ViewModelSearch<T> where T : ViewModel
	{
		public TextSearch<T> TextSearch { get; private set; }
		public OptionSearch<T> OptionSearch { get; private set; }

		protected ViewModelSearch(TextSearch<T> textSearch)
		{
			if (textSearch == null) throw new ArgumentNullException("textSearch");

			this.TextSearch = textSearch;
		}

		protected ViewModelSearch(OptionSearch<T> optionSearch)
		{
			if (optionSearch == null) throw new ArgumentNullException("optionSearch");

			this.OptionSearch = optionSearch;
		}

		protected ViewModelSearch(TextSearch<T> textSearch, OptionSearch<T> optionSearch)
		{
			if (textSearch == null) throw new ArgumentNullException("textSearch");
			if (optionSearch == null) throw new ArgumentNullException("optionSearch");

			this.TextSearch = textSearch;
			this.OptionSearch = optionSearch;
		}

		public List<T> Search(List<T> viewModels, string search, SearchOption<T> option)
		{
			if (viewModels == null) throw new ArgumentNullException("viewModels");
			if (search == null) throw new ArgumentNullException("search");
			if (option == null) throw new ArgumentNullException("option");

			var current = viewModels;
			var textSearch = this.TextSearch;
			if (textSearch != null)
			{
				current = textSearch.FindAll(viewModels, search);
			}
			var optionSearch = this.OptionSearch;
			if (optionSearch != null)
			{
				current = optionSearch.FindAll(current, option);
			}
			return current;
		}
	}
}