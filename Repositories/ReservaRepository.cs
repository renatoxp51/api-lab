using LabReserva.Data;
using LabReserva.Model;
using Microsoft.EntityFrameworkCore;

namespace LabReserva.Repositories
{
    public class ReservaRepository : IReservaRepository
    {

        private readonly ReservaLabContext _context;

        public ReservaRepository(ReservaLabContext context)
        {
            _context = context;
        }

        // implementando a busca de todas as reservas
        public async Task<List<Reserva>> ListarReservas()
        {            
            return await _context.TbReservas.ToListAsync();
        }

        // implementando a busca de uma reserva pelo id
        public async Task<Reserva> BuscaReservaById(int id)
        {
            return await _context.TbReservas.FirstOrDefaultAsync(r => r.IdReserva == id);
                      
        }

        // implementando a verificação de disponibilidade de uma reserva a partir do id laboratorio e datetime
        public async Task<bool> VerificaDisponibilidadeReserva(int id, DateTime diaHora)
        {
            var reserva = await _context.TbReservas.FirstOrDefaultAsync(r => r.IdLaboratorio == id && r.DiaHorarioReserva == diaHora);

            // caso encontrar um registro com o id laboratorio no mesmo dia/horario
            if (reserva != null)
            {                
                // a função retornará false
                return false;
            }

            return true;
        }

        // buscando todas as reservas de um usuário específico
        public async Task<List<Reserva>> ListarReservasByUsuarioId(int id)
        {
            return await _context.TbReservas.Where(r => r.IdUsuario == id).ToListAsync();
        }

        // verificar se um labotório está agendado
        // essa consulta será utilizada para saber se o laboratório pode ser desativado ou não
        public async Task<bool> ListarReservasByLaboratorioId(int id)
        {
            DateTime dt = DateTime.Now;

            var reserva = await _context.TbReservas.FirstOrDefaultAsync(r => r.IdLaboratorio == id && r.DiaHorarioReserva >= dt);

            if (reserva != null)
            {
                return true;
            } else
            {
                return false;
            }


        }

        // implementando a inclusão de nova reserva
        public async Task<Reserva> AdicionarReserva(Reserva reserva)
        {

            await _context.TbReservas.AddAsync(reserva);
            _context.SaveChanges();

            return reserva;
        }
    }
}
