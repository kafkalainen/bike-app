# Solita.Bike
## Description of the project
`Solita.Bike` is an application that displays views on Helsinki and Vantaa bicycle stations, and the journeys that have been traveled between those stations. 
It has API, Database project, Application to consume API endpoints and Shared library for handling Responses and Dtos. You can use Docker launch the entire stack.

## Features

### Data Import
- Import data from the CSV files to a database or in-memory storage
- Validate data before importing
- Don't import journeys that lasted for less than ten seconds
- Don't import journeys that covered distances shorter than 10 meters

- Data Import is implemented in `Solita.Bike.Database` project.

### Extra:
- Shows percentage of the import for journeys, and the time it took for the import.
- Downloading the csv files to container.

### List journeys
- For each journey show departure and return stations, covered distance in kilometers and duration in minutes
- Pagination

- List journeys view is visible at http://localhost:2341/journeys and its been implemented in `Solita.Bike.Api` and `Solita.Bike.App` projects.

### Station list
- List all the stations
- Pagination

- List stations view is visible at http://localhost:2341/stations and its been implemented in `Solita.Bike.Api` and `Solita.Bike.App` projects.

### Single station view
- Station name
- Station address
- Total number of journeys starting from the station
- Total number of journeys ending at the station

- Single stations view is visible at http://localhost:2341/stations, and clicking `View` button. Its been implemented in `Solita.Bike.Api` and `Solita.Bike.App` projects.

### Suprising elements
- Running backend in Docker

## Technology choices
### Solita.Bike.Api
- I used ASP.NET Core web application as base for the project. 
- I decided to use EF Core when adding data to database, since I can validate data prior to the import, it also allowed me to write the entire stack with one language.
- To see API in action, I used Swagger to preview service.

### Solita.Bike.Database
- I decided to use regular console application, since I only needed to run script once. 

### Solita.Bike.App
- I created app using Blazor Server template, I hadn't used it before, but I was interested in using it, and it seemed really nice to use.

### Docker
- Decided to use Alpine image in app, seed and api images, as it was smaller in size.

## Prerequisites
Reviewer needs .NET 7.0 SDK to be installed on their machine. Moreover, dotnet ef must be installed as a global or local tool

    dotnet tool install --global dotnet-ef
[Install EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/install)

To deploy backend as Docker container, developer needs Docker that supports Docker compose V2, if they are using `start.sh` or `shutdown.sh`
helper scripts.

The data is not included due Github large file limit. `Solita.Bike.Database` will automatically download the csv files, and import them to the database.
Should you wish to download them yourself, they have to placed in following directories:
- Journeys *.csv files need to be placed in path `Solita.Bike.Database/Data/Journeys/`
- Stations *.csv file needs to be placed in path `Solita.Bike.Database/Data/Stations/`

Reviewer also requires `.env` file, which is not included in this repository, but sent as required.

## How to run the project?
Repository includes an utility script `start.sh` that can be used to run Docker compose command to start the containers.
Seeding takes approximately 30 minutes with given data. This was due to Foreign key constraint validation, which
cause more round trips. See [`Solita.Bike.Database`](Solita.Bike.Database/Program.cs)

To see the application in action, after firing up the containers, go to address: http://localhost:2341
