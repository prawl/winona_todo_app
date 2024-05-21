# TaskTamer

# TaskTamer: Because Life Needs a List

![IMG_9506](https://github.com/prawl/winona_todo_app/assets/3402498/73e4c8e2-6f89-49b7-8c61-eec068c34822)

Ever missed an anniversary because you forgot to buy flowers? Yeah, us too. Welcome to TaskTamer, where you can finally keep track of all the important (and not-so-important) things in life. Add tasks, create subtasks, and clean up your list with ruthless efficiency. Mark them complete and feel that sweet, sweet relief. Tame your to-dos and never miss a beat again.

# Running the app locally
1.
```
$ docker-compose build
$ docker-compose up
```

2. Access the app at http://localhost:4200/tasks



## Prerequisites
[VS Code](https://code.visualstudio.com/download)  (optional)
[C# for Visual Studio Code (latest version)](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
[.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

- Clone the [WINONA_TODO_APP] from  to your local machine
- Open VS Code and open the Terminal window

- Navigate to the TodoApi folder in your terminal.

- .NET Core development certificate
    - Run `dotnet dev-certs https -t` to generate and trust the local development certificate, if it doesn't already exist
    - Click "Yes" when prompted

- Run the following commands to add the appropriate NuGet packages:
`dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design`
`dotnet add package Microsoft.EntityFrameworkCore.Design`
`dotnet add package Microsoft.EntityFrameworkCore.InMemory`
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
`dotnet add package Microsoft.EntityFrameworkCore.Tools`
`dotnet add package Moq`
`dotnet add package Swashbuckle.AspNetCore`
`dotnet add package xunit`
`dotnet tool uninstall -g dotnet-aspnet-codegenerator`
`dotnet tool install -g dotnet-aspnet-codegenerator`
`dotnet tool update -g dotnet-aspnet-codegenerator`

Build the project.

To run the Web API locally run the following command:
`dotnet run --launch-profile https`

The default browser is launched to https://localhost:5220. Append /swagger to the URL, https://localhost:5220/swagger to view the Swagger.

# Getting Started Web Client

## Prerequisites
[Node.js](https://nodejs.org/en) which includes Node Package Manager and NPM

Install the CLI using the npm package manager:
`npm install -g @angular/cli`

Open your terminal and navigate to `/winona_todo_app/web-client`

Run the following commands:
`npm install`
`npm ci`
`ng build`

The web client project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 17.3.7.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

  - [How to fix error with Python / node-gyp](https://hisk.io/how-to-fix-node-js-gyp-err-cant-find-python-executable-python-on-windows/)

  ## Angular App Organization
The Angular app uses multiple modules to organize code:
- App
    - The `AppModule` is the entry-point into the application.  Its main job is to load the root `AppComponent`, manage the top-level routing, and import the `SharedModule`.
- Shared
    - The `SharedModule` consists of models, components, and directives that are used by multiple other modules, typically feature modules.  Any services that are used by the shared components can be provided here as well.
    - Third-party components (such as Angular Material) can be imported and re-exported here to make them easily available to other modules.
    - This module is typically imported into feature modules.
- Feature Modules
   - These are the "domain-level" features of the application, and are stored within the `app/modules` folder. Tasks or Todo Items etc. will all live in their own modules, if more pages of the application were to be added, they would be in their own module. This allows for lazy loading of the components only when going to that specific route. 
   - Feature modules should be organized such that they are independent of one another (common functionality or components can be moved into the `SharedModule`).
   - Each feature module should also have its own `RoutingModule` to handle its internal navigation.

## Suggested VS Code Extensions

Within the `.vscode` folder, you'll find some recommended extensions to make your life easier. ;->
