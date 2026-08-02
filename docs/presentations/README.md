# Workshop Presentations

This directory contains modular Marp presentations for the workshop.

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

## Module Catalog

See [index.md](index.md) for complete catalog of available modules.

**Quick Links:**
- [Part 1: Fundamentals](modules/part1/) (7 modules)
- [Part 2: Advanced](modules/part2/) (8 modules)

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
  modules/part1/02-copilot-instructions-tdd.md \
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
- **Facilitator Guides**: [docs/FACILITATOR_GUIDE.md](../FACILITATOR_GUIDE.md)
- **Workshop Index**: [index.md](index.md)

---
