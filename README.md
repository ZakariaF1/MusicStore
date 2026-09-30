# Music Store

ASP.NET Core 2.1 Chinook music catalog: MySQL-backed API plus MVC web UI (Telerik/Kendo grids).

## Commands

### How to run

Prerequisites: .NET / ASP.NET Core **2.1** runtime (or SDK that can build `netcoreapp2.1`), MySQL with `chinookdatabase`, and (for the web UI) the Telerik NuGet feed + trial package.

```powershell
# MySQL (example — Docker)
docker run -d --name musicstore-mysql -e MYSQL_ALLOW_EMPTY_PASSWORD=yes -e MYSQL_DATABASE=chinookdatabase -p 3306:3306 mysql:8.0 --default-authentication-plugin=mysql_native_password
Get-Content "docs\chinook new.sql" -Raw | docker exec -i musicstore-mysql mysql -uroot

# API (Kestrel)
dotnet run --project MusicStore\MusicStore\MusicStore.csproj --urls "http://localhost:5000;https://localhost:5001"

# Web UI (separate terminal; point MusicStoreUrl at the API)
dotnet run --project MusicStoreWebApp\MusicStoreWebApp\MusicStoreWebApp.csproj --urls "http://localhost:5002;https://localhost:5003"
```

In Visual Studio, run **MusicStore** (IIS Express → `https://localhost:44333`) and **MusicStoreWebApp**; set `MusicStoreUrl` in `MusicStoreWebApp/MusicStoreWebApp/appsettings.json` to that API base URL.

### Command table

| Command | What it does |
| ------- | ------------ |
| `dotnet restore MusicStore\MusicStore.sln` | Restore API solution packages |
| `dotnet build MusicStore\MusicStore.sln` | Build API + domain + repositories |
| `dotnet test MusicStore\MusicStore.sln` | Run automated tests |
| `dotnet run --project MusicStore\MusicStore\MusicStore.csproj` | Start HTTP API (default launch profile URLs) |
| `dotnet restore MusicStoreWebApp\MusicStoreWebApp.sln` | Restore web UI (needs Telerik package source) |
| `dotnet build MusicStoreWebApp\MusicStoreWebApp.sln` | Build web UI |
| `dotnet run --project MusicStoreWebApp\MusicStoreWebApp\MusicStoreWebApp.csproj` | Start MVC UI |

### Typical workflow

1. Start MySQL and import `docs/chinook new.sql`.
2. `dotnet run --project MusicStore\MusicStore\MusicStore.csproj` (or F5 MusicStore in VS).
3. Confirm `GET /api/v1/artists` on the API base URL.
4. Run MusicStoreWebApp with `MusicStoreUrl` matching that base URL.

### Setup notes (details)

- Telerik trial + NuGet source: see `docs/Read This.txt` and [Telerik NuGet install](https://docs.telerik.com/aspnet-core/installation/nuget-install). Feed: `https://nuget.telerik.com/v3/index.json` (username `api-key`).
- Connection string: `MusicStore/MusicStore/appsettings.json` → `ChinookDatabaseConnection` (default `Server=localhost;User=root;Database=chinookdatabase;`).
- Architecture: [`docs/architecture/ARD.md`](docs/architecture/ARD.md). Agent standards: [`AGENTS.md`](AGENTS.md), [`docs/engineering-charter.md`](docs/engineering-charter.md).

## Solutions

| Path | Contents |
| ---- | -------- |
| `MusicStore/MusicStore.sln` | API host, Domain, Repository ports, MySQL/MsSql adapters, shared Api client library |
| `MusicStoreWebApp/MusicStoreWebApp.sln` | MVC frontend (references `MusicStore.Api`) |
