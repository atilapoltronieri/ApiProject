# RpaAluraSearch

## Database

Configure database connection in **appsettings.json**. 
Auto migration enabled. 
Just start application.

## Entry Point

It's a gateway pattern, single entry point.
Send the **search query** through **AluraCoursesController.GetAluraCourses**.

The expected return will be a list of courses containing:
 - **Title** 
 - **Description** 
 - **Workload**
 - **Professors**
 
 ## Exceptions
 
 All exceptions are handled in **ExceptionHandlingMiddleware.cs** file. 
 When one occurs during navigation throught [Alura Website](https://www.alura.com.br/) it will be of type **AluraNavigationException**.
 Other types will return a default message.
 
 ## Chrome navigation
 
 Chrome is the navigator used for this application.
