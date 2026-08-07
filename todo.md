# To Do List

- [ ] Support GitHub Copilot App
- [ ] Section on file locations for each tool (GitHub Copilot CLI, VS Code GitHub Copilot, Claude Code)
- [ ] Section on using a plugin marketplace
- [ ] Account for newer skills and patterns
- [ ] Account for Squad or similar?
- [ ] Account for prompt cache details
- [ ] Account for model selection guidance
- [ ] Review workshop checklist to ensure guidance on using GitHub Copilot CLI
- [ ] `@workspace` is no longer current (superseded by automatic workspace context / `#codebase`) — updated the labs, but presentations, guidebooks, and other materials still need the same fix once integrated
- [ ] Sample `src/` TaskManager app is being built by someone else in a separate PR — once merged, revisit labs to confirm file paths/structure referenced in prompts still match, and drop any "reference-only" caveats
- [ ] Fix or remove the stale `../guides/` and `../presentations/` links in docs/labs/README.md and lab files — being addressed later once that content is integrated
- [ ] Scaffold `.claude/skills/` and `.claude/commands/` mirroring the intended `.github/skills/` and slash commands — `.claude/agents/` subagents (architecture-reviewer, backlog-generator, test-strategist) are now scaffolded; skills and commands are still deferred
- [ ] **Bug:** `.github/agents/architecture-reviewer.agent.md`, `backlog-generator.agent.md`, `test-strategist.agent.md`, `planner.agent.md`, and `engineer.agent.md` referenced throughout Labs 03 (demo) and 07-09 do not actually exist in the repo (only `.github/agents/.gitkeep` is present) — same for `.github/skills/` (directory doesn't exist at all). The Claude Code equivalents were scaffolded in `.claude/agents/`, but the Copilot-side files still need to be authored so the labs' Agent Mode exercises work as written