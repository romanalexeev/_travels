using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TonkostiLocationParser.Db;
using TonkostiLocationParser.Domain;
using TonkostiLocationParser.Parse;

namespace TonkostiLocationParser
{
	public class MainViewController
	{
		private const string CountriesSqliteFilePath = "../../../../../db/tonkosti_locations.db";

		private const string CountriesJsonFilePath = "../../../../../text/countries.json";
		private const string LocationsJsonFilePath = "../../../../../text/locations_{0}.json";

		private readonly MainViewModel _viewModel;
		public MainViewModel ViewModel { get { return _viewModel; } }

		public MainViewController()
		{
			_viewModel = new MainViewModel();
			_viewModel.Countries = new ObservableCollection<Country>();

			//_viewModel.CountryList.Add(new Country("qwer", "/qwer", false));
		}

		public void SaveCountriesJson()
		{
			string contents = JsonConvert.SerializeObject(ViewModel.Countries);

			File.WriteAllText(CountriesJsonFilePath, contents);
		}


		public void SaveCountriesSqlite()
		{
			if (ViewModel.Countries != null)
			{
				SqliteCountriesSaver.Save(ViewModel.Countries, CountriesSqliteFilePath);
			}
		}

		public void LoadCountries()
		{
			string contents = File.ReadAllText(CountriesJsonFilePath);

			ViewModel.Countries = JsonConvert
				.DeserializeObject<List<Country>>(contents)
				.ToObservableCollection();
		}

		public void ParseCountries()
		{
			List<Country> countryList = CountriesParser.Parse();

			ViewModel.Countries = countryList.ToObservableCollection();
		}


		public void SaveLocations()
		{
			if (ViewModel.SelectedCountry != null)
			{
				string contents = JsonConvert.SerializeObject(ViewModel.SelectedCountry.Locations);

				File.WriteAllText(string.Format(LocationsJsonFilePath, ViewModel.SelectedCountry.Name), contents);
			}
		}

		public void LoadLocations()
		{
			if (ViewModel.SelectedCountry != null)
			{
				string contents = File.ReadAllText(string.Format(LocationsJsonFilePath, ViewModel.SelectedCountry.Name));

				List<Location> locationList = JsonConvert
					.DeserializeObject<List<Location>>(contents);

				ViewModel.SelectedCountry.Locations.Reload(locationList);

				ViewModel.UpdateLocations();
			}
		}

		public void ParseLocationsSelectedCountry()
		{
			if (ViewModel.SelectedCountry != null)
			{
				List<Location> locationList = LocationsParser.Parse(ViewModel.SelectedCountry);

				ViewModel.SelectedCountry.Locations.Reload(locationList);

				ViewModel.UpdateLocations();

				ViewModel.OnPropertyChanged("Countries");
			}
		}

		public void ParseLocationsAll()
		{
			for (int i = 0; i < ViewModel.Countries.Count - 1; i++)
			{
				//if (!ViewModel.SelectedCountry.Locations.Any())
				{
					ViewModel.SelectedCountry = ViewModel.Countries[i];

					ParseLocationsSelectedCountry();
				}
			}
		}


	}
}
