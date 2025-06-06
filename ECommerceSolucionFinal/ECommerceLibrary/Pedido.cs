using System.Collections.Generic;

namespace ECommerceLibrary
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public List<string> Articulos { get; set; }

        public Pedido(int id, string cliente, List<string> articulos)
        {
            Id = id;
            Cliente = cliente;
            Articulos = articulos;
        }
    }
}
