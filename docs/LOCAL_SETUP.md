# Local Setup Guide (No Dev Container)

Use this guide if you **cannot run Dev Containers** (Docker not available, corporate policy, resource constraints, etc.).

---

## 📋 Required

You will need Visual Studio Code, a GitHub account and either GitHub Copilot or Claude Code.

### 1. GitHub Account & Copilot Subscription

- [ ] **GitHub account** — [github.com](https://github.com)
- [ ] **GitHub Copilot subscription active**
  - Individual: $10/month or $100/year
  - Business: Assigned by your organization admin
- [ ] **Verify subscription** at [github.com/settings/copilot](https://github.com/settings/copilot) — should show "GitHub Copilot is active"

---

### 2. Claude Code (optional if using GitHub Copilot)

- [ ] **Claude subscription** - [Claude subscription](https://claude.com/product/claude-code)
- [ ] A Pro, Max, Team, or Enterprise account

---

### 3. Git

- [ ] **Install Git**
  - **macOS**: `xcode-select --install` or via [Homebrew](https://brew.sh/): `brew install git`
  - **Windows**: [git-scm.com/download/win](https://git-scm.com/download/win)
  - **Linux**: `sudo apt install git` / `sudo dnf install git`
- [ ] **Verify**: `git --version` → `git version 2.30` or later
- [ ] **Configure identity**:
  ```bash
  git config --global user.name "Your Name"
  git config --global user.email "your.email@example.com"
  ```

---

### 4. Visual Studio Code

- [ ] **Install VS Code** (version 1.98 or later) — [code.visualstudio.com](https://code.visualstudio.com/)
- [ ] **Verify**: `code --version`
- [ ] **macOS extra step**: Open Command Palette (`Cmd+Shift+P`) → "Shell Command: Install 'code' command in PATH"

---

### 5. VS Code Extensions

| Extension               | ID                           | Purpose                                  |
| ----------------------- | ---------------------------- | ---------------------------------------- |
| GitHub Copilot          | `GitHub.copilot`             | Inline AI completions                    |
| GitHub Copilot Chat     | `GitHub.copilot-chat`        | Chat interface, agents, instructions     |
| Claude Code for VS Code | `anthropic.claude-code`      | Provide Claude Code interface in VS Code |
| REST Client             | `humao.rest-client`          | Test HTTP endpoints from `.http` files   |
| Markdown Mermaid        | `bierner.markdown-mermaid`   | Diagram previews in Markdown             |
| Marp for VS Code        | `marp-team.marp-vscode`      | Workshop slide previews                  |
| Markdown All in One     | `yzhang.markdown-all-in-one` | Markdown editing helpers                 |

**Install via command line** (copy/paste all at once):
```bash
code --install-extension GitHub.copilot
code --install-extension GitHub.copilot-chat
code --install-extension anthropic.claude-code
code --install-extension humao.rest-client
code --install-extension bierner.markdown-mermaid
code --install-extension marp-team.marp-vscode
code --install-extension yzhang.markdown-all-in-one
```

After installing, sign in to GitHub when VS Code prompts you to activate Copilot.

---

### 6. Clone the Repository

```bash
git clone https://github.com/mcollier/ai-dev-workshop.git
cd ai-dev-workshop
```

Create your personal branch:
```bash
git checkout main
git pull
git checkout -b your-name-workshop
```

Open in VS Code:
```bash
code .
```

---

## .NET 10 SDK

- [ ] **Download .NET 10 SDK** (not just the Runtime) — [dotnet.microsoft.com/download/dotnet/10.0](https://dotnet.microsoft.com/download/dotnet/10.0)
- [ ] **Verify**:
  ```bash
  dotnet --version
  ```
  Expected: `10.0.x`

**Common issues:**
- `command not found` → Restart terminal or reboot after install
- Old version showing → Restart terminal; the installer updates PATH

### .NET VS Code Extensions

| Extension  | ID                        | Purpose                                   |
| ---------- | ------------------------- | ----------------------------------------- |
| C# Dev Kit | `ms-dotnettools.csdevkit` | C# IntelliSense, debugging, test explorer |

```bash
code --install-extension ms-dotnettools.csdevkit
```

> C# Dev Kit automatically pulls in the base **C#** extension (`ms-dotnettools.csharp`) as a dependency.

### Verify the .NET Build

```bash
# From the repository root
dotnet restore
dotnet build TaskManager.slnx
dotnet test
```

Expected build output:
```text
Build succeeded.
  TaskManager.Domain succeeded
  TaskManager.Application succeeded
  TaskManager.Infrastructure succeeded
  TaskManager.Api succeeded
  TaskManager.UnitTests succeeded
  TaskManager.IntegrationTests succeeded
```

Expected test output:
```text
Test summary: total: 11, failed: 11, succeeded: 0
```

> ✅ **11 failing tests is correct!** These are placeholders you will implement during the workshop.

### .NET HTTPS Dev Certificate

```bash
dotnet dev-certs https --trust
```

Accept any OS prompt to trust the certificate.

---

## ✅ Verify GitHub Copilot Is Working

After setup, confirm Copilot is active:

1. Open a source file in VS Code:
   - **.NET**: `src/TaskManager.Domain/Tasks/Task.cs`
2. Check the **status bar** (bottom-right of VS Code window) — the Copilot icon should be active (not red/crossed out)
3. Add a new line and type a comment, e.g., `// Method to validate task title`
4. Press Enter — you should see gray "ghost text" suggestions
5. Press **Tab** to accept, **Esc** to dismiss
6. Delete the test line

**Copilot Chat test:**
1. Open Copilot Chat: `Cmd/Ctrl+Shift+I` (or click the chat icon in the sidebar)
2. Type: `What testing frameworks are used in this project?`
3. You should get a relevant response — ✅ Copilot is working!

**Not working?**
- Click the Copilot status bar icon → "Sign in to GitHub"
- Check your subscription: [github.com/settings/copilot](https://github.com/settings/copilot)
- Reload VS Code: Command Palette → "Developer: Reload Window"

---

## 🆘 Troubleshooting

| Problem                           | Solution                                                  |
| --------------------------------- | --------------------------------------------------------- |
| `dotnet: command not found`       | Restart terminal after install; check PATH                |
| Copilot suggestions not appearing | Wait 1-2 seconds; check status bar icon; reload window    |
| Build fails with "SDK not found"  | Confirm SDK version with `dotnet --list-sdks`             |
| Extensions not loading            | Ensure VS Code 1.95+; reload window; reinstall extensions |

---

## 📋 Quick-Check Before the Workshop

Run these commands the morning of the workshop to confirm everything is still set up:

**For .NET participants:**
```bash
dotnet --version        # 10.0.x
cd ai-dev-workshop
git pull origin main
dotnet build TaskManager.slnx
```

Then open VS Code (`code .`) and confirm the Copilot status bar icon is active.

---

## 📚 Related Documentation

- [Workshop README](../README.md) — overview and lab links

---

**Still stuck?** Arrive 15 minutes early on workshop day — facilitators will help. As a last resort, [GitHub Codespaces](https://github.com/features/codespaces) provides a browser-based VS Code environment that requires no local setup.