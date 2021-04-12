import express from "express";
import comprasRouter from "./routes/compras.js";

const app = express();
const PORT = 3000;

app.use("/compras", comprasRouter);
// app.use("/clientes", clientesRouter);
// app.use("/inventario", inventarioRouter);
// app.use("/empleados", empleadosRouter);

app.all("*", (req, res) => res.send({
    success: false,
    msg: "This route does not exist"}, 404));

app.listen(PORT, () => console.log(`Server running on port ${PORT}`));
