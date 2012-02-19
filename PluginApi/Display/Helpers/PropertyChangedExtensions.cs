using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace PluginApi.Display.Helpers
{
	public static class PropertyChangedExtensions
	{
		public static void Raise(this PropertyChangedEventHandler handler, object sender, [NotNull] Expression<Func<object>> propertyExpression)
		{
			if (handler == null)
				return;
			var e = new PropertyChangedEventArgs(_PropertyNameFrom(propertyExpression));
			handler(sender, e);
		}

		public static void PropagateFrom<TSource>(this PropertyChangedEventHandler handler, object sender, [NotNull] Expression<Func<object>> propertyExpression,
			[NotNull] TSource originator, [NotNull] Expression<Func<TSource, object>> whenThisFiresExpression) where TSource : INotifyPropertyChanged
		{
			var propertyToPropagate = _PropertyNameFrom(whenThisFiresExpression);
			originator.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == propertyToPropagate)
					handler.Raise(sender, propertyExpression);
			};
		}

		[NotNull]
		private static string _PropertyNameFrom([NotNull] Expression<Func<object>> propertyExpression)
		{
			var body = _GetMemberExpression(propertyExpression);
			_VerifyIsExpressionOfType<ConstantExpression>(body);

			// Extract the name of the property
			return body.Member.Name;
		}

		[NotNull]
		private static string _PropertyNameFrom<T>([NotNull] Expression<Func<T, object>> propertyExpression)
		{
			var body = _GetMemberExpression(propertyExpression);
			_VerifyIsExpressionOfType<ParameterExpression>(body);

			// Extract the name of the property
			return body.Member.Name;
		}

		private static void _VerifyIsExpressionOfType<TExpected>([NotNull] MemberExpression body) where TExpected : class
		{
			if (!(body.Expression is TExpected))
				throw new ArgumentException("The expression body should be a simple direct access to the appropriate property. Please simplify your lambda.");
		}

		[NotNull]
		private static MemberExpression _GetMemberExpression([NotNull] Expression<Func<object>> propertyExpression)
		{
			MemberExpression body;
			if (propertyExpression.Body.NodeType == ExpressionType.Convert)
				body = ((UnaryExpression) propertyExpression.Body).Operand as MemberExpression;
			else
				body = propertyExpression.Body as MemberExpression;
			if (body == null)
				throw new ArgumentException("'propertyExpression' should be a member expression");
			return body;
		}

		[NotNull]
		private static MemberExpression _GetMemberExpression<T>([NotNull] Expression<Func<T, object>> propertyExpression)
		{
			MemberExpression body;
			if (propertyExpression.Body.NodeType == ExpressionType.Convert)
				body = ((UnaryExpression) propertyExpression.Body).Operand as MemberExpression;
			else
				body = propertyExpression.Body as MemberExpression;
			if (body == null)
				throw new ArgumentException("'propertyExpression' should be a member expression");
			return body;
		}
	}
}
