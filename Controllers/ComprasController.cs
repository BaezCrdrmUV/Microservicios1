using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayTienda.Clients;
using GatewayTienda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSClientes.Models;
using MSCompras.Models;
using MSInventario.Models;

namespace GatewayTienda
{
    [ApiController]
    [Route("[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly ILogger<ComprasController> _logger;
        private InventarioClient inventarioClient;
        private ClientesClient clientesClient;
        private readonly ComprasClient comprasClient;

        public ComprasController(ILogger<ComprasController> logger)
        {
            _logger = logger;
            inventarioClient = new InventarioClient();
            clientesClient = new ClientesClient();
            comprasClient = new ComprasClient();
        }

        [HttpGet("clientes")]
        public async Task<ActionResult<Cliente>> Get([FromQuery]string nombre = "", [FromQuery]int idCliente = -1)
        {
            var value = await clientesClient.BuscaClientes(nombre, idCliente);
            if(value.Count() > 0)
            {
                return Ok(value);
            } else return BadRequest();
        }

        [HttpGet("dispositivo")]
        public async Task<ActionResult<Dispositivo>> GetDispositivo()
        {
            var value = await inventarioClient.BuscaDispositivo();
            if(value.Count() > 0)
            {
                return Ok(value);
            } else return BadRequest();
        }

        [HttpGet("buscarCompra")]
        public async Task<ActionResult<Compra>> ObtenerCompra([FromQuery]int idCompra = -1)
        {
            ActionResult resultado = BadRequest();
            Compra[] compras = await comprasClient.BuscaCompra(idCompra);

            if (compras != null)
            {
                resultado = Ok(compras);
            }

            return resultado;
        }

        [HttpGet("detallesCompra/{idCompra}")]
        public async Task<ActionResult<CompraCliente>> ObtenerDetallesCompra(int idCompra)
        {
            ActionResult resultado = BadRequest();
            Compra compra = await comprasClient.ObtenerDetallesCompra(idCompra);
            if(compra != null)
            {
                Cliente[] clientes = await clientesClient.BuscaClientes("", compra.IdCliente);
                
                if(clientes != null && clientes.Count() > 0)
                {
                    Cliente cliente = clientes[0];
                    CompraCliente comprasCliente = new CompraCliente(compra, cliente);
                    resultado = Ok(comprasCliente);

                    return resultado;
                } else return BadRequest("Usuario no encontrado");
            } else return BadRequest("Id no válido");
        }
    }
}