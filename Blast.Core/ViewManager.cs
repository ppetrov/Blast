using System.Collections.Generic;
using System.Collections.ObjectModel;
using Blast.Core.Search;
using Blast.Core.Setup;
using Blast.Core.Sort;

namespace Blast.Core
{
	public abstract class ViewManager<T> : ViewModel
		where T : ViewModel
	{
		private readonly ICollectionSetup<T> _collectionSetup;
		public ICollectionSetup<T> CollectionSetup
		{
			get { return _collectionSetup; }
		}

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

		protected ViewManager()
			: this(new OverwriteCollectionSetup<T>())
		{ }

		protected ViewManager(ICollectionSetup<T> input)
		{
			if (input == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Input);

			_collectionSetup = input;
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
				var option = sortOption ?? sort.OptionSort.Current;
				if (search != null && sort != null)
				{
					allViewModels = sort.Sort(new ObservableCollection<T>(items), option);
					viewModels = search.Search(allViewModels, value, searchOption);
				}
				else
				{
					if (search == null)
					{
						allViewModels = sort.Sort(new ObservableCollection<T>(items), option);
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
			this.Setup(viewModels);
		}

		public void Sort(SortOption<T> option = null)
		{
			if (this.ModelSort == null) ExceptionHelper.ThrowInvalidOperationException();

			var current = option ?? this.ModelSort.OptionSort.Current;
			this.AllViewModels = this.ModelSort.Sort(this.AllViewModels, current);
			this.Setup(this.ModelSort.Sort(this.ViewModels, current));
		}

		public void Search(string value = null, SearchOption<T> option = null)
		{
			if (this.ModelSearch == null) ExceptionHelper.ThrowInvalidOperationException();

			this.Setup(this.ModelSearch.Search(this.AllViewModels, value, option));
		}

		private void Setup(ObservableCollection<T> items)
		{
			this.ViewModels = this.CollectionSetup.Setup(this.ViewModels, items);
		}
	}
}