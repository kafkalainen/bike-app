# bike-app
## Description of the project: What is the purpose of the project and what features it has?

## Technology choices: List chosen technologies. It’s also nice to know why you chose those technologies.
I decided to use EF Core when adding data to database, since I can validate data prior to the import.
Installed Pomelo.EntityFrameworkCore.Mysql as database connector.
Decided to use Alpine image as it is smaller. 

## Prerequisites

Should the reviewer install something on their computer before they can compile and run the project? Does the project only work on Windows or Linux? List all steps that need to be done before trying to run the project. If versions are important, remember to mention those as well. 
Project target framework is .net7.0, so developer needs .net7.0 SDK to be installed on their machine.
Moreover, dotnet ef must be installed as a global or local tool

    dotnet tool install --global dotnet-ef
[Install EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/install)

To deploy backend as Docker container, developer needs Docker that supports docker compose v2.

## Configurations

Do you have to configure for example database connections locally?
Provide clear instructions on what needs to be changed and where.
In case you have an .env file which you, of course, should not add to GitHub,
you can send that file to the reviewers by other means.

# How to run the project?

Repository includes an utility script `start.sh` that can be used to run Docker compose command to start the containers.
Seeding takes approximately 10 minutes with current dataset.   
Do you have to install some packages or compile the code?
If you have separate services for example for backend and frontend,
remember to write instructions for all needed services.

# Tests

If your project has tests, include instructions on how to run them.

# TODO: If some things are missing or not working, you can list them in README.
