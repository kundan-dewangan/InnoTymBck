
app.controller('transactionHistoryController', function ($scope, $rootScope, transactionService, localStorageService) {
    $scope.Transactions = [];
    //$rootScope.listOfUser;
    $rootScope.listOfUser = localStorageService.get('userData');
    $scope.getTransHistoryList = function () {
        var userid = $rootScope.listOfUser.Id;
        //var filter = "&$filter=";
        //filter = filter + "UserId eq " + $rootScope.listOfUser.Id;
       // +  " or RefId eq " + $rootScope.listOfUser.Id
        transactionService.getTransactionCustome(userid).then(function (result) {
           // alert(result);
           // console.log(result);
            $scope.Transactions = result.data;
           // alert("jj");
            //alert($scope.Transactions[1].RefId);
        });
    }
});


