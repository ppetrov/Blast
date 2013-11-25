using System;

namespace Blast.Core
{
	public abstract class BaseOption : ViewModel
	{
		public string Name { get; private set; }
		public bool IsDefault { get; private set; }

		protected BaseOption(string name, bool isDefault = false)
		{
			if (name == null) throw new ArgumentNullException("name");

			this.Name = name;
			this.IsDefault = isDefault;
		}
	}
}