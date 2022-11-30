const api = {
    wallet: {
        get: "http://wsl:82/wallet/get",
        create: "http://wsl:82/wallet/create"
    },
    payment:
    {
        create: 'http://wsl:83/payment/create'
    },
    auth:
    {
        token: 'http://wsl:84/auth/token'
    }
};

module.exports = api