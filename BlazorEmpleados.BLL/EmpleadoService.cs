using AutoMapper;
using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DAL.UnitOfWork.UnitOfWorkFolder;
using BlazorEmpleados.DTOs.Empleado;
using BlazorEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.BLL
{
    public class EmpleadoService : IEmpleadoService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmpleadoService( IMapper mapper, IUnitOfWork unitOfWork)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<EmpleadoResponseDTO> Create(EmpleadoCreateRequestDTO empleadoCreate)
        {

            var depto = await _unitOfWork.DepartamentoRepository.GetById(empleadoCreate.IdDepartamento);
            if (depto == null)
                return null;

            var empleado = _mapper.Map<Empleado>(empleadoCreate);
            empleado.IdDepartamento = depto.IdDepartamento;

            await _unitOfWork.EmpleadoRepository.Create(empleado);
            int flag = await _unitOfWork.Save();
            if (flag == 0)
                return null;

            var result = _mapper.Map<EmpleadoResponseDTO>(empleado);
            return result;
        }

        public async Task<bool> Delete(string nroDocumento)
        {
            var empleado = await _unitOfWork.EmpleadoRepository.GetByNroDocumento(nroDocumento);
            if(empleado == null) 
                return false;
            _unitOfWork.EmpleadoRepository.Delete(empleado);

            int flag = await _unitOfWork.Save();

            if (flag == 0)
                return false;

            return true;
        }

        public async Task<List<EmpleadoResponseDTO>> GetAll()
        {
            var empleados = await _unitOfWork.EmpleadoRepository.GetAllWithDepartamento();
            var result = _mapper.Map<List<EmpleadoResponseDTO>>(empleados);
            return result;
        }

        public async Task<List<EmpleadoResponseDTO>> GetByNombre(string nombre)
        {
            var empleado = await _unitOfWork.EmpleadoRepository.GetByNombre(nombre);
            if (empleado == null)
                return null;
            var result = _mapper.Map<List<EmpleadoResponseDTO>>(empleado);
            return result;
        }

        public async Task<EmpleadoResponseDTO> GetByNroDocumento(string nroDocumento)
        {
            var empleado = await _unitOfWork.EmpleadoRepository.GetByNroDocumento(nroDocumento);
            if (empleado == null)
                return null;
            var result = _mapper.Map<EmpleadoResponseDTO>(empleado);
            return result;
        }

        public async Task<EmpleadoResponseDTO> Update(EmpleadoUpdateRequestDTO empleadoUpdate, string nroDocumento)
        {
            var empleadoEdit = await _unitOfWork.EmpleadoRepository.GetByNroDocumento(nroDocumento);
            var existeDepto = await _unitOfWork.DepartamentoRepository.GetById((int)empleadoUpdate.IdDepartamento);
            if (empleadoEdit != null)
            {
                empleadoEdit.NombreCompleto = empleadoUpdate.NombreCompleto ?? empleadoEdit.NombreCompleto;

                if (existeDepto != null)
                    empleadoEdit.IdDepartamento = (int)empleadoUpdate.IdDepartamento;
                else
                    return null;

                empleadoEdit.Sueldo = empleadoUpdate.Sueldo ?? empleadoEdit.Sueldo;
                empleadoEdit.NroDocumento = empleadoUpdate.NroDocumento ?? empleadoEdit.NroDocumento;
                empleadoEdit.FechaContrato = empleadoUpdate.FechaContrato ?? empleadoEdit.FechaContrato;

                _unitOfWork.EmpleadoRepository.Update(empleadoEdit);
                var result = await _unitOfWork.Save();

                if (result >= 1)
                {
                    var newEmpleadoEdit = await _unitOfWork.EmpleadoRepository.GetById(empleadoEdit.IdEmpleado);
                    var empleadoResponseDTO = _mapper.Map<EmpleadoResponseDTO>(newEmpleadoEdit);
                    return empleadoResponseDTO;
                }
                else
                    return null;
            }
            else
                return null;

        }
    }
}
