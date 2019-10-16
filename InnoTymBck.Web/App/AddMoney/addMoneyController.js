
app.controller('addMoneyController', function ($scope, $rootScope, userService) {
    $rootScope.listOfUser;
    $scope.msg;
    $scope.add = function (moneyDetail) {
        var tatalAmount = parseInt($rootScope.listOfUser.Amount) + parseInt(moneyDetail.rs);
        $rootScope.listOfUser.Amount = tatalAmount.toString();
        var obj = {
            userId: $rootScope.listOfUser.Id,
            money: moneyDetail.rs
        };

        userService.addMoney(obj).then(function (results) {
            if (results.data == true) {
                $scope.msg = "Added Money To Your Account Successfully";
            } else {
                $scope.msg = "Added Money Failed";
            }          

        }), function (error) {
            alert("Erro occured while Added Money.");
        }
    }


});


