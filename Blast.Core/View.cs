using System.Collections.Generic;
using System.Collections.ObjectModel;
using Blast.Core.Search;
using Blast.Core.Sort;

namespace Blast.Core
{
	public abstract class View<T> : ViewModel
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

		public virtual void Display(ObservableCollection<T> items)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			this.ViewModels = items;
		}

		public void Load(ICollection<T> items, SortOption<T> sortOption = null, string value = null, SearchOption<T> searchOption = null)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			ObservableCollection<T> allViewModels;
			ObservableCollection<T> viewModels;

			var search = this.ModelSearch;
			var sort = this.ModelSort;
			if (search == null && sort == null)
			{
				allViewModels = new ObservableCollection<T>(items);
				viewModels = new ObservableCollection<T>(items);
			}
			else
			{
				if (search != null && sort != null)
				{
					allViewModels = sort.Sort(new ObservableCollection<T>(items), sortOption ?? sort.OptionSort.Current);
					viewModels = search.Search(allViewModels, value, searchOption);
				}
				else
				{
					if (search == null)
					{
						allViewModels = sort.Sort(new ObservableCollection<T>(items), sortOption ?? sort.OptionSort.Current);
						viewModels = new ObservableCollection<T>(allViewModels);
					}
					else
					{
						allViewModels = new ObservableCollection<T>(items);
						viewModels = search.Search(allViewModels, value, searchOption);
					}
				}
			}

			this.AllViewModels = allViewModels;
			this.Display(viewModels);
		}

		public void Sort(SortOption<T> option = null)
		{
			if (this.ModelSort == null) ExceptionHelper.ThrowInvalidOperationException();

			var modelSort = this.ModelSort;
			var current = option ?? modelSort.OptionSort.Current;
			this.AllViewModels = modelSort.Sort(this.AllViewModels, current);
			this.Display(modelSort.Sort(this.ViewModels, current));
		}

		public void Search(string value = null, SearchOption<T> option = null)
		{
			if (this.ModelSearch == null) ExceptionHelper.ThrowInvalidOperationException();

			this.Display(this.ModelSearch.Search(this.AllViewModels, value, option));
		}
	}
}