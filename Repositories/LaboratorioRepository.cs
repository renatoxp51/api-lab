
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

        public async Task UpdateLaboratorio(Laboratorio laboratorio)
        {
            _context.Entry(laboratorio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

       /* public async Task RemoverLaboratorio(int laboratorioId)
        {
            var laboratorio = await _context.TbLaboratorios.FindAsync(laboratorioId);

            if (laboratorio == null)
            {
                throw new InvalidOperationException("Laboratório não encontrado.");
            }

            _context.TbLaboratorios.Remove(laboratorio);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteLaboratorioNoAndar(int andar)
        {
            return await _context.TbLaboratorios.AnyAsync(l => l.AndarLaboratorio == andar);
        }

        public async Task<Laboratorio> BuscarLaboratorioPorId(int laboratorioId)
        {
            return await _context.TbLaboratorios.FindAsync(laboratorioId);
        }

        public Task<List<Laboratorio>> ListarLaboratorios()
        {
            throw new NotImplementedException();
        }
        */
    }
}
