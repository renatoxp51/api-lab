using LabReserva.Data;
using LabReserva.Model;

namespace LabReserva.Repositories
{
    public interface IReservaRepository
    {
        // listar todas as reseras
        Task<List<Reserva>> ListarReservas();

        // buscar reserva by id
        Task<Reserva> BuscaReservaById(int id);

        // verificar disponibilidade
        Task<bool> VerificaDisponibilidadeReserva(int id, DateTime diaHora);

        // verificar se laboratório está reservado por alguém
        // o intuito é saber se o laboratório pode ser desativado ou não
        Task<bool> ListarReservasByLaboratorioId(int id);

        // buscar reservas by id usuario
        Task<List<Reserva>> ListarReservasByUsuarioId(int id);

        // adicionar reserva
        Task<Reserva> AdicionarReserva(Reserva reserva);
    }
}
