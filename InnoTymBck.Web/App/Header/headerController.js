
app.controller('headerController', function ($scope, $rootScope, $location, localStorageService) {
    if (localStorageService === null) {
        localStorageService.remove('userData');
    } else {
        $rootScope.listOfUser = localStorageService.get('userData');
    }
        
    $scope.logout = function () {
        localStorageService.remove('userData');
    $rootScope.listOfUser = {};
    $location.path("/login")
    }

});


