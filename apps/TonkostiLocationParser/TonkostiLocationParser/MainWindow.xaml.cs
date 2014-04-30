using System;
using System.Windows;
using TonkostiLocationParser.Domain;

namespace TonkostiLocationParser
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainViewController _controller;

		public MainWindow()
		{
			InitializeComponent();

			_controller = new MainViewController();

			DataContext = _controller.ViewModel;
		}

		private void ShowException(Exception ex)
		{
			ExceptionTextBlock.Text = ex.Message;
			ExceptionTextBlock.Visibility = Visibility.Visible;
		}

		private void HideException()
		{
			ExceptionTextBlock.Text = string.Empty;
			ExceptionTextBlock.Visibility = Visibility.Collapsed;
		}

		private void OnSaveCountriesJsonButtonClick(object sender, RoutedEventArgs e)
		{
			HideException();

			try
			{
				_controller.SaveCountriesJson();
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}

		private void OnSaveCountriesSqliteButtonClick(object sender, RoutedEventArgs e)
		{
			HideException();

			try
			{
				_controller.SaveCountriesSqlite();
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}

		private void OnLoadCountriesButtonClick(object sender, RoutedEventArgs e)
		{
			HideException();

			try
			{
				_controller.LoadCountries();
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}

		private void OnParseCountriesButtonClick(object sender, RoutedEventArgs e)
		{
			HideException();

			try
			{
				_controller.ParseCountries();
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}


		private void OnSaveLocationsButtonClick(object sender, RoutedEventArgs e)
		{
			HideException();

			try
			{
				_controller.SaveLocations();
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}

		private void OnLoadLocationsButtonClick(object sender, RoutedEventArgs e)
		{
			HideException();

			try
			{
				_controller.LoadLocations();
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}

		private void OnParseLocationsSelectedCountryButtonClick(object sender, RoutedEventArgs e)
		{
			HideException();

			try
			{
				_controller.ParseLocationsSelectedCountry();
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}

		private void OnParseLocationsAllButtonClick(object sender, RoutedEventArgs e)
		{
			HideException();

			try
			{
				_controller.ParseLocationsAll();
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
		}
	}
}
