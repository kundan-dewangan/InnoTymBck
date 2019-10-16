app.factory('transactionService', function ($http) {

    var transactionFactory = {};
    //Transaction list details
    transactionFactory.getTransactionCustome = function (userid) {
        return $http.post("http://localhost/InnoTymBck.Api/api/Transactions/getTransList?userid=" + userid);        
        }
    return transactionFactory;
});