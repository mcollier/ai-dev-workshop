# Progress Details — 02-upgrade-all-projects

## Files Modified
- src/TaskManager.Api/TaskManager.Api.csproj
- src/TaskManager.Application/TaskManager.Application.csproj
- src/TaskManager.ConsoleApp/TaskManager.ConsoleApp.csproj
- src/TaskManager.Domain/TaskManager.Domain.csproj
- src/TaskManager.Infrastructure/TaskManager.Infrastructure.csproj
- src/TaskManager.IntegrationTests/TaskManager.IntegrationTests.csproj
- src/TaskManager.UnitTests/TaskManager.UnitTests.csproj
- src/TaskManager.Api/Extensions/EndpointExtensions.cs (removed deprecated `.WithOpenApi()` call)

## TargetFramework Changes
All 7 projects: `net9.0` → `net10.0`

## Package Version Changes

| Project          | Package                                     | Old                | New                               |
| ---------------- | ------------------------------------------- | ------------------ | --------------------------------- |
| Api              | Microsoft.AspNetCore.OpenApi                | 9.0.0              | 10.0.10                           |
| Api              | Microsoft.OpenApi (new explicit direct ref) | (transitive 2.0.0) | 2.11.0                            |
| Api              | OpenTelemetry.Exporter.Console              | 1.12.0             | 1.17.0                            |
| Api              | OpenTelemetry.Extensions.Hosting            | 1.12.0             | 1.17.0                            |
| Api              | OpenTelemetry.Instrumentation.AspNetCore    | 1.12.0             | 1.17.0                            |
| Application      | Microsoft.Extensions.Logging.Abstractions   | 9.0.8              | 10.0.10                           |
| ConsoleApp       | Microsoft.Extensions.Hosting                | 9.0.8              | 10.0.10                           |
| UnitTests        | Microsoft.NET.Test.Sdk                      | 17.11.1            | 18.8.1                            |
| UnitTests        | xunit.v3                                    | 3.2.2              | 3.2.2 (unchanged, already latest) |
| UnitTests        | xunit.runner.visualstudio                   | 3.1.5              | 3.1.5 (unchanged, already latest) |
| UnitTests        | FakeItEasy                                  | 8.3.0              | 9.0.1                             |
| UnitTests        | coverlet.collector                          | 6.0.2              | 10.0.1                            |
| IntegrationTests | Microsoft.NET.Test.Sdk                      | 17.11.1            | 18.8.1                            |
| IntegrationTests | xunit.v3                                    | 3.2.2              | 3.2.2 (unchanged)                 |
| IntegrationTests | xunit.runner.visualstudio                   | 3.1.5              | 3.1.5 (unchanged)                 |
| IntegrationTests | coverlet.collector                          | 6.0.2              | 10.0.1                            |

Versions resolved via `get_supported_package_version` MCP tool for net10.0 target framework.

## Issues Encountered / Resolved
1. **NU1903 vulnerability warning (Microsoft.OpenApi 2.0.0 transitive dependency)**: The
   `Microsoft.AspNetCore.OpenApi` 10.0.10 package brings in `Microsoft.OpenApi` 2.0.0
   transitively, which has a known high-severity advisory (GHSA-v5pm-xwqc-g5wc).
   - First attempt: pinned `Microsoft.OpenApi` to latest 3.9.0 directly — this broke the
     build because the `Microsoft.AspNetCore.OpenApi` 10.0.10 source generator emits code
     against the 2.x `IOpenApiMediaType.Example` writable-property API, which is read-only
     in 3.x (CS0200 compile errors).
   - Resolution: added an explicit direct `PackageReference` to `Microsoft.OpenApi` 2.11.0
     (latest stable 2.x line) to pin above the vulnerable 2.0.0 version while remaining API
     compatible. This resolved the NU1903 warning without breaking the source generator.
2. **ASPDEPR002 warning**: `EndpointExtensions.cs` called `.WithOpenApi()` on the `/health`
   endpoint, which is deprecated in the updated `Microsoft.AspNetCore.OpenApi` package
   (OpenAPI metadata is now generated automatically without this call). Removed the
   `.WithOpenApi()` call; endpoint behavior and route metadata (`WithName`) unchanged.
3. No other compile errors were encountered — the remaining 5 non-Api/test projects built
   cleanly with only TFM bump.

## Test Projects
Did NOT touch `TaskServiceTests.cs`/`TaskApiIntegrationTests.cs` (UnitTest1.cs) test bodies.
Verified `dotnet test` on TaskManager.UnitTests still produces the same intentional
`NotImplementedException` failures (6 failed / 0 passed) as before the upgrade — these are
expected workshop placeholder stubs and remain unchanged.

## Final Build Status
`dotnet build src/TaskManager.sln`: **Build succeeded — 0 Warnings, 0 Errors.**
