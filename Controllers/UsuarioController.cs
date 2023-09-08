using LabReserva.Data;
using LabReserva.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabReserva.Repositories;

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

        [HttpGet("{IdUsuario}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int IdUsuario)
        {

            return await _repository.GetUsuario(IdUsuario);

        }


    }
}
