const co = require("co");
const Voucher = require("../models/voucherModel");

module.exports = {
  create: (req, res) => {
    // const { voucher } = req.body;
    co(function* () {
      const checkVoucher = yield Voucher.findOne({ voucher: req.body.voucher });
      if (checkVoucher) {
        return Promise.reject(new Error("Voucher is Exiteds"));
      }

      const voucher = yield Voucher.create(req.body);
      return voucher;
    })
      .then((data) => res.status(201).json(data))
      .catch((err) => res.status(500).json(err));
  },
  get: (req, res) => {
    co(function* () {
      const voucher = yield Voucher.findOne({ voucher: req.body.voucher });
      if (!voucher) {
        return Promise.reject(new Error("Voucher is Exiteds"));
      }
      return voucher;
    })
      .then((data) => res.status(201).json(data))
      .catch((err) => res.status(500).json(err));
  },
  getAll: (req, res) => {
    co(function* () {
      const vouchers = yield Voucher.find();
      return vouchers;
    })
      .then((data) => res.status(201).json(data))
      .catch((err) => res.status(500).json(err));
  },
  update: (req, res) => {
    co(function* () {
      const voucher = yield Voucher.findByIdAndUpdate(req.body.id, req.body);
      if (!voucher) {
        return Promise.reject(new Error("Voucher not found"));
      }

      return voucher;
    })
      .then((data) => res.status(201).json(data))
      .catch((err) => res.status(500).json(err));
  },
  delete: (req, res) => {
    co(function* () {
      const voucher = yield Voucher.findByIdAndDelete(req.params.id);
      if (!voucher) {
        return Promise.reject(new Error("Voucher not found"));
      }

      return voucher;
    })
      .then((data) => res.status(201).json(data))
      .catch((err) => res.status(500).json(err));
  },
};
