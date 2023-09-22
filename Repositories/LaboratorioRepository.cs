
using LabReserva.Data;
using LabReserva.Model;
using LabReserva.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LabReserva.Repositories
{
    public class LaboratorioRepository : ILaboratorioRepository
    {
        private readonly ReservaLabContext _context;

        public LaboratorioRepository(ReservaLabContext context)
        {
            _context = context;
        }

        // implementando a consulta de todos os laboratorios
        public async Task<List<Laboratorio>> ListarLaboratorios()
        {
            return await _context.TbLaboratorios.ToListAsync();
        }

        //implementando o cadastro laboratorio
        public async Task<Laboratorio> CadastrarLaboratorio(Laboratorio laboratorio)
        {
            var laboratorioExistente = await _context.TbLaboratorios.FirstOrDefaultAsync(l => l.AndarLaboratorio == laboratorio.AndarLaboratorio && l.NomeLaboratorio == laboratorio.NomeLaboratorio );

            if (laboratorioExistente == null)

                try
            {
                // Adicionar o laboratório ao banco de dados e salvar as alterações
                _context.TbLaboratorios.Add(laboratorio);
                await _context.SaveChangesAsync();
                return laboratorio;

            } catch (Exception ex) 
            {
                throw new Exception("Não foi possível cadastrar o laboratório!", ex);
            }
            throw new Exception("Laboratório já existente!");

        }

        // implementando a atualização de um laboratório
        public async Task UpdateLaboratorio(Laboratorio laboratorio)
        {
            _context.Entry(laboratorio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // implementando a busca de um laboratório pelo id
        public async Task<Laboratorio> GetLaboratorioById(int laboratorioId)
        {
            return await _context.TbLaboratorios.FirstOrDefaultAsync(l => l.IdLaboratorio == laboratorioId);
        }

        
        // implementando a remoção do laboratório
        public async Task<bool> DeleteLaboratorio(int laboratorioId)
        {
            var laboratorio = await _context.TbLaboratorios.FirstOrDefaultAsync(l => l.IdLaboratorio == laboratorioId);

            if (laboratorio == null)
            {
                return false;
            }

            _context.Remove(laboratorio);
            _context.SaveChanges();
            return true;

        }
        
    }
}
