using System;

namespace Blast.Core.Search
{
	public abstract class SearchOption<T> : BaseOption
		where T : ViewModel
	{
		public Func<T, bool> IsMatch { get; private set; }
		private int _count;
		public int Count
		{
			get { return _count; }
			set { this.SetProperty(ref _count, value); }
		}

		protected SearchOption(string name, Func<T, bool> isMatch, bool isDefault = false)
			: base(name, isDefault)
		{
			if (isMatch == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Match);

			this.IsMatch = isMatch;
		}
	}
}