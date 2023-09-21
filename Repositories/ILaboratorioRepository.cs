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

        // Obter laboratório pelo ID
        Task<Laboratorio> GetLaboratorioById(int laboratorioId)

        /*// Remover laboratório
        Task RemoverLaboratorio(int laboratorioId);

        // Verificar se já existe um laboratório no mesmo andar
        Task<bool> ExisteLaboratorioNoAndar(int andar);

        // Buscar laboratório pelo ID
        Task<Laboratorio> BuscarLaboratorioPorId(int laboratorioId);

        // Obter laboratório pelo ID
        Task<Laboratorio> UpdateLaboratorio (int laboratorioId)
        
*/
    }
}
