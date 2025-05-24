# <div align="center">🎮 AutoStroke</div>

<div align="center">
  <img src="https://img.shields.io/badge/platform-Windows-blue?style=for-the-badge&logo=windows" alt="Windows"/>
  <img src="https://img.shields.io/badge/.NET-6.0-512BD4?style=for-the-badge&logo=dotnet" alt=".NET 6.0"/>
  <img src="https://img.shields.io/badge/theme-Dracula-BD93F9?style=for-the-badge" alt="Dracula Theme"/>
</div>

<br>

<div align="center">
  <p><i><strong>AutoStroke is a Windows application that automatically presses a specified keyboard key at regular intervals for a set duration. It features a stylish Dracula dark theme with a modern UI design.</i></strong></p>
</div>

<hr>

## 📘 User Guide

### 🚀 Getting Started

AutoStroke is a standalone application that requires no installation:
1. Download the latest release
2. Extract the ZIP file if needed
3. Run `AutoStroke.exe`

### ⚙️ Settings

<table>
  <tr>
    <td><b>Key Selection</b></td>
    <td>Choose any key via the virtual keyboard interface</td>
  </tr>
  <tr>
    <td><b>Interval</b></td>
    <td>Set time between key presses (in seconds)</td>
  </tr>
  <tr>
    <td><b>Duration</b></td>
    <td>Set total running time (in minutes)</td>
  </tr>
  <tr>
    <td><b>Run in Tray</b></td>
    <td>Minimize to system tray while running</td>
  </tr>
  <tr>
    <td><b>Auto-quit</b></td>
    <td>Automatically close the application when complete</td>
  </tr>
</table>

### 🔍 System Tray Access

When minimized to the system tray:
- **Double-click** the tray icon to open the main window
- **Right-click** the tray icon for a menu with Open and Exit options

<hr>

## 👨‍💻 Developer Information

### 🛠️ Building the Application

- **Prerequisites**
    - Windows 10 or later
    - .NET 6.0 SDK

- **Build Options**

    - <details><summary><b>Quick Build (Using <code>build.bat</code>)</b></summary>

        - Ensure .NET 6.0 SDK is installed
        - Run `build.bat` from the command line
        - Follow the prompts to create distribution package

    </details>

    - <details><summary><b>Manual Build</b></summary>

        - Open a command prompt in the solution directory
        - Run: `dotnet publish -c Release`
        - The executable will be in `AutoStroke\bin\Release\net6.0-windows\win-x64\publish\`

    </details>

### 📦 Deployment

The application is published as a self-contained executable that:
- ✅ Requires no installation
- ✅ Runs without .NET runtime dependencies
- ✅ Consumes minimal system resources

### 🎨 Color Palette

<table>
  <thead>
    <tr>
      <th colspan="4" align="center"><b>Core Dracula Colors</b></th>
    </tr>
    <tr>
      <th>Color</th>
      <th>Hex</th>
      <th>Description</th>
      <th>Usage</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><img src="https://img.shields.io/badge/-%23282a36-282a36" alt="Background"/></td>
      <td><code>#282a36</code></td>
      <td>Background</td>
      <td>Main window background, UI elements</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%23f8f8f2-f8f8f2" alt="Foreground"/></td>
      <td><code>#f8f8f2</code></td>
      <td>Foreground</td>
      <td>Text, labels, default UI elements</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%2344475a-44475a" alt="Selection"/></td>
      <td><code>#44475a</code></td>
      <td>Selection</td>
      <td>Input controls, panels, keyboard keys</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%236272a4-6272a4" alt="Comment"/></td>
      <td><code>#6272a4</code></td>
      <td>Comment</td>
      <td>Borders, inactive elements</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%238be9fd-8be9fd" alt="Cyan"/></td>
      <td><code>#8be9fd</code></td>
      <td>Cyan</td>
      <td>Hover states, navigation controls</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%2350fa7b-50fa7b" alt="Green"/></td>
      <td><code>#50fa7b</code></td>
      <td>Green</td>
      <td>Success indicators, active status</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%23ffb86c-ffb86c" alt="Orange"/></td>
      <td><code>#ffb86c</code></td>
      <td>Orange</td>
      <td>Warnings, secondary actions</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%23ff79c6-ff79c6" alt="Pink"/></td>
      <td><code>#ff79c6</code></td>
      <td>Pink</td>
      <td>Start button, accent elements</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%23bd93f9-bd93f9" alt="Purple"/></td>
      <td><code>#bd93f9</code></td>
      <td>Purple</td>
      <td>Selected elements, primary buttons</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%23ff5555-ff5555" alt="Red"/></td>
      <td><code>#ff5555</code></td>
      <td>Red</td>
      <td>Stop button, close buttons, errors</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%23f1fa8c-f1fa8c" alt="Yellow"/></td>
      <td><code>#f1fa8c</code></td>
      <td>Yellow</td>
      <td>Highlights, important information</td>
    </tr>
  </tbody>
</table>

<table>
  <thead>
    <tr>
      <th colspan="4" align="center"><b>Additional UI Colors</b></th>
    </tr>
    <tr>
      <th>Color</th>
      <th>Hex</th>
      <th>Description</th>
      <th>Usage</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><img src="https://img.shields.io/badge/-%2344475a-44475a" alt="Card Background"/></td>
      <td><code>#44475a</code> with alpha</td>
      <td>Semi-transparent gray</td>
      <td>Card Background</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%236272a4-6272a4" alt="Card Hover"/></td>
      <td><code>#6272a4</code> with alpha</td>
      <td>Semi-transparent blue</td>
      <td>Card Hover</td>
    </tr>
    <tr>
      <td><img src="https://img.shields.io/badge/-%23000000-000000" alt="Shadow"/></td>
      <td><code>#000000</code> with alpha</td>
      <td>Translucent black</td>
      <td>UI Shadows</td>
    </tr>
  </tbody>
</table>

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.