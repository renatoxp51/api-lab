using LabReserva.Data;
using LabReserva.Model;
using LabReserva.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        private readonly ILaboratorioRepository _laboratorioRepository;

        public LaboratorioController(ILaboratorioRepository repository)
        {
            _laboratorioRepository = repository;
        }

        // rota para listar todos os laboratórios
        [HttpGet]
        [Authorize]
        public Task<List<Laboratorio>> ListarTbLaboratorios()
        {
            return _laboratorioRepository.ListarLaboratorios();
        }

        // rota para criar um novo laboratorio
        [HttpPost]
        [Authorize]
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
        [Authorize]
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

        
        // rota para inativar um laboratorio pelo id
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DesativarLaboratorioById(int id)
        {

            // validar se o laboratório está sendo utilizado ou não
            // ...

            if (await _laboratorioRepository.DesativarLaboratorioById(id)) 
            {
                return NoContent();
            }

            return BadRequest();
        }

        /*

        // rota para ativar um laboratorio pelo id
        [HttpPut("ativar/{id}")]
        [Authorize]
        public async Task<ActionResult> AtivarLaboratorioById(int id)
        {
            if(await _laboratorioRepository.AtivarLaboratorioById(id))
            {
                return NoContent();
            }

            return BadRequest();
        }

        */


        // rota para buscar um laboratorio pelo id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Laboratorio> GetLaboratorioById(int id)
        {
            return await _laboratorioRepository.GetLaboratorioById(id);
        }


    }

}

