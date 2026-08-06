# Lab 09 Demo: Advanced Agent Configuration Examples & Handoffs

**Duration:** 15 minutes
**Format:** Facilitator-led demo

> **This is a demo, not a hands-on exercise.** The facilitator walks through the
> property-combination examples and the handoffs workflow live while
> participants watch and discuss. Extracted from
> [Lab 09: Designing Effective Custom Agents](lab-09-agent-design.md) to keep
> that lab's hands-on time focused on the core component model and iterating
> on agent instructions.

## Part 1: Property Combination Examples

You can combine the frontmatter properties covered in
[Lab 09's Agent Frontmatter Properties](lab-09-agent-design.md#agent-frontmatter-properties)
for sophisticated agent control.

### Example 1: Public Orchestrator Agent
```yaml
---
name: "workflow-orchestrator"
user-invocable: true              # Users can select it
disable-model-invocation: false   # Other agents can use it
agents: ['planner', 'implementer', 'reviewer']  # Can call these agents
---
```

### Example 2: Internal Helper Agent
```yaml
---
name: "code-formatter"
user-invocable: false             # Hidden from dropdown
disable-model-invocation: false   # But other agents can call it
agents: []                        # Doesn't call other agents
---
```

### Example 3: Manual-Only Terminal Agent
```yaml
---
name: "security-auditor"
user-invocable: true              # Users can select it
disable-model-invocation: true    # But agents cannot auto-invoke it
agents: []                        # Doesn't delegate to other agents
---
```

---

## Part 2: Handoffs — Orchestrating Multi-Agent Workflows

**Handoffs** enable you to create guided sequential workflows that transition between agents with suggested next steps. After an agent completes its response, handoff buttons appear, allowing users to move to the next agent with relevant context and a pre-filled prompt.

### Why Use Handoffs?

✅ **Guided workflows**: Lead users through multi-step processes  
✅ **Context preservation**: Carry context between agent transitions  
✅ **Human-in-the-loop**: Users review and approve each step before proceeding  
✅ **Workflow standardization**: Encode best practices as agent chains

### Handoff Definition

Handoffs are defined in the agent's YAML frontmatter:

```yaml
---
name: "planner"
description: "Creates implementation plans"
tools: ['read', 'search']
handoffs:
  - label: "Start Implementation"
    agent: "implementer"
    prompt: "Implement the plan outlined above"
    send: false
    model: Claude Sonnet 4.5
---
```

#### Handoff Properties

| Property | Required | Description |
|----------|----------|-------------|
| `label` | Yes | Button text shown to user (e.g., "Start Implementation") |
| `agent` | Yes | Target agent identifier to switch to |
| `prompt` | Yes | Pre-filled prompt sent to target agent |
| `send` | No | Auto-submit prompt (default: false). If true, workflow continues automatically |
| `model` | No | Override model for this handoff (optional) |

### Common Handoff Patterns

#### Pattern 1: Plan → Implement → Review

**Planner Agent:**
```yaml
handoffs:
  - label: "Start Implementation"
    agent: "implementer"
    prompt: "Implement the feature plan outlined above"
    send: false
```

**Implementer Agent:**
```yaml
handoffs:
  - label: "Request Code Review"
    agent: "code-reviewer"
    prompt: "Review the implementation for quality and standards"
    send: false
```

**Usage Flow:**
1. User invokes `@planner`: "Plan a user authentication feature"
2. Planner provides plan
3. User clicks "Start Implementation" → switches to `@implementer`
4. Implementer generates code
5. User clicks "Request Code Review" → switches to `@code-reviewer`

---

#### Pattern 2: Write Failing Tests → Make Tests Pass

**Test-First Agent:**
```yaml
handoffs:
  - label: "Implement to Pass Tests"
    agent: "implementer"
    prompt: "Implement the code to make the tests above pass"
    send: false
```

**Usage Flow:**
1. User invokes `@test-first`: "Create tests for order validation"
2. Agent generates failing tests (easier to review than big implementations)
3. User reviews tests, clicks "Implement to Pass Tests"
4. Implementer writes code to satisfy tests

---

#### Pattern 3: Architecture → Documentation

**Architect Agent:**
```yaml
handoffs:
  - label: "Generate Documentation"
    agent: "documenter"
    prompt: "Document the architectural decisions made above"
    send: false
```

---

### Multiple Handoffs

Agents can define multiple handoff options:

```yaml
handoffs:
  - label: "Implement Full Feature"
    agent: "implementer"
    prompt: "Implement the complete feature as planned"
    send: false
  
  - label: "Prototype Only"
    agent: "prototyper"
    prompt: "Create a quick prototype to validate the approach"
    send: false
  
  - label: "Create Architecture Doc"
    agent: "architect"
    prompt: "Document architectural decisions for this plan"
    send: false
```

Users see all three handoff buttons and choose the appropriate next step.

---

### Auto-Send Handoffs

Setting `send: true` makes the workflow continue automatically:

```yaml
handoffs:
  - label: "Auto-Validate"
    agent: "validator"
    prompt: "Validate the implementation above"
    send: true  # Automatically submits when clicked
```

⚠️ **Use cautiously**: Auto-send removes human review checkpoint. Best for:
- Simple validation steps
- Non-destructive operations
- Established workflows where auto-proceed is safe

---

### Handoff Best Practices

#### ✅ Do:
- **Keep handoff chains short** (3-4 agents max)
- **Use descriptive labels** ("Request Security Review" not just "Next")
- **Pre-fill useful prompts** with specific context
- **Design for human review** (prefer `send: false`)
- **Document handoff paths** in agent descriptions

#### ❌ Don't:
- Create circular handoffs (A → B → A)
- Auto-send without clear justification
- Skip human review for destructive operations
- Make handoff chains too complex

---

### Live Design Exercise: A Handoff Workflow

**Scenario:** You want to create a workflow for adding a new feature.

**Agents Involved:**
1. **Planner** - Creates feature plan
2. **Implementer** - Writes code
3. **Test Strategist** - Proposes test scenarios

**Discuss as a group:**
1. What should each agent's handoff button say?
2. Should any handoffs use `send: true`? Why or why not?
3. In what order should agents be chained?

<details>
<summary>Example Solution</summary>

**Planner:**
```yaml
handoffs:
  - label: "Start Implementation"
    agent: "implementer"
    prompt: "Implement the feature plan above, starting with domain layer"
    send: false  # Human reviews plan first
```

**Implementer:**
```yaml
handoffs:
  - label: "Generate Test Strategy"
    agent: "test-strategist"
    prompt: "Propose comprehensive test scenarios for the implementation above"
    send: false  # Human reviews implementation first
```

**Flow:** Plan → Review → Implement → Review → Test Strategy

**Why `send: false` everywhere?**  
Human reviews ensure each step is correct before proceeding. No auto-proceed for code changes.

</details>

## Next Steps

Return to [Lab 09: Designing Effective Custom Agents](lab-09-agent-design.md)
to continue with Exercise 2: Iterating on Agent Instructions.
