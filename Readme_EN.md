# Advertising Location Service – C# Web API

A simple web service that quickly finds the appropriate advertising platforms for a given location. All data is stored **in-memory**, and the service is optimized for fast queries.

# ! Potential issues and their solutions are listed in the file. Please review them carefully.

---

## Requirements

- The project is built with .NET 9.0. Therefore, running it on this version is the safest option.
- Any IDE. (The project was developed using VS Code, so instructions are based on that.)

---

## Setup

### Backend Swagger Testing

1. Clone the repository:

```bash
   git clone https://github.com/TurkerM/AdPlatformsService.git
   cd AdPlatformsService/AdPlatformService
```

2. Install dependencies and build the project:

```bash
dotnet restore
dotnet build
```

3. Run the service:

```bash
dotnet run
```

The service will run by default at `http://localhost:5292`.

### If you encounter errors

Some auto-generated files can cause issues due to unit tests. You can fix this in one of two ways:

1. Run the following commands in the `AdPlatformService\ProjectTests` directory:

```bash
powershell -Command "(Get-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs') | Where-Object {$_ -ne (Get-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs')[2]} | Set-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs'"
powershell -Command "(Get-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs') | Where-Object {$_ -ne (Get-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs')[11]} | Set-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs'"
```

2. Alternatively, manually delete the following lines from the files indicated in the error messages:
- Line 3 of `obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs`
- Line 12 of `obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs`

After applying either of these methods, rerun the web API:

```bash
dotnet run
```

---

## API Usage

The API can be accessed via Swagger. After running the application, you can inspect and test the endpoints at the following URL:

- All endpoints, required parameters, and example responses are available in Swagger.
  `http://localhost:5292/swagger`

## Handling Invalid Data

- The service does not crash on missing or improperly formatted input.
- If there are invalid lines in a file, they are skipped while other data is loaded.
- Invalid parameters in location queries will result in a `"No results found!"` response.

---

## Unit Tests

Unit tests are located in the `ProjectTests` folder. To run them:

1. Navigate to the directory:

```bash
cd AdPlatformService\ProjectTests
```

2. Run the tests:

```bash
dotnet test
```

### If you encounter errors

Some auto-generated files can cause issues due to unit tests. You can fix this in one of two ways:

1. Run the following commands in the `AdPlatformService\ProjectTests` directory:

```bash
powershell -Command "(Get-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs') | Where-Object {$_ -ne (Get-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs')[2]} | Set-Content 'obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs'"
powershell -Command "(Get-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs') | Where-Object {$_ -ne (Get-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs')[11]} | Set-Content 'obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs'"
```

2. Or manually delete the following lines from the files indicated in the error messages:
- Line 3 of `obj\Debug\net9.0\.NETCoreApp,Version=v9.0.AssemblyAttributes.cs`
- Line 12 of `obj\Debug\net9.0\AdPlatformService.AssemblyInfo.cs`

After applying either method, rerun the tests:

```bash
dotnet test
```

---

## Frontend Requirements

- Node.js >= 18
- npm >= 8

## Frontend Setup

If you want to test the API via the React frontend we developed, follow these steps:

1. Ensure the API is running at `http://localhost:5292`.

2. Navigate to the frontend directory:

```bash
cd AdPlatformsService/ad-platform-frontend
```

3. Install dependencies:

```bash
npm install
```

4. Run the frontend in development mode:

```bash
npm run dev
```

The Vite service will run by default at `http://localhost:5173`.