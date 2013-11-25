using System.Collections.Generic;

namespace Blast.Core.Search
{
	public abstract class TextSearch<T> where T : ViewModel
	{
		public string Value { get; private set; }

		protected TextSearch()
		{
			this.Value = string.Empty;
		}

		public List<T> FindAll(List<T> items, string value)
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);
			if (value == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Value);

			this.Value = value;

			if (items.Count > 0 && value != string.Empty)
			{
				var result = new List<T>();

				foreach (var v in items)
				{
					if (this.IsMatch(v, value))
					{
						result.Add(v);
					}
				}

				return result;
			}

			return items;
		}

		public abstract bool IsMatch(T viewModel, string value);
	}
}