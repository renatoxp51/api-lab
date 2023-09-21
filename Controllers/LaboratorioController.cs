using LabReserva.Data;
using LabReserva.Model;
using LabReserva.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabReserva.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        // private List<Laboratorio> laboratorios = new List<Laboratorio>();

        private readonly ILaboratorioRepository _laboratorioRepository;

        public LaboratorioController(ReservaLabContext context, ILaboratorioRepository repository)
        {
            _laboratorioRepository = repository;
        }


        [HttpGet]
        public Task<List<Laboratorio>> ListarTbLaboratorios()
        {
            return _laboratorioRepository.ListarLaboratorios();
        }

        [HttpPost]
        public async Task<Laboratorio> CadastrarLaboratorio([FromBody] NovoLaboratorio laboratorio)
        {

            Laboratorio novolaboratorio = new Laboratorio
            {
                NomeLaboratorio = laboratorio.NomeLaboratorio,
                AndarLaboratorio = laboratorio.AndarLaboratorio,
                DescricaoLaboratorio = laboratorio.DescricaoLaboratorio,
                IsActivate = true

            };


            await _laboratorioRepository.CadastrarLaboratorio(novolaboratorio);

            return novolaboratorio;


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Laboratorio>> UpdateLaboratorio(int id, NovoLaboratorio laboratorio)
        {
            var laboratorioExistente = await _laboratorioRepository.UpdateLaboratorio(id);

            if (laboratorioExistente == null)
            {
                return NotFound(); 
            }

            laboratorioExistente.NomeLaboratorio = laboratorio.NomeLaboratorio;
            laboratorioExistente.AndarLaboratorio = laboratorio.AndarLaboratorio;
            laboratorioExistente.DescricaoLaboratorio = laboratorio.DescricaoLaboratorio;

            await _laboratorioRepository.AtualizarLaboratorio(laboratorioExistente);

            return Ok(laboratorioExistente); // Retorna o laboratório atualizado
        }
    }

}

