using System;
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

		public List<T> FindAll(List<T> viewModels, string value)
		{
			if (viewModels == null) throw new ArgumentNullException("viewModels");
			if (value == null) throw new ArgumentNullException("value");

			this.Value = value;

			if (viewModels.Count > 0 && value != string.Empty)
			{
				var result = new List<T>();

				foreach (var v in viewModels)
				{
					if (this.IsMatch(v, value))
					{
						result.Add(v);
					}
				}

				return result;
			}

			return viewModels;
		}

		public abstract bool IsMatch(T viewModel, string value);
	}
}