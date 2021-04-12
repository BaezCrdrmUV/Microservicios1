import express from "express";
import { BuscaClientes } from "../clients/http/clientes.js";
import { ObtenerDetallesCompra } from "../clients/http/compras.js";

const router = express.Router();

router.get("/clientes", async (req, res) => {
    // Función;
    // console.log("Entró a endpoint de clientes");
    const {nombre, idCliente} = req.query;
    console.log("nombre", nombre);
    console.log("idCliente", idCliente);

    // let values = await BuscaClientes(nombre, idCliente);
    // console.log("data", values);
    // let response = {
    //     success: true,
    //     data: values.data || {}
    // }
    // res.send(response);

    BuscaClientes(nombre, idCliente)
    .then(values => {
        console.log("data", values);
        let response = {
            success: true,
            data: values.data || {}
        }
        res.send(response);
    })
    .catch(error => {
        console.error("Busca Cliente compras/clientes", error);
    });
});

router.get("/detallesCompra/:idCompra", (req, res) => {
    // Función
    // console.log("idCompra", req.params);
    let idCompra = req.params.idCompra || -1;
    // console.log("Entró a endpoint de detallesCompra", req.params);

    if(idCompra >= 0)
    {
        ObtenerDetallesCompra(idCompra)
        .then(resp => {
            // console.log("Resp >>", resp);
            if(resp.statusText == 'OK')
            {
                BuscaClientes({idCliente: resp.data.idCliente})
                .then(values => {
                    // console.log("data", values);
                    let cliente = {
                        success: true,
                        data: values.data || {}
                    }

                    let response = {
                        cliente: cliente.data,
                        compra: resp.data
                    }
                    res.send(response);
                })
                .catch(error => {
                    console.error("Busca Cliente compras/clientes", error);
                    res.send({
                        success: false,
                        msg: "Error en Busca",
                        error: error
                    });
                });
            }
        })
        .catch(error => {
            console.error("ObtenerDetallesCompra compras/clientes", error);
            res.send({
                success: false,
                msg: "Error en ObtenerDetallesCompra",
                error: error
            });
        });
    }
});

export default router;
