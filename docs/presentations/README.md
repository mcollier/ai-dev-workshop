# Workshop Presentations

This directory contains modular Marp presentations for the workshop.

## 📚 Module Catalog

### Part 1: Fundamentals (3 hours)

| Module | Topic | Duration | File |
| -------- | ------- | ---------- | ------ |
| 00 | Kickoff & Setup | 15 min | [00-kickoff-and-setup.md](modules/part1/00-kickoff-and-setup.md) |
| 01 | Features Tour | 15 min | [01-features-tour.md](modules/part1/01-features-tour.md) |
| 02 | Instructions & TDD | 30 min | [02-instructions-tdd.md](modules/part1/02-instructions-tdd.md) |
| 03 | Requirements to Code | 45 min | [03-requirements-to-code.md](modules/part1/03-requirements-to-code.md) |
| 04 | Generation & Refactoring | 45 min | [04-generation-refactoring.md](modules/part1/04-generation-refactoring.md) |
| 05 | Testing & Documentation | 15 min | [05-testing-documentation.md](modules/part1/05-testing-documentation.md) |
| 06 | Wrap-Up & Discussion | 15 min | [06-wrapup-discussion.md](modules/part1/06-wrapup-discussion.md) |

**Total:** ~3 hours

---

### Part 2: Advanced Copilot (3 hours)

| Module | Topic | Duration | File |
| -------- | ------- | ---------- | ------ |
| 00 | Welcome & Recap | 10 min | [00-welcome-recap.md](modules/part2/00-welcome-recap.md) |
| 01 | Interaction Models | 25 min | [01-interaction-models.md](modules/part2/01-interaction-models.md) |
| 02 | Skills & Customization | 30 min | [02-skills-customization.md](modules/part2/02-skills-customization.md) |
| 03 | Custom Agents Intro | 25 min | [03-custom-agents-intro.md](modules/part2/03-custom-agents-intro.md) |
| 04 | Workflow Agents | 30 min | [04-workflow-agents.md](modules/part2/04-workflow-agents.md) |
| 05 | Agent Design | 30 min | [05-agent-design.md](modules/part2/05-agent-design.md) |
| 06 | Capstone Lab | 35 min | [06-capstone-lab.md](modules/part2/06-capstone-lab.md) |
| 07 | Wrap-Up & Next Steps | 10 min | [07-wrapup-next-steps.md](modules/part2/07-wrapup-next-steps.md) |

**Total:** ~3 hours

---

## Quick Start

### Option 1: View in VS Code (Recommended)

1. **Install Marp Extension**
   - Open VS Code
   - Install [Marp for VS Code](https://marketplace.visualstudio.com/items?itemName=marp-team.marp-vscode)

2. **Preview a Module**
   - Open any `.md` file in `modules/` folder
   - Click "Preview Marp Slide Deck" icon (top right)
   - Or use command: `Ctrl+Shift+P` → "Marp: Open Preview to the Side"

3. **Navigate Between Modules**
   - Use navigation links at bottom of each module
   - Or open modules sequentially from file explorer

### Option 2: Export to PDF/HTML

```bash
# Export single module
npx @marp-team/marp-cli modules/part1/00-kickoff-and-setup.md --pdf

# Export all Part 1 modules as one PDF
npx @marp-team/marp-cli modules/part1/*.md --pdf --output part1-complete.pdf

# Export with presenter notes
npx @marp-team/marp-cli modules/part1/*.md --pdf --pdf-notes --output part1-with-notes.pdf

# Export to HTML (interactive)
npx @marp-team/marp-cli modules/part2/*.md --html --output part2-interactive.html
```

---

## Common Use Cases

### 1. Full Workshop

**Delivery:** Instructor-led, 2 x 3-hour sessions

```text
Day 1 Morning (Part 1):
  → modules/part1/00 through 06

Day 1 Afternoon (Part 2):
  → modules/part2/00 through 07
```

### 2. Self-Paced Learning

**Delivery:** Individual learners

```text
1. Start with Part 1, Module 00
2. Follow "Next Module" links at bottom of each deck
3. Complete lab exercises referenced in modules
4. Progress at your own pace
5. Jump to Part 2 when ready
```

---

## Technical Details

### Marp Frontmatter

Each module uses this Marp configuration:

```yaml
---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---
```

**To customize:**
- `theme`: Change to custom theme (see Marp docs)
- `paginate`: Add slide numbers
- `backgroundColor`: Set background color

### Export Commands Reference

```bash
# Single module to PDF
npx @marp-team/marp-cli modules/part1/00-kickoff-and-setup.md --pdf

# Single module to HTML
npx @marp-team/marp-cli modules/part1/00-kickoff-and-setup.md --html

# All Part 1 modules to single PDF
npx @marp-team/marp-cli modules/part1/*.md --pdf --output part1-fundamentals.pdf

# All Part 2 modules to single PDF with notes
npx @marp-team/marp-cli modules/part2/*.md --pdf --pdf-notes --output part2-advanced.pdf

# Custom selection (specific modules only)
npx @marp-team/marp-cli \
  modules/part1/02-instructions-tdd.md \
  modules/part1/03-requirements-to-code.md \
  modules/part2/02-skills-customization.md \
  --pdf --output custom-selection.pdf

# Export to PPT (requires conversion tool)
npx @marp-team/marp-cli modules/part1/*.md --pptx
```

### CI/CD Integration

```yaml
# Example GitHub Actions workflow
name: Export Presentations
on: [push]
jobs:
  export:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Export Part 1
        run: |
          npx @marp-team/marp-cli docs/presentations/modules/part1/*.md \
            --pdf --output part1.pdf
      - name: Upload artifact
        uses: actions/upload-artifact@v3
        with:
          name: presentations
          path: '*.pdf'
```

---

## Troubleshooting

### "Preview doesn't work"
- **Fix**: Install Marp extension, reload VS Code
- **Command**: `code --install-extension marp-team.marp-vscode`

### "Export command fails"
- **Fix**: Install Marp CLI globally
- **Command**: `npm install -g @marp-team/marp-cli`

### "Slides look different in preview vs export"
- **Fix**: Use same Marp version for consistency
- **Check versions**: `npx @marp-team/marp-cli --version`

### "Navigation links don't work in PDF"
- **Expected**: PDFs don't support cross-file links
- **Workaround**: Export all modules as single PDF

---

## Additional Resources

- **Marp Documentation**: <https://marp.app/>
- **VS Code Extension**: <https://marketplace.visualstudio.com/items?itemName=marp-team.marp-vscode>
- **Lab Guides**: [docs/labs/](../labs/)

---
