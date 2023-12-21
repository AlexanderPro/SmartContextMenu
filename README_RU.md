![SmartContextMenuLogo128](https://github.com/AlexanderPro/SmartContextMenu/assets/8102586/e1aaac4b-483a-41ec-9dac-b881cd14ecfa) SmartContextMenu
=============

- [English](/)
- Русский

---

SmartContextMenu добавляет контекстное меню для всех окон в Вашей ОС.
Эта программа является улучшенной версией [SmartSystemMenu](https://github.com/AlexanderPro/SmartSystemMenu).
Надеюсь она будет более удобной в использовании, т.к. поддерживает работу со всеми окнами, включая и окна без системного меню.
Так же она должна быть более стабильной и легковесной, т.к. не использует хуки в отдельных dll модулях.
Для использования приложения необходимо запустить файл SmartContextMenu.exe, навести указатель мыши на нужное окно и воспользоваться комбинацией клавиш Ctrl + правая кнопка мыши.
Все настройки меню и быстрые клавиши, можно менять в интерфейсе приложения в системном трее, а так же в файле SmartContextMenu.xml. Доступные пункты меню:

* **Информация.** Отображает диалог с детальной информацией об окне и процессе, которому принадлежит окно.
* **Свернуть вверх.** Позволяет свернуть вверх до заголовка окно и обратно.
* **Aero Glass.** Включает для текущего окна режим "Aero Glass". (Поддерживается в Windows Vista and выше. Удобно по большей части для консольных окон.)
* **Поверх остальных.** Отображает окно поверх остальных окон системы.
* **Переместить вниз.** Отображает окно за всеми окнами системы.
* **Сохранить снимок окна.** Позволяет сохранить скриншот окна в файл.
* **Открыть в проводнике.** Открывает File Explorer с выделенным файлом процесса, которому принадлежит окно.
* **Клик сквозь окно.** Позволяет выполнить клик сквозь окно.
* **Скрыть для Alt+Tab.** Позволяет скрыть окно на панели задач и при переключении Alt+Tab.
* **Изменить размер.** Позволяет изменить размер окна.
* **Переместить.** Позволяет перенести окно на другой монитор.
* **Выравнивание.** Позволяет выравнить окно в одной из 9 позиций.
* **Прозрачность.** Позволяет изменить прозрачность окна.
* **Приоритет.** Позволяет изменить приоритет процесса, которому принадлежит окно.
* **Буфер обмена.** Позволяет скопировать текст окна в буфер обмена или очистить буфер обмена.
* **Кнопки.** Позволяет отключить кнопки минимизации, максимизации и закрытия окна.
* **Системный трей.** Сворачивает окно в системный трей.
* **Системное меню.** Содержит элементы системного меню.
* **Другие окна.** Позволяет закрыть или минимизировать все окна системы кроме текущего.
* **Пуск.** Запускает любой процесс заданный через настройки программы.

Скриншоты
------------------

![alt tag](https://github.com/AlexanderPro/SmartContextMenu/assets/8102586/b1fa5cf4-fcfb-46e0-adcf-477405704c72)
![alt tag](https://github.com/AlexanderPro/SmartContextMenu/assets/8102586/e0cf1d93-e85a-47f9-ba6a-35a6cc09a336)
![alt tag](https://github.com/AlexanderPro/SmartContextMenu/assets/8102586/0113aff7-b518-4418-90dc-6afbab8f529d)

Интерфейс командной строки
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
   --transparency     [0 ... 100]
   --alwaysontop      [on, off]
-g --aeroglass        [on, off]
   --hidealttab       [on, off]
   --clickthrough     [on, off]
   --minimizebutton   [on, off]
   --maximizebutton   [on, off]
   --sendtobottom     Send To Bottom
-o --openinexplorer   Open File In Explorer
-c --copytoclipboard  Copy Window Text To Clipboard
   --copyscreenshot   Copy Screenshot To Clipboard
   --clearclipboard   Clear Clipboard
-n --nogui            No GUI

Example:
SmartContextMenu.exe --title "Untitled - Notepad" -a topleft -p high --alwaysontop on --nogui
```

Установка
--------------------

* Скачайте последнюю версию [SmartContextMenu](https://github.com/AlexanderPro/SmartContextMenu/releases) в zip файле

Требования к системе
--------------------

* ОС Windows XP SP3 и выше. Поддержка x86 и x64 систем.
* .NET Framework 4.0 и выше.

Файлы
--------------------

* SmartContextMenu.exe
* SmartContextMenu.xml (Располагается в каталоге профиля пользователя. Если планируете использовать приложение как портативное, то скопируйте этот файл в каталог где лежит SmartContextMenu.exe)