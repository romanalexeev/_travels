using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TonkostiLocationParser.Parse
{
	public static class Request
	{
		public static string GetWebText(string url, string encodingName = "windows-1251")
		{
			byte[] data;

			using (WebClient client = new WebClient())
			{
				data = client.DownloadData(url);
			}

			string text = Encoding.GetEncoding(encodingName).GetString(data);
			return text;
		}

		private static string ConvertStringEncoding(Encoding encodingFrom, Encoding encodingTo, string content)
		{
			byte[] fromBytes = encodingFrom.GetBytes(content);
			byte[] toBytes = Encoding.Convert(encodingFrom, encodingTo, fromBytes);
			string converted = encodingTo.GetString(toBytes);
			return converted;
		}
	}
}
