# WatchHub

A Sample N-layered .NET Core Project Demonstrating Clean Architecture and the Generic Repository Pattern.

## Packages

### Application Core
```
Install-Package Ardalis.Specification
```

### Infrastructure
```
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
Install-Package Npgsql.EntityFramework.PostgreSQL
Install-Package Ardalis.Specification.EntityFrameworkCore
```
## Migrations
Before running the following commands, make sure that Web is set as startup project. Run the following commands on the project "Infrastructure". 

### Infrastructure
```
Add-Migration InitialCreate -Context WatchHubContext -OutputDir Data/Migrations
Update-Database -Context WatchHubContext

Add-Migration InitialIdentity -Context AppIdentityDbContext -OutputDir Identity/Migrations
Update-Database -Context AppIdentityDbContext
```
