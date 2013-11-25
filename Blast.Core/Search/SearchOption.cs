using System;

namespace Blast.Core.Search
{
	public abstract class SearchOption<T> : BaseOption
		where T : ViewModel
	{
		public Func<T, bool> Search { get; private set; }
		private int _count;
		public int Count
		{
			get { return _count; }
			set { this.SetProperty(ref _count, value); }
		}

		protected SearchOption(string name, Func<T, bool> search, bool isDefault = false)
			: base(name, isDefault)
		{
			if (search == null) throw new ArgumentNullException("search");

			this.Search = search;
		}
	}
}