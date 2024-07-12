using BlazorEmpleados.BLL;
using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DTOs.Departamento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEmpleados.API.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;
        private readonly ILogger<DepartamentoController> _logger;
        public DepartamentoController(IDepartamentoService departamentoService, ILogger<DepartamentoController> logger)
        {
            _departamentoService = departamentoService;
            _logger = logger;

        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DepartamentoResponseDTO>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint GetAll");
                var result = await _departamentoService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al obtener datos en GetAll");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<ActionResult<DepartamentoResponseDTO>> Create(DepartamentoCreateDTO departamento)
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint Create");
                var result = await _departamentoService.Create(departamento);
                if (result == null)
                    return BadRequest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al crear deapartamento");
                return BadRequest(ex.Message);
            }
        }
    }
}
