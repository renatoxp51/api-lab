using System.Collections.Generic;
using System.Threading.Tasks;
using LabReserva.Model;

namespace LabReserva.Repositories
{
    public interface ILaboratorioRepository
    {
        // Listar laboratórios
        Task<List<Laboratorio>> ListarLaboratorios();

        // Cadastrar laboratorio
        Task<Laboratorio> CadastrarLaboratorio(Laboratorio laboratorio);

        // Atualizar laboratório
        Task UpdateLaboratorio(Laboratorio laboratorio);

        // Obter laboratório pelo id
        Task<Laboratorio> GetLaboratorioById(int laboratorioId);
        
        
        // Inativar um laboratorio id
        Task<bool> DesativarLaboratorioById(int laboratorioId);

        // Ativar um laboratorio pelo id
        Task<bool> AtivarLaboratorioById(int laboratorioId);
    }
}
