# 03-final-validation: Validate full build and test baseline, document follow-ups

Run a full solution build to confirm 0 errors/warnings introduced by the upgrade.
Run the test suite for TaskManager.UnitTests and TaskManager.IntegrationTests and
confirm the same pre-existing tests fail with the same `NotImplementedException`
reasons as before the upgrade (no new failures caused by build/runtime regressions
from the TFM bump). Document any deferred recommendations (e.g., further package
updates not required for net10.0 compatibility) for follow-up.

**Done when**: Solution builds cleanly on net10.0 across all 7 projects, and the
test run shows only the same pre-existing expected failures with no new
upgrade-introduced regressions.
