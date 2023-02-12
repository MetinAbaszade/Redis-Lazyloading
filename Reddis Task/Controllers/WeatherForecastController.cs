using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Reddis_Task.Entities;
using Reddis_Task.Services.Abstract;
using Reddis_Task.Services.Concrete;
using System.Text;

namespace Reddis_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly IRedisService _redisService;

        public WeatherForecastController(WeatherService weatherService, IRedisService redisService)
        {
            _weatherService = weatherService;
            _redisService = redisService;
        }

        [HttpPost]
        public async Task<ActionResult<WeatherData>> GetWeatherData(string City)
        {
            try
            {
                var WeatherData = await _weatherService.GetWeatherData(City);
                return WeatherData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}






