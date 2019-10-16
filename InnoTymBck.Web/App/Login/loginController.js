
app.controller('loginController', function ($scope, $location, $rootScope, localStorageService, transactionService, userService, $timeout) {

    $scope.login = function (submitUser) {
        var isContinue = true;
        $scope.UserData;
        $scope.success;
        $scope.User = {};
        $scope.User.emailId = submitUser.emailId;
        $scope.User.password = submitUser.password;
        userService.userLogin($scope.User).then(function (results) {
            if (results.data.data1 == true) {
                $rootScope.listOfUser = results.data.data2;
                localStorageService.set('userData', results.data.data2);
                //$scope.User= "Login successful..."
                $location.path("/performTransaction")
                isContinue = true;
            } else {
                $scope.UserData = "Username or password is incorrect"
                $location.path("/login")
            }
        }, function (error) {
            //alert("Username or password is incorrect");
            $scope.UserData = "Username or password is incorrect"
        })
    }

  

   // alert("hello kundan");
    $scope.registration = function (registrationDetails) {
        //alert(registration);
        registrationDetails.CreateDate = new Date();
        registrationDetails.IsAdmin = false;
        userService.userRegistration(registrationDetails).then(function (results) {
            //console.log(results);
            $scope.success = "User successfull register.";
            $timeout(function () { $location.path("/login"); }, 3000);
            //$location.path("/login");
            
        }), function (error) {
            alert("Erro occured while registering.");
        }
    }




    
});




