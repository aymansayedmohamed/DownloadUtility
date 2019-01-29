Backend

	Please don't miss any of the following points:
	  - Build the Backend project to load the nuget packeages
	  - Open sql server and run Database Script.sql to create the database
	  - Change the connection strings located in Backend\Services\Web.config, either the main project or testing projects.
	  - NInject classes registeration located at Backend\Services\App_Start\Ninject.Web.Common.cs	
	  - Change the host url located in Backend\Services\Web.config, under appSettings section with config key: "HostUrl" to your service host



Frontend
	Please don't miss any of the following points:
	 - open the cmd then write the fillowing commands 
		* npm install           [to download the packages]
		* npm start             [to run the project]
		* npm run test:watch    [to run the test cases]