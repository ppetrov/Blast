using System;
using System.Collections.Generic;
using System.Linq;

namespace Blast.Core.Search
{
	public abstract class OptionSearch<T> where T : ViewModel
	{
		public SearchOption<T> Current { get; private set; }
		public SearchOption<T>[] Options { get; private set; }

		protected OptionSearch(SearchOption<T>[] options)
		{
			if (options == null) throw new ArgumentNullException("options");
			if (options.Length == 0) throw new ArgumentOutOfRangeException("options");

			this.Options = options;
			this.Current = this.GetDefaultOrFirst();
		}

		public SearchOption<T> GetDefaultOrFirst()
		{
			foreach (var o in this.Options)
			{
				if (o.IsDefault)
				{
					return o;
				}
			}
			return this.Options.First();
		}

		public void SetupCount(List<T> viewModels)
		{
			if (viewModels == null) throw new ArgumentNullException("viewModels");

			foreach (var o in this.Options)
			{
				o.Count = viewModels.Count(o.Search);
			}
		}

		public void SetupCurrent(SearchOption<T> option)
		{
			if (option == null) throw new ArgumentNullException("option");

			this.Current = option;
		}

		public void Setup(List<T> viewModels, SearchOption<T> option)
		{
			if (viewModels == null) throw new ArgumentNullException("viewModels");
			if (option == null) throw new ArgumentNullException("option");

			this.SetupCurrent(option);
			this.SetupCount(viewModels);
		}

		public List<T> FindAll(List<T> viewModels, SearchOption<T> option)
		{
			if (viewModels == null) throw new ArgumentNullException("viewModels");
			if (option == null) throw new ArgumentNullException("option");

			this.Setup(viewModels, option);

			var matching = new List<T>();
			foreach (var v in viewModels)
			{
				if (option.Search(v))
				{
					matching.Add(v);
				}
			}
			return matching;
		}
	}
}