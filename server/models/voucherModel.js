const mongoose = require("mongoose");
const Voucher = new mongoose.Schema(
  {
    voucher: {
      type: String,
      required: true,
      unique: true,
    },
    dateDie: {
      type: Date,
      default: null,
    },
    sales: {
      type: Number,
      required: true,
    },
  },
  { timestamps: true }
);

module.exports = mongoose.model("Voucher", Voucher);
