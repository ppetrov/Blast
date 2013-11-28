using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blast.Core.Search
{
	public abstract class TextSearch<T> where T : ViewModel
	{
		public string Current { get; private set; }

		protected TextSearch()
		{
			this.Current = string.Empty;
		}

		public void SetupCurrent(string value)
		{
			if (value == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Value);

			this.Current = value;
		}

		public ObservableCollection<T> FindAll(ObservableCollection<T> items, string value)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (value == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Value);

			var result = new List<T>();

			foreach (var v in items)
			{
				if (this.IsMatch(v, value))
				{
					result.Add(v);
				}
			}

			return new ObservableCollection<T>(result);
		}

		public ObservableCollection<T> Search(ObservableCollection<T> items, string value)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (value == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Value);

			this.SetupCurrent(value);

			return this.FindAll(items, value);
		}

		public abstract bool IsMatch(T viewModel, string value);
	}
}