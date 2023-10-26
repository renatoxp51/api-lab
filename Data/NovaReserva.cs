using LabReserva.Model;

namespace LabReserva.Data
{
    public class NovaReserva
    {

        public int IdUsuario { get; set; }

        public int IdLaboratorio { get; set; }

        public DateTime DiaHorarioReserva { get; set; }

        public int NumeroBoleto {get; set; }

    }
}
