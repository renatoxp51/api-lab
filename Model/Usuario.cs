using System;
using System.Collections.Generic;

namespace LabReserva.Model;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdTipoUsuario { get; set; }

    public string? NomeUsuario { get; set; }

    public string? EmailUsuario { get; set; }

    public string? SenhaUsuario { get; set; }

    public bool? IsActivate { get; set; }

    public string? CpfCnpjUsuario { get; set; }

    public string? TelefoneUsuario { get; set; }

    public virtual TipoUsuario? IdTipoUsuarioNavigation { get; set; }

    public virtual ICollection<Reserva> TbReservas { get; set; } = new List<Reserva>();
}
