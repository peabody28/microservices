
module.exports = {
    getAuthHeader:function (accessToken)
    {
        return {
            headers: { Authorization: "Bearer " + accessToken }
        }
    }
}

