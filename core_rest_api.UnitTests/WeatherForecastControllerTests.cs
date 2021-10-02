using System;
using NUnit.Framework;
using core_rest_api;
using core_rest_api.Models;
using core_rest_api.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace core_rest_api.UnitTests
{
    public class WeatherForecastControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Get_WhenCalled_ReturnsHardcodedWeatherInfo()
        {
            //Arrange
            var myWeatherForecastController = new WeatherForecastController();

            //Act
            IEnumerable<WeatherForecast> forecast = myWeatherForecastController.Get();

            string[] validWeathers = new[]{
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            //Assert
            Assert.Contains(forecast.First().Summary, validWeathers);
        }
    }
}