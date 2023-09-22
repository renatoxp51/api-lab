using LabReserva.Data;
using LabReserva.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabReserva.Repositories;
using LabReserva.Services;
using Microsoft.AspNetCore.Authorization;

namespace LabReserva.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ReservaLabContext _context; 
        private readonly IUsuarioRepository _repository;

        public UsuarioController(ReservaLabContext context, IUsuarioRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet("testeconnection")]
        public IActionResult TestConnection()
        {
            try
            {
                _context.Database.OpenConnection();
                _context.Database.CloseConnection();

                return Ok("Conexão Está Funcionando!");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao conectar ao banco de dados!");
            }
        }

        // rota para criar um usuário
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] SimplesUsuario usuario)
        {

            var novoUsuario = new Usuario()
            {
                // mapeando os atributos do novo usuário para uma classe usuário
                IdTipoUsuario = usuario.IdTipoUsuario,
                NomeUsuario = usuario.NomeUsuario,
                EmailUsuario = usuario.EmailUsuario,
                SenhaUsuario = usuario.SenhaUsuario,
                CpfCnpjUsuario = usuario.CpfCnpjUsuario,
                TelefoneUsuario = usuario.TelefoneUsuario,
                IsActivate = true
            };

            var novo_usuario = await _repository.CreateUsuario(novoUsuario);
            return CreatedAtAction(nameof(CreateUsuario), new { id = novo_usuario.IdUsuario }, novo_usuario);
        }

        // rota para inativar um usuário
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteUsuario([FromBody] LoginUsuario body)
        {

            if (await _repository.DeleteUsuario(body.EmailUsuario, body.SenhaUsuario))
            {
                return NoContent();
            } 

            return BadRequest();

        }

        // rota para login do usuário
        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> LoginUsuario([FromBody] LoginUsuario login)
        {
            // chamando o método de login do repository
            var usuario =  await _repository.LoginUsuario(login.EmailUsuario, login.SenhaUsuario);

            if (usuario==null)
            {
                return NotFound(new { message = "Usuário ou Senha Inválidos!" });
            }

            // gerando o token
            var token = new TokenService();
            var strToken = token.Generate(login);

            // retornando o token e o usuário
            return new
            {
                MyToken = strToken,
                Usuario = usuario
            };

        }

        // rota para atualizar o usuário
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Usuario>> UpdateUsuario([FromBody] SimplesUsuario body)
        {

            // buscar o usuário com base no cpf_cnpj
            Usuario usuario_buscado = await _repository.SearchUsuarioByCpf(body.CpfCnpjUsuario);

            // atribuir o usuário buscado a variável "usuario_modificado"
            Usuario usuario_modificado = usuario_buscado;

            // modificando os atributos da nova variável
            usuario_modificado.NomeUsuario = body.NomeUsuario;
            usuario_modificado.EmailUsuario = body.EmailUsuario;
            usuario_modificado.SenhaUsuario = body.SenhaUsuario;
            usuario_modificado.CpfCnpjUsuario = body.CpfCnpjUsuario;
            usuario_modificado.TelefoneUsuario = body.TelefoneUsuario;

            // chamando a função do repository
            await _repository.AtualizarUsuario(usuario_modificado);

            // retornando o usuário já alterado
            return usuario_modificado;

        }

        // rota teste para gerar token
        /*
        rota para retornar um token
        [HttpPost("testeToken")]
        public string GenerateToken([FromBody] LoginUsuario loginUsuario)
        {

            var token = new TokenService();
            return token.Generate(loginUsuario);
        }
        */


    }
}
