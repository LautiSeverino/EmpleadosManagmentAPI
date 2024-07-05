using BlazorEmpleados.DTOs.Departamento;

namespace BlazorEmpleados.Client.Service.Interface
{
    public interface IDepartamentoService
    {
        Task<List<DepartamentoResponseDTO>> GetAll();
        Task<bool> Create(DepartamentoCreateDTO departamento);
        Task<bool> Delete(int id);
    }
}
