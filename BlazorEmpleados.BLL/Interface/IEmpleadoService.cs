using BlazorEmpleados.DTOs.Empleado;

namespace BlazorEmpleados.BLL.Interface
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoResponseDTO>> GetAll();
        Task<List<EmpleadoResponseDTO>> GetByNombre(string nombre);
        Task<EmpleadoResponseDTO> GetByNroDocumento(string nroDocumento);
        Task<EmpleadoResponseDTO> Create(EmpleadoCreateRequestDTO empleado);
        Task<EmpleadoResponseDTO> Update(EmpleadoUpdateRequestDTO empleado, string nroDocumento);
        Task<bool> Delete(string nroDocumento);
    }
}
