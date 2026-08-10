# Lab 09: Designing Effective Custom Agents

**Module:** 4  
**Duration:** 25 minutes  
**Part:** Advanced GitHub Copilot / Claude Code (Part 2)

## Objectives

By the end of this lab, you will:
- Understand the key components of agent instructions
- Learn how to design agents around roles, not tasks
- Practice iterating on agent behavior through instruction refinement
- Recognize patterns for creating reliable, trustworthy agents

## Prerequisites

- Completion of [Lab 08: Workflow Agents](lab-08-workflow-agents.md)
- VS Code with the GitHub Copilot extension, and/or Claude Code installed
- Access to the TaskManager workshop repository

## Background

### Agents Are Products, Not Prompts

Creating a custom agent isn't just writing a prompt. It's designing a **reusable product** that your team will rely on. The component model below applies **equally to GitHub Copilot's `.agent.md` files and Claude Code's `.claude/agents/*.md` subagents** — only the frontmatter and invocation mechanics differ (see [Authoring for Both Tools](#authoring-for-both-tools)).

**Key Principle:** Design for roles (specialists), not tasks (one-off actions)

❌ **Task-based (Bad):** "Generate unit tests for a method"  
✅ **Role-based (Good):** "Test Strategist - Proposes comprehensive test strategies"

---

## Core Components of Agent Instructions

Every effective agent definition has:

### 1. **Identity & Role**
- Who is this agent?
- What expertise does it embody?

```markdown
You are an expert software architect specializing in Clean Architecture...
```

### 2. **Responsibilities**
- What does this agent do?
- What is in scope vs out of scope?

```markdown
## Responsibilities
- Analyze code structure for architectural boundary violations
- Identify dependency direction issues
- Review domain model design for DDD patterns
```

### 3. **Context**
- What does the agent need to know about the project?
- What standards, patterns, or constraints apply?

```markdown
## Context
This project follows Clean Architecture with these layers:
- Domain: Business logic (no external dependencies)
- Application: Use cases (depends on Domain only)
...
```

### 4. **Constraints**
- What should the agent ALWAYS do?
- What should it NEVER do?

```markdown
## Constraints
- ALWAYS check for circular dependencies
- NEVER recommend breaking Clean Architecture boundaries
```

### 5. **Process/Approach**
- How should the agent work through the task?
- What steps should it follow?

```markdown
## Analysis Process
1. Identify which layer(s) the code belongs to
2. Check dependencies against allowed directions
3. Review domain modeling
```

### 6. **Output Format**
- How should results be structured?
- What sections or headings should appear?

```markdown
## Output Format
Provide your review in this structured format:

### Architecture Review Summary
- **Scope:** [what was reviewed]
- **Overall Assessment:** [Pass/Needs Attention/Refactor Required]
...
```

### 7. **Tone & Approach**
- How should the agent communicate?
- What's the personality or style?

```markdown
## Tone
- Be direct and constructive
- Explain WHY something is a concern (educational)
- Acknowledge good practices when present
```

You can see all seven components applied together in the real
`.claude/agents/architecture-reviewer.md`, `backlog-generator.md`, and
`test-strategist.md` subagent files in this repository.

---

## Authoring for Both Tools

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot                                          | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code                                 |
| --------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- |
| File: `.github/agents/<name>.agent.md`                                                                                            | File: `.claude/agents/<name>.md`                                                                                  |
| Frontmatter: `name`, `description`, `tools` (array), `model`, plus advanced properties covered below                              | Frontmatter: `name`, `description`, `tools` (comma-separated list), `model` — a deliberately smaller surface area |
| Selected from the **Agent Mode dropdown**, or auto-invoked as a subagent depending on `user-invocable`/`disable-model-invocation` | Invoked by `@name` mention, or auto-delegated by Claude when the task matches the subagent's `description`        |
| Body: identical seven-component structure (Identity, Responsibilities, Context, Constraints, Process, Output Format, Tone)        | Body: same seven-component structure                                                                              |

This repository's three workshop agents (`architecture-reviewer`,
`backlog-generator`, `test-strategist`) exist as real files on both sides —
`.claude/agents/*.md` and `.github/agents/*.agent.md` — so you can study the
component model from either tool's actual files.

---

## 🎥 Demo: Analyze Existing Agents (10 minutes)

See [Lab 09 Demo: Analyze Existing Agents](lab-09-demo-analyze-existing-agents.md).
The facilitator maps the three workshop agents onto the component model above
while the group discusses patterns and gaps.

---

## Advanced Agent Configuration

Now that you understand the core components, let's explore **advanced agent capabilities** that give GitHub Copilot fine-grained control over agent behavior. Claude Code's subagent frontmatter is intentionally simpler and does not expose direct equivalents for these properties — see the note at the end of this section.

### Agent Frontmatter Properties

The YAML frontmatter at the top of `.agent.md` files controls how agents appear and behave. Beyond the basic `name` and `description`, several optional properties provide powerful customization.

#### Basic Properties (Review)

```yaml
---
name: "agent-name"
description: "What the agent does"
tools: ['read', 'search', 'write']  # Tools available to agent
model: Claude Sonnet 4.5  # AI model to use
---
```

#### Advanced Properties (New)

##### 1. `user-invocable` (boolean, default: true)

Controls whether the agent appears in the agents dropdown.

```yaml
user-invocable: true   # Visible in dropdown (default)
user-invocable: false  # Hidden from dropdown
```

**Use Cases:**
- **true (default)**: General-purpose agents users select explicitly
- **false**: Internal agents only called by other agents as subagents

**Example:** A "validation" agent that only runs as part of other workflows:
```yaml
---
name: "internal-validator"
description: "Validates code quality metrics"
user-invocable: false  # Users don't directly invoke this
---
```

---

##### 2. `disable-model-invocation` (boolean, default: false)

Prevents other agents from calling this agent as a subagent.

```yaml
disable-model-invocation: false  # Can be called as subagent (default)
disable-model-invocation: true   # Prevents subagent invocation
```

**Use Cases:**
- **false (default)**: Agent can be used both directly and as subagent
- **true**: Agent requires direct user interaction (UI agents, approval workflows)

**Example:** An agent that requires human confirmation:
```yaml
---
name: "deployment-approver"
description: "Reviews and approves deployments"
disable-model-invocation: true  # Must be invoked manually
---
```

---

##### 3. `agents` (array)

Specifies which agents this agent can call as subagents.

```yaml
agents: ['*']                    # Can call any agent (default behavior)
agents: ['architect', 'planner'] # Can only call specific agents
agents: []                       # Cannot call any agents
```

**Use Cases:**
- **`['*']`**: Orchestrator agents that coordinate multiple specialists
- **Specific list**: Controlled workflows with defined handoff paths
- **`[]`**: Terminal agents that don't delegate (e.g., implementers)

**Example:** A planner that can only handoff to specific implementers:
```yaml
---
name: "feature-planner"
description: "Plans feature implementation"
agents: ['implementer', 'test-strategist']  # Controlled handoffs
---
```

---

##### 4. `argument-hint` (string)

Provides hint text shown in chat input when agent is selected as slash command.

```yaml
argument-hint: "[file path] [options]"
```

**Use Cases:**
- Guide users on what information to provide
- Document expected parameters or context

**Example:**
```yaml
---
name: "code-reviewer"
description: "Reviews code for quality issues"
argument-hint: "[file or directory to review]"
---
```

When user types `/code-reviewer`, they see hint: `[file or directory to review]`

---

#### What About Claude Code?

Claude Code subagents don't expose direct equivalents for `user-invocable`,
`disable-model-invocation`, `agents`, `argument-hint`, or handoffs:

- **Visibility/invocation control** — every subagent in `.claude/agents/` can
  be `@mentioned` directly *and* auto-delegated to based on its
  `description`. There's no flag to hide a subagent from direct invocation
  the way `user-invocable: false` does in Copilot.
- **Restricting which agents a subagent can call** — Claude Code doesn't
  currently support a documented `agents` allow-list field; scoping which
  subagents get invoked is done by keeping each subagent's `description`
  narrow and specific.
- **`argument-hint`** — not applicable, since subagents aren't exposed as
  slash commands in the same way.

If your workflow depends on these fine-grained controls, that's a reason to
prefer Copilot's agent model for that specific use case — call this out
explicitly when discussing tool choice with your team.

---

## 🎥 Demo: Advanced Configuration Examples & Handoffs (15 minutes)

See [Lab 09 Demo: Advanced Agent Configuration Examples & Handoffs](lab-09-demo-advanced-config-and-handoffs.md).
The facilitator walks through property-combination examples and the handoffs
workflow (definition, patterns, best practices, and a live design exercise)
while the group discusses. **Handoffs are a Copilot-specific capability** —
see the demo file for how to approximate a similar guided workflow in Claude
Code without a native handoffs feature.

---

## Exercise 2: Iterating on Agent Instructions (15 minutes)

### Scenario

The **Test Strategist** agent sometimes provides too many tests, including low-value scenarios. You want to refine it to focus on **high-value tests only**.

### Part A: Baseline Behavior

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| Open Copilot Chat in Agent Mode and select **Test Strategist**                           | In the Claude Code REPL, mention `@test-strategist`                               |

Prompt: `Propose test scenarios for a simple getter method that returns a task's title`

**Observe:** Does it over-test? Does it recommend unnecessary tests?

---

### Part B: Refine the Agent

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| Open `.github/agents/test-strategist.agent.md`                                           | Open `.claude/agents/test-strategist.md`                                          |

Add this constraint to the **Constraints** section:

```markdown
- Focus on HIGH-VALUE tests only (avoid trivial getters/setters)
- Skip tests for auto-implemented properties or simple pass-through methods
- Prioritize tests that verify business logic, invariants, and edge cases
```

**Save the file.**

---

### Part C: Re-test Behavior

Return to the same tool and re-invoke **Test Strategist** (`@test-strategist` in
Claude Code, or the Agent Mode dropdown in Copilot) with the
same prompt: `Propose test scenarios for a simple getter method that returns a task's title`

**Observe:** Did the agent's behavior change? Did it decline or simplify the recommendation?

---

### Reflection Questions

1. **Did the constraint reduce over-testing?**
2. **What other refinements would improve this agent?**
3. **How many iterations would you expect before an agent is "production-ready"?**

---

## Design Patterns for Reliable Agents

### Pattern 1: Role-Based Scope
✅ **Do:** "You are a code reviewer specializing in security"  
❌ **Don't:** "Generate code for feature X"

### Pattern 2: Explicit Constraints
✅ **Do:** "NEVER recommend breaking layer boundaries"  
❌ **Don't:** Leave implicit assumptions unstated

### Pattern 3: Structured Outputs
✅ **Do:** Define sections, headings, and formats  
❌ **Don't:** Allow free-form, unpredictable responses

### Pattern 4: Educational Tone
✅ **Do:** "Explain WHY this is a concern"  
❌ **Don't:** Just list issues without context

### Pattern 5: Boundaries & Disclaimers
✅ **Do:** "This agent reviews; humans decide"  
❌ **Don't:** Imply the agent is authoritative

These patterns apply whether you're authoring a Copilot `.agent.md` file or a
Claude Code `.claude/agents/*.md` subagent.

---

## Governance Considerations

### Versioning
- Agents should be versioned (like code)
- Track changes in git commit history
- Consider semantic versioning for major changes

### Review Process
- Agent changes require **pull request review**
- Test agent behavior before merging
- Document breaking changes

### Team Alignment
- Agents encode **team decisions**, not individual preferences
- Discuss agent behavior in retrospectives
- Update agents as practices evolve

### Documentation
- Document when to use each agent, and whether it exists for Copilot, Claude Code, or both
- Provide examples of good vs bad usage

---

## Common Pitfalls

### ❌ Pitfall 1: Task-Based Agents
Creating agents for single, one-off tasks instead of repeatable roles.

**Fix:** Design for workflows, not individual actions.

### ❌ Pitfall 2: Vague Instructions
Leaving agent behavior open to interpretation.

**Fix:** Use explicit constraints and structured outputs.

### ❌ Pitfall 3: Over-Scoping
Making agents do too much.

**Fix:** Keep agents focused on one role or domain.

### ❌ Pitfall 4: Under-Testing
Deploying agents without validating behavior.

**Fix:** Test agents with real scenarios before sharing.

### ❌ Pitfall 5: No Iteration Loop
Treating agents as "set and forget."

**Fix:** Continuously refine based on usage and feedback.

---

## Key Takeaways

✅ **Agents are products** - Design, test, and maintain them like code  
✅ **Role-based design** - Specialists, not task executors  
✅ **Explicit constraints** - State what the agent must/must not do  
✅ **Structured outputs** - Consistency requires format  
✅ **Iterate continuously** - Refine based on real usage  
✅ **Govern as team assets** - Version, review, and document  
✅ **The component model is tool-agnostic** - Copilot `.agent.md` files and Claude Code subagents share the same seven-part structure; only frontmatter and invocation mechanics differ

---

## Next Steps

Apply everything you've learned by building and iterating on your own custom agent for a real workflow in your team.

---

## Additional Resources

- [Agent Design Guide](../guides/agent-design-guide.md)
- [Agent Governance](../guides/agent-governance.md)
- [Claude Code Documentation: Subagents](https://docs.claude.com/en/docs/claude-code/sub-agents)
