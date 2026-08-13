---
name: git-commit
description: Writes a Conventional Commits-formatted commit message for staged changes and commits them. Use whenever the user asks to "commit" or "commit the files" — analyze the actual diff to determine type, scope, and message rather than asking the user to specify the format.
argument-hint: "[optional: scope hint]"
user-invocable: true
disable-model-invocation: false
---

# Git Commit Skill

This skill produces standardized, semantic git commits using the
[Conventional Commits](https://www.conventionalcommits.org/) specification.
It analyzes the actual staged diff to determine the appropriate type, scope,
and message — it does not ask the user to dictate the format.

## When to Use This Skill

✅ **Use this skill when:**
- The user asks to "commit", "commit the files", "commit this", or similar,
  with no further formatting instructions.
- A commit message needs to be generated or reviewed before running
  `git commit`.

❌ **Don't use this skill for:**
- Deciding *whether* to commit or stage files (confirm with the user first).
- Writing PR descriptions (a separate concern, even though it's often a
  follow-up step).

## Format

```text
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

- Subject line: `<type>([optional scope]): <description>`, 72-character
  limit.
- One logical change per commit; use scope to denote layer/feature
  (`api`, `domain`, `infrastructure`, `docs`, etc.).

### Commit Types

| Type       | Purpose                        |
|------------|---------------------------------|
| `feat`     | New feature                     |
| `fix`      | Bug fix                         |
| `docs`     | Documentation only               |
| `style`    | Formatting/style (no logic)      |
| `refactor` | Code refactor (no feature/fix)   |
| `perf`     | Performance improvement           |
| `test`     | Add/update tests                  |
| `build`    | Build system/dependencies         |
| `ci`       | CI/config changes                 |
| `chore`    | Maintenance/misc                  |
| `revert`   | Revert commit                     |

### Breaking Changes

```text
# Exclamation mark after type/scope
feat!: remove deprecated endpoint

# BREAKING CHANGE footer
feat: allow config to extend other configs

BREAKING CHANGE: `extends` key behavior changed
```

## Procedure

1. **Inspect the staged diff** (`git diff --staged`) — never guess the
   type/scope from the request alone.
2. **Pick the type** from the table above based on what actually changed.
3. **Pick the scope** from the affected layer/feature (e.g. `api`, `domain`,
   `tests`, or omit if repo-wide).
4. **Write the subject** (<=72 chars) and, for anything beyond a trivial
   change, a body explaining what and why.
5. **Run the commit** (e.g. `git commit -m "..."` or `git commit -F-` for a
   multi-line message).

## Examples

```text
feat(api): add order endpoint
fix(domain): correct order validation logic
test(order): add unit tests for order creation
chore: update dependencies
```

## Constraints

- ALWAYS base type/scope/description on the actual diff, not assumptions.
- ALWAYS keep the subject line under 72 characters.
- NEVER include secrets, tokens, or credentials in a commit message.
