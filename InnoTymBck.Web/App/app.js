var app = angular.module('InnoTymApp', ['ngRoute','LocalStorageModule']);


app.config(function ($routeProvider) {

    $routeProvider.when('/header', {
        controller: 'headerController',
        templateUrl: 'App/Header/header.html'
    });
    
    $routeProvider.when('/login', {
        controller: 'loginController',
        templateUrl: 'App/Login/login.html'
    });
    $routeProvider.when('/registration', {
        controller: 'loginController',
        templateUrl: 'App/Login/registration.html'
    });
    $routeProvider.when('/footer', {
        controller: 'footerController',
        templateUrl: 'App/Footer/footer.html'
    });

    $routeProvider.when('/addMoney', {
        controller: 'addMoneyController',
        templateUrl: 'App/AddMoney/addMoney.html'
    });

    $routeProvider.when('/performTransaction', {
        controller: 'performTransactionController',
        templateUrl: 'App/PerformTransaction/performTransaction.html'
    });
    $routeProvider.when('/transactionHistory', {
        controller: 'transactionHistoryController',
        templateUrl: 'App/transactionHistory/transactionHistory.html'
    });
    $routeProvider.when('/userList', {
        controller: 'userListController',
        templateUrl: 'App/UserList/userList.html'
    });
    $routeProvider.otherwise({
        redirectTo: "/login",
    });
});