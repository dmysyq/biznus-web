# Настройка логирования через Seq

## Установка Seq

Seq можно установить несколькими способами:

### 1. Docker (рекомендуется)
```bash
docker run -d --name seq -p 5341:80 -p 5342:5341 -e ACCEPT_EULA=Y datalust/seq:latest
```

### 2. Windows
Скачайте установщик с https://datalust.co/download

### 3. Linux
```bash
sudo apt-get update
sudo apt-get install seq
```

## Настройка в приложении

Логирование уже настроено в `appsettings.json`:

```json
{
  "Serilog": {
    "WriteTo": [
      {
        "Name": "Seq",
        "Args": {
          "serverUrl": "http://localhost:5341"
        }
      }
    ]
  }
}
```

## Переменная окружения

Можно переопределить URL Seq через переменную окружения:
```bash
export SEQ_URL=http://your-seq-server:5341
```

## Просмотр логов

1. Откройте браузер и перейдите на http://localhost:5341
2. Логи будут автоматически отправляться в Seq
3. Можно фильтровать, искать и анализировать логи

## Что логируется

- Все запросы к API (через `RequestLoggingMiddleware`)
- Действия API контроллеров (через `ApiLoggingFilter`)
- Ошибки и исключения (через `GlobalExceptionHandlerMiddleware`)
- Стандартные логи ASP.NET Core
- Пользовательские логи из контроллеров

## Примеры запросов в Seq

- `Application = "BiznusWeb"` - все логи приложения
- `Level = "Error"` - только ошибки
- `RequestPath = "/api/*"` - только API запросы
- `UserEmail = "test@example.com"` - логи конкретного пользователя

