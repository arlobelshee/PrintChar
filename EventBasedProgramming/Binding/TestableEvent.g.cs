using System;
using System.Collections.Generic;
using System.Linq;
using EventBasedProgramming.Binding.Impl;
using System.Reflection;

namespace EventBasedProgramming.Binding
{
	public class TestableEventBase
	{
		protected readonly List<BindingInfo> _handlers = new List<BindingInfo>();

		protected void _BindTo(BindingInfo handler)
		{
			_handlers.Add(handler);
		}

		protected void _UnbindFrom(MethodInfo method, object target)
		{
			_handlers.RemoveAll(h => h.Method == method && h.Target == target);
		}

		protected bool _IsBoundTo(MethodInfo method, object target)
		{
			return _handlers.Any(h => h.Method == method && h.Target == target);
		}
	}

	public class TestableEvent : TestableEventBase
	{
		public void BindTo(Action handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call()
		{
			_handlers.Each(h => h.Call());
		}
	}

	public class TestableEvent<TArg1> : TestableEventBase
	{
		public void BindTo(Action<TArg1> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1)
		{
			_handlers.Each(h => h.Call(arg1));
		}
	}

	public class TestableEvent<TArg1, TArg2> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2)
		{
			_handlers.Each(h => h.Call(arg1, arg2));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
		}
	}

	public class TestableEvent<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> : TestableEventBase
	{
		public void BindTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> handler)
		{
			_BindTo(new BindingInfo(handler.Method, handler.Target));
		}

		public void UnbindFrom(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}

		public bool IsBoundTo(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> handler)
		{
			return _IsBoundTo(handler.Method, handler.Target);
		}

		public void Call(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15, TArg16 arg16)
		{
			_handlers.Each(h => h.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
		}
	}
}
