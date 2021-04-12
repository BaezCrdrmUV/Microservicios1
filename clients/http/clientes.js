import axios from "axios";

export const BuscaClientes = async ({nombre, idCliente}) => {
    let url = "http://localhost:8092" + "/clientes/buscar";
    try {
        let response = await axios.get(url, {
            params: {
                nombre: nombre,
                idCliente: idCliente
            }
        });
        // console.log("Resp", response);
        return response;
    } catch (error) {
        console.error("BuscaCliente", error);
        return null;
    }
}
