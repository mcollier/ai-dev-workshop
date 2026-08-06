# Lab 03 Demo: Plan First with Agents (Custom Agents Demo)

**Format:** Facilitator-led demo

> **This is a demo, not a hands-on exercise.** The facilitator models this
> workflow live while participants watch and discuss. Extracted from
> [Lab 3: Code Generation & Refactoring](lab-03-generation-and-refactoring.md)
> to keep that lab focused on hands-on generation and refactoring work.

## Why Plan First?

Before making major changes, use Copilot (in Agent Mode) to generate a plan
first. This helps you:
- Understand the scope and impact of your changes
- Catch misunderstandings or missing steps early
- Collaborate and iterate on the approach before any code is changed

## What the Facilitator Does

- In Copilot Chat (Agent Mode), ask: "Propose a step-by-step plan to refactor LegacyTaskProcessor to use async/await, add logging, and follow Object Calisthenics."
- Review the plan. Edit or reorder steps as needed.
- Only then, ask Copilot (or a custom agent like `@engineer`) to implement the plan, one step at a time or all at once.

### Custom Agents Demo

- Use `@planner` to generate/refine the plan
- Use `@engineer` to execute the approved plan

## Discuss as a Group

- Did planning first catch any issues you would have missed?
- Was the implementation smoother or more predictable?

## Next Steps

Return to [Lab 3: Code Generation & Refactoring](lab-03-generation-and-refactoring.md)
to continue with the hands-on Overview and Part 1.
