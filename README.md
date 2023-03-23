# Management

Тестовое задание от компании "АСУ "МЕНЕДЖМЕНТ""

## Структура папок

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

В приложении присутствует docker-compose файл. В нем настроена конфигурация запуска бэкенда и фронтенда.
Не забудьте поменять ConnectionString в классе Management.Infrastructure/DependencyInjection

Также необходимо будет поменять URL бэкенда во фронтенд приложении.

## О приложении

CRUD приложение. Фронтенд - react vite, Бэкекнд - .Net 6

API имеет 10 endpoint`ов.

![image](https://user-images.githubusercontent.com/91565374/227377931-ffb2cfc4-d88e-440b-9c6f-e55ecb3f9113.png)

## Пример отправки запроса для создания Order

**Request:**
```json
POST /order HTTP/1.1
Accept: application/json
Content-Type: application/json
Content-Length: xy

{
  "number": "Test",
  "date": "01/01/2022",
  "providerId": 1,
  "items": [
    {
      "name": "TestOrderItem",
      "quantity": 1,
      "unit": "TestOrderUnit"
    }
  ]
}
```
**Successful Response:**
```json
HTTP/1.1 200 OK
Server: Management
Content-Type: application/json
Content-Length: xy

{
  "number": "Test",
  "date": "2022-01-01",
  "providerId": 1,
  "items": [
    {
      "name": "TestOrderItem",
      "quantity": 1,
      "unit": "TestOrderUnit"
    }
  ]
}
```
**Failed Response:**
```json
HTTP/1.1 404 BadRequest
Server: Management
Content-Type: application/json
Content-Length: xy

{
    "code": 404,
    "message": "bad request",
}

``` 

