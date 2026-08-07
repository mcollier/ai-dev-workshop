# .NET Upgrade Plan — net9.0 → net10.0

## Selected Strategy

**All-At-Once** — All 7 projects upgraded simultaneously in a single operation.
**Rationale**: 7 projects, all already on modern .NET (net9.0), SDK-style, shallow
dependency graph (Domain → Application → Infrastructure → {Api, ConsoleApp} →
{UnitTests, IntegrationTests}), no .NET Framework projects, no high-risk package
migrations. Ideal fit for All-At-Once per strategy criteria.

## Project List (single group — no tiers/phases)
- **Libraries**: TaskManager.Domain, TaskManager.Application, TaskManager.Infrastructure
- **Applications**: TaskManager.Api, TaskManager.ConsoleApp
- **Tests**: TaskManager.UnitTests, TaskManager.IntegrationTests

---

## 01-prerequisites: Verify SDK toolchain and global.json compatibility

Confirm the .NET 10 SDK is installed and available on the build machine, and check
whether a `global.json` file exists at the repo root or solution level pinning an
SDK version. If present, update it to a .NET 10 SDK version compatible with the
upgrade; if absent, no action is needed. Also confirm the two previously-fixed
.sln/.csproj path bugs remain fixed and the solution currently builds cleanly on
net9.0 as the pre-upgrade baseline.

**Done when**: .NET 10 SDK is confirmed installed, `global.json` (if present) is
compatible with .NET 10, and the baseline solution build succeeds before any TFM
changes are made.

## 02-upgrade-all-projects: Upgrade all 7 projects to net10.0 in a single pass

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

## 03-final-validation: Validate full build and test baseline, document follow-ups

Run a full solution build to confirm 0 errors/warnings introduced by the upgrade.
Run the test suite for TaskManager.UnitTests and TaskManager.IntegrationTests and
confirm the same pre-existing tests fail with the same `NotImplementedException`
reasons as before the upgrade (no new failures caused by build/runtime regressions
from the TFM bump). Document any deferred recommendations (e.g., further package
updates not required for net10.0 compatibility) for follow-up.

**Done when**: Solution builds cleanly on net10.0 across all 7 projects, and the
test run shows only the same pre-existing expected failures with no new
upgrade-introduced regressions.
