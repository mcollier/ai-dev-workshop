---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 6

## Wrap-Up & Next Steps
### Governance and Continuous Improvement

**Duration:** 10 minutes

---

## Discussion Questions

1. Which workflows benefit most from agents?
2. What should be standardized at team vs org level?
3. How do we prevent "prompt sprawl"?
4. How should agents be reviewed and evolved?

---

## Best Practices for Teams

## Start Small
- Begin with **reviewer agents**, not executors
- Validate value before scaling

## Maintain a Catalog
- Centralized registry of agents
- Clear usage guidelines

---

## Govern as Assets
- Version control
- PR review process
- Regular updates

---

## Define Your Approach

Adopt a team-wide approach - everyone follows the same process (agents, skills, etc.)

Org/Team plugin marketplace

- Research -> Plan -> Implement -> Review (Microsoft HVE)
- `/grill-me` and `/implement` (Matt Pocock)
- `/spec` -> `/plan` -> `/build` -> `/test` -> `/review` -> `/ship` (Addy Osmani)
- Spec-Driven Development ([GitHub Spec Kit](https://github.com/github/spec-kit))
- BMad Method ([https://docs.bmad-method.org/](https://docs.bmad-method.org/))

---

### Squad for GitHub Copilot
- Human-led AI development team for GitHub Copilot.
- Specialist agents (frontend, tester, designer, scribe, lead) in your repo.
- Parallel execution.
- Persists decisions in markdown files; reference for the future.

Learn more at [https://bradygaster.github.io/squad/](https://bradygaster.github.io/squad/)

---

## Keep Humans Accountable

> Agents advise, humans decide

- Never blindly trust agent output
- Validate recommendations
- Use agents as **first pass**, not final word
- Maintain human oversight

---

## Taking Agents to Production

**Before sharing:**
1. Test with real scenarios
2. Get peer review
3. Document edge cases
4. Add to team catalog
5. Set up governance

---

**Continuous improvement:**
- Collect usage feedback
- Track common issues
- Update based on lessons
- Retire obsolete agents

---

## Adoption Roadmap

**Week 1:** Use existing agents in daily work  
**Week 2:** Identify repetitive workflow → draft agent  
**Week 3:** Test and refine with real scenarios  
**Week 4:** Share with team and gather feedback

---

## Key Takeaways

✅ **Ask, Plan, Agent** - Use the right interaction mode  
✅ **Customization hierarchy** - Prompts → Instructions → Skills → Agents  
✅ **Skills vs Agents** - Knowledge vs Workflows  
✅ **Slash commands** - Discover with /help, /agents, /skills  
✅ **Custom agents** - Specialists with tool access  
✅ **Handoffs** - Orchestrate sequential workflows  
✅ **Role-based design** - Focus on WHO, not WHAT  
✅ **Iterate continuously** - Agents/Skills improve over time  
✅ **Govern as assets** - Version, review, maintain  
✅ **Humans accountable** - AI assists, you decide

---

## Resources

📚 **Documentation**
- [Customization Decision Guide](../../../guides/customization-decision-guide.md)
- [Agent Design Guide](../../../guides/agent-design-guide.md)
- [Agent Governance](../../../guides/agent-governance.md)
<!-- - [Custom Agent Catalog](../../../guides/custom-agent-catalog.md) -->

🔗 **Labs**
- All labs in `docs/labs/`
- Agent definitions in `.github/agents/`

---

<!-- markdownlint-disable-next-line MD025 -->
# Thank You

## Questions?

## Next Steps

1. **Practice** with existing agents
2. **Build** your own agent
3. **Share** with your team
4. **Iterate** based on feedback
5. **Govern** as team assets

**Remember:** Agents are products, not prompts

<!-- --- -->

<!-- markdownlint-disable-next-line MD025 -->
<!-- # Part 2 Complete -->

<!-- **Previous Module:** [Capstone Lab](06-capstone-lab.md) -->

<!-- **Return to Part 1:** [Fundamentals](../part1/00-kickoff-and-setup.md) -->
