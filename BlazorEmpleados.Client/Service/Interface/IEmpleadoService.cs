using BlazorEmpleados.DTOs.Empleado;

namespace BlazorEmpleados.Client.Service.Interface
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoResponseDTO>> GetAll();
        Task<bool> Delete(string nroDocumento);
        Task<bool> Create(EmpleadoCreateRequestDTO empleado);
        Task<List<EmpleadoResponseDTO>> GetByNombre(string nombre);
        Task<EmpleadoResponseDTO> GetByNroDocumento(string nroDocumento);
        Task<bool> Update(EmpleadoUpdateRequestDTO empleado, string nroDocumento);
    }
}
