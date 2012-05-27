using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace EventBasedProgramming.Tests.MakeBindingsTestable
{
	[TestFixture]
	public class HelperClassMakesEventSupportQuerying
	{
		private int _arg;

		[Test]
		public void EventShouldSupportRegistrationOfHandlersThatCanBeQueried()
		{
			var testSubject = new TestableEvent<int>();
			_arg = -1;
			testSubject.BindTo(_Target);
			_arg.Should().Be(-1);
			testSubject.Call(3);
			_arg.Should().Be(3);
		}

		private void _Target(int arg)
		{
			_arg = arg;
		}
	}

	public class TestableEvent<TArg1>
	{
		private readonly List<Action<TArg1>> _handlers = new List<Action<TArg1>>();

		public void BindTo(Action<TArg1> handler)
		{
			_handlers.Add(handler);
		}

		public void Call(TArg1 arg1)
		{
			_handlers.Each(h => h(arg1));
		}
	}
}
