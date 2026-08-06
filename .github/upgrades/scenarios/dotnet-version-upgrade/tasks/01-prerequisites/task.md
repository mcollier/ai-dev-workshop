# 01-prerequisites: Verify SDK toolchain and global.json compatibility

Confirm the .NET 10 SDK is installed and available on the build machine, and check
whether a `global.json` file exists at the repo root or solution level pinning an
SDK version. If present, update it to a .NET 10 SDK version compatible with the
upgrade; if absent, no action is needed. Also confirm the two previously-fixed
.sln/.csproj path bugs remain fixed and the solution currently builds cleanly on
net9.0 as the pre-upgrade baseline.

**Done when**: .NET 10 SDK is confirmed installed, `global.json` (if present) is
compatible with .NET 10, and the baseline solution build succeeds before any TFM
changes are made.
