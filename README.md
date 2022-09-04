# move-it-app

## Backend
The backend solution is a .NET WEB API application, built using .NET 6 Framework and Visual Studio 2022.

N tier architecture is applied to the backend, using the Repository pattern for organizing the data access layer.
The solution uses an SQL database, Entity Framework Core is used as an ORM and the migrations need to be applied (using update-database command)
in order to get the initial database structure and data.

The main business logic is contained in the services. The rules for distance price calculations are formed as distance ranges
with fixed price and price per kilometer. There are also rules for providing fixed price for moving of a specific types of objects (for example piano).
There needs to be added at least one type of area (living or attic). Following the table given in the presentation, if both areas are provided we need 2 cars.

There is a login and register functionality, and the authentication and authorization are based on JWT tokens.

The following functionalities are implented:
- Register
- Login
- Creating a proposal based on user requirement
- Listing user proposals
- Displaying proposal details