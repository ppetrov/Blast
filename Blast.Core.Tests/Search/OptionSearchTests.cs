using System;
using System.Collections.ObjectModel;
using Blast.Core.Search;
using NUnit.Framework;

namespace Blast.Core.Tests.Search
{
	public class OptionSearchTests
	{
		private TestOptionSearch CreateOptionSearch()
		{
			return this.CreateOptionSearch(new SearchOption<TestViewModel>[]
			                               {
				                               new TestSearchOption(string.Empty, v => true),
				                               new TestSearchOption(string.Empty, v => true, true),
			                               });
		}

		private TestOptionSearch CreateOptionSearch(SearchOption<TestViewModel>[] options)
		{
			return new TestOptionSearch(options);
		}

		[Test]
		public void Constructor_WithNullOptions_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new TestOptionSearch(null));
		}

		[Test]
		public void Constructor_WithEmptyOptions_ThrowsException()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new TestOptionSearch(new SearchOption<TestViewModel>[0]));
		}

		[Test]
		public void Constructor_InitializeCurrent_WithFirstWhenNoDefault()
		{
			var options = new SearchOption<TestViewModel>[]
			              {
				              new TestSearchOption(string.Empty, v => true),
				              new TestSearchOption(string.Empty, v => true),
			              };

			var t = new TestOptionSearch(options);

			Assert.AreSame(options, t.Options);
			Assert.AreSame(options[0], t.Current);
			foreach (var option in options)
			{
				Assert.IsFalse(option.IsDefault);
			}
		}

		[Test]
		public void Constructor_InitializeCurrent_WithDefaultOption()
		{
			var options = new SearchOption<TestViewModel>[]
			                    {
				                    new TestSearchOption(string.Empty, v => true),
				                    new TestSearchOption(string.Empty, v => true, true),
			                    };
			var t = this.CreateOptionSearch(options);

			Assert.AreSame(options, t.Options);
			Assert.AreSame(options[1], t.Current);
		}

		[Test]
		public void SetupCount_ThrowsException_WithNullItems()
		{
			var t = this.CreateOptionSearch();

			Assert.Throws<ArgumentNullException>(() => t.SetupCount(null));
		}

		[Test]
		public void SetupCount_SetCount_ForAllOptions()
		{
			var t = this.CreateOptionSearch();
			var items = new ObservableCollection<TestViewModel>();
			items.Add(new TestViewModel());

			t.SetupCount(items);

			Assert.IsNotEmpty(t.Options);
			foreach (var o in t.Options)
			{
				Assert.AreEqual(1, o.Count);
			}
		}

		[Test]
		public void SetupCurrent_ThrowsException_WithNullOption()
		{
			var t = this.CreateOptionSearch();

			Assert.Throws<ArgumentNullException>(() => t.SetupCurrent(null));
		}

		[Test]
		public void SetupCurrent_SetCount_ForAllOptions()
		{
			var t = this.CreateOptionSearch();
			var items = new ObservableCollection<TestViewModel>();
			items.Add(new TestViewModel());

			t.SetupCurrent(t.Options[0]);

			Assert.AreSame(t.Current, t.Options[0]);
		}

		[Test]
		public void FindAll_WithNullItems_ThrowsException()
		{
			var t = this.CreateOptionSearch();
			Assert.Throws<ArgumentNullException>(() => t.FindAll(null, null));
		}

		[Test]
		public void FindAll_WithNullOption_ThrowsException()
		{
			var t = this.CreateOptionSearch();
			Assert.Throws<ArgumentNullException>(() => t.FindAll(new ObservableCollection<TestViewModel>(), null));
		}

		[Test]
		public void FindAll_WithItems_ReturnMatchingItems()
		{
			var t = this.CreateOptionSearch(new SearchOption<TestViewModel>[]
			                                {
				                                new TestSearchOption(string.Empty, v =>
				                                                                   {
					                                                                   v.Name = @"matched";
					                                                                   return true;
				                                                                   }),
			                                });
			var items = new ObservableCollection<TestViewModel>();
			items.Add(new TestViewModel { Name = @"A" });
			items.Add(new TestViewModel { Name = @"B" });

			var result = t.FindAll(items, t.Options[0]);

			Assert.AreEqual(2, result.Count);
			foreach (var v in result)
			{
				Assert.AreEqual(@"matched", v.Name);
			}
		}

		[Test]
		public void Search_WithNullItems_ThrowsException()
		{
			var t = this.CreateOptionSearch();
			Assert.Throws<ArgumentNullException>(() => t.Search(null, null));
		}

		[Test]
		public void Search_WithNullOption_ThrowsException()
		{
			var t = this.CreateOptionSearch();
			Assert.Throws<ArgumentNullException>(() => t.Search(new ObservableCollection<TestViewModel>(), null));
		}

		[Test]
		public void Search_WithItems_ReturnMatchingItems()
		{
			var t = this.CreateOptionSearch(new SearchOption<TestViewModel>[]
			                                {
				                                new TestSearchOption(string.Empty, v =>
				                                                                   {
					                                                                   v.Name = @"matched";
					                                                                   return true;
				                                                                   }),
			                                });
			var items = new ObservableCollection<TestViewModel>();
			items.Add(new TestViewModel { Name = @"A" });
			items.Add(new TestViewModel { Name = @"B" });

			var result = t.Search(items, t.Options[0]);

			Assert.AreEqual(2, result.Count);
			foreach (var v in result)
			{
				Assert.AreEqual(@"matched", v.Name);
			}
			Assert.AreSame(items[0], result[0]);
		}
	}

	public class TestOptionSearch : OptionSearch<TestViewModel>
	{
		public static TestOptionSearch Create()
		{
			return new TestOptionSearch(new [] { new TestSearchOption(string.Empty, v => true), });
		}

		public TestOptionSearch(SearchOption<TestViewModel>[] options)
			: base(options)
		{
		}
	}
}