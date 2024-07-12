using AutoMapper;
using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DAL.UnitOfWork.UnitOfWorkFolder;
using BlazorEmpleados.DTOs.Departamento;
using BlazorEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.BLL
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DepartamentoService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<DepartamentoResponseDTO> Create(DepartamentoCreateDTO departamento)
        {
            var departamentoEntity = _mapper.Map<Departamento>(departamento);
            await _unitOfWork.DepartamentoRepository.Create(departamentoEntity);
            if (await _unitOfWork.Save() == 0)
                return null;
            var result = _mapper.Map<DepartamentoResponseDTO>(departamentoEntity);
            return result;
        }
        public async Task<List<DepartamentoResponseDTO>> GetAll()
        {
            var departamentos = await _unitOfWork.DepartamentoRepository.GetAll();
            var result = _mapper.Map<List<DepartamentoResponseDTO>>(departamentos);
            return result;
        }
    }
}
