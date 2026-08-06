# .NET Upgrade Assessment: net9.0 → net10.0

> **Note**: The automated `generate_dotnet_upgrade_assessment` tool did not produce output
> for this solution. This assessment was compiled manually from direct inspection of the
> project files and verified with `dotnet build` / `dotnet sln list`.

## Solution File Issue — RESOLVED

`src/TaskManager.sln` originally had two path bugs (fixed prior to this assessment):
1. All 5 app project paths had a duplicated `src\` prefix (the .sln itself lives inside
   `src/`, so paths must be relative to that, e.g. `TaskManager.Domain\TaskManager.Domain.csproj`,
   not `src\TaskManager.Domain\...`).
2. The two test projects were referenced at `tests\TaskManager.UnitTests\...` and
   `tests\TaskManager.IntegrationTests\...`, but the actual project folders are
   `src\TaskManager.UnitTests\` and `src\TaskManager.IntegrationTests\` (no `tests/` folder
   exists). Test project `.csproj` files also had matching `ProjectReference` path bugs
   (`..\..\src\...` instead of `..\...`), now fixed.

Verified via `dotnet sln TaskManager.sln list` — all 7 projects now resolve correctly, and
`dotnet build TaskManager.sln` succeeds with 0 errors/0 warnings.

## Project Inventory (7 projects, verified)

| Project                      | Path                                                                 | Current TFM | Target TFM | Type                                         |
| ---------------------------- | -------------------------------------------------------------------- | ----------- | ---------- | -------------------------------------------- |
| TaskManager.Domain           | src/TaskManager.Domain/TaskManager.Domain.csproj                     | net9.0      | net10.0    | Class library (SDK-style)                    |
| TaskManager.Application      | src/TaskManager.Application/TaskManager.Application.csproj           | net9.0      | net10.0    | Class library (SDK-style)                    |
| TaskManager.Infrastructure   | src/TaskManager.Infrastructure/TaskManager.Infrastructure.csproj     | net9.0      | net10.0    | Class library (SDK-style)                    |
| TaskManager.Api              | src/TaskManager.Api/TaskManager.Api.csproj                           | net9.0      | net10.0    | ASP.NET Core Web API (Microsoft.NET.Sdk.Web) |
| TaskManager.ConsoleApp       | src/TaskManager.ConsoleApp/TaskManager.ConsoleApp.csproj             | net9.0      | net10.0    | Console exe                                  |
| TaskManager.UnitTests        | src/TaskManager.UnitTests/TaskManager.UnitTests.csproj               | net9.0      | net10.0    | xUnit v3 test project                        |
| TaskManager.IntegrationTests | src/TaskManager.IntegrationTests/TaskManager.IntegrationTests.csproj | net9.0      | net10.0    | xUnit v3 test project                        |

Dependency order (leaf → root): Domain → Application → Infrastructure → {Api, ConsoleApp} → {UnitTests, IntegrationTests}

## Package Inventory

| Package                                   | Project(s)                  | Current       | Notes                                                                |
| ----------------------------------------- | --------------------------- | ------------- | -------------------------------------------------------------------- |
| Microsoft.AspNetCore.OpenApi              | Api                         | 9.0.0         | Bump to 10.0.x                                                       |
| OpenTelemetry.Exporter.Console            | Api                         | 1.12.0        | Check latest compatible with net10.0                                 |
| OpenTelemetry.Extensions.Hosting          | Api                         | 1.12.0        | Check latest compatible with net10.0                                 |
| OpenTelemetry.Instrumentation.AspNetCore  | Api                         | 1.12.0        | Check latest compatible with net10.0                                 |
| Microsoft.Extensions.Logging.Abstractions | Application                 | 9.0.8         | Bump to 10.0.x                                                       |
| Microsoft.Extensions.Hosting              | ConsoleApp                  | 9.0.8         | Bump to 10.0.x                                                       |
| Microsoft.NET.Test.Sdk                    | UnitTests, IntegrationTests | 17.11.1       | Check latest stable; not strictly net10.0-tied but recommend refresh |
| xunit.v3 / xunit.runner.visualstudio      | UnitTests, IntegrationTests | 3.2.2 / 3.1.5 | Check latest stable                                                  |
| FakeItEasy                                | UnitTests                   | 8.3.0         | Check latest stable                                                  |
| coverlet.collector                        | UnitTests, IntegrationTests | 6.0.2         | Check latest stable                                                  |

No security vulnerabilities identified in a manual pass; re-verify via
`dotnet list package --vulnerable` during execution.

## Code/API Risk Notes

- `src/TaskManager.Infrastructure/Legacy/LegacyTaskProcessor.cs` is **intentionally bad
  legacy code** kept for a workshop refactoring exercise (Lab 3). Do not refactor/fix as
  part of the upgrade — only ensure it still compiles under net10.0.
- `src/TaskManager.Domain/Tasks/Task.cs` contains intentional `TODO` stubs (missing
  validation) — these are workshop exercise placeholders, not upgrade defects. Do not
  "fix" them.
- No obvious deprecated API usage found in a manual scan (no BinaryFormatter, no WCF,
  no old System.Web references) — straightforward net9→net10 TFM bump expected for all
  5 projects.

## Test Projects — Expected Pre-Existing Failures

`TaskManager.UnitTests` and `TaskManager.IntegrationTests` are workshop exercise projects
containing intentionally-unimplemented stub tests (`throw new NotImplementedException(...)`
in `TaskServiceTests` and `TaskApiIntegrationTests`). These are marked `Lab 2`/`Lab 4`
placeholders for workshop participants to implement using Copilot — **their failure is
expected and must NOT be fixed as part of this upgrade**. Additionally, before this fix,
tests could not even execute because only the net10.0 runtime was installed and projects
targeted net9.0 (`dotnet test` aborted with a missing-runtime error) — this will be
resolved automatically once these projects are retargeted to net10.0.

**Execution rule**: after retargeting to net10.0, run `dotnet test` and confirm the test
projects execute (no runtime-mismatch abort) and the same **stub methods** fail with
`NotImplementedException` as before — do not modify any test method bodies, do not
implement the TODO stubs, and do not "fix" `LegacyTaskProcessor.cs` or `Task.cs` validation
gaps referenced by them.

## Toolchain

- Target framework: **net10.0** (LTS, support ends Nov 2028) per scenario-instructions.md.
- Run `validate_dotnet_sdk_installation` / `validate_dotnet_sdk_in_globaljson` during
  execution stage to confirm the net10.0 SDK is available (no global.json found in repo).

## Summary for Orchestrator

- 7 projects total, all net9.0 → net10.0: 5 app projects (straightforward TFM/package bump)
  + 2 test projects (retarget only — contents must not be modified).
- .sln path bugs fixed and verified (`dotnet build` succeeds, `dotnet sln list` shows all 7).
- Test projects have intentionally-failing stub tests (`NotImplementedException`) — expected,
  do not fix. Verify they still fail the same way post-upgrade (not due to new build breaks).
