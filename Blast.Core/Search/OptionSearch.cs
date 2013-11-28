using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blast.Core.Search
{
	public abstract class OptionSearch<T> where T : ViewModel
	{
		public SearchOption<T> Current { get; private set; }
		public SearchOption<T>[] Options { get; private set; }

		protected OptionSearch(SearchOption<T>[] options)
		{
			if (options == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Options);
			if (options.Length == 0) ExceptionHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Options);

			this.Options = options;
			SearchOption<T> option = null;
			foreach (var o in this.Options)
			{
				if (o.IsDefault)
				{
					option = o;
				}
			}
			this.Current = option ?? options[0];
		}

		public void SetupCount(ObservableCollection<T> items)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			foreach (var o in this.Options)
			{
				o.Count = items.Count(o.IsMatch);
			}
		}

		public void SetupCurrent(SearchOption<T> option)
		{
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.Current = option;
		}

		public ObservableCollection<T> FindAll(ObservableCollection<T> items, SearchOption<T> option)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			var result = new List<T>();

			foreach (var v in items)
			{
				if (option.IsMatch(v))
				{
					result.Add(v);
				}
			}

			return new ObservableCollection<T>(result);
		}

		public ObservableCollection<T> Search(ObservableCollection<T> items, SearchOption<T> option)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.SetupCurrent(option);
			this.SetupCount(items);

			return this.FindAll(items, option);
		}
	}
}