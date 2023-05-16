using Newtonsoft.Json;
using RestSharp;
using System;
using System.Windows;
using WeatherForecast.MVVM.Model;

namespace WeatherForecast.API
{
    public class ApiAccess
    {
        private RestClient _client=new();
        private string _apikey = "b8843d6244cb0e09a670b98926b16aa9";
        private string _getWeatherUri = "https://api.openweathermap.org/data/2.5/weather";
        private string _searchUri = "http://api.openweathermap.org/geo/1.0/direct";
        //поиск городов по названию
        public City[] CitySearch(string searchqueue)
        {
            var request=new RestRequest(_searchUri, Method.Get);
            request.AddParameter("q",searchqueue);
            request.AddParameter("limit", 5);
            request.AddParameter("appid", _apikey);
            RestResponse response = new();
            try
            {
                response = _client.GetAsync(request).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                MessageBox.Show("City search failure");
            }
            return JsonConvert.DeserializeObject<City[]>(response.Content);

        }
        //получение информации о погоде по координатам
        public WeatherData GetWeather(float lat,float lon)
        {
            var request = new RestRequest(_getWeatherUri, Method.Get);
            request.AddParameter("lat", lat);
            request.AddParameter("lon", lon);
            request.AddParameter("appid", _apikey);
            RestResponse response = new();
            try
            {
                response = _client.GetAsync(request).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                MessageBox.Show("Weather loading failure");
            }
            return JsonConvert.DeserializeObject<WeatherData>(response.Content);
        }
    }
}
