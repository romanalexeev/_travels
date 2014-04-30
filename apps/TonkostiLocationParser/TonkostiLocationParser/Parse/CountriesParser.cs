using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using TonkostiLocationParser.Domain;

namespace TonkostiLocationParser.Parse
{
	public class CountriesParser
	{
		public List<Country> Parse(bool withLocations = false)
		{
			List<Country> countryList = new List<Country>();

			string html;

			using (WebClient client = new WebClient())
			{
				html = Request.GetWebText(
					url: "http://tonkosti.ru/", 
					encodingName: "utf-8");
			}

			string tableOfCountries = html
				.Substrings(
					"<!-- Start: Table of countries (displaying element) -->", 
					"<!-- End: Table of countries (displaying element) -->")
				.First();

			string countryPattern = "<li(?<class>.*)><a href=\"(?<href>.*)\">(?<inner>.*)</a></li>";

			if (!Regex.IsMatch(tableOfCountries, countryPattern))
				throw new Exception("countryPattern not matched");

			MatchCollection matches = Regex.Matches(tableOfCountries, countryPattern);

			foreach (Match match in matches)
			{
				Country country = new Country(
					name: match.Result("${inner}"),
					url: match.Result("${href}"),
					isHot: match.Result("${class}").Contains("hots"));

				countryList.Add(country);
			}

			return countryList;
		}
	}
}
