using System;
using System.ComponentModel;
using Blast.Core.Search;
using NUnit.Framework;

namespace Blast.Core.Tests.Search
{
	public class SearchOptionTests
	{
		[Test]
		public void Contructor_WithNullName_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new TestSearchOption(null, null));
		}

		[Test]
		public void Contructor_WithNullMatch_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new TestSearchOption(string.Empty, null));
		}

		[Test]
		public void Contructor_Initialize_Properties()
		{
			var name = @"A";
			Func<TestViewModel, bool> f = v => true;

			var o = new TestSearchOption(name, f);

			Assert.AreEqual(name, o.Name);
			Assert.AreSame(f, o.IsMatch);
		}

		[Test]
		public void Count_SetValue_FiresEvent()
		{
			var value = 23;
			var name = @"A";
			Func<TestViewModel, bool> f = v => true;

			var fired = false;
			var o = new TestSearchOption(name, f);
			PropertyChangedEventHandler handler = (sender, args) => { fired = true; };
			try
			{
				o.PropertyChanged += handler;
				o.Count = value;
			}
			finally
			{
				o.PropertyChanged -= handler;
			}

			Assert.AreEqual(name, o.Name);
			Assert.AreSame(f, o.IsMatch);
			Assert.AreEqual(value, o.Count);
			Assert.IsTrue(fired);
		}
	}

	public class TestSearchOption : SearchOption<TestViewModel>
	{
		public TestSearchOption(string name, Func<TestViewModel, bool> isMatch, bool isDefault = false)
			: base(name, isMatch, isDefault)
		{
		}
	}
}