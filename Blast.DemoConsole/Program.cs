using System;
using System.Collections.Generic;
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
				//m.Search(m.ModelSearch.OptionSearch.Options.Last());

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
				Console.WriteLine("'" + ms.TextSearch.Value + "'");
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
