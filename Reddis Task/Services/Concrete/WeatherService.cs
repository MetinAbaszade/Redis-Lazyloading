using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Reddis_Task.Entities;
using Reddis_Task.Services.Abstract;

namespace Reddis_Task.Services.Concrete
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IRedisService _redisService;

        public WeatherService(IRedisService redisService)
        {
            _httpClient = new HttpClient();
            _redisService = redisService;
        }

        public async Task<WeatherData> GetWeatherData(string city)
        {
            WeatherData weatherData;

            var redisdata = await _redisService.GetDataAsync(city);

            if (string.IsNullOrEmpty(redisdata))
            {
                // If the data is not in cache, fetch it from the API
                string apiKey = "f8c667e0bd93b1e29e75c3e7520410d0";
                string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseString = await response.Content.ReadAsStringAsync();
                weatherData = JsonConvert.DeserializeObject<WeatherData>(responseString);

                // Store the data in cache for 1 hour
                try
                {
                    // TTL
                    TimeSpan oneHour = TimeSpan.FromHours(1);
                    _redisService.AddDataAsync(city, JsonConvert.SerializeObject(weatherData), oneHour);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                weatherData.Source = "API";
                return weatherData;

            }
            weatherData = JsonConvert.DeserializeObject<WeatherData>(redisdata);
            weatherData.Source = "Redis";
            return weatherData;
        }
    }
}
