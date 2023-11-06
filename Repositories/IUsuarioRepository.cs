using LabReserva.Model;

namespace LabReserva.Repositories
{
    public interface IUsuarioRepository
    {

        //listar todos os usuários (adm)
        Task<List<Usuario>> GetAllUsuario();

        //criar usuário
        Task<Usuario> CreateUsuario(Usuario usuario);

        // remover usuário (is_activate = false)
        Task<bool> DesativarUsuario(string EmailUsuario, string SenhaUsuario);

        // login usuário
        Task<Usuario> LoginUsuario(string EmailUsuario, string SenhaUsuario);

        // atualizar usuario
        Task AtualizarUsuario(Usuario usuario);

        // busca usuário pelo cpf_cnpj
        Task<Usuario> SearchUsuarioByCpf(string cpf);

        // atualizar usuário pelo id
        Task<bool> AtivarUsuarioById(int id);

        // busca usuário pelo Id
        Task<bool> BuscaUsuarioById(int id);
    }
}
