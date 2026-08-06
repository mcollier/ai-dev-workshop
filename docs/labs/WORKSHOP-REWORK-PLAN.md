# Workshop Rework Plan: .NET + GitHub Copilot + Claude Code Labs

> **Status**: Working document. Delete this file once the rework is complete and merged.

## Goal

Restructure the existing lab content (Part 1: Fundamentals, Part 2: Advanced) so that it:

- Targets **.NET only** (Java/Spring Boot content removed — done, see below)
- Teaches the same workflows using **both GitHub Copilot and Claude Code** side by side,
  not just Copilot

**Out of scope for now**: schedule/timing, agenda blocks, breaks, lunch. This pass is
about lab content and structure only.

## Progress So Far

- [x] Removed all Spring Boot/Java references (146 matches across 8 files)
- [x] Deleted `lab-02/03/04-java.md` variants
- [x] Flattened `lab-01-tdd-with-copilot.md` and `lab-10-capstone-build-agent.md` from
      dual 🔷/🟩 stack paths to single .NET-only content
- [x] Renamed `lab-01-tdd-with-copilot.md` → `lab-01-tdd.md` (dropping the tool-specific
      suffix now that labs cover both GitHub Copilot and Claude Code)
- [x] Cleaned "Also available: Java/Spring Boot" cross-links in lab-02, lab-03,
      `labs/README.md`
- [x] Committed (`docs(labs): remove Spring Boot/Java content, .NET only`)

## Current State Audit

- Repo is **docs-only**: no `src/` exists yet. Labs reference paths
  (`src/TaskManager.*`, `.github/instructions/dotnet.instructions.md`) that don't exist
  in this trimmed-down repo — the actual sample app must live elsewhere or needs
  scaffolding. **Needs a decision**: is the sample app added separately, or do these labs
  stay aspirational/reference-only for this repo?
- `labs/README.md` links to `../presentations/modules/...` and `../guides/...` — neither
  `docs/presentations/` nor `docs/guides/` exist in this repo. These links are broken
  today, independent of this rework.
- All content (Labs 1–10 + bonus) is written **Copilot-only**: Copilot Chat, slash
  commands (`/tests`, `/doc`, `/refactor`, `/check`), `@workspace`, custom agents
  (`.github/agents/*.agent.md`), Skills (`.github/skills/*/SKILL.md`). None of this has
  a Claude Code equivalent yet.
- Timing table in `labs/README.md` (lines ~420–437) is stale/inconsistent (Part 2 totals
  don't sum correctly) and Copilot-only. Leaving as-is for now — timing/agenda is out of
  scope for this pass.

## Copilot ↔ Claude Code Concept Mapping (to embed per-lab)

| GitHub Copilot | Claude Code | Notes |
|---|---|---|
| Copilot Chat (Ask/Edit/Agent mode) | Claude Code interactive REPL | Both support conversational + agentic modes |
| `/tests`, `/doc`, `/refactor`, `/check` slash commands | Custom slash commands in `.claude/commands/*.md` | Need to author equivalent commands |
| `.github/copilot-instructions.md` | `CLAUDE.md` (repo root) | Both are always-loaded repo context |
| `.github/instructions/*.instructions.md` (path-scoped) | No direct path-scoped equivalent — closest is `CLAUDE.md` sections or imported files | Needs guidance on how to approximate |
| `.github/skills/*/SKILL.md` | Claude Code Skills (`.claude/skills/*/SKILL.md`) | Same shape, format is compatible |
| `.github/agents/*.agent.md` (custom agents) | Claude Code subagents (`.claude/agents/*.md`) | Similar YAML frontmatter + prompt body pattern |
| `@workspace` context | Automatic project context / `@file` mentions | Different context injection model |
| Agent picker dropdown | `/agents` command or CLI flag | |

**Decided**: present Copilot and Claude Code side by side as a two-column table per
step/prompt (one column per tool), not interleaved prose or separate tracks. See
"Per-Tool Table Format" below for the exact template.

## Per-Tool Table Format

Each step that differs by tool gets a two-column table, one column per tool, using the
logos in `docs/images/` to make the columns visually scannable at a glance:

```markdown
| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
|---|---|
| Prompt/step text for Copilot | Equivalent prompt/step text for Claude Code |
```

- Use this for prompts, slash/custom commands, and any tool-specific instructions.
- Steps that are identical for both tools (e.g. TDD concepts, general explanations) stay
  as normal prose above/below the table — no need to duplicate those into columns.
- Logos live at `docs/images/githubcopilot.svg` and `docs/images/claude-color.svg`;
  adjust the relative path per lab file's location.

## Per-Lab Action Items

> Mark **Reviewed** with ✅ (optionally add your initials, e.g. `✅ SW`) once you've
> reviewed or completed a lab's rework, so the team can track progress at a glance.

| Lab | Action | Reviewed |
|---|---|---|
| Lab 1 – TDD | Add Claude Code prompts alongside existing Copilot prompts for interface/test/impl generation | ✅ |
| Lab 2 – Requirements to Code | Add Claude Code equivalents for backlog generation, full-stack TDD flow | ⬜ |
| Lab 3 – Generation & Refactoring | Add Claude Code equivalents for `@workspace`, `/refactor`, multi-file edits | ⬜ |
| Lab 4 – Testing/Docs/Workflow | Add Claude Code equivalents for `/tests`, `/doc`, commit workflow | ⬜ |
| ~~Lab 5 – Interaction Models~~ | Deleted per user decision — no longer part of the lab set | \u2014 |
| Lab 6 – Skills & Customization | Add Claude Code Skills + `CLAUDE.md` sections; compare hierarchy across tools | ⬜ |
| Lab 7 – Custom Agents Intro | Add Claude Code subagents equivalent walkthrough | ⬜ |
| Lab 8 – Workflow Agents | Extend scenarios to invoke both Copilot agents and Claude Code subagents | ⬜ |
| Lab 9 – Agent Design | Keep tool-agnostic (design principles apply to both); add a short "authoring for both tools" note | ⬜ |
| Lab 10 – Capstone | Update template so participants build one agent definition and adapt it for both tools | ⬜ |
| ~~Bonus – Test Planning~~ | Deleted per user decision — no longer part of the lab set | \u2014 |
| README.md | Update tech stack section (mention both tools), fix/remove broken `../guides/` and `../presentations/` links. Timing table left as-is for now. | ⬜ |

## Open Questions for User

1. ~~Interleave Copilot + Claude Code per lab, or keep them as separate labeled
   sections?~~ **Resolved**: side-by-side two-column tables (one column per tool) per
   step, using the `docs/images/` logos to differentiate. See "Per-Tool Table Format"
   above.
2. ~~Does the sample `src/` application exist elsewhere and get added back, or do labs
   stay reference-only until an app is scaffolded?~~ **Resolved**: someone else is
   working on the sample app; it'll arrive via a separate PR. Tracked in `./todo.md`.
3. ~~Should the stale `../guides/` and `../presentations/` links be fixed (content needs
   creating) or just removed for now?~~ **Resolved**: being addressed separately later.
   Tracked in `./todo.md`.
4. ~~Do we scaffold real `.claude/` files (commands, skills, subagents) alongside
   `.github/`?~~ **Resolved for now**: not yet — Claude Code steps are described in the
   lab tables only. Real `.claude/` scaffolding is likely eventually but out of scope
   for this rework pass. Tracked in `./todo.md`.
