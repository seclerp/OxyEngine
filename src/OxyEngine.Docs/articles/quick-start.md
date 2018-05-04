# TODO: Rewrite

В данном уроке будет показано как можно легко начать разработку игры используя OxyEngine с нуля.

Этот урок будет полезен в первую очередь разработчикам, использующим Windows и Visual Studio как средства разработки.
Однако с минимальными правками данный урок можно адаптировать и под другие операционные системы.

**[Исходный код урока](https://github.com/OxyTeam/WIki/raw/master/Tutorials/quick-start-for-building-prototypes/Sources)**

## Создание нового проекта

Если вы установили MonoGame for Visual Studio, то у вас присутствуют все необходимые шаблоны проектов.

Создайте новый проект MonoGame. На данный момент поддерживается только MonoGame. 

### Visual Studio без готовых шаблонов проектов

1. Создайте проект Console Application.
2. Добавьте NuGet пакет MonoGame.Framework.DesktopGL.
3. Добавьте NuGet пакет OxyEngine.Desktop.

После этого у вас будет готовый к использованию проект.

## Запуск проекта

### Windows

Просто соберите проект и запустите его, используя средства вашего IDE.

### Mac OS или Linux

Если ваша IDE не поддерживает функционал запуска Mono Runtime из-под редактора, создайте Shell-скрипт, который будет запускать ваш проект для отладки на Mono.
Пример такого скрипта:
```
#!/bin/sh
# Replace GAME_NAME with your .exe filename
/usr/bin/mono GAME_NAME.exe "$@"
```

Убедитесь что GAME_NAME.sh является исполняемым (если нет, выполните `chmod +x GAME_NAME.sh`), после чего запустите его.

`/path/to/GAME_NAME.sh`

---

Если вы все сделали правильно то увидите примерно такое окно:

![Окно с запущеным пустым проектом](https://github.com/OxyTeam/Wiki/raw/master/Tutorials/oxyplayground-quick-start-for-building-prototypes/Screenshots/1.png)

Если все же ничего не произошло, попробуйте запустить из-под Mono в режиме отладки:

`mono --debug /path/to/OxyPlayground.exe "path/to/project/folder"`

Чтобы увидеть текст ошибки.

## Hello, World!

Настало время написать простейшую игру, где картинка будет перемещаться по экрану стрелками на клавиатуре. Начнем с добавления картинки.

### Загрузка контента

OxyEngine базируется на MonoGame и использует формат ресурсов MonoGame - `.xnb`. Для того, чтобы удобно "собирать" игровые ресурсы можно воспользоваться MonoGame Content Pipeline GUI. 

Запустите "MonoGame Pipeline" приложение используя поиск в меню Пуск.

По умолчанию он расположен по пути `C:\Program Files (x86)\MSBuild\MonoGame\v3.0\Tools\Pipeline.exe`

#### Mac OS и Linux

Выполните команду `mono /path/to/Pipeline.exe`

---

Если никаких ошибок не последовало, вы увидите следующее:

![MonoGame Pipeline](https://github.com/OxyTeam/WIki/raw/master/Tutorials/quick-start-for-building-prototypes/Screenshots/2.png)

Поместите любую понравившуюся вам картинку в папку Content. Структура проекта после этого должна выглядеть примерно так:
```
Project
  Content
    Content.mgcb
    image.png
  Game1.cs
  Project.csproj
  ...
```

Теперь нужно добавить эту картинку в MonoGame Pipeline. Для этого нажмите правой кнопкой по Content в левой части окна и выберете "Add/Existing item..." и укажите вашу картинку. В итоге окно будет выглядеть следующим образом:

![MonoGame Pipeline](https://github.com/OxyTeam/WIki/raw/master/Tutorials/quick-start-for-building-prototypes/Screenshots/3.png)

При сборке вашего проекта, если никаких ошибок в .mgcb файле нет, то в папке с бинарными файлами будет создана папка Content со скомпилированным контентом.

Это все необходимые приготовления для того, чтобы написать первый проект на OxyPlayground.

### Пишем код

Откройте любым удобным редактором файл `entry.py` и введите туда следующий код: (мы разберем его ниже)

```python
from OxyEngine import *

content = {}


def onLoad(sender, args):
    content['image'] = Oxy.Resources.LoadTexture('Content/image')


def onDraw(sender, args):
    Oxy.Graphics.Draw(content['image'], 50, 50)


Oxy.Events.Global.StartListening("load", onLoad)
Oxy.Events.Global.StartListening("draw", onDraw)
```

Сохраните и запустите проект. Если вы все сделали правильно, то вы увидите следующее:

![Hello, World!](https://github.com/OxyTeam/WIki/raw/master/Tutorials/quick-start-for-building-prototypes/Screenshots/4.png)

Отлично! Теперь мы видим картинку, которая расположена в координатах 50 50 относительно левого верхнего угла окна. Теперь давайте разберем код:

```python
from OxyEngine import *
``` 
Загружаем все необходимые модули из OxyEngine. Эта строка необходима в любом Python файле, где вы используете функционал движка.

```python
content = {}
``` 
Просто для удобства: используем словарь для хранения ресурсов, который в последствие наполняем.

```python
def onLoad(sender, args):
    content['image'] = Oxy.Resources.LoadTexture('Content/image')
```
Весь движок OxyEngine построен на принципе событий. Происходит событие -> вызываются слушатели. Подробнее об паттерне Publisher-Subscriber можно почитать **[тут](https://en.wikipedia.org/wiki/Publish%E2%80%93subscribe_pattern)**. 

В данном примере мы создаем функцию onLoad, которую в дальнейшем привяжем к событию "load", которое происходит когда игра загружается. Тут мы загружаем нашу картинку используя API OxyEngine. `Oxy.Resources.LoadTexture` принимает в качестве аргумента строку, которая описывает путь к файлу с текстурой (в нашем случае `image.xnb`). **Расширение указывать не нужно**. sender - это обьект, который начал событие, а args - это обьект, содержащий аргументы. В данном примере мы их игнорируем, так как они не несут ничего полезного.

```python
def onDraw(sender, args):
    Oxy.Graphics.Draw(content['image'], 50, 50)
```
Тут мы создаем другой слушатель событий, на этот раз события "draw". Оно отвечает за любую отрисовку на экран, тут необходимо помещать код рисования, что мы и сделали. Первый параметр `Oxy.Graphics.Draw` отвечает за текстуру, которую мы рисуем, второй и третий - за позицию на экране.

```python
Oxy.Events.Global.StartListening("load", onLoad)
Oxy.Events.Global.StartListening("draw", onDraw)
```
Ключевой момент - тут происходит связывание слушателей с событиями. "load" -> onLoad, "draw" -> onDraw.

### Добавим интерактивности

Сейчас мы просто видим картинку, которая ничего не делает. Давайте добавим возможность перемещения по экрану. Для этого изменим наш код следующим образом:

```python
from OxyEngine import *

content = {}
properties = {}


def onLoad(sender, args):
    content['image'] = Oxy.Resources.LoadTexture('Content/image')
    properties['x'] = 50
    properties['y'] = 50
    properties['speed'] = 150 # pixels per second


def onUpdate(sender, args):
    if Oxy.Input.IsKeyPressed('right'):
        properties['x'] += properties['speed'] * args.DeltaTime
    if Oxy.Input.IsKeyPressed('left'):
        properties['x'] -= properties['speed'] * args.DeltaTime
    if Oxy.Input.IsKeyPressed('down'):
        properties['y'] += properties['speed'] * args.DeltaTime
    if Oxy.Input.IsKeyPressed('up'):
        properties['y'] -= properties['speed'] * args.DeltaTime


def onDraw(sender, args):
    Oxy.Graphics.Draw(content['image'], properties['x'], properties['y'])


Oxy.Events.Global.StartListening("load", onLoad)
Oxy.Events.Global.StartListening("update", onUpdate)
Oxy.Events.Global.StartListening("draw", onDraw)
```

Разберем новый код:
```python
properties = {}
```
Тут мы будем хранить разного рода переменные, которые нам нужны.

```python
properties['x'] = 50
properties['y'] = 50
properties['speed'] = 150 # pixels per second
```
Установим координаты картинки и скорость.

```python
def onUpdate(sender, args):
    if Oxy.Input.IsKeyPressed('right'):
        properties['x'] += properties['speed'] * args.DeltaTime
    if Oxy.Input.IsKeyPressed('left'):
        properties['x'] -= properties['speed'] * args.DeltaTime
    if Oxy.Input.IsKeyPressed('down'):
        properties['y'] += properties['speed'] * args.DeltaTime
    if Oxy.Input.IsKeyPressed('up'):
        properties['y'] -= properties['speed'] * args.DeltaTime
```
Новый слушатель событий. Событие "update" вызывается перед "draw", служит для основной игровой логики. В нашем случае мы с помощью `Oxy.Input.IsKeyPressed` проверяем нажата ли определенная кнопка клавиатуры (в данном случае стрелки up, right, down, left). Мы специально умножаем на `args.DeltaTime`, это число секунд, которое прошло с прошлого вызова "update". Умножив мы сделаем так, что speed пикселей будет пройдено ровно за 1 секунду (s = v * t из физики). Это также даст полезный эффект, при котором у людей с разным FPS скорость движения будет расчитываться исходя из этого же FPS.

```python
Oxy.Events.Global.StartListening("update", onUpdate)
```
Ну и наконец добавим слушатель "update" событий.

---

Многим этот урок покажется излишне сложным, ведь мы производили начальную настройку всего проекта. В будущем вместе с OxyPlayground будет поставлятся специальная утилита для генерации проектов, а пока мы будем основываться в дальнейших статьях именно на это шаблоне. Исходный код урока вы можете найти **[тут](https://github.com/OxyTeam/WIki/raw/master/Tutorials/quick-start-for-building-prototypes/Sources)**.


Успехов!