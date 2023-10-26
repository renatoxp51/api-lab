using LabReserva.Data;
using LabReserva.Model;
using LabReserva.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LabReserva.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private readonly IReservaRepository _repository;

        public ReservaController(IReservaRepository repository)
        {
            _repository = repository;
        } 

        // rota para listar todas as reservas
        [HttpGet]
        public async Task<List<Reserva>> ListarReservas()
        {
            return await _repository.ListarReservas();
        }

        // rota para buscar reserva pelo id reserva
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> BuscaReservaById(int id)
        {
            var reserva = await _repository.BuscaReservaById(id);

            if (reserva == null)
            {
                return BadRequest();
            }

            return reserva;
        }

        // rota para buscar reserva(s) pelo id usuario
        [HttpGet("usuario/{id_usuario}")]
        public async Task<List<Reserva>> ListarReservasByUsuarioId(int id_usuario)
        {
            return await _repository.ListarReservasByUsuarioId(id_usuario);
        }

        // rota para verificar disponibilidade do laboratorio
        /*
        [HttpGet("disponivel/{id}")]
        public async Task<ActionResult<bool>> VerificaDisponibilidade(int id,[FromBody] DateTime diaHora)
        {
            var resultado = await _repository.VerificaDisponibilidadeReserva(id, diaHora);

            if (resultado == false)
            {
                return false;
            }

            return true;

        }
        */

        [HttpPost]
        public async Task<ActionResult<Reserva>> AdicionarReserva([FromBody] NovaReserva novaReserva)
        {
            // verificar usuário (se existe/está ativo)

            // verificar laboratio (se existe/está ativo)


            // verificar se já o laboratorio não está reservado no mesmo dia/horario
            var verificaDisponibilidade = await _repository.VerificaDisponibilidadeReserva(novaReserva.IdLaboratorio, novaReserva.DiaHorarioReserva);

            if (verificaDisponibilidade == false)
            {
                return BadRequest();
            }

            // pagar boleto
            // ...

            // criando um objeto Reserva a partir do novaReserva
            Reserva reserva = new Reserva
            {
                IdLaboratorio = novaReserva.IdLaboratorio,
                IdUsuario = novaReserva.IdUsuario,
                DiaHorarioReserva = novaReserva.DiaHorarioReserva
            };

            // adicionando
            var resultado = await _repository.AdicionarReserva(reserva);
            return resultado;

        }
    }
}
