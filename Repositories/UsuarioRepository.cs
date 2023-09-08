using LabReserva.Data;
using LabReserva.Model;

namespace LabReserva.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        public readonly ReservaLabContext _context;

        public UsuarioRepository(ReservaLabContext context)
        {
            _context = context; 
        }

        public async Task<Usuario> GetUsuario(int IdUsuario)
        {

            return await _context.TbUsuarios.FindAsync(IdUsuario);
;
        }

        public Task<IEnumerable<Usuario>> GetUsuarios()
        {
            throw new NotImplementedException();
        }
    }

}
