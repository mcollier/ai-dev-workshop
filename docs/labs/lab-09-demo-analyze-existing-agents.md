# Lab 09 Demo: Analyze Existing Agents

**Duration:** 10 minutes
**Format:** Facilitator-led demo

> **This is a demo, not a hands-on exercise.** The facilitator walks through each
> agent definition live while participants watch and discuss. Extracted from
> [Lab 09: Designing Effective Custom Agents](lab-09-agent-design.md) to keep
> that lab's hands-on time focused on applying the component model.

## Purpose

Reinforce the [Core Components of Agent Instructions](lab-09-agent-design.md#core-components-of-agent-instructions)
from Lab 09 by mapping them onto the three real agents already in the
workshop repository.

## What the Facilitator Does

Open each of the three custom agents and map their components live, narrating
each one against the seven components (Identity & Role, Responsibilities,
Context, Constraints, Process/Approach, Output Format, Tone & Approach).

> Both sides are scaffolded — use whichever file matches your tool. The
> `.claude/agents/*.md` files and their `.github/agents/*.agent.md`
> counterparts describe the same behavior, just in each tool's frontmatter
> format.

### Architecture Reviewer

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| `.github/agents/architecture-reviewer.agent.md`                                           | `.claude/agents/architecture-reviewer.md`                                         |

- **Role:** [Identify the role]
- **Key Responsibilities:** [List 3]
- **Critical Constraints:** [List 2-3]
- **Output Structure:** [Describe the format]

### Backlog Generator

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| `.github/agents/backlog-generator.agent.md`                                               | `.claude/agents/backlog-generator.md`                                             |

- **Role:** [Identify the role]
- **Key Responsibilities:** [List 3]
- **Critical Constraints:** [List 2-3]
- **Output Structure:** [Describe the format]

### Test Strategist

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| `.github/agents/test-strategist.agent.md`                                                 | `.claude/agents/test-strategist.md`                                               |

- **Role:** [Identify the role]
- **Key Responsibilities:** [List 3]
- **Critical Constraints:** [List 2-3]
- **Output Structure:** [Describe the format]

## Discuss as a Group

- What patterns do you notice across all three agents?
- Which component seems most critical for consistency?
- Are there any missing components you'd add?

## Next Steps

Return to [Lab 09: Designing Effective Custom Agents](lab-09-agent-design.md)
to continue with Advanced Agent Configuration and Handoffs.
