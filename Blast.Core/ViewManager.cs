using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Blast.Core.Search;
using Blast.Core.Sort;

namespace Blast.Core
{
	public abstract class ViewManager<T> : ViewModel
		where T : ViewModel
	{
		protected List<T> AllViewModels { get; private set; }

		private ObservableCollection<T> _viewModels = new ObservableCollection<T>();
		public ObservableCollection<T> ViewModels
		{
			get { return _viewModels; }
			private set { this.SetProperty(ref _viewModels, value); }
		}

		public ViewModelSort<T> ModelSort { get; set; }
		public ViewModelSearch<T> ModelSearch { get; set; }

		protected ViewManager()
		{
			this.AllViewModels = new List<T>();
		}

		public void Load(List<T> viewModels, SortOption<T> sortOption = null, SearchOption<T> searchOption = null, string value = null)
		{
			if (viewModels == null) throw new ArgumentNullException("viewModels");

			this.AllViewModels = viewModels;

			this.Sort(viewModels, sortOption);
			this.SearchImp(value ?? this.GetCurrentValue(), searchOption ?? this.GetCurrentOption());

			if (this.ModelSearch == null)
			{
				this.SetupViewModels(viewModels);
			}
		}

		public void Sort(SortOption<T> option = null)
		{
			var modelSort = this.ModelSort;
			if (modelSort != null)
			{
				var current = option ?? modelSort.Current;
				modelSort.Sort(this.AllViewModels, current);
				this.ViewModels = modelSort.Sort(this.ViewModels, current);
			}
		}

		public void Search(string value)
		{
			if (value == null) throw new ArgumentNullException("value");

			this.SearchImp(value, this.GetCurrentOption());
		}

		public void Search(SearchOption<T> option)
		{
			if (option == null) throw new ArgumentNullException("option");

			this.SearchImp(this.GetCurrentValue(), option);
		}

		public void Search(string value, SearchOption<T> option)
		{
			if (value == null) throw new ArgumentNullException("value");
			if (option == null) throw new ArgumentNullException("option");

			this.SearchImp(value, option);
		}

		private void SearchImp(string value, SearchOption<T> option)
		{
			var modelSearch = this.ModelSearch;
			if (modelSearch != null)
			{
				this.SetupViewModels(modelSearch.Search(this.AllViewModels, value, option));
			}
		}

		private void Sort(List<T> viewModels, SortOption<T> option)
		{
			var modelSort = this.ModelSort;
			if (modelSort != null)
			{
				modelSort.Sort(viewModels, option ?? modelSort.Current);
			}
		}

		private SearchOption<T> GetCurrentOption()
		{
			SearchOption<T> current = null;

			var filter = this.ModelSearch;
			if (filter != null)
			{
				var optionSearch = filter.OptionSearch;
				if (optionSearch != null)
				{
					current = optionSearch.Current ?? optionSearch.GetDefaultOrFirst();
				}
			}
			return current;
		}

		private string GetCurrentValue()
		{
			var current = string.Empty;

			var filter = this.ModelSearch;
			if (filter != null)
			{
				var textSearch = filter.TextSearch;
				if (textSearch != null)
				{
					current = textSearch.Value;
				}
			}

			return current;
		}

		private void SetupViewModels(IEnumerable<T> viewModels)
		{
			this.ViewModels = new ObservableCollection<T>(viewModels);
		}
	}
}