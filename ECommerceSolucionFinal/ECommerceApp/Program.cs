using System;
using System.Collections.Generic;
using System.Threading;
using ECommerceLibrary;

namespace ECommerceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Pedido pedido = new Pedido(1, "Juan Pérez", new List<string> { "Laptop", "Mouse" });
            ProcesadorDePedido procesador = new ProcesadorDePedido();

            Console.WriteLine("== Iniciando procesamiento del pedido ==");

            bool pedidoValido = false;
            bool pagoExitoso = false;

            Thread validarThread = new Thread(() => { pedidoValido = procesador.ValidarPedido(pedido); });
            validarThread.Start();
            validarThread.Join();

            if (!pedidoValido)
            {
                Console.WriteLine("Procesamiento cancelado: error en validación.");
                return;
            }

            Thread pagoThread = new Thread(() => { pagoExitoso = procesador.CobrarPago(pedido); });
            pagoThread.Start();
            pagoThread.Join();

            if (!pagoExitoso)
            {
                Console.WriteLine("Procesamiento cancelado: pago no aprobado.");
                return;
            }

            Thread inventarioThread = new Thread(() => procesador.ActualizarInventario(pedido));
            Thread notificacionThread = new Thread(() => procesador.NotificarCliente(pedido));

            inventarioThread.Start();
            notificacionThread.Start();

            inventarioThread.Join();
            notificacionThread.Join();

            Console.WriteLine("== Pedido procesado exitosamente ==");
        }
    }
}
