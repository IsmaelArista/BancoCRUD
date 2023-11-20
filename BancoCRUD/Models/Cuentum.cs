using System;
using System.Collections.Generic;

namespace BancoCRUD.Models;

public partial class Cuentum
{
    public int IdCuenta { get; set; }

    public int IdUsuario { get; set; }

    public string NumeroCuenta { get; set; } = null!;

    public string TipoCuenta { get; set; } = null!;

    public float SaldoCuenta { get; set; }

    public virtual User IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Tarjetum> Tarjeta { get; set; } = new List<Tarjetum>();

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
