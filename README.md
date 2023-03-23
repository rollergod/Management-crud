# Management

тестовое задание от компании "АСУ "МЕНЕДЖМЕНТ""

## Структура папок

Provide an overview of the directory structure and files, for example:
```bash
├───client
│   ├───public
│   └───src
│       ├───api
│       ├───assets
│       ├───components
│       └───pages
├───Management.Api
│   ├───Controllers
│   ├───Extensions
│   ├───obj
│   └───Properties
├───Management.Application
│   ├───Common
│   │   └───Interfaces
│   │       ├───Repositories
│   │       └───Services
│   ├───Configs
│   └───Shared
│       ├───Dto
│       ├───Errors
│       │   └───Exceptions
│       │       └───Abstractions
│       └───RequestFeatures
├───Management.Domain
│   ├───Entities
├───Management.Infrastructure
│   ├───Logger
│   ├───Persistance
│   │   ├───Migrations
│   │   └───Queries
│   ├───Repository
│   └───Services
```
## Запуск приложения

Coming soon...

## О приложении

CRUD приложение. Фронтенд - react vite, Бэкекнд - .Net 6
Апи имеет 3 endpoint`а

![image](https://user-images.githubusercontent.com/91565374/225708103-4d2c3e4d-b5e5-4728-878c-50486ad74e2b.png)

* *get* **/api/game/turn** - возвращает ход игрока - X или O.
* *post* **/api/game** - принимает два параметра - координата по оси x и коордитана по оси y. Два раза одни и те же координаты указывать нельзя.
* *post* **/api/game/new-game** - создает новую игру

## Пример ввода координат

**Request:**
```json
POST /game HTTP/1.1
Accept: application/json
Content-Type: application/json
Content-Length: xy

{
    "xCord": "1",
    "yCord": "1" 
}
```
**Successful Response:**
```json
HTTP/1.1 200 OK
Server: TicTacToe
Content-Type: application/json
Content-Length: xy

{
   "Ход сделан"
}

```
**Failed Response:**
```json
HTTP/1.1 404 BadRequest
Server: TicTacToe
Content-Type: application/json
Content-Length: xy

{
    "code": 404,
    "message": "bad request",
    "resolve": "Координата с позицией x и y занята"
}

HTTP/1.1 404 BadRequest
Server: TicTacToe
Content-Type: application/json
Content-Length: xy

{
    "code": 404,
    "message": "bad request",
    "resolve": "Координаты выходят за пределы таблицы"
}

``` 

