# Upgrade Options — TaskManager

Assessment: 7 projects, all net9.0 → net10.0, all SDK-style, no incompatible packages, mechanical TFM bump.

## Strategy

### Upgrade Strategy
All projects are already on modern .NET (net9.0) with a 5-tier dependency graph (Domain → Application → Infrastructure → {Api, ConsoleApp} → {UnitTests, IntegrationTests}) but a small project count (7) and no high-risk migrations — signals are ambiguous between a single-pass and staged approach, so both are presented.

| Value                      | Description                                                                                                                                                                                |
| -------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **All-at-Once** (selected) | Upgrade all 7 projects to net10.0 in a single atomic pass — fastest, no multi-targeting overhead; fits the small scope and mechanical nature of this TFM bump.                             |
| Top-Down                   | Upgrade entry-point apps (Api, ConsoleApp) first while multi-targeting shared libraries, consolidating afterward — adds overhead not justified by this solution's small size and low risk. |
