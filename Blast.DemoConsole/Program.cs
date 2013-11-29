using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blast.Core;
using Blast.Core.Search;

namespace Blast.DemoConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var current = new List<string> { @"B", @"C" };
			var items = new List<string> { @"A", @"B", @"C" }; 

			var inputCount = items.Count;
			var viewModels = current;
			var common = Math.Min(viewModels.Count, inputCount);
			for (var i = 0; i < common; i++)
			{
				viewModels[i] = items[i];
			}
			while (viewModels.Count > inputCount)
			{
				viewModels.RemoveAt(viewModels.Count - 1);
			}
			while (viewModels.Count < inputCount)
			{
				var index = viewModels.Count;
				viewModels.Insert(index, items[index]);
			}

			Console.WriteLine(current.Count);
			foreach (var v in current)
			{
				Console.WriteLine(v);
			}

			return;
			try
			{
				var m = new MessageViewManager();
				m.ModelSearch = new MessageViewModelSearch(new MessageViewModelTextSearch(),
					new MessageOptionSearch(new SearchOption<MessageViewModel>[]
					                        {
						                        new MessageViewModelSearchOption(@"All", v => true, true),
						                        new MessageViewModelSearchOption(@"A", v => v.Name == @"A"),
						                        new MessageViewModelSearchOption(@"B", v => v.Name == @"B"),
					                        }));
				m.ModelSearch = null;
				m.ModelSort = null;
				m.Load(new List<MessageViewModel>
				       {
					       new MessageViewModel(new Message {Name = @"A"}),
					       new MessageViewModel(new Message {Name = @"B"}),
					       //new MessageViewModel(new Message {Name = @"B1"}),
				       });

				DisplayView(m);

				// Reset text filter
				m.Search(string.Empty);

				DisplayView(m);

				// Set to 'B'
				//m.IsMatch(m.ModelSearch.input.Options.Last());

				DisplayView(m);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private static void DisplayView(MessageViewManager m)
		{
			var ms = m.ModelSearch;
			if (ms != null)
			{
				Console.WriteLine("'" + ms.TextSearch.Current + "'");
				foreach (var o in ms.OptionSearch.Options)
				{
					var v = o.Name + "(" + o.Count + ")";
					if (ms.OptionSearch.Current == o)
					{
						v += "*";
					}
					Console.WriteLine(v);
				}
			}

			Console.WriteLine();
			foreach (var viewModel in m.ViewModels)
			{
				Console.WriteLine(viewModel.Name);
			}
			Console.WriteLine();
		}
	}
}
