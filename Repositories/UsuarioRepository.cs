using LabReserva.Data;
using LabReserva.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabReserva.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        public readonly ReservaLabContext _context;

        public UsuarioRepository(ReservaLabContext context)
        {
            _context = context; 
        }



        /*
        public async Task<Usuario> GetUsuario(int IdUsuario)
        {

            return await _context.TbUsuarios.FindAsync(IdUsuario);
;
        }
        */

        // implementando a criação de um novo usuário
        public async Task<Usuario> CreateUsuario(Usuario usuario)
        {

            // verificando se já existe um usuário com o mesmo e-mail e/ou mesmo cpf_cnpj
            var query = _context.TbUsuarios.
                FromSql($"select * from tb_usuario where email_usuario = {usuario.EmailUsuario} or cpf_cnpj_usuario = {usuario.CpfCnpjUsuario}");
            var qtdLinhas = await query.CountAsync();


            if (qtdLinhas > 0)
            {
                throw new Exception("Já existe um usuário com o e-mail e/ou cpf_cnpj informado!");
            }

            try
            {
                // adicionando o usuário no banco de dados e salvando a transação.
                await _context.TbUsuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return usuario;

            } catch (Exception ex)
            {
                throw new Exception("Não foi possível cadastrar um novo usuário! Verifique os campos ou tente novamente mais tarde.", ex);
            }
;        }

        // implementando a inativação do usuário
        public async Task<bool> DesativarUsuario(string EmailUsuario, string SenhaUsuario)
        {
            try
            {
                // buscando o usuário que contém o e-mail e senha informado nos parâmetros
                var usuario = await _context.TbUsuarios.FirstOrDefaultAsync(u => u.EmailUsuario == EmailUsuario && u.SenhaUsuario == SenhaUsuario);
                

                // caso seja encontrado, o campo "IsActivate" será alterado para falso, assim inativando o usuário.
                if (usuario != null)
                {
                    usuario.IsActivate = false;
                    await _context.SaveChangesAsync();
                    return true;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Email e/ou Senha não correspondem.");
            }

            return false;
        }

        //implementando o login do usuário
        public async Task<Usuario> LoginUsuario(string EmailUsuario, string SenhaUsuario)
        {
            //verificar se existe um usuário com o e-mail e senha informado
            var usuario = await _context.TbUsuarios.
                FirstOrDefaultAsync(u => u.EmailUsuario == EmailUsuario && u.SenhaUsuario == SenhaUsuario && u.IsActivate != false);

            return usuario;
        }

        // implementando a atualização do usuário
        public async Task AtualizarUsuario(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // implementando a busca do usuário pelo cpf
        public async Task<Usuario> SearchUsuarioByCpf(string cpf)
        {

            var usuario = await _context.TbUsuarios.FirstOrDefaultAsync(u => u.CpfCnpjUsuario == cpf);

            if (usuario!=null)
            {
                return usuario;
            }

            throw new Exception("CPF/CNPJ Não encontrado!");

        }

        // implementando a ativação do usuário pelo id
        public async Task<bool> AtivarUsuarioById(int id)
        {
            var usuario = await _context.TbUsuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario.IsActivate == false)
            {
                usuario.IsActivate = true;
                _context.SaveChangesAsync();
                return true;
            }

            return false;


        }

        // busca de todos os usuários
        public async Task<List<Usuario>> GetAllUsuario()
        {
            return await _context.TbUsuarios.ToListAsync();
        }

        public async Task<bool> BuscaUsuarioById(int id)
        {
            var usuario = await _context.TbUsuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && u.IsActivate == true);

            if (usuario != null)
            {
                return true;
            }

            return false;


        }
    }

}
