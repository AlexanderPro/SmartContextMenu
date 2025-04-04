<div align="center">

<img src="./SmartContextMenu/Images/SmartContextMenuLogo.png" alt="logo" width="128">

# SmartContextMenu

</div>

🌏: [**English**](/) [Русский](/README_RU.md) [中文版](/README_CN.md)

---

SmartContextMenu adds a context menu to all windows in the system. 
This program is enhanced version of [SmartSystemMenu](https://github.com/AlexanderPro/SmartSystemMenu).
I hope it will be more convenient, because it supports all types of windows, including windows without a system menu.
It should also be more stable and lightweight, as it does not use hooks in separate dll modules.
To use the app, please, run the file SmartContextMenu.exe, move the mouse cursor to the necessary window and use the hotkeys “**Ctrl + Right Mouse Button**”.
All the menu settings and hotkeys can be changed in the settings dialog of the system tray, as well as in the file SmartContextMenu.xml.
Available menu items:

* **Information.** Shows a dialog with information of the current window and process: the window handle, the window caption, the window style, the window class, the process name, the process id, the path to the process.
* **Aero Glass.** Allows to add the "Aero Glass" blur to the current window. (Windows Vista and higher. Mostly for console windows.)
* **Always On Top.** Allows the current window to stay on top of all other windows.
* **Borderless.** Allows you to turn your windowed games into "Borderless" mode.
* **Dimmer.** Dims all but the currently focused window.
* **Hide.** Allows to hide the current window.
* **Roll Up.** Allows to roll up and down the current window.
* **Send To Bottom.** Allows to send to bottom the current window.
* **Change Icon.** Allows to change the icon of the current window.
* **Change Title.** Allows to change the text in the title bar.
* **Save Screenshot.** Allows to save the current window screenshot in a file.
* **Open File In Explorer.** Allows to open a process file in a File Explorer.
* **Click Through.** Allows to click through the current window.
* **Hide For Alt+Tab.** Allows to hide the current window for the Taskbar and Alt+Tab switch.
* **Resize.** Allows to change the size of the current window.
* **Move To.** Allows to move the current window to another monitor.
* **Alignment.** Allows the current window to be aligned with any of the 9 positions on the desktop.
* **Transparency.** Allows to change the transparency of the current window.
* **Priority.** Allows to change the current window's program priority.
* **Clipboard.** Allows to copy all window texts (including console, ms office products, etc.) to clipboard and clear clipboard.
* **Buttons.** Allows to disable "Minimize", "Maximize" and "Close" button.
* **System Tray.** Allows to minimize or suspend the current window to the system tray.
* **System Menu.** Contains System Menu items.
* **Other Windows.** Allows to close and minimize all windows in the system except the current.
* **Start Program.** Allows to start programs which is in the settings.

Screenshots
------------------

![Resize](./SmartContextMenu/Images/SmartContextMenuEn1.png)
![Alignment](./SmartContextMenu/Images/SmartContextMenuEn2.png)
![System Menu](./SmartContextMenu/Images/SmartContextMenuEn3.png)

Command Line Interface
--------------------

```bash
   --help             The help
   --title            Title
   --titleBegins      Title begins 
   --titleEnds        Title ends
   --titleContains    Title contains
   --handle           Handle (1234567890) (0xFFFFFF)
   --processId        PID (1234567890)
-d --delay            Delay in milliseconds
-l --left             Left
-t --top              Top
-w --width            Width
-h --height           Height
-i --information      Information dialog
-s --savescreenshot   Save Screenshot
-m --monitor          [0, 1, 2, 3, ...]
-a --alignment        [topleft,
                       topcenter,
                       topright,
                       middleleft,
                       middlecenter,
                       middleright,
                       bottomleft,
                       bottomcenter,
                       bottomright,
                       centerhorizontally,
                       centervertically]
-p --priority         [realtime,
                       high,
                       abovenormal,
                       normal,
                       belownormal,
                       idle]
   --systemmenu       [restore,
                       minimize,
                       maximize,
                       close]
   --transparency     [0 ... 100]
   --alwaysontop      [on, off]
-g --aeroglass        [on, off]
   --hide             [on, off]
   --hidealttab       [on, off]
   --clickthrough     [on, off]
   --minimizebutton   [on, off]
   --maximizebutton   [on, off]
   --sendtobottom     Send To Bottom
-b --borderless       Borderless
-r --rollup           Roll Up
-o --openinexplorer   Open File In Explorer
-c --copytoclipboard  Copy Window Text To Clipboard
   --copyscreenshot   Copy Screenshot To Clipboard
   --clearclipboard   Clear Clipboard
-n --nogui            No GUI

Example:
SmartContextMenu.exe --title "Untitled - Notepad" -a topleft -p high --alwaysontop on --nogui
```

Install
--------------------

* Download the [SmartContextMenu](https://github.com/AlexanderPro/SmartContextMenu/releases) in the zip file
* [Chocolatey](https://chocolatey.org/): `choco install smartcontextmenu`
* [Scoop](https://scoop.sh/): `scoop bucket add extras` and `scoop install extras/smartcontextmenu`

Requirements
--------------------

* OS Windows XP SP3 and later. Supports x86 and x64 systems.
* .NET Framework 4.0

Files
--------------------

* SmartContextMenu.exe
* SmartContextMenu.xml (It is placed in roaming user directory. If you are planing to use SmartContextMenu as a portable app, then copy the file to the directory with SmartContextMenu.exe)