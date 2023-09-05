using System;
using System.Collections.Generic;

namespace LabReserva.Model;

public partial class Laboratorio
{
    public int IdLaboratorio { get; set; }

    public string? NomeLaboratorio { get; set; }

    public int? AndarLaboratorio { get; set; }

    public string? DescricaoLaboratorio { get; set; }

    public bool? IsActivate { get; set; }

    public virtual ICollection<Reserva> TbReservas { get; set; } = new List<Reserva>();
}
