app.factory('userService', function ($http) {

    var userFactory = {};
    //Get all user list
    userFactory.adminList = function () {
        return $http.post("http://localhost/InnoTymBck.Api/api/Users/getAdminCustome");
    }

    //Send money to selected user
    userFactory.performTransaction = function (model) {
        return $http.post("http://localhost/InnoTymBck.Api/api/Users/performTransCustome", model);
    }

    //user login 
    userFactory.userLogin = function (model) {
        return $http.post("http://localhost/InnoTymBck.Api/api/Users/getUserCustome", model);
    }

    //Registration process
    userFactory.userRegistration = function (model) {
        return $http.post("http://localhost/InnoTymBck.Api/api/Users/PostUser", model);
    }

    //Add Money process
    userFactory.addMoney = function (model) {
        return $http.post("http://localhost/InnoTymBck.Api/api/Users/addMoneyCustome", model);
    }

    return userFactory;
});