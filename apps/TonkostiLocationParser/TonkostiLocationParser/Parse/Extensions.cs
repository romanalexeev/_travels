using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TonkostiLocationParser.Parse
{
	public static class Extensions
	{
		/// <returns>ла[from]Первый элемент[to]лалала[from]Второй элемент[to]лала</returns>
		public static List<string> Substrings(this string text, string from, string to)
		{
			List<string> items = new List<string>();

			// позиция начала открывающего тэга
			int openTagStartIndex = text.IndexOf(from);

			while (openTagStartIndex > -1)
			{
				// позиция начала содержимого
				int contentStartIndex = openTagStartIndex + from.Length;

				// позиция начала закрывающего тэга
				int closeTagStartIndex = text.IndexOf(to, contentStartIndex);

				if (closeTagStartIndex > -1)
				{
					// длина содержимого между открытием и закрытием тэга
					int itemLength = closeTagStartIndex - contentStartIndex;

					// содержимое между открытием и закрытием тэга
					string itemSubstring = text.Substring(contentStartIndex, itemLength);

					items.Add(itemSubstring);

					// позиция начала следующего открывающего тэга
					openTagStartIndex = text.IndexOf(from, closeTagStartIndex + to.Length);
				}
				else
				{
					break;
				}
			}

			return items;
		}


	}
}
