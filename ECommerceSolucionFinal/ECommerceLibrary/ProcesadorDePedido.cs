using System;
using System.Collections.Generic;
using System.Threading;

namespace ECommerceLibrary
{
    public class ProcesadorDePedido
    {
        private Dictionary<string, int> inventario = new Dictionary<string, int>
        {
            { "Laptop", 5 },
            { "Mouse", 10 },
            { "Teclado", 7 }
        };

        private decimal saldoCliente = 1000m;

        public bool ValidarPedido(Pedido pedido)
        {
            Console.WriteLine("Validando pedido...");
            foreach (var item in pedido.Articulos)
            {
                if (!inventario.ContainsKey(item) || inventario[item] <= 0)
                {
                    Console.WriteLine($"Artículo {item} no disponible en inventario.");
                    return false;
                }
            }
            Thread.Sleep(1000);
            Console.WriteLine("Pedido validado.");
            return true;
        }

        public bool CobrarPago(Pedido pedido)
        {
            Console.WriteLine("Procesando pago...");
            decimal costo = pedido.Articulos.Count * 150;
            if (saldoCliente >= costo)
            {
                saldoCliente -= costo;
                Thread.Sleep(1000);
                Console.WriteLine($"Pago de ${costo} procesado con éxito.");
                return true;
            }
            else
            {
                Console.WriteLine("Fondos insuficientes del cliente.");
                return false;
            }
        }

        public void ActualizarInventario(Pedido pedido)
        {
            Console.WriteLine("Actualizando inventario...");
            foreach (var item in pedido.Articulos)
            {
                if (inventario.ContainsKey(item))
                    inventario[item]--;
            }
            Thread.Sleep(1000);
            Console.WriteLine("Inventario actualizado.");
        }

        public void NotificarCliente(Pedido pedido)
        {
            Console.WriteLine($"Enviando confirmación de pedido a {pedido.Cliente}...");
            Thread.Sleep(500);
            Console.WriteLine("Correo de confirmación enviado.");
        }
    }
}
