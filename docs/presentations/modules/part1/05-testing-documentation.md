---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 5

## Testing, Documentation & Workflow
### Completing the Lifecycle

**Duration:** 15 minutes

---

## Lab 04 Overview

**Complete the development lifecycle:**

1. Generate comprehensive tests
2. Add XML documentation
3. Update API documentation
4. Commit changes
5. Generate PR description

**Time:** 15 minutes  
**Goal:** AI-assisted workflow end-to-end

---

<style scoped>
section {
  font-size: 24px;
}
</style>

## Generate Tests with /tests

```text
# In Copilot Chat (VS Code)
/tests for CreateTaskCommand
```

**Copilot CLI** (no `/tests` shortcut — just ask) or **Claude Code CLI** (`/tests` is a custom command in this repo — see `.claude/commands/tests.md`):

```text
Generate comprehensive tests for CreateTaskCommand
```

**Generates:**
- Happy path tests
- Edge cases
- Boundary conditions
- Error scenarios

**Your job:** Review, adjust, run

---

## Documentation with /doc

```text
# In Copilot Chat (VS Code)
/doc for ITaskService
```

**Copilot CLI** (no `/doc` shortcut — just ask) or **Claude Code CLI** (`/doc` is a custom command in this repo — see `.claude/commands/doc.md`):

```text
Add XML documentation comments for ITaskService
```

**Generates:**
- XML documentation comments
- Parameter descriptions
- Return value docs
- Example usage

**Result:** IntelliSense-ready documentation

---

<style scoped>
section {
  font-size: 20px;
}
</style>

## Conventional Commits

**Format:** `<type>(<scope>): <description>`

### Types
- `feat` - New feature
- `fix` - Bug fix
- `docs` - Documentation
- `test` - Tests
- `refactor` - Code restructuring
- `chore` - Maintenance

**Example:** `feat(domain): add Priority value object`

### Enforced via a skill, not memory
- Both tools load the **`git-commit` skill** on demand — the format
  guidance isn't kept in every prompt's context.
- **Copilot**: `.github/skills/git-commit/SKILL.md`
- **Claude Code**: `.claude/skills/git-commit/SKILL.md`
- Just ask to "commit the files" — the skill supplies the format.

---

<style scoped>
section {
  font-size: 20px;
}
</style>

## PR Description

**VS Code Copilot Chat** — Ask mode drafts text only; **Agent mode** (with GitHub CLI (`gh`) or
GitHub MCP) can create the PR itself:

```text
#codebase Generate a PR description for my changes and open the PR
```

**Copilot CLI** (`/pr` — drafts *and* opens the PR via GitHub integration):

**Claude Code CLI** (drafts a description, then runs the GitHub CLI itself):

```text
Draft a PR description for my changes, then create the PR with gh pr create
```

**All three provide:**
- Summary of changes
- Files modified
- Testing notes
- Breaking changes (if any)

💡_Try `/review` (Copilot CLI) or `/code-review` (Claude Code) to review changes. Also `/security-review`._

---

<!-- markdownlint-disable-next-line MD025 -->
# Hands-On Time

**Lab Guide:** [Lab 04: Testing & Documentation](../../../labs/lab-04-testing-documentation-workflow.md)

**Next Module:** [Wrap-Up & Discussion](06-wrapup-discussion.md)

**Previous Module:** [Generation & Refactoring](04-generation-refactoring.md)
