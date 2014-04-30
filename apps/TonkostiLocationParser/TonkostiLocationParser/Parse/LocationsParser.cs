using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TonkostiLocationParser.Domain;

namespace TonkostiLocationParser.Parse
{
	public class LocationsParser
	{
		public List<Location> Parse(Country country)
		{
			if (country == null)
				throw new ArgumentNullException("country");

			List<Location> locationList = new List<Location>();

			string html1;

			using (WebClient client = new WebClient())
			{
				html1 = Request
					.GetWebText(
						url: string.Format("http://tonkosti.ru{0}", country.Url),
						encodingName: "utf-8");
			}

			// ищем в тексте страницы ссылку на "Города и курорты"
			string locationsLinkPattern = "<a href=\"(?<href>[^<]*)\" title=\"[^<]*\">Города и курорты</a>";

			if (!Regex.IsMatch(html1, locationsLinkPattern, RegexOptions.CultureInvariant | RegexOptions.Multiline))
				return locationList;

			Match hrefMatch = Regex.Match(html1, locationsLinkPattern, RegexOptions.CultureInvariant | RegexOptions.Multiline);

			string locationsUrl = hrefMatch.Result("${href}");


			string html2;

			using (WebClient client = new WebClient())
			{
				html2 = Request
					.GetWebText(
						url: string.Format("http://tonkosti.ru{0}", locationsUrl),
						encodingName: "utf-8")
					.Replace("\n", string.Empty);
			}

			// ищем в тексте страницы "Города и курорты" локации сгруппированные по регионам (исключаем список по алфавиту)
			string byRegionsTablePattern = "<div id=\"RegionsListByRegions\">(?<by_regions>.*)</div><noindex><div id=\"RegionsListByAlphabet\" style=\"display: none;\">";

			Match byRegionsTableMatch = Regex.Match(html2, byRegionsTablePattern, RegexOptions.CultureInvariant | RegexOptions.Multiline);

			string byRegionsTable = byRegionsTableMatch.Result("${by_regions}");


			// загружаем фрагмент как xml

			XDocument doc = XDocument.Parse(byRegionsTable);

			var level1 = doc
				.Root
				.Elements("tr")
				.SelectMany(tr => tr.Elements("td"))
				.SelectMany(td => td.Elements("ul"))
				.Where(ul => ul.Attribute("class").Value == "Level1")
				.SelectMany(ul => ul.Elements("li"))
				.ToList();

			foreach (var liL1 in level1)
			{
				string nameL1 = liL1.Element("a") != null
					? liL1.Element("a").Value
					: liL1.FirstNode.ToString();

				bool isCapitalL1 = liL1.Attribute("class") != null
					? liL1.Attribute("class").Value == "Capital"
					: false;

				var level2 = liL1
					.Elements("ul")
					.Where(ul => ul.Attribute("class").Value == "Level2")
					.SelectMany(ul => ul.Elements("li"))
					.ToList();

				Location locationL1 = new Location(
					name: nameL1,
					level: 1,
					isCapital: isCapitalL1);

				foreach (var liL2 in level2)
				{
					string nameL2 = liL2.Element("a") != null
						? liL2.Element("a").Value
						: liL2.FirstNode.ToString();

					bool isCapitalL2 = liL2.Attribute("class") != null
						? liL2.Attribute("class").Value == "Capital"
						: false;

					var level3 = liL2
						.Elements("div")
						.SelectMany(div => div.Elements("span"))
						.ToList();

					Location locationL2 = new Location(
						name: nameL2,
						level: 2,
						isCapital: isCapitalL2);

					foreach (var spanL3 in level3)
					{
						string nameL3 = spanL3.Element("a").Value;

						Location locationL3 = new Location(
							name: nameL3,
							level: 3,
							isCapital: false);	// может ли элемент третьего уровня быть столицей? если да - то как определить?

						locationL2.Locations.Add(locationL3);
					}

					locationL1.Locations.Add(locationL2);
				}

				locationList.Add(locationL1);
			}


			return locationList;
		}
	}
}
