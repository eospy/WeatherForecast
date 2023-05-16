using System;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherForecast.API;
using WeatherForecast.Core;
using WeatherForecast.MVVM.Model;

namespace WeatherForecast.MVVM.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        //город введенный в строке поиска
        private string _searchString="";
        //команда кнопки поиска города
        private RelayCommand _searchButtonCommand;
        //результаты поиска
        public ObservableCollection<City> SearchResults { get; set; }
        //выбранный город и погода в нём
        private City _selectedCity;
        private WeatherData _weatherData;
        //подсказки при поиске
        private bool _searchPopupIsOpen;
        private string _title = "";
        private string _temperature = "";
        private string _humidity = "";
        private string _windSpeed = "";
        private string _clouds = "";
        private string _pressure = "";
        private readonly ApiAccess api;
        public MainWindowModel()
        {
            api = new();
            SearchResults= new ObservableCollection<City>();
            _selectedCity= new City();
            _weatherData= new WeatherData();
        }
        public string SearchString
        {
            get => _searchString;
            set=> SetProperty(ref _searchString,value);
        }
        public string Title
        {
            get=>_title;
            set=> SetProperty(ref _title, value);
        }
        public string Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }
        public string Humidity
        {
            get => _humidity;
            set => SetProperty(ref _humidity, value);
        }
        public string WindSpeed
        {
            get => _windSpeed;
            set => SetProperty(ref _windSpeed, value);
        }
        public string Clouds
        {
            get => _clouds;
            set => SetProperty(ref _clouds, value);
        }
        public string Pressure
        {
            get => _pressure;
            set => SetProperty(ref _pressure, value);
        }
        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (value != null)
                {
                    _selectedCity = value;
                    SearchString = value.name;
                    SearchPopupIsOpen = false;
                    _weatherData = api.GetWeather(_selectedCity.lat, _selectedCity.lon);
                    Title = "Погода в городе "+_selectedCity.name;
                    float temp= _weatherData.main.temp - 273.15f;
                    Temperature = "Температура (°C)" +Math.Ceiling(temp);
                    Humidity = "Влажность (%)" + _weatherData.main.humidity ;
                    WindSpeed = "Скорость ветра " + _weatherData.wind.speed + "м/с Направление "+_weatherData.wind.deg+ "°";
                    Clouds = "Облачность (%) " + _weatherData.clouds.all;
                    Pressure = "Давление(мм.рт.ст) " + _weatherData.main.pressure;
                }
                
            }
        }
        //команда поиска
        public RelayCommand SearchButtonCommand => _searchButtonCommand ?? new RelayCommand(o => SearchCities());
        private void SearchCities()
        {
            SearchResults.Clear();
            var searchresult= api.CitySearch(_searchString);
            searchresult.ToList().ForEach(c=>SearchResults.Add(c));
            SearchPopupIsOpen = true;
        }
        public bool SearchPopupIsOpen
        {
            get => _searchPopupIsOpen;
            set => SetProperty(ref _searchPopupIsOpen, value);
        }
    }
}
