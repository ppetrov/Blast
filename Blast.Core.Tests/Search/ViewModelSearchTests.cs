using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Blast.Core.Search;
using NUnit.Framework;

namespace Blast.Core.Tests.Search
{
	public class ViewModelSearchTests
	{
		[Test]
		public void Constructor_WithNullTextSearch_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new TestViewModelSearch(default(TextSearch<TestViewModel>)));
		}

		[Test]
		public void Constructor_WithNullOptionSearch_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new TestViewModelSearch(default(OptionSearch<TestViewModel>)));
		}

		[Test]
		public void Constructor_WithNullSearch_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new TestViewModelSearch(null, TestOptionSearch.Create()));
		}

		[Test]
		public void Constructor_WithNullSearch_ThrowsException2()
		{
			Assert.Throws<ArgumentNullException>(() => new TestViewModelSearch(new TestTextSearch(), null));
		}

		[Test]
		public void Search_WithNullItems_ThrowsException()
		{
			var t = new TestViewModelSearch(new TestTextSearch(), TestOptionSearch.Create());

			Assert.Throws<ArgumentNullException>(() => t.Search(null));
		}

		[Test]
		public void Search_WithTextValue_PerformTextSearch()
		{
			var t = new TestViewModelSearch(new TestTextSearch());

			t.Search(new ObservableCollection<TestViewModel>(), @"A");
		}
	}

	public class TestViewModelSearch : ViewModelSearch<TestViewModel>
	{
		public TestViewModelSearch(TextSearch<TestViewModel> input)
			: base(input)
		{
		}

		public TestViewModelSearch(OptionSearch<TestViewModel> input)
			: base(input)
		{
		}

		public TestViewModelSearch(TextSearch<TestViewModel> x, OptionSearch<TestViewModel> y)
			: base(x, y)
		{
		}
	}
}