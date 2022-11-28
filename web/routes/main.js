'use strict';
var path = require('path');
var express = require('express');
var router = express.Router();

router.get('/', function (req, res) {
    res.sendFile(path.resolve('views/main.html'));
});
module.exports = router;
