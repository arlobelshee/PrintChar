using System;
using System.IO;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	public static class Convert
	{
		private static TEnum _Enum<TEnum>(object value)
		{
			var enumType = typeof (TEnum);
			if (!Enum.IsDefined(enumType, value))
				throw new InvalidCastException(String.Format("{0} is not a valid value for enumeration {1}", value, enumType));
			return (TEnum) Enum.ToObject(enumType, value);
		}

		public static T To<T>(this int value)
		{
			return _Enum<T>(value);
		}

		public static T To<T>(this uint value)
		{
			return _Enum<T>(value);
		}

		public static T To<T>(this long value)
		{
			return _Enum<T>(value);
		}

		public static T To<T>(this ulong value)
		{
			return _Enum<T>(value);
		}

		public static T To<T>(this short value)
		{
			return _Enum<T>(value);
		}

		public static T To<T>(this ushort value)
		{
			return _Enum<T>(value);
		}

		public static T To<T>(this byte value)
		{
			return _Enum<T>(value);
		}

		public static T To<T>(this sbyte value)
		{
			return _Enum<T>(value);
		}

		public static int? NInt(string value)
		{
			int result;
			return Int32.TryParse(value, out result) ? (int?) result : null;
		}

		[NotNull]
		public static HtmlNode ToDetailsNode([NotNull] this string text)
		{
			var doc = new HtmlDocument();
			doc.Load(new StringReader(text));
			var result = doc.DocumentNode.SelectSingleNode("//div[@id='detail']");
			if (result == null)
				throw new ParseError("Attempted to parse data that was not a valid compendium result. Failed to find <div id='detail'>. Please verify your data source.");
			return result;
		}
	}
}
