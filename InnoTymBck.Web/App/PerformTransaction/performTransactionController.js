
app.controller('performTransactionController', function ($scope, $rootScope, userService, transactionService) {

    $rootScope.listOfUser;
   

        $scope.Users = [];
        $scope.Transactions = [];   
        $scope.errordata;
        $scope.errordatasuccess;
        //get all user in dropdown list
        $scope.performTransList = function () {
            userService.adminList().then(function (result) {
                $scope.Users = result.data;
            });
        }

        $scope.send = function (money) {
            var userName = money.allData.UName;
            var obj =
           {
               userId: money.allData.Id,
               userAmount: money.amount,
               loginUser: $rootScope.listOfUser.Id
           };
            userService.performTransaction(obj).then(function (result) {
                if (result.data == true) {
                    $scope.errordatasuccess = "Payment Successful with "+userName;
                    $scope.errordata = null;
                } else {
                    $scope.errordata = "Insufficient Balance.";
                    $scope.errordatasuccess = null;
                }
                

            });

        }

    //put data debit from select drop down list in user table
    //    $scope.send = function (money) {
    //        var amountcheck =parseInt($rootScope.listOfUser.Amount);
    //        if (amountcheck >= money.amount) {             //check balance
    //        $scope.InisialAmountUser=[]
    //        $scope.InisialAmountUser = $rootScope.listOfUser.Amount;
    //        var totalAmount = parseInt($rootScope.listOfUser.Amount) - parseInt(money.amount);
    //        $rootScope.listOfUser.Amount = totalAmount.toString();
    //        userService.putUserCustome($rootScope.listOfUser).then(function (results) {
    //       // $scope.Users = results.data.value;

    //                //put data credit from select drop down list in user table
    //                var totalAmount = parseInt(money.allData.Amount) + parseInt(money.amount);
    //                $scope.creditUserDetails = {
    //                    Amount: totalAmount.toString(),
    //                    CreateDate: new Date(),
    //                    Email: money.allData.Email,
    //                    Gender: money.allData.Gender,
    //                    Id: money.allData.Id,
    //                    ImageUrl: money.allData.ImageUrl,
    //                    IsAdmin: money.allData.IsAdmin,
    //                    PasswordHash: money.allData.PasswordHash,
    //                    PhoneNumber: money.allData.PasswordHash,
    //                    UName: money.allData.UName
    //                }
    //                userService.putUser($scope.creditUserDetails).then(function (resultsFirst) {
    //                    //Post data from login user in transaction table
    //                    $scope.creditObj = {
    //                        UserId: $rootScope.listOfUser.Id,
    //                        RefId: money.allData.Id,
    //                        TransType: "Debit",
    //                        InisialAmount: $scope.InisialAmountUser,
    //                        TransAmount: money.amount,
    //                        TransDate: new Date()
    //                    }
    //                    transactionService.postTransaction($scope.creditObj).then(function (resultsSecond) {
    //                       // $rootScope.listOfUser = resultsSecond.data.value;

    //                        //Post data from select drop down list in transaction table
    //                        $scope.debitObj = {
    //                            UserId: money.allData.Id,
    //                            RefId: $rootScope.listOfUser.Id,
    //                            TransType: "Credit",
    //                            InisialAmount: money.allData.Amount,
    //                            TransAmount: money.amount,
    //                            TransDate: new Date()
    //                        }
    //                        transactionService.postTransaction($scope.debitObj).then(function (resultsThird) {
    //                            $scope.errordatasuccess = "Payment Successfull";
    //                            $scope.errordata = null;
    //                        }), function (error) {
    //                            alert("Erro occured while post transaction.");
    //                        }
    //                    }), function (error) {
    //                        alert("Erro occured while post transaction.");
    //                    }
    //                }), function (error) {
    //                    alert("Erro occured while put user.");
    //                }
    //        }), function (error) {
    //            alert("Erro occured while Added Money.");
    //        }
      
    //        } else { //if condition end
    //            $scope.errordata = "Insufficient Balance."
    //            $scope.errordatasuccess = null;
    //        }
    //}
                   

});


