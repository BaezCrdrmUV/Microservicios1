using MSClientes.Models;
using MSCompras.Models;

namespace GatewayTienda.Models
{
    public class CompraCliente
    {
        public Compra Compra { get; set; }
        public Cliente Cliente { get; set; }

        public CompraCliente(Compra _compra, Cliente _cliente)
        {
            this.Compra = _compra;
            this.Cliente = _cliente;
        }
    }
}