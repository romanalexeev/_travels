using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TonkostiLocationParser.Domain;
using TonkostiLocationParser.Parse;

namespace TonkostiLocationParser
{
	public class MainViewController
	{
		private const string CountriesFilePath = "../../../../../text/countries.json";
		private const string LocationsFilePath = "../../../../../text/locations_{0}.json";

		private readonly MainViewModel _viewModel;
		public MainViewModel ViewModel { get { return _viewModel; } }

		public MainViewController()
		{
			_viewModel = new MainViewModel();
			_viewModel.Countries = new ObservableCollection<Country>();

			//_viewModel.CountryList.Add(new Country("qwer", "/qwer", false));
		}

		public void SaveCountries()
		{
			string contents = JsonConvert.SerializeObject(ViewModel.Countries);

			File.WriteAllText(CountriesFilePath, contents);
		}

		public void LoadCountries()
		{
			string contents = File.ReadAllText(CountriesFilePath);

			ViewModel.Countries = JsonConvert
				.DeserializeObject<List<Country>>(contents)
				.ToObservableCollection();
		}

		public void ParseCountries()
		{
			CountriesParser parser = new CountriesParser();

			List<Country> countryList = parser.Parse();

			ViewModel.Countries = countryList.ToObservableCollection();
		}


		public void SaveLocations()
		{
			if (ViewModel.SelectedCountry != null)
			{
				string contents = JsonConvert.SerializeObject(ViewModel.SelectedCountry.Locations);

				File.WriteAllText(string.Format(LocationsFilePath, ViewModel.SelectedCountry.Name), contents);
			}
		}

		public void LoadLocations()
		{
			if (ViewModel.SelectedCountry != null)
			{
				string contents = File.ReadAllText(string.Format(LocationsFilePath, ViewModel.SelectedCountry.Name));

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
				LocationsParser parser = new LocationsParser();

				List<Location> locationList = parser.Parse(ViewModel.SelectedCountry);

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
