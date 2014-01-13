using System;
using System.Diagnostics;
using Blast.Core;
using Blast.Core.Search;
using Blast.Core.Sort;

namespace Blast.DemoConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var m = new UserViewManager();

			m.ModelSort = new UserViewModelSort(new UserOptionSort(new SortOption<UserViewModel>[]
			                                                       {
				                                                       new UserSortOption(@"By Name", (x,y)=>string.Compare(x.Name, y.Name, StringComparison.Ordinal), true), 
			                                                       }));
			m.ModelSearch = new UserViewModelSearch(new UserTextSearch());
			m.ModelSort = null;

			m.Load(new[]
			       {
				       new UserViewModel{Name = @"A"}, 
				       new UserViewModel{Name = @"C"}, 
			       });
			m.Search(@"D");

			foreach (var vm in m.ViewModels)
			{
				Console.WriteLine(vm.Name);
			}
			Console.WriteLine();

			//m.Search();

			var item = new UserViewModel { Name = @"B" };
			m.Insert(item);

			foreach (var vm in m.ViewModels)
			{
				Console.WriteLine(vm.Name);
			}
			Console.WriteLine();

			item.Name = @"D";
			m.Update(item);
			foreach (var vm in m.ViewModels)
			{
				Console.WriteLine(vm.Name);
			}
			Console.WriteLine();




		}
	}

	public class UserViewModel : ViewModel
	{
		public string Name { get; set; }
	}

	public class UserViewModelSort : ViewModelSort<UserViewModel>
	{
		public UserViewModelSort(OptionSort<UserViewModel> option)
			: base(option)
		{
		}
	}

	public class UserOptionSort : OptionSort<UserViewModel>
	{
		public UserOptionSort(SortOption<UserViewModel>[] options)
			: base(options)
		{
		}
	}

	public class UserSortOption : SortOption<UserViewModel>
	{
		public UserSortOption(string name, Comparison<UserViewModel> comparison, bool isDefault = false)
			: base(name, comparison, isDefault)
		{
		}
	}

	public class UserViewModelSearch : ViewModelSearch<UserViewModel>
	{
		public UserViewModelSearch(TextSearch<UserViewModel> input)
			: base(input)
		{
		}
	}

	public class UserTextSearch : TextSearch<UserViewModel>
	{
		public override bool IsMatch(UserViewModel viewModel, string value)
		{
			return viewModel.Name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
		}
	}

	public class UserViewManager : ViewManager<UserViewModel>
	{

	}
}
