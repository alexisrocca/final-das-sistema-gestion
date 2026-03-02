using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public enum TipoMovimiento
    {
        Ingreso = 1,
        Egreso = 2
    }

    public enum TipoEgreso
    {
        Venta = 1,
        Merma = 2,
        Devolucion = 3,
        Otro = 4
    }
}