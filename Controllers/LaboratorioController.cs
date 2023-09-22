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

        // rota para listar todos os laboratórios
        [HttpGet]
        public Task<List<Laboratorio>> ListarTbLaboratorios()
        {
            return _laboratorioRepository.ListarLaboratorios();
        }

        // rota para criar um novo laboratorio
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

        // método para atualizar um laboratório pelo id
        [HttpPut("{id}")]
        public async Task<ActionResult<Laboratorio>> UpdateLaboratorio(int id,[FromBody] NovoLaboratorio laboratorio)
        {

            var laboratorioExistente = await _laboratorioRepository.GetLaboratorioById(id);

            if (laboratorioExistente == null)
            {
                return NotFound(); 
            }
                        
            laboratorioExistente.NomeLaboratorio = laboratorio.NomeLaboratorio;
            laboratorioExistente.AndarLaboratorio = laboratorio.AndarLaboratorio;
            laboratorioExistente.DescricaoLaboratorio = laboratorio.DescricaoLaboratorio;
            laboratorioExistente.IsActivate = laboratorio.IsActivate;

            await _laboratorioRepository.UpdateLaboratorio(laboratorioExistente);

            return Ok(laboratorioExistente);
        }

        
        // rota para inativar um laboratorio
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLaboratorio(int id)
        {

            // validar se o laboratório está sendo utilizado ou não
            // ...

            if (await _laboratorioRepository.DeleteLaboratorio(id)) 
            {
                return NoContent();
            }

            return BadRequest();
        }
        
    }

}

