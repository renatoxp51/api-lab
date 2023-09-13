using LabReserva.Model;

namespace LabReserva.Repositories
{
    public interface IUsuarioRepository
    {

        //criar usuário
        Task<Usuario> CreateUsuario(Usuario usuario);

        // remover usuário (is_activate = false)
        Task<bool> DeleteUsuario(string EmailUsuario, string SenhaUsuario);

        // login usuário
        Task<Usuario> LoginUsuario(string EmailUsuario, string SenhaUsuario);

        // atualizar usuario
        Task AtualizarUsuario(Usuario usuario);

        // busca usuário pelo cpf_cnpj
        Task<Usuario> SearchUsuarioByCpf(string cpf);
    }
}
