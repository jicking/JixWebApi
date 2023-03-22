# JixWebApp
A simple preconfigured Web API template.
Intended for small web app projects.

 - Github Actions Build pipeline/CI
 - Core library
 - Base Unit Tests
 - Data Persistence (InMemoryDB EF Core)
 - Logging
 - API response wrapper
 - Custom scaffold templates

## Installing Template
on root dir of solution
```
dotnet new -u .
dotnet new -i .
```
Then use the template for new project using vs2022 or cli.
```
dotnet new JixWebApp -n YourProjectName
```

## Running app in local dev machine
Open './content/JixWebApp.sln' to open project in vs2022, then run startup app.

## Configurations
By default all azure related configs are left blank, on local please ovewrite them using local secrets.
By default app uses inmemory db for easier prototyping, switch to sql when needed (you need to disable JixWebAppDbContextFactory as well).

## Architecture
JixWebApp.Core contains members that are intended to be unit tested and segrated away from the startup web project.
By design, project defined Command/Query encapsulates process logic and is then exposed via web api or razor page.

## My use case
For my use case , i scaffold pages and api using enties for prototyping.
I then create a command or query to encapsulate process logic with unit tests.


@jicking
