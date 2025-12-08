# Проверка работы Seq

## Шаг 1: Убедитесь, что Seq запущен

1. Откройте браузер и перейдите на **http://localhost:5341**
2. Если Seq не запущен, вы увидите ошибку подключения

### Запуск Seq через Docker:
```bash
docker run -d --name seq -p 5341:80 -p 5342:5341 -e ACCEPT_EULA=Y datalust/seq:latest
```

### Или через установщик Windows:
Скачайте с https://datalust.co/download

## Шаг 2: Проверьте подключение приложения

1. Запустите приложение
2. В консоли вы должны увидеть:
   ```
   === BiznusWeb Application Starting ===
   Seq URL: http://localhost:5341
   Environment: Development
   Application started successfully
   ```

## Шаг 3: Отправьте тестовые логи

1. Откройте в браузере: **http://localhost:5096/test-logging**
2. Это отправит тестовые логи всех уровней в Seq
3. Проверьте в Seq (http://localhost:5341) - должны появиться логи

## Шаг 4: Проверьте логи в Seq

1. Откройте http://localhost:5341
2. В поиске введите: `Application = "BiznusWeb"`
3. Должны отобразиться все логи приложения

## Что логируется:

- ✅ Запуск приложения
- ✅ Все HTTP запросы (через RequestLoggingMiddleware)
- ✅ API запросы (через ApiLoggingFilter)
- ✅ Ошибки (через GlobalExceptionHandlerMiddleware)
- ✅ Логи из контроллеров (HomeController, ContactController, и т.д.)

## Если логи не появляются:

1. **Проверьте, что Seq запущен** - откройте http://localhost:5341
2. **Проверьте консоль приложения** - должны быть сообщения о подключении
3. **Проверьте файрвол** - порт 5341 должен быть открыт
4. **Проверьте URL в appsettings.json** - должен быть `http://localhost:5341`
5. **Проверьте уровень логирования** - в appsettings.json установлен `Information`

## Тестовые запросы для проверки:

- `http://localhost:5096/` - главная страница (логируется)
- `http://localhost:5096/test-logging` - тестовые логи
- `http://localhost:5096/api/products` - API запрос (логируется)
- `http://localhost:5096/Contact` - контактная форма (логируется)

