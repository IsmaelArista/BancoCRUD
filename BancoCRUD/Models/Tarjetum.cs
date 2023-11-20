using System;
using System.Collections.Generic;

namespace BancoCRUD.Models;

public partial class Tarjetum
{
    public int IdTarjeta { get; set; }

    public int IdCuenta { get; set; }

    public int NumeroTarjeta { get; set; }

    public string TipoTarjeta { get; set; } = null!;

    public DateTime FechaExpiracion { get; set; }

    public virtual Cuentum IdCuentaNavigation { get; set; } = null!;
}
