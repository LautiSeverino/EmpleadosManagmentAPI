using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DTOs.Empleado;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEmpleados.API.Controllers
{
   [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;

        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<EmpleadoResponseDTO>>> GetAll()
        {
            var result = await _empleadoService.GetAll();
            return Ok(result);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<EmpleadoResponseDTO>> Create(EmpleadoCreateRequestDTO empleado)
        {
            try
            {
                var result = await _empleadoService.Create(empleado);
                if (result == null)
                    return BadRequest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetByNombre/{nombre}")]
        public async Task<ActionResult<List<EmpleadoResponseDTO>>> GetByNombre(string nombre)
        {
            var result = await _empleadoService.GetByNombre(nombre);
            if (result.Count == 0)
            {
                return NotFound("No hay empleados con ese nombre");
            }
            return Ok(result);
        }

        [HttpGet("GetByNroDocumento/{nroDocumento}")]
        public async Task<ActionResult<EmpleadoResponseDTO>> GetByNroDocumento(string nroDocumento)
        {
            var result = await _empleadoService.GetByNroDocumento(nroDocumento);
            if (result == null)
            {
                return NotFound("No hay empleados con ese nombre");
            }
            return Ok(result);
        }
        [HttpPut("Update/{nroDocumento}")]
        public async Task<ActionResult<EmpleadoResponseDTO>> Update(string nroDocumento, [FromBody] EmpleadoUpdateRequestDTO empleado)
        {
            var result = await _empleadoService.Update(empleado, nroDocumento);
            if (result == null)
                return NotFound("No se encontró el empleado");
            return Ok(result);
        }

        [HttpDelete("Delete/{nroDocumento}")]
        public async Task<ActionResult<bool>> Delete(string nroDocumento)
        {
            var result = await _empleadoService.Delete(nroDocumento);
            if (result == false)
                return false;
            return Ok(true);
        }
    }
}
