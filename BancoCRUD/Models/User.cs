using System;
using System.Collections.Generic;

namespace BancoCRUD.Models;

public partial class User
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ContrasenaUsuario { get; set; } = null!;

    public string TipoUsuario { get; set; } = null!;

    public float SaldoUsuario { get; set; }

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
