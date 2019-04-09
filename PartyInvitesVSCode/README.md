// create new donet core empty web app project: web, mvc, xunit
dotnet new web --language C# --framework netcoreapp2.0 -o PartyInvites

// create .bowerrc and bower.json file to manage client-side packages (bootstrap)
.bowerrc // install into specific directory
{
    "directory": "wwwroot/lib"
}

bower.json
{
    "name": "PartyInvites",
    "private": true,
    "dependencies": {
        "bootstrap": "latest"
    }
}

// install bower packages
bower install

// add a package to work with Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite

// restore packages
dotnet restore

// create db migrations
donet ef migrations add Initial

// migrate database
dotnet ef database update

// run the application
dotnet run

// run tests
dotnet test