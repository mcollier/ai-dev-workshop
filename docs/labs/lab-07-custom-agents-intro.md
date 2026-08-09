# Lab 07: Introduction to Custom Agents

**Module:** 2  
**Duration:** 30 minutes  
**Part:** Advanced GitHub Copilot / Claude Code (Part 2)

## Objectives

By the end of this lab, you will:
- Understand what custom agents are and how they work in GitHub Copilot and Claude Code
- Differentiate between agents, prompts, and repo-wide instructions
- Use a pre-built custom agent
- Recognize when custom agents improve consistency over ad-hoc prompting

## Prerequisites

- Completion of [Lab 06: Skills & Customization](lab-06-skills-and-customization.md)
- VS Code with the GitHub Copilot extension, and/or Claude Code installed
- Access to the TaskManager workshop repository

## Background

### What Are Custom Agents?

Custom agents are **specialized AI assistants** that provide consistent,
role-based guidance for specific workflows. Think of them as expert
consultants you can invoke when needed.

**Key Characteristics:**
- **Named entities** - Selectable from a dropdown (Copilot) or invoked by name (Claude Code)
- **Role-based personas** - Architecture reviewer, test strategist, backlog generator, etc.
- **Defined scope** - Clear responsibilities and constraints
- **Structured outputs** - Consistent format for results
- **Team-aligned** - Encode team practices and standards

### Mental Model: The Specialist Analogy

```text
Standard Chat = General AI Assistant
Custom Agent = Domain Expert Consultant

You wouldn't ask a general assistant to:
- Review architecture (you'd ask an architect)
- Plan testing strategy (you'd ask a QA specialist)
- Generate backlog items (you'd ask a product analyst)

Custom agents ARE those specialists.
```

### Where Agents Live

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code                                                                   |
| ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------- |
| `.github/agents/*.agent.md` — selected from the Agent Mode dropdown                      | `.claude/agents/*.md` — invoked by name (e.g. `@architecture-reviewer`) or auto-delegated by Claude when the task matches the agent's `description` |

> **Known gap:** this repository's `.github/agents/` directory currently only
> contains a `.gitkeep` placeholder — the three `.agent.md` files described
> below (`architecture-reviewer`, `backlog-generator`, `test-strategist`)
> **do not exist yet** on the Copilot side. This is tracked as a bug in
> `todo.md`. The Claude Code equivalents **do exist** at
> `.claude/agents/architecture-reviewer.md`, `.claude/agents/backlog-generator.md`,
> and `.claude/agents/test-strategist.md`, so Claude Code users can complete
> every exercise in this lab today. Copilot users should read the agent
> descriptions below and, until the `.agent.md` files land, paste the
> equivalent `.claude/agents/*.md` prompt body into standard Copilot Chat to
> approximate the same behavior.

### How Agents Differ From

| Feature         | Repo-wide Instructions | Ad-hoc Prompts    | Custom Agents       |
| --------------- | ---------------------- | ----------------- | ------------------- |
| **Scope**       | Always active          | One-off           | Invoked on demand   |
| **Purpose**     | Global guardrails      | Specific task     | Repeatable workflow |
| **Reusability** | Implicit               | Manual copy/paste | Built-in            |
| **Consistency** | Background rules       | Variable          | Structured          |
| **Best for**    | Coding standards       | Exploration       | Workflow automation |

### When to Use Custom Agents

✅ **Use agents when:**
- You have **repeated workflows** (reviews, planning, analysis)
- You need **consistent outputs** across team members
- You want to **encode expert knowledge** in a reusable form
- You're performing **validation or review** tasks

❌ **Don't use agents when:**
- A simple prompt suffices
- You're exploring or learning
- The task is one-off or unique

---

## Exercise 1: Using the Architecture Reviewer Agent (15 minutes)

### Setup

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot                                                                                                                                                                               | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code                  |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------- |
| Would use a pre-built **Architecture Reviewer** agent at `.github/agents/architecture-reviewer.agent.md` — **not yet scaffolded, see the callout above**. Read the equivalent Claude Code file to preview the intended behavior, then approximate it in standard Chat. | Uses the pre-built **Architecture Reviewer** subagent at `.claude/agents/architecture-reviewer.md` |

### Scenario

You suspect there might be architectural issues in the Task entity or the repository implementation. You want an expert review.

### Instructions

1. **Locate the agent:**
   - Claude Code: open `.claude/agents/architecture-reviewer.md`. Copilot: open `.claude/agents/architecture-reviewer.md` as a stand-in reference until `.github/agents/architecture-reviewer.agent.md` is authored.
   - Read the agent's responsibilities and constraints
   - Note the structured output format

2. **Open your AI coding tool:**

   | <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
   | ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
   | Open Copilot Chat and switch to **Agent Mode**                                           | Open the Claude Code REPL (`claude` in the integrated terminal)                   |

3. **Select the Architecture Reviewer agent:**

   | <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot                                                                                                                  | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code                   |
   | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------- |
   | Select **Architecture Reviewer** from the agent dropdown *(unavailable until `.github/agents/architecture-reviewer.agent.md` is created — use standard Chat with the copied prompt body in the meantime)* | Mention `@architecture-reviewer` explicitly, or let Claude auto-delegate based on its `description` |

4. **Use this prompt:**
   ```text
   Review the Task entity and TaskRepository for architectural compliance with Clean Architecture and DDD patterns.
   ```

5. **Observe the agent's behavior:**
   - Does it follow its defined structure?
   - Does it reference the layers (Domain, Application, Infrastructure, Api)?
   - Does it provide the expected sections (Strengths, Concerns, Violations, Recommendations)?

6. **Compare to standard chat:**
   - Try the same prompt without the agent
   - Note the differences in depth, structure, and consistency

### Expected Outcome

The Architecture Reviewer agent should:
- Analyze the code through a Clean Architecture lens
- Identify specific boundary violations (if any)
- Provide structured findings (✅ Strengths, ⚠️ Concerns, 🚫 Violations)
- Give actionable recommendations
- Reference project conventions (ADRs, DDD patterns)

### Reflection Questions

1. **How did the agent's response differ from standard chat?**
2. **Did the agent follow its defined output format?**
3. **Would this consistency be valuable for team code reviews?**
4. **What happens if you ask the agent something outside its scope?**

---

## Exercise 2: Compare Agent vs. Standard Chat (10 minutes)

### Instructions

Perform the same task twice:

#### Round 1: Standard Chat
1. Open your AI tool's standard chat (no agent selected)
2. Prompt: `Review the Task domain model for DDD compliance`
3. Record the response structure and depth

#### Round 2: Architecture Reviewer Agent

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot              | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ----------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| Switch to Agent Mode and select **Architecture Reviewer** *(pending scaffolding — see callout above)* | Mention `@architecture-reviewer`                                                  |

3. Same prompt: `Review the Task domain model for DDD compliance`
4. Record the response structure and depth

### Comparison Table

Fill in based on your observations:

| Aspect                | Standard Chat      | Architecture Reviewer Agent |
| --------------------- | ------------------ | --------------------------- |
| **Response Format**   | [Your observation] | [Your observation]          |
| **Depth of Analysis** | [Your observation] | [Your observation]          |
| **Consistency**       | [Your observation] | [Your observation]          |
| **Actionability**     | [Your observation] | [Your observation]          |
| **Repeatability**     | [Your observation] | [Your observation]          |

---

## Exercise 3: Exploring Other Agents (5 minutes)

The repository includes definitions for three custom agents. Briefly explore each:

### 1. Backlog Generator

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot             | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ---------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| **Location:** `.github/agents/backlog-generator.agent.md` *(not yet scaffolded — see callout above)* | **Location:** `.claude/agents/backlog-generator.md`                               |

- **Try:** "Generate user stories for adding task comments feature"
- **Observe:** Structured user stories with acceptance criteria

### 2. Test Strategist

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot           | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| -------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| **Location:** `.github/agents/test-strategist.agent.md` *(not yet scaffolded — see callout above)* | **Location:** `.claude/agents/test-strategist.md`                                 |

- **Try:** "Propose test scenarios for Task creation"
- **Observe:** Categorized test scenarios (unit, integration, edge cases)

### Questions

- Which agent would you use most frequently in your work?
- Can you think of other agents your team might need?

---

## Key Insights

### If Repo-wide Instructions Are Guardrails

**Repo-wide Instructions** (`.github/copilot-instructions.md` / `CLAUDE.md`) = Background rules always enforced
(e.g., "Use Clean Architecture, write tests first, follow DDD")

**Custom Agents** = Specialists you consult on demand
(e.g., "Architecture Reviewer, analyze this design")

### Agents Are Products, Not Prompts

- Agents should be **versioned and reviewed** (like code)
- Agents **encode team knowledge** and standards
- Agents provide **repeatable, consistent outcomes**
- Agents improve **onboarding** (new team members use the same expert guidance)

---

## Key Takeaways

✅ **Custom agents provide role-based expertise** on demand
✅ **Agents ensure consistency** across team members
✅ **Agents are reusable** - define once, use repeatedly
✅ **Agents complement Instructions** - not a replacement
⚠️ **Agents require maintenance** - treat them as team assets

---

## Common Questions

**Q: Can I use multiple agents in one session?**
A: Yes! Switch agents as needed for different workflow steps. Claude Code can also auto-delegate to the right subagent based on its `description`.

**Q: Do agents replace repo-wide Instructions?**
A: No. Instructions/`CLAUDE.md` are always-on guardrails; agents are on-demand specialists.

**Q: Can I create my own agent?**
A: Absolutely! That's covered in [Lab 09: Agent Design](lab-09-agent-design.md) and [Lab 10: Build Your Own](lab-10-capstone-build-agent.md).

**Q: What if an agent gives incorrect advice?**
A: Agents are assistants, not authorities. You're accountable for the final decision. Iterate on agent instructions to improve accuracy.

---

## Next Steps

In [Lab 08: Workflow Agents in Action](lab-08-workflow-agents.md), you'll apply these agents to real development workflows and compare their outputs to ad-hoc prompting.

---

## Additional Resources

- [Agent vs Instructions vs Prompts Diagram](../design/diagrams/agent-vs-instructions-vs-prompts.md)
- [GitHub Documentation: Custom Agents](https://docs.github.com/copilot)
- [Claude Code Documentation: Subagents](https://docs.claude.com/en/docs/claude-code/sub-agents)
