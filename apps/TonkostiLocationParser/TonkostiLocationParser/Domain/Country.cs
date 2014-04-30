using System;
using System.Collections.Generic;
using TonkostiLocationParser.Common;

namespace TonkostiLocationParser.Domain
{
	public class Country
	{
		private readonly string _name;
		private readonly string _url;
		private readonly bool _isHot;
		private readonly List<Location> _locations;

		public string Name { get { return _name; } }
		public string Url { get { return _url; } }
		public bool IsHot { get { return _isHot; } }
		public List<Location> Locations { get { return _locations; } }

		public Country(string name, string url, bool isHot, List<Location> locations = null)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("argument is null or whitespace", "name");

			if (string.IsNullOrWhiteSpace(url))
				throw new ArgumentException("argument is null or whitespace", "url");

			_name = name;
			_url = url;
			_isHot = isHot;

			_locations = locations != null
				? locations
				: new List<Location>();
		}

		public override string ToString()
		{
			return string.Format("Name={0}, Url={1}, IsHot={2}, Locations={3}", _name, _url, _isHot, _locations.Count);
		}

	}
}
