using System;
using System.Collections.Generic;

namespace LabReserva.Model;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdLaboratorio { get; set; }

    public DateTime? DiaHorarioReserva { get; set; }

    public virtual Laboratorio? IdLaboratorioNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
