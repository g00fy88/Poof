# Poof
The Open Source Server-Client App of "Points Of Good"

## Usage
If you want to run the app locally on your computer, just clone the repository and run the ```src/Poof.WebApp/Server``` project. 
You can run the app in Debug by starting the project with Visual Studio.

## Repository Structure
The Poof.WebApp is a Blazor WebAssembly App with a server, that hosts the user accounts over an SQL-Database.

The ```src\Poof.WebApp\Client``` projects holds the UI, which is build with razor components.

The ```src/Poof.Core``` project holds the core functionality of the backend.  
The ```src/Poof.DB``` project encapsulates the SQL-Database access and uses the [EntityFramework Core](https://docs.microsoft.com/en-us/ef/core/).  
The ```src/Poof.Talk``` project holds objects for the Client (UI) to talk with the backend.

The architecture in this software project follows the principles of [Elegant Objects](https://www.elegantobjects.org/) as well as the theories of Robert Cecil Martin and his books *Clean Architecture* and *Clean Coding*

### Poof.Core
While the server project is hosting the UI-Layer and configures the infrastructure, the Poof.Core projects holds the Usecase-Layer and the Entities-Layer (according to the [theory of Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)).

