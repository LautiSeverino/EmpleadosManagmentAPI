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
        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DepartamentoResponseDTO>>> GetAll()
        {
            var result = await _departamentoService.GetAll();
            return Ok(result);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<DepartamentoResponseDTO>> Create(DepartamentoCreateDTO departamento)
        {
            var result = await _departamentoService.Create(departamento);
            return Ok(result);
        }
    }
}
