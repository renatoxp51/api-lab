using System;
using System.Collections.Generic;

namespace LabReserva.Model;

public partial class TipoUsuario
{
    public int IdTipoUsuario { get; set; }

    public string? Descricao { get; set; }

    public virtual ICollection<Usuario> TbUsuarios { get; set; } = new List<Usuario>();
}
