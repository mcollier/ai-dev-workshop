# .NET Version Upgrade

## Preferences
- **Flow Mode**: Guided
- **Target Framework**: .NET 10 (LTS — Support ends Nov 2028)

## Notes
- This is a demo app. It currently has some unit tests that are expected to fail.
- Do NOT change or "fix" any currently-failing unit tests — their failure is expected/pre-existing.
  Only ensure they still fail for the same reason (not due to new upgrade-introduced build errors).

## Source Control
- **Source Branch**: feature/dotnet-upgrade
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: After Each Task

## Upgrade Options
- **Strategy**: All-at-Once (user confirmed) — upgrade all 7 projects together in a single pass, no multi-targeting.

## Strategy
**Selected**: All-At-Once
**Rationale**: 7 projects, all already on net9.0 (modern .NET), SDK-style, shallow
dependency graph (3-4 levels), no .NET Framework projects, no high-risk package
migrations — ideal fit per strategy criteria.

### Execution Constraints
- Single atomic upgrade — all 7 projects updated together in one pass (no tier ordering)
- Update TFMs + bump packages + fix code issues in one bounded pass; single rebuild to confirm, not an iterative retry loop
- Do NOT fix/modify the intentional NotImplementedException stub test failures in TaskManager.UnitTests / TaskManager.IntegrationTests — only ensure they still compile and fail the same way
- Testing/validation happens after the atomic upgrade completes successfully
- Full solution build validation is the final task's gate
