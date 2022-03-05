using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DapperAPI.Services;
using DapperAPI.DataServices;
using System.Collections.Generic;

namespace DapperAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IAdiantamentoService _adiantamentoRepository;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAdiantamentoService adiantamentoRepository)
        {
            _logger = logger;
            _adiantamentoRepository = adiantamentoRepository;
        }

        //[HttpGet]
        //public IEnumerable<Entities.ADIANTAMENTO> Get()
        //{
        //    //var adt = new Entities.ADIANTAMENTO(){ ID=0, CONTRATOPROFITSHAREID=11, DATAADIANT=DateTime.Now, PERIODOREF = 21322, VALORADIANT=Convert.ToDecimal("23234,656") };
        //    //return _adiantamentoRepository.GetAll();
        //}
    }
}
