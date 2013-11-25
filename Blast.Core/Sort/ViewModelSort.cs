using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blast.Core.Sort
{
	public abstract class ViewModelSort<T> where T : ViewModel
	{
		public SortOption<T> Current { get; private set; }
		public SortOption<T>[] Options { get; private set; }

		protected ViewModelSort(SortOption<T>[] options)
		{
			if (options == null) throw new ArgumentNullException("options");
			if (options.Length == 0) throw new ArgumentOutOfRangeException("options");

			this.Options = options;
			this.Current = options.First(o => o.IsDefault) ?? options.First();
		}

		public void SetupCurrent(SortOption<T> option)
		{
			if (option == null) throw new ArgumentNullException("option");

			this.Current = option;
		}

		public void SetupDicrection(SortOption<T> option)
		{
			if (option == null) throw new ArgumentNullException("option");

			if (option.Direction.HasValue)
			{
				option.Direction = option.Direction.Value == SortDirection.Ascending
					? SortDirection.Descending
					: SortDirection.Ascending;
			}
			else
			{
				option.Direction = SortDirection.Ascending;
			}
			foreach (var o in this.Options)
			{
				if (o != option)
				{
					o.Direction = null;
				}
			}
		}

		public void Setup(SortOption<T> option)
		{
			if (option == null) throw new ArgumentNullException("option");

			this.SetupCurrent(option);
			this.SetupDicrection(option);
		}

		public void Sort(List<T> viewModels, SortOption<T> option)
		{
			if (viewModels == null) throw new ArgumentNullException("viewModels");
			if (option == null) throw new ArgumentNullException("option");

			this.Setup(option);

			viewModels.Sort(option.Sort);
		}

		public ObservableCollection<T> Sort(ObservableCollection<T> viewModels, SortOption<T> option)
		{
			if (viewModels == null) throw new ArgumentNullException("viewModels");
			if (option == null) throw new ArgumentNullException("option");

			this.Setup(option);

			var items = new T[viewModels.Count];
			viewModels.CopyTo(items, 0);
			Array.Sort(items, option.Sort);

			return new ObservableCollection<T>(items);
		}
	}
}