var express = require("express");
var router = express.Router();

/* GET home page. */
router.get("/", function (req, res, next) {
  res.render("index", { title: "Express" });
});

router.use("/cart", require("./cart"));
router.use("/voucher", require("./voucher"));

module.exports = router;
