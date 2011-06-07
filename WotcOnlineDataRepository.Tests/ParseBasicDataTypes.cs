using System;
using NUnit.Framework;

namespace WotcOnlineDataRepository.Tests
{
	[TestFixture]
	public class ParseBasicDataTypes
	{
		[Test]
		public void ToEnum()
		{
			const int validValue = (int)_TestEnum.ValidValue;
			Assert.That(validValue.To<_TestEnum>(), Is.EqualTo(_TestEnum.ValidValue));
		}

		[Test]
		public void ToEnumFailsWithInvalidValue()
		{
			Assert.Throws(typeof (InvalidCastException), () => 3339.To<_TestEnum>());
		}

		private enum _TestEnum
		{
			ValidValue = 3,
		}
	}
}
