const voucherController = require("../controllers/voucherController");

const Router = require("express").Router();

Router.get("/gets", voucherController.getAll);
Router.post("/get", voucherController.get);
Router.post("/create", voucherController.create);
Router.put("/update/:id", voucherController.update);
Router.delete("/delete/:id", voucherController.delete);

module.exports = Router;
