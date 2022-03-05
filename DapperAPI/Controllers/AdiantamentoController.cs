using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdiantamentoController : ControllerBase
    {
        private readonly ILogger<Entities.ADIANTAMENTO> _logger;
        public Services.IAdiantamentoService _adtoService { get; set; }

        public AdiantamentoController(ILogger<Entities.ADIANTAMENTO> logger, Services.IAdiantamentoService adtoRepository)
        {
            _logger = logger;
            _adtoService = adtoRepository;
        }

        // GET: api/<AdiantamentoController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entities.ADIANTAMENTO>>> Get()
        {
            return Ok( await _adtoService.GetAll());
        }

        // GET api/<AdiantamentoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.ADIANTAMENTO>> Get(int id)
        {
            return Ok( await _adtoService.GetById(id));
        }

        // POST api/<AdiantamentoController>
        [HttpPost]
        public async Task<ActionResult<Entities.ADIANTAMENTO>> Post(Entities.ADIANTAMENTO adiantamento)
        {
            try
            {
                var adtoNew = await _adtoService.Insert(adiantamento);
                _logger.LogInformation($"Registro Id {adtoNew.ID} incluído com sucesso");
                return Ok(adtoNew);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar criar registro {Environment.NewLine + ex.Message}");
                return BadRequest(ex);
                throw;
            }
        }

        // PUT api/<AdiantamentoController>/5
        [HttpPut]
        public async Task<IActionResult> Put(Entities.ADIANTAMENTO adiantamento)
        {
            try
            {
                await _adtoService.Update(adiantamento);
                _logger.LogInformation($"Registro Id {adiantamento.ID} alterado com sucesso");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar alterar registro {Environment.NewLine + ex.Message}");
                return BadRequest(ex);
                throw;
            }
        }

        // DELETE api/<AdiantamentoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adtoService.Delete(id);
                _logger.LogInformation($"Registro Id {id} excluído com sucesso");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - Erro ao tentar excluir registro {Environment.NewLine + ex.Message}");
                return BadRequest(ex);
                throw;
            }
        }
    }
}
