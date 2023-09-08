using LabReserva.Model;

namespace LabReserva.Repositories
{
    public interface IUsuarioRepository
    {

        Task<IEnumerable<Usuario>> GetUsuarios();

        Task<Usuario> GetUsuario(int id);
    }
}
