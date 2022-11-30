'use strict';
var path = require('path');
var express = require('express');
var router = express.Router();
var axios = require('axios');
var api = require('./../constants/apiRoutes')

const urlencodedParser = express.urlencoded({ extended: false });

router.get('/', function (req, res) {
    res.sendFile(path.resolve('views/login.html'));
});

router.post('/', urlencodedParser, async function (req, res) {
    if (!req.body) return res.sendStatus(400);

    await axios.post(api.auth.token, {
        name: req.body.userName,
        password: req.body.password
    })
    .then(function (response) {
        res.cookie('accessToken', response.data.accessToken)

        res.status(200).json({status: true, redirectUrl: "/main"})
    })
    .catch(function (error) {
        res.status(500).json({ status: false, message: error });
    });
});

module.exports = router;
