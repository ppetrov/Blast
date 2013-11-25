using System.Collections.Generic;
using System.Collections.ObjectModel;
using Blast.Core.Search;
using Blast.Core.Sort;

namespace Blast.Core
{
	public abstract class ViewManager<T> : ViewModel
		where T : ViewModel
	{
		private ObservableCollection<T> _allViewModels = new ObservableCollection<T>();
		public ObservableCollection<T> AllViewModels
		{
			get { return _allViewModels; }
			private set { this.SetProperty(ref _allViewModels, value); }
		}

		private ObservableCollection<T> _viewModels = new ObservableCollection<T>();
		public ObservableCollection<T> ViewModels
		{
			get { return _viewModels; }
			private set { this.SetProperty(ref _viewModels, value); }
		}

		public ViewModelSort<T> ModelSort { get; set; }
		public ViewModelSearch<T> ModelSearch { get; set; }

		public void Load(List<T> items, SortOption<T> sortOption = null, SearchOption<T> searchOption = null, string value = null)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Item);

			this.AllViewModels = new ObservableCollection<T>(items);

			this.Sort(items, sortOption);
			this.SearchImp(value, searchOption);

			if (this.ModelSearch == null)
			{
				this.SetupViewModels(items);
			}
		}

		public void Sort(SortOption<T> option = null)
		{
			var modelSort = this.ModelSort;
			if (modelSort != null)
			{
				modelSort.Sort(this.AllViewModels);
				this.ViewModels = modelSort.Sort(this.ViewModels);
			}
		}

		public void Search(string value)
		{
			if (value == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Value);

			this.SearchImp(value, null);
		}

		public void Search(SearchOption<T> option)
		{
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.SearchImp(null, option);
		}

		public void Search(string value, SearchOption<T> option)
		{
			if (value == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Value);
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

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
				modelSort.Sort(viewModels, option);
			}
		}

		private void SetupViewModels(IEnumerable<T> viewModels)
		{
			this.ViewModels = new ObservableCollection<T>(viewModels);
		}
	}
}