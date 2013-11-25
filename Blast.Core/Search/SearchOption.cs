using System;

namespace Blast.Core.Search
{
	public abstract class SearchOption<T> : BaseOption
		where T : ViewModel
	{
		public Func<T, bool> Match { get; private set; }
		private int _count;
		public int Count
		{
			get { return _count; }
			set { this.SetProperty(ref _count, value); }
		}

		protected SearchOption(string name, Func<T, bool> match, bool isDefault = false)
			: base(name, isDefault)
		{
			if (match == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Match);

			this.Match = match;
		}
	}
}