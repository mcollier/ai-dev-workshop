# 02-upgrade-all-projects: Upgrade all 7 projects to net10.0 in a single pass

Update `TargetFramework` from `net9.0` to `net10.0` across all 7 projects
(TaskManager.Domain, TaskManager.Application, TaskManager.Infrastructure,
TaskManager.Api, TaskManager.ConsoleApp, TaskManager.UnitTests,
TaskManager.IntegrationTests) in one atomic pass — no tier ordering. Bump package
references to their latest net10.0-supported stable versions: Microsoft.AspNetCore.OpenApi,
the 3 OpenTelemetry.* packages, Microsoft.Extensions.Logging.Abstractions,
Microsoft.Extensions.Hosting, plus test-related packages (test SDK, xunit,
FakeItEasy, coverlet) in the two test projects. Restore dependencies, then build the
full solution and fix all resulting compilation errors/warnings in a single bounded
pass (do not iterate ambiguously — fix everything found, then rebuild once to confirm).

Do NOT modify the intentional `NotImplementedException` stub failures in
TaskManager.UnitTests and TaskManager.IntegrationTests — these are pre-existing,
expected test failures and must remain failing for the same reason after the
upgrade. Only ensure the test projects themselves compile cleanly on net10.0.

**Done when**: All 7 projects target net10.0, all package references are updated to
supported net10.0 versions, and the solution builds with 0 errors.
