using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blast.Core.Sort
{
	public abstract class OptionSort<T> where T : ViewModel
	{
		public SortOption<T> Current { get; private set; }
		public SortOption<T>[] Options { get; private set; }

		protected OptionSort(SortOption<T>[] options)
		{
			if (options == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Options);
			if (options.Length == 0) ExceptionHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Options);

			this.Options = options;
			this.Current = this.GetDefaultOrFirst();
		}

		public void SetupCurrent(SortOption<T> option)
		{
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.Current = option;
		}

		public void SetupDicrection(SortOption<T> option)
		{
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

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
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.SetupCurrent(option);
			this.SetupDicrection(option);
		}

		public void Sort(List<T> items, SortOption<T> option)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.Setup(option);

			items.Sort(option.Comparison);
		}

		public ObservableCollection<T> Sort(ObservableCollection<T> items, SortOption<T> option)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.Setup(option);

			var array = new T[items.Count];
			items.CopyTo(array, 0);
			Array.Sort(array, option.Comparison);

			return new ObservableCollection<T>(array);
		}

		private SortOption<T> GetDefaultOrFirst()
		{
			foreach (var o in this.Options)
			{
				if (o.IsDefault)
				{
					return o;
				}
			}
			return this.Options[0];
		}
	}
}