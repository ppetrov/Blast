using System;

namespace Blast.Core.Sort
{
	public abstract class SortOption<T> : BaseOption
		where T : ViewModel
	{
		public Comparison<T> Sort { get; private set; }
		private SortDirection? _direction;
		public SortDirection? Direction
		{
			get { return _direction; }
			set { this.SetProperty(ref _direction, value); }
		}

		protected SortOption(string name, Comparison<T> sort, bool isDefault = false)
			: base(name, isDefault)
		{
			if (sort == null) throw new ArgumentNullException("sort");

			this.Sort = sort;
		}
	}
}