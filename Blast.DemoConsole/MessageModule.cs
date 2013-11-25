﻿using System;
using System.Globalization;
using Blast.Core;
using Blast.Core.Search;

namespace Blast.DemoConsole
{
	public class MessageModule
	{
		 
	}

	public class Message : BaseObject
	{
		public string Name;

		public Message()
		{
			this.Name = string.Empty;
		}
	}

	public class MessageViewModel : ViewModel
	{
		public Message Message { get; private set; }

		public string Number { get; private set; }
		public string Name { get; private set; }

		public MessageViewModel(Message message)
		{
			if (message == null) throw new ArgumentNullException("message");

			this.Message = message;
			this.Number = message.Id.ToString(CultureInfo.InvariantCulture);
			this.Name = message.Name;
		}
	}

	public class MessageViewManager : ViewManager<MessageViewModel>
	{

	}

	public class MessageViewModelSearch : ViewModelSearch<MessageViewModel>
	{
		public MessageViewModelSearch(TextSearch<MessageViewModel> textSearch)
			: base(textSearch)
		{
		}

		public MessageViewModelSearch(OptionSearch<MessageViewModel> optionSearch)
			: base(optionSearch)
		{
		}

		public MessageViewModelSearch(TextSearch<MessageViewModel> textSearch, OptionSearch<MessageViewModel> optionSearch)
			: base(textSearch, optionSearch)
		{
		}
	}

	public class MessageViewModelTextSearch : TextSearch<MessageViewModel>
	{
		public override bool IsMatch(MessageViewModel viewModel, string value)
		{
			return viewModel.Number.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0 ||
				   viewModel.Name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;

		}
	}

	public class MessageViewModelSearchOption : SearchOption<MessageViewModel>
	{
		public MessageViewModelSearchOption(string name, Func<MessageViewModel, bool> search, bool isDefault = false)
			: base(name, search, isDefault)
		{
		}
	}

	public class MessageOptionSearch : OptionSearch<MessageViewModel>
	{
		public MessageOptionSearch(SearchOption<MessageViewModel>[] options)
			: base(options)
		{
		}
	}
}