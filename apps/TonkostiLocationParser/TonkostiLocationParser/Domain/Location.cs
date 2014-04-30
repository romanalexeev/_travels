using System;
using System.Collections.Generic;
using System.Linq;
using TonkostiLocationParser.Common;

namespace TonkostiLocationParser.Domain
{
	public class Location
	{
		private readonly string _name;
		private readonly int _level;
		private bool _isCapital;
		private readonly List<Location> _locations;

		public string Name { get { return _name; } }
		public int Level { get { return _level; } }
		public bool IsCapital { get { return _isCapital; } }
		public List<Location> Locations { get { return _locations; } }

		public Location(string name, int level, bool isCapital, List<Location> locations = null)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("argument is null or whitespace", "name");

			_name = name;
			_level = level; 
			_isCapital = isCapital;

			_locations = locations != null
				? locations
				: new List<Location>();
		}

		public override string ToString()
		{
			return string.Format("Name={0}, Level={1}, IsCapital={2}, Locations={3}", _name, _level, _isCapital, _locations.Count);
		}

	}
}
