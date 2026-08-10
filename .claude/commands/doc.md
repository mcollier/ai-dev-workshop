---
description: Add or update XML documentation comments for a class, method, or file, following this repo's documentation standards.
argument-hint: [file path, class/method name, or paste code]
---

Add XML documentation comments for: $ARGUMENTS

Follow the documentation conventions in `CLAUDE.md`:

- Document all public types and members: `<summary>`, `<param>` for each
  parameter, `<returns>` where applicable, and `<exception>` for any
  exception the caller should expect (e.g. guard-clause validation
  failures).
- Keep summaries concise and focused on *why*/*intent*, not a restatement
  of the method name — well-named identifiers already say *what*.
- Don't document private/internal members unless the logic is genuinely
  non-obvious.
- Preserve existing code exactly; only add or update the doc comments.
- If the type is a DDD aggregate, entity, or value object, mention any
  invariant the member enforces (e.g. "Throws if title is empty").
