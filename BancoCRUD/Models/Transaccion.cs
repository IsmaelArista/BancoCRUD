using System;
using System.Collections.Generic;

namespace BancoCRUD.Models;

public partial class Transaccion
{
    public int IdTransaccion { get; set; }

    public int IdCuenta { get; set; }

    public DateTime FechaTransaccion { get; set; }

    public string TipoTransaccion { get; set; } = null!;

    public float MontoTransaccion { get; set; }

    public string DescripcionTransaccion { get; set; } = null!;

    public virtual Cuentum IdCuentaNavigation { get; set; } = null!;
}
