using System;
using System.Collections.Generic;

namespace Blast.Core
{
	public sealed class Cache
	{
		private readonly Dictionary<Type, object> _values = new Dictionary<Type, object>();

		public void Add<T>(Dictionary<long, T> items) where T : ViewModel
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			_values.Add(typeof(T), items);
		}

		public void Replace<T>(Dictionary<long, T> items) where T : ViewModel
		{
			if (items == null) ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.Items);

			_values[typeof(T)] = items;
		}

		public void Clear()
		{
			_values.Clear();
		}

		public Dictionary<long, T> Get<T>() where T : ViewModel
		{
			return (Dictionary<long, T>)_values[typeof(T)];
		}
	}
}