Deployment on Azure

// Create deployment profile using Visual Studio Publish...
// create 2 databases called: products and identity
// get 2 connection strings and put it in appsettings.json (for production, appsettings.Development.json for development)
// modify Services.cs file for production build
// add /Error for error handler for more friendly exception on production

// Package manager console
// set the environment to production
$env:ASPNETCORE_ENVIRONMENT="Production"
set ASPNETCORE_ENVIRONMENT=Production

// Migrate database in production (need to be re-deploy)
dotnet ef database update --context ApplicationContext --project SportsStore
dotnet ef database update --context AppIdentityDbContext --project SportsStore