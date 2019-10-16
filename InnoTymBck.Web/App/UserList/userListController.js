
app.controller('userListController', function ($scope, userService) {
    $scope.Users = [];

    $scope.getUserList = function () {
        userService.adminList().then(function (result) {
            $scope.Users = result.data;
        });
    }
});


