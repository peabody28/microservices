'use strict';
var path = require('path');
var express = require('express');
var router = express.Router();
var axios = require('axios');
var api = require('./../constants/apiRoutes')
var httpHelper = require('./../helpers/httpHelper')

const urlencodedParser = express.urlencoded({ extended: false });

router.get('/', async function (req, res) {

    var wallets = []

    await axios.get(api.wallet.get,
        httpHelper.getAuthHeader(req.cookies.accessToken)).then(function (response)
    {
        wallets = response.data
    }).catch(function (response) {
        res.status(400).json(response);
    });

    res.render("payment", { wallets: wallets });
});

router.post('/', urlencodedParser, async function (req, res) {
    if (!req.body) return res.sendStatus(400);

    var data = {
        walletNumber: req.body.wallet,
        amount: req.body.amount,
        balanceOperationTypeCode: req.body.balanceOperationType
    }

    await axios.post(api.payment.create, data,
        httpHelper.getAuthHeader(req.cookies.accessToken)).then(function (response)
    {
        res.status(200).json({ status: true })

    }).catch(function (response) {
        res.status(400).json(response);
    });;
});

module.exports = router;
