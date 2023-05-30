# bike-app
## Description of the project
`Solita.Bike` has API, Database project, Application to consume API endpoints and Shared library for handling Responses and Models.
You can use Docker launch the entire stack.

## Technology choices: List chosen technologies. It’s also nice to know why you chose those technologies.
### Solita.Bike.Api
- I used ASP.NET Core webapplication as base for the project. 
- I decided to use EF Core when adding data to database, since I can validate data prior to the import, it also allowed me to write the entire stack with one language.
- I installed `Pomelo.EntityFrameworkCore.Mysql` as database connector.

### Solita.Bike.Database
- I decided to use regular console application, since I only needed to run script once. 

### Solita.Bike.App
- I created app using Blazor Server template, I hadn't used it before, but I was interested in using it, and it seemed really nice to use.

### Docker
- Decided to use Alpine image in app and api images, as it was smaller in size.
- MySQL 

## Prerequisites

Project target framework is .net7.0, so reviewer needs .NET 7.0 SDK to be installed on their machine.
Moreover, dotnet ef must be installed as a global or local tool

    dotnet tool install --global dotnet-ef
[Install EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/install)

To deploy backend as Docker container, developer needs Docker that supports docker compose V2, if they are using `start.sh` or `shutdown.sh`
helper scripts.

The datadumps are not included due Github large file limit. They have to placed in following directories:
- Journeys *.csv files need to be placed in path `Solita.Bike.Database/Data/Journeys/`
- Stations *.csv file needs to be placed in path `Solita.Bike.Database/Data/Stations/`

## Configurations

Do you have to configure for example database connections locally?
Provide clear instructions on what needs to be changed and where.
In case you have an .env file which you, of course, should not add to GitHub,
you can send that file to the reviewers by other means.

## How to run the project?

Repository includes an utility script `start.sh` that can be used to run Docker compose command to start the containers.
Seeding takes approximately 60 minutes with current dataset. This was due to Foreign key constraint validation in [`Solita.Bike.Database`](Solita.Bike.Database/Program.cs)

To see the application in action, after firing up the containers, go to address:
