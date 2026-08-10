---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 5

## Designing Effective Agents
### Principles, Properties & Patterns

**Duration:** 30 minutes

---

## Core Principle

> Agents are products, not prompts

Design, test, and maintain them **like code**

---

## Agent Components

## Core Instructions
1. **Identity & Role** - Who is this agent?
2. **Responsibilities** - What does it do?
3. **Context** - What does it need to know?
4. **Constraints** - Rules it must follow
5. **Process** - How it approaches tasks

---

6. **Output Format** - Structured results
7. **Tone** - Communication style

## YAML Frontmatter
![w:16](../../../images/githubcopilot.svg) _Copilot-specific properties:_
- `user-invocable`, `disable-model-invocation`
- `agents`, `argument-hint`, `handoffs`

💡_Claude Code subagents use simpler frontmatter and have no built-in handoffs equivalent._

---

## Design Pattern: Role-Based Scope

✅ **Do:** "You are a code reviewer specializing in security"

❌ **Don't:** "Generate code for feature X"

**Focus on WHO, not WHAT**

---

## Design Pattern: Explicit Constraints

✅ **Do:**
```markdown
## Constraints
- ALWAYS check for circular dependencies
- NEVER recommend breaking layer boundaries
```

❌ **Don't:** Leave assumptions unstated

---

## Design Pattern: Structured Outputs

✅ **Do:** Define sections and format

```markdown
## Output Format

### Review Summary
- **Scope:** [what was reviewed]
- **Assessment:** [Pass/Needs Attention/Refactor]

### Findings
...
```

❌ **Don't:** Allow free-form responses

---

## Iteration Loop

```text
Define → Test → Observe → Refine → Repeat
```

**Example refinement:**
- Agent over-tests simple getters
- Add constraint: "Focus on high-value tests only"
- Re-test with same scenario
- Observe improved behavior

---

## Advanced Agent Properties

![w:16](../../../images/githubcopilot.svg) _Copilot-specific — Claude Code subagents use simpler frontmatter._

### user-invocable
- `true`: Visible in dropdown (default)
- `false`: Hidden, only for subagents

### disable-model-invocation
- `true`: Prevents auto-invocation by other agents
- `false`: Callable as subagent (default)

### handoffs
- Sequential workflow buttons
- Guide users through processes
- Human-in-the-loop between steps

---

## Handoffs: Orchestrated Workflows

![w:16](../../../images/githubcopilot.svg) _Copilot-specific feature — Claude Code has no built-in handoffs equivalent._

```yaml
handoffs:
  - label: "Start Implementation"
    agent: "implementer"
    prompt: "Implement the plan above"
    send: false  # Wait for user approval
```

**Use cases:**
- Plan → Implement → Review
- Generate Tests → Make Pass
- Architecture → Documentation

**Key:** `send: false` keeps human in the loop

---

## Governance Considerations

### Versioning
- Track changes in git
- Semantic versioning for major updates

### Review Process
- Agent **and skill** changes require PR review
- Test before merging

### Team Alignment
- Agents/Skills encode **team decisions**
- Update as practices evolve

---

## Common Pitfalls

❌ **Task-based agents** → Use role-based  
&nbsp;&nbsp;&nbsp;_(named for a single instruction, e.g. "Generate code for feature X" —
narrow, hard to reuse, and encourages vague/one-off prompting instead of a
persistent specialist)_  
❌ **Vague instructions** → Be explicit  
❌ **Over-scoping** → Keep focused  
❌ **No testing** → Validate before sharing  
❌ **Set-and-forget** → Iterate continuously

---

<!-- markdownlint-disable-next-line MD025 -->
# Homework

Create a custom agent of your own.  Test, iterate, & refine.


**Lab Guide:** [Lab 09: Agent Design](../../../labs/lab-09-agent-design.md)

**Next Module:** [Wrap-Up & Next Steps](06-wrapup-next-steps.md)

**Previous Module:** [Workflow Agents](04-workflow-agents.md)
