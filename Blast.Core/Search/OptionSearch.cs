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
			this.Current = this.GetDefaultOrFirst();
		}

		public void SetupCount(ObservableCollection<T> items)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			foreach (var o in this.Options)
			{
				o.Count = items.Count(o.Match);
			}
		}

		public void SetupCurrent(SearchOption<T> option)
		{
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.Current = option;
		}

		public void Setup(ObservableCollection<T> items, SearchOption<T> option)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.SetupCurrent(option);
			this.SetupCount(items);
		}

		public ObservableCollection<T> FindAll(ObservableCollection<T> items, SearchOption<T> option)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (option == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Option);

			this.Setup(items, option);

			var matching = new List<T>();
			foreach (var v in items)
			{
				if (option.Match(v))
				{
					matching.Add(v);
				}
			}

			return new ObservableCollection<T>(matching);
		}

		private SearchOption<T> GetDefaultOrFirst()
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