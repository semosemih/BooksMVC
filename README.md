# Books MVC

ASP.NET Core MVC (.NET 8) project, Books domain: **Book**, **Author**, **Genre**, **BookGenre**.

- 1 to many: Author → Books
- Many to many: Book ↔ Genre (through BookGenre)

Solution structure (same as the course projects):

- **CORE**: Record, CommandResponse, Service, DbService, IService
- **APP**: Domain (entities, Db, DbFactory), Models (requests/responses), Services
- **MVC**: Controllers, Views, Templates, Program.cs (IoC), appsettings.json (connection string)

## Run

```bash
dotnet tool install --global dotnet-ef   # once
cd MVC
dotnet ef migrations add v1 --project ../APP --startup-project .
dotnet ef database update --project ../APP --startup-project .
dotnet run
```

Then open `/Db/Seed` once to insert sample data.

## Phases

- Phase 1: CRUD operations for all entities ✅
- Final phase: Cookie authentication, authorization (Admin role), session

## Team

- Mevlüt Yılmaz
- Semih Şengül
