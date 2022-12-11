const mongoose = require("mongoose");

const Product = mongoose.Schema({
  id: String,
  name: String,
  description: String,
  producerId: Number,
  count: Number,
  image: String,
  type: String,
  price: String,
  hsd: String,
  hsx: String,
});

const Cart = mongoose.Schema(
  {
    products: [Product],
    voucher: String,
    status: {
      type: String,
      enum: ["danggiao", "hoantat", "dahuy", "choxacnhan"],
      default: "choxacnhan",
    },
  },
  { timestamps: true }
);

const CartModal = mongoose.Schema({
  cart: [Cart],
  UID: String,
  nameOfUser: String,
  phone: Number,
  address: String,
});

module.exports = mongoose.model("cart", CartModal);
