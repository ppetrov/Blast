using System;
using NUnit.Framework;

namespace Blast.Core.Tests
{
	public class BaseOptionTests
	{
		[Test]
		public void Constructor_WithNullName_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new TestBaseOption(default(string)));
		}

		[Test]
		public void Constructor_WithNullName_ThrowsException2()
		{
			Assert.Throws<ArgumentNullException>(() => new TestBaseOption(default(string), true));
		}

		[Test]
		public void Constructor_Initialize_Properties()
		{
			var name = @"A";
			var o = new TestBaseOption(name);

			Assert.AreEqual(name, o.Name);
			Assert.AreEqual(false, o.IsDefault);
		}

		[Test]
		public void Constructor_Initialize_Properties2()
		{
			var name = @"A";
			var o1 = new TestBaseOption(name, true);

			Assert.AreEqual(name, o1.Name);
			Assert.AreEqual(true, o1.IsDefault);
		}
	}

	public class TestBaseOption : BaseOption
	{
		public TestBaseOption(string name, bool isDefault = false)
			: base(name, isDefault)
		{
		}
	}
}