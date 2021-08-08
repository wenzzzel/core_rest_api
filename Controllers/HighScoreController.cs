using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace core_rest_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HighScoreController : ControllerBase
    {
        private static readonly string[] UserNames = new[]
        {
            "wenzzzel", "davwen", "Allyer", "Dailiesst", "Geniusys", "PlotCampy", "ShiyaPeach", "Tinnysve", "FamousBorn", "per"
        };

        private readonly ILogger<HighScoreController> _logger;

        public HighScoreController(ILogger<HighScoreController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<HighScore> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new HighScore
            {
                UserName = UserNames[rng.Next(UserNames.Length)],
                Date = DateTime.Now.AddDays(index),
                Score = rng.Next(-20, 55)
            })
            .ToArray();
        }
    }
}
