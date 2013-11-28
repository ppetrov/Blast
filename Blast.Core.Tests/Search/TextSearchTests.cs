using System;
using System.Collections.ObjectModel;
using Blast.Core.Search;
using NUnit.Framework;

namespace Blast.Core.Tests.Search
{
	public class TextSearchTests
	{
		[Test]
		public void SetupCurrent_WithNullItems_ThrowsException()
		{
			var t = CreateTextSearch();
			Assert.Throws<ArgumentNullException>(() => t.SetupCurrent(null));
		}

		[Test]
		public void SetupCurrent_WithValue_SetCurrent()
		{
			var t = CreateTextSearch();

			Assert.AreEqual(string.Empty, t.Current);

			t.SetupCurrent(@"A");

			Assert.AreEqual(@"A", t.Current);
		}

		[Test]
		public void FindAll_WithNullItems_ThrowsException()
		{
			var t = CreateTextSearch();
			Assert.Throws<ArgumentNullException>(() => t.FindAll(null, null));
		}

		[Test]
		public void FindAll_WithNullValue_ThrowsException()
		{
			var t = CreateTextSearch();
			Assert.Throws<ArgumentNullException>(() => t.FindAll(new ObservableCollection<TestViewModel>(), null));
		}

		[Test]
		public void FindAll_WithItems_CallsIsMatchForEveryItem()
		{
			var t = CreateTextSearch();
			var items = new ObservableCollection<TestViewModel>();
			items.Add(new TestViewModel { Name = @"A" });
			items.Add(new TestViewModel { Name = @"B" });

			var result = t.FindAll(items, "A");

			Assert.AreEqual(2, result.Count);
			foreach (var v in result)
			{
				Assert.AreEqual(@"matched", v.Name);
			}
		}

		[Test]
		public void Search_WithNullItems_ThrowsException()
		{
			var t = CreateTextSearch();
			Assert.Throws<ArgumentNullException>(() => t.Search(null, null));
		}

		[Test]
		public void Search_WithNullValue_ThrowsException()
		{
			var t = CreateTextSearch();
			Assert.Throws<ArgumentNullException>(() => t.Search(new ObservableCollection<TestViewModel>(), null));
		}

		[Test]
		public void Search_WithItems_CallsIsMatchForEveryItem()
		{
			var t = CreateTextSearch();
			var items = new ObservableCollection<TestViewModel>();
			items.Add(new TestViewModel { Name = @"A" });
			items.Add(new TestViewModel { Name = @"B" });

			var result = t.Search(items, "A");

			Assert.AreEqual(@"A", t.Current);
			Assert.AreEqual(2, result.Count);
			foreach (var v in result)
			{
				Assert.AreEqual(@"matched", v.Name);
			}
		}

		private static TestTextSearch CreateTextSearch()
		{
			return new TestTextSearch();
		}
	}

	public class TestTextSearch : TextSearch<TestViewModel>
	{
		public override bool IsMatch(TestViewModel viewModel, string value)
		{
			viewModel.Name = @"matched";
			return true;
		}
	}
}