'use strict';
var path = require('path');
var express = require('express');
var router = express.Router();
var axios = require('axios');

const urlencodedParser = express.urlencoded({ extended: false });

router.get('/', async function (req, res) {
    // get wallets
    var wallets = []
    await axios.get('http://wsl:82/wallet/get', {
        headers:
        {
            Authorization: "Bearer " + req.cookies.accessToken
        }
    }).then(function (response) {
        wallets = response.data
    });

    res.render("payment", { wallets: wallets });
});

router.post('/', urlencodedParser, async function (req, res) {
    if (!req.body) return res.sendStatus(400);

    var hds = {
        headers:
        {
            Authorization: "Bearer " + req.cookies.accessToken
        }
    }
    var data = {
        walletNumber: req.body.wallet,
        amount: req.body.amount,
        balanceOperationTypeCode: req.body.balanceOperationType
    }

    await axios.post('http://wsl:83/payment/create', data, hds).then(function (response)
    {
        res.status(200).json({status:true})
    });
});

module.exports = router;
