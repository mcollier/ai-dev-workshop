---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 6

## Wrap-Up & Discussion
### Lessons Learned

**Duration:** 15 minutes

---

## What We Covered

✅ **Instructions** - Team-wide consistency  
✅ **TDD with AI** - Tests first, always  
✅ **Requirements → Code** - Systematic transformation  
✅ **Clean Architecture** - Maintained via AI  
✅ **Refactoring** - Modernize legacy code  
✅ **Full lifecycle** - Tests, docs, commits, PRs

---

## Key Takeaways

### 1. AI Amplifies Good Practices
- TDD becomes faster
- Architecture patterns enforced
- Documentation debt reduced

### 2. Context Matters
- VS Code: `#codebase`, `#file`, `#selection`
- Copilot CLI / Claude Code: `@file-or-directory` + agent auto-discovery
- Instructions files encode team knowledge (`copilot-instructions.md`/`.instructions.md` or `CLAUDE.md`)
- Better prompts = better results

---

### 3. Human Accountability
- Review all AI suggestions
- Tests validate correctness
- You own the code

---

## Common Pitfalls to Avoid

❌ **Accepting suggestions blindly**  
✅ Review, understand, test

❌ **Skipping tests to go faster**  
✅ Tests first catches errors early

❌ **Generic prompts**  
✅ Use context variables and be specific

❌ **Ignoring architecture**  
✅ Instructions enforce patterns

---

## Anti-Patterns

## Over-reliance
- AI is a tool, not a replacement for thinking
- Understand what code does

## Under-leveraging
- Use Copilot / Claude Code for repetitive tasks
- Don't type boilerplate manually

## Inconsistent standards
- Use instructions
- Encode team decisions

---

## Best Practices

✅ **Start with plan** - Ask for an approach first  
✅ **Tests first** - TDD even with AI  
✅ **Review iteratively** - Don't wait until the end  
✅ **Leverage instructions** - Team knowledge encoded  
✅ **Commit frequently** - Small, focused changes

---

## Discussion Questions

1. **What surprised you most** about AI-assisted development?
2. **Where did GitHub Copilot / Claude Code excel?** Where did it struggle?
3. **How would you use this** in your daily work?
4. **What team standards** should you encode in instructions?
5. **What concerns** do you still have?

---

## Next Steps: Part 2

**Customizing your Agentic Engineering Workflow:**
- Interaction models (Ask, Plan, Agent)
- Skills & Customization Hierarchy
- Custom Agents
- Agent design and handoffs
- Build your own production agent

**When:** [Scheduled time]  
**Duration:** 3 hours

---

## Additional Practice

**Reference Implementation:**
- Branch: `solutions`
- All labs completed
- Best practices demonstrated

**Use it to:**
- Compare your solutions
- See patterns in action
- Continue learning

```bash
git checkout solutions
```

---

## Resources

📚 **Lab Guides:** `docs/labs/`  
📝 **Copilot Instructions:** `.github/instructions/` (context-aware)
📝 **Claude Code:** `CLAUDE.md`

**GitHub Copilot Docs:**  
[https://docs.github.com/copilot](https://docs.github.com/copilot)

**Claude Code Docs**
[https://code.claude.com/docs](https://code.claude.com/docs)

---

## Immediate Actions

**This week:**
1. Try Copilot or Claude Code for **one TDD task**
2. Create **instructions** for your repo (Copilot: `copilot-instructions.md`; Claude Code: `CLAUDE.md`)
3. Use `/tests` and `/doc` (VS Code) or the natural-language equivalent (Copilot CLI / Claude Code) in daily work

**Next month:**
1. Encode **team standards** in instructions
2. Measure **velocity improvement**
3. Share **learnings with team**

---

## Thank You

**See you in Part 2:**  
Customizing your Agentic Engineering Workflow

---

## Remember

> AI is a force multiplier  
> Good practices + Copilot/Claude Code = Great results  
> You are accountable for the code

**Keep learning, keep improving!**

---

<!-- markdownlint-disable-next-line MD025 -->
# Part 1 Complete

**Previous Module:** [Testing & Documentation](05-testing-documentation.md)

**Continue to Part 2:** [Customizing your Agentic Engineering Workflow](../part2/00-welcome-recap.md)
