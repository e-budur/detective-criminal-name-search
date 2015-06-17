// create the module and name it scotchApp
var scotchApp = angular.module('scotchApp', ['ngRoute']);

// configure our routes
scotchApp.config(function ($routeProvider) {
	$routeProvider

	// route for the home page
		.when('/', {
			templateUrl: 'pages/home.html',
			controller: 'mainController'
		})

	// route for the about page
		.when('/search/:query', {
			templateUrl: 'pages/search.html',
			controller: 'searchController'
		})

	// route for the contact page
		.when('/contact', {
			templateUrl: 'pages/contact.html',
			controller: 'contactController'
		})
			
	// route for the details page
		.when('/details/:query', {
			templateUrl: 'pages/details.html',
			controller: 'detailsController'
		})
		
	// route for the profile page
		.when('/profile/:query', {
			templateUrl: 'pages/profile.html',
			controller: 'profileController'
		});
});

// create the controller and inject Angular's $scope
scotchApp.controller('mainController', ['$scope', '$location', function ($scope, $location) {
	// create a message to display in our view
		
	$scope.onFormSubmit = function () {

		var query = document.getElementById("lst-ib").value;

		if (query == null || query.trim() == "")
			return;

		$location.path('search/' + query);

	}

}]);

scotchApp.controller('searchController', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {
	var query = $routeParams.q;
	console.log('query');
	console.log(query);
	console.log($routeParams);
	var query = $routeParams.query;
	$scope.query = query;
	$http.get('http://noodle.org.uk/Detective.Api/search/' + query).
		success(function (data, status, headers, config) {
			console.log(data);
			$scope.namecount = data.AllNameCount;
			$scope.resultset = data;
			$scope.search_stats = "About " + data.AllNameCount + " results " + " (" + data.SearchTime + ")";


		}).
		error(function (data, status, headers, config) {
			// log error
			console.log(data);
		});

	$scope.onFormSubmit = function () {

		var query = document.getElementById("lst-ib").value;

		if (query == null || query.trim() == "")
			return;

		$location.path('search/' + query);

	}

}]);

scotchApp.controller('contactController', function ($scope) {
	$scope.message = 'Contact us! JK. This is just a demo.';
});

scotchApp.controller('detailsController', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {
	var query = $routeParams.query;
	console.log(query);
	$scope.query = query;

	$scope.ResetShowValues = function () {
		
		$scope.showGeneral = false;
		$scope.showAddress = false;
		$scope.showAka = false;
		$scope.showCitizenship = false;
		$scope.showDateOfBirth = false;
		$scope.showID = false;
		$scope.showNationality = false;
		$scope.showPlaceOfBirth = false;
		$scope.showProgram = false;
		$scope.showVesselInfo = false;
	}
	function process_data(data)
	{
		console.log(data.SearchItems[0].SDN.sdn_type);
		switch(data.SearchItems[0].SDN.sdn_type)
		{
			case "Individual":
				$scope.profileImage = "images/detective_individual.png";
				break;
			default:
				$scope.profileImage = "images/detective_entity.png";
				break;
		}
	}
	$http.get('http://noodle.org.uk/Detective.Api/details/' + query).
		success(function (data, status, headers, config) {
			console.log(data);
			$scope.result = data;
			process_data(data);
		}).
		error(function (data, status, headers, config) {
			// log error
			console.log(data);
		});

	
	
	$scope.OnGeneralClick = function () {
		var prev = $scope.showGeneral;
		$scope.ResetShowValues();
		$scope.showGeneral = !prev;
	}
	
	$scope.OnAddressClick = function () {
		var prev = $scope.showAddress;
		$scope.ResetShowValues();
		$scope.showAddress = !prev;
	}
	
	$scope.OnAkaClick = function () {
		var prev = $scope.showAka;
		$scope.ResetShowValues();
		$scope.showAka = !prev;
	}
	
	$scope.OnCitizenshipClick = function () {
		var prev = $scope.showCitizenship;
		$scope.ResetShowValues();
		$scope.showCitizenship = !prev;
	}

	$scope.OnDateOfBirthClick = function () {
		var prev = $scope.showDateOfBirth;
		$scope.ResetShowValues();
		$scope.showDateOfBirth = !prev;
	}
	
	$scope.OnIDClick = function () {
		var prev = $scope.showID;
		$scope.ResetShowValues();
		$scope.showID = !prev;
	}
	
	$scope.OnNationalityClick = function () {
		var prev = $scope.showNationality;
		$scope.ResetShowValues();
		$scope.showNationality = !prev;
	}
	
	$scope.OnPlaceOfBirthClick = function () {
		var prev = $scope.showPlaceOfBirth;
		$scope.ResetShowValues();
		$scope.showPlaceOfBirth = !prev;
	}
	
	$scope.OnProgramClick = function () {
		var prev = $scope.showProgram;
		$scope.ResetShowValues();
		$scope.showProgram = !prev;
	}
	
	$scope.OnVesselInfoClick = function () {
		var prev = $scope.showVesselInfo;
		$scope.ResetShowValues();
		$scope.showVesselInfo = !prev;
	}
}]);




scotchApp.controller('profileController', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {
	var query = $routeParams.query;
	console.log(query);
	$scope.query = query;

	$scope.ResetShowValues = function () {
		$scope.showBio = false;
		$scope.showPrograms = false;
		$scope.showLinks = false;
	}
	
	function process_data(data)
	{
		console.log(data.SearchItems[0].SDN.sdn_type);
		switch(data.SearchItems[0].SDN.sdn_type)
		{
			case "Individual":
				$scope.profileImage = "images/detective_individual.png";
				break;
			case "Aircraft":
				$scope.profileImage = "images/detective_aircraft.png";
				break;
			case "Vessel":
				$scope.profileImage = "images/detective_vessel.png";
				break;
			default:
				$scope.profileImage = "images/detective_entity.png";
				break;
		}
	} 
	$http.get('http://noodle.org.uk/Detective.Api/details/' + query).
		success(function (data, status, headers, config) {
			console.log(data);
			$scope.result = data;
			process_data(data);
			console.log("testtt");
			$scope.showBio = true;
		}).
		error(function (data, status, headers, config) {
			// log error
			console.log(data);
		});

	$scope.OnBioClick = function () {
		var prev = $scope.showBio;
		if(prev)
			return;
		$scope.ResetShowValues();
		$scope.showBio = !prev;
	}
	$scope.OnProgramsClick = function () {
		var prev = $scope.showPrograms;
		$scope.ResetShowValues();
		$scope.showPrograms = !prev;
	}
	
	$scope.OnLinksClick = function () {
		var prev = $scope.showLinks;
		$scope.ResetShowValues();
		$scope.showLinks = !prev;
	}
}]);