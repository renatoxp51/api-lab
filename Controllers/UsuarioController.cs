using LabReserva.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabReserva.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ReservaLabContext _context; 

        public UsuarioController(ReservaLabContext context)
        {
            _context = context;
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


    }
}
