const cartUserController = require("../controllers/cartUserController");

const router = require("express").Router();

router.get("/gets", cartUserController.gets);
router.post("/create", cartUserController.create);
router.get("/:id", cartUserController.get);
router.put("/update/:id", cartUserController.update);
router.delete("/delete/:id", cartUserController.delete);
router.get("/user/:UID", cartUserController.getOrderByUID);
router.delete("/user/delete/:UID", cartUserController.deleteOrderByUID);

module.exports = router;
