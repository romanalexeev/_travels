using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using TonkostiLocationParser.Common;
using TonkostiLocationParser.Domain;

namespace TonkostiLocationParser
{
	public class MainViewModel : BindableBase
	{
		private ObservableCollection<Country> _countries;
		public ObservableCollection<Country> Countries
		{
			get { return _countries; }
			set { SetProperty(ref _countries, value); }
		}

		private Country _selectedCountry;
		public Country SelectedCountry
		{
			get { return _selectedCountry; }
			set
			{
				SetProperty(ref _selectedCountry, value);

				Locations = _selectedCountry.Locations.ToObservableCollection();

				UpdateLocations();
			}
		}

		private ObservableCollection<Location> _locations;
		public ObservableCollection<Location> Locations
		{
			get { return _locations; }
			set { SetProperty(ref _locations, value); }
		}

		public void UpdateLocations()
		{
			Locations = _selectedCountry
				.Locations
				.SelectMany(l1 => l1.Locations.Where(l2 => l2.Level == 2).InsertRange(l1))
				.SelectMany(l2 => l2.Locations.Where(l3 => l3.Level == 3).InsertRange(l2))
				.ToObservableCollection();
		}
	}
}
