using System;
using JetBrains.Annotations;

namespace PluginApi
{
	public static class Require
	{
		[AssertionMethod]
		public static void That([AssertionCondition(AssertionConditionType.IS_TRUE)] bool condition,
			Func<Exception> failureMessage)
		{
			if (!condition)
				throw failureMessage();
		}

		[AssertionMethod]
		public static void NotNull([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] object o)
		{
			Not(o == null, () => new ArgumentNullException());
		}

		[AssertionMethod]
		public static void Not([AssertionCondition(AssertionConditionType.IS_FALSE)] bool condition, Func<Exception> failureMessage)
		{
			if (condition)
				throw failureMessage();
		}
	}
}
