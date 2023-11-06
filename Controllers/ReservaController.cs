using LabReserva.Boleto;
using LabReserva.Data;
using LabReserva.Model;
using LabReserva.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserva.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private readonly IReservaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILaboratorioRepository _laboratorioRepository;
        private readonly InterfaceBoleto _boletoInterface;

        public ReservaController(IReservaRepository repository, IUsuarioRepository usuario, ILaboratorioRepository laboratorio, InterfaceBoleto boleto)
        {
            _repository = repository;
            _usuarioRepository = usuario;
            _laboratorioRepository = laboratorio;
            _boletoInterface = boleto;
        } 

        // rota para listar todas as reservas
        [HttpGet]
        [Authorize]
        public async Task<List<Reserva>> ListarReservas()
        {
            return await _repository.ListarReservas();
        }

        // rota para buscar reserva pelo id reserva
        [HttpGet("{id}")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<Reserva>> AdicionarReserva([FromBody] NovaReserva novaReserva)
        {
            // seleciona em variáveis separadas o id do usuário e do laboratório
            int userId = novaReserva.IdUsuario;
            int labId = novaReserva.IdLaboratorio;

            // verificar usuário (se existe/está ativo)
            if (!await _usuarioRepository.BuscaUsuarioById(userId))
            {
                return BadRequest("Usuario Não Existe/Está inativo!");
            }


            // verificar laboratio (se existe/está ativo)
            if (!await _laboratorioRepository.VerificaLaboratorioById(labId))
            {
                return BadRequest("Laboratório não existe/está inativo!");
            }
            

            // verificar se já o laboratorio não está reservado no mesmo dia/horario
            var verificaDisponibilidade = await _repository.VerificaDisponibilidadeReserva(novaReserva.IdLaboratorio, novaReserva.DiaHorarioReserva);

            if (verificaDisponibilidade == false)
            {
                return BadRequest("Laboratório já está reservado! Selecione outro dia/horário.");
            }

            // boleto
            // convertendo para string
            string boleto = novaReserva.NumeroBoleto.ToString();

            // verificando se o boleto possui 8 digitos
            if (boleto.Length != 8 )
            {
                return BadRequest("Boleto Inválido! Insira-o novamente.");
            }

            var resultBoleto = await _boletoInterface.pagarBoleto(boleto, novaReserva.IdUsuario);

            if (resultBoleto.status != "approved")
            {
                return BadRequest("Boleto Não Aprovado! Tente novamente mais tarde.");
            }

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

        /*
        [HttpPost("teste/boleto")]
        public async Task<BoletoResponseContent> TesteBoleto([FromBody] BoletoBody boletoBody)
        {

            return await _boletoInterface.pagarBoleto(boletoBody.boleto, boletoBody.user_id);

        }
        */

    }
}
