using FINAL_POO_Sistema_de_Gestion.Controllers;
using System;
using System.Collections.Generic;

namespace FINAL_POO_Sistema_de_Gestion.Entidades
{
    public class ServicioCheckeoVencimientos
    {
        private readonly StockController _stockController;
        private readonly ProductoController _productoController;
        private readonly Func<DateTime> _obtenerFechaActual;
        public event Action<List<string>> ProductosVencidosProcesados;

        public ServicioCheckeoVencimientos(StockController stockController, ProductoController productoController, Func<DateTime> obtenerFechaActual)
        {
            _stockController = stockController;
            _productoController = productoController;
            _obtenerFechaActual = obtenerFechaActual;
        }

        public void Check()
        {
            if (_productoController.ListarTodosLosProductos().Count == 0) return;

            var procesados = _stockController.ProcesarProductosVencidos(_obtenerFechaActual());
            if (procesados.Count > 0)
                ProductosVencidosProcesados?.Invoke(procesados);
        }
    }
}
