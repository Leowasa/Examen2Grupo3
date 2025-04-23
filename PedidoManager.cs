using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Examen2Grupo3.RegistroPedidos;

namespace Examen2Grupo3
{
    class PedidoManager
    {
        private string rutaArchivo = "historialPedidos.json";

        public List<Pedido> CargarPedidos()
        {
            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
            }

            return new List<Pedido>();
        }

        public void GuardarPedido(Pedido pedido)
        {
            List<Pedido> listaPedidos = CargarPedidos();
            listaPedidos.Add(pedido);

            string json = JsonSerializer.Serialize(listaPedidos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(rutaArchivo, json);
        }
    }
}
