using SQLite;
using System;
using System.Collections.Generic;
using TonkostiLocationParser.Domain;

namespace TonkostiLocationParser.Db
{
	public static class SqliteCountriesSaver
	{
		private static LocationDataObject GetLocationDataObject(Location location, int? parent_id, int country_id)
		{
			if (location == null)
				throw new ArgumentNullException("location");

			LocationDataObject location1DataObject = new LocationDataObject
			{
				country_id = country_id,
				parent_id = parent_id,
				name = location.Name,
				level = location.Level,
				is_capital = location.IsCapital ? 1 : 0
			};

			return location1DataObject;
		}

		public static void Save(IEnumerable<Country> countryList, string connectionString)
		{
			if (countryList == null)
				throw new ArgumentNullException("countryList");

			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				foreach (Country country in countryList)
				{
					CountryDataObject countryDataObject = new CountryDataObject
					{
						name = country.Name,
						is_important = country.IsHot ? 1 : 0
					};

					connection.Insert(countryDataObject);

					foreach (Location location1 in country.Locations)
					{
						LocationDataObject location1DataObject = GetLocationDataObject(
							location: location1,
							parent_id: null,
							country_id: countryDataObject.id);

						connection.Insert(location1DataObject);

						foreach (Location location2 in location1.Locations)
						{
							LocationDataObject location2DataObject = GetLocationDataObject(
								location: location2,
								parent_id: location1DataObject.id,
								country_id: countryDataObject.id);

							connection.Insert(location2DataObject);

							foreach (Location location3 in location2.Locations)
							{
								LocationDataObject location3DataObject = GetLocationDataObject(
									location: location3,
									parent_id: location2DataObject.id,
									country_id: countryDataObject.id);

								connection.Insert(location3DataObject);
							}
						}
					}
				}
			}

		}
	}
}
