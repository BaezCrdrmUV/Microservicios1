using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using MSInventario.Models;
using MSCompras.Models;

namespace GatewayTienda.Clients
{
    public class ComprasClient
    {
        private readonly HttpClient client;
        private readonly string urlServicio = "";

        public ComprasClient()
        {
            urlServicio = Environment.GetEnvironmentVariable("URL_MS_COMPRAS");
            client = new HttpClient();
        }

        public async Task<Compra[]> BuscaCompra(int idCompra = -1)
        {
            Compra[] values = null;
            string url = urlServicio + "/compras/buscar?";

            if (idCompra >= 0)
            {
                url += "CompraId=" + idCompra;
            }

            try
            {
                string responseBody = await client.GetStringAsync(url);
                values = JsonConvert.DeserializeObject<Compra[]>(responseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError al obtener una respuesta.");
                Console.WriteLine("Error: {0}", ex.Message);
            }

            return values;
        }

        public async Task<Compra> ObtenerDetallesCompra(int idCompra)
        {
            Compra compra = null;
            string url = urlServicio + "/compras/detalles/";

            if (idCompra >= 0)
            {
                url += idCompra;
            }

            try
            {
                string responseBody = await client.GetStringAsync(url);
                compra = JsonConvert.DeserializeObject<Compra>(responseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError al obtener una respuesta.");
                Console.WriteLine("Error: {0}", ex.Message);
            }

            return compra;
        }
    }
}