using System;
using System.ComponentModel;
using System.Reflection;
using Blast.Core.Search;
using NSubstitute;
using NUnit.Framework;

namespace Blast.Core.Tests.Search
{
	public class SearchOptionTests
	{
		[Test]
		public void Contructor_WithNullName_ThrowsException()
		{
			Assert.Throws<TargetInvocationException>(() => Substitute.For<SearchOption<ViewModel>>(null, null, false));
		}

		[Test]
		public void Contructor_WithNullMatch_ThrowsException()
		{
			Assert.Throws<TargetInvocationException>(() => Substitute.For<SearchOption<ViewModel>>(string.Empty, null, false));
		}

		[Test]
		public void Contructor_Initialize_Properties()
		{
			var name = @"A";
			Func<ViewModel, bool> f = v => true;

			var o = Substitute.For<SearchOption<ViewModel>>(name, f, false);

			Assert.AreEqual(name, o.Name);
			Assert.AreSame(f, o.IsMatch);
		}

		[Test]
		public void Count_SetValue_FiresEvent()
		{
			var value = 23;
			var name = @"A";
			Func<ViewModel, bool> f = v => true;

			var fired = false;
			var o = Substitute.For<SearchOption<ViewModel>>(name, f, false);
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