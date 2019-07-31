# BusStopDashBoard
Project that shows the arrival time based on the description of the task.
The project is a simple GraphQL API + a single Angular 1 page to display the results.


How to Run it?
2 options:

1) Download and run on computer.

    1 - It may require Visual Studio 2019.
    2 - Download the project and open the file "BusSchedulemanager.sln" using VS 2019.
    3 - Press Open in IIS Express. An instance of the application will open in the browser.
    
2) Run on deployed version.
I deployed a version for quick access here: http://busstopdashboardrolando.azurewebsites.net/views/index.html


# Some Explanation:

- The start file will be "/views/index.html", this will have a light angular 1 application ( it was fast and easy to set up).
- It will request query the bus schedule.
- The api is limited to the requirements of the task.
- The request will be sent to "BusScheduleManager/Controllers/GraphQLController.cs" at the ActionResult Post.
- This will be proccessed and send to the document executer that will invoke at "BusScheduleManager/Queries/BusRouteQuery.cs"
- It will end up proccessing the query at "BusSchedulemanager.DataAccess/Repositories/BusRouteRepository.cs"
- I decided to cache some results, but it is not fully scalable, it helps relieve the stress on medium size applications. 
