import axios from "axios";

export const ObtenerDetallesCompra = async (idCompra) => {
    let url = "http://localhost:8091" + "/compras/detalles/";
    if (idCompra >= 0)
    {
        url += idCompra;
    }

    try {
        let response = await axios.get(url);
        console.log("Compras Resp", response.data);
        return response;
    } catch (error) {
        console.error("ObtenerDetallesCompra", error);
        return null;
    }
}