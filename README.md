# bike-app
## Description of the project
`Solita.Bike` has API, Database project, Application to consume API endpoints and Shared library for handling Responses and Models.
You can use Docker launch the entire stack.

## Technology choices
### Solita.Bike.Api
- I used ASP.NET Core web application as base for the project. 
- I decided to use EF Core when adding data to database, since I can validate data prior to the import, it also allowed me to write the entire stack with one language.
- I used `Pomelo.EntityFrameworkCore.Mysql` as database connector.
- To see API in action, I used Swagger to preview service.

### Solita.Bike.Database
- I decided to use regular console application, since I only needed to run script once. 

### Solita.Bike.App
- I created app using Blazor Server template, I hadn't used it before, but I was interested in using it, and it seemed really nice to use.

### Docker
- Decided to use Alpine image in app, seed and api images, as it was smaller in size.

## Prerequisites
Project target framework is .net7.0, so reviewer needs .NET 7.0 SDK to be installed on their machine.
Moreover, dotnet ef must be installed as a global or local tool

    dotnet tool install --global dotnet-ef
[Install EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/install)

To deploy backend as Docker container, developer needs Docker that supports Docker compose V2, if they are using `start.sh` or `shutdown.sh`
helper scripts.

The data is not included due Github large file limit. `Solita.Bike.Database` will automatically download the csv files, and import them to the database.
Should you wish to download them yourself, they have to placed in following directories:
- Journeys *.csv files need to be placed in path `Solita.Bike.Database/Data/Journeys/`
- Stations *.csv file needs to be placed in path `Solita.Bike.Database/Data/Stations/`

Reviewer also requires .env file, which is not included in this repository, but sent as required.

## How to run the project?
Repository includes an utility script `start.sh` that can be used to run Docker compose command to start the containers.
Seeding takes approximately 15 minutes with current dataset. This was due to Foreign key constraint validation, which
cause more round trips. See [`Solita.Bike.Database`](Solita.Bike.Database/Program.cs)

To see the application in action, after firing up the containers, go to address: [http://localhost:2341] to see this page:
