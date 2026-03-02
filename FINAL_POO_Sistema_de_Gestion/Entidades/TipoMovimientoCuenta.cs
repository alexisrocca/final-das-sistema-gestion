using System;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public enum TipoMovimientoCuenta
    {
        Debito,   // Compra a cuenta corriente (aumenta deuda)
        Credito   // Pago del cliente (reduce deuda)
    }
}
