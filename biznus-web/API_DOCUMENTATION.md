# Web API Documentation

## Базовый URL
```
/api
```

## Аутентификация

### POST /api/ApiAuth/login
Вход в систему и получение JWT токена.

**Request Body:**
```json
{
  "email": "test@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "guid",
  "expires": "2025-12-09T20:00:00Z",
  "user": {
    "id": "user-id",
    "email": "test@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "fullName": "John Doe"
  }
}
```

### POST /api/ApiAuth/register
Регистрация нового пользователя.

**Request Body:**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "test@example.com",
  "password": "password123"
}
```

### GET /api/ApiAuth/me
Получить информацию о текущем пользователе (требует авторизации).

**Headers:**
```
Authorization: Bearer {token}
```

---

## Товары

### GET /api/ApiProducts
Получить список товаров.

**Query Parameters:**
- `category` (optional) - фильтр по категории
- `search` (optional) - поиск по названию/описанию
- `page` (optional, default: 1) - номер страницы
- `pageSize` (optional, default: 12) - размер страницы
- `culture` (optional) - язык для локализации (ru-RU, en-US, kk-KZ, fr-FR)

**Example:**
```
GET /api/ApiProducts?category=Tents&page=1&pageSize=12&culture=ru-RU
```

**Response:**
```json
{
  "products": [
    {
      "id": 1,
      "name": "Белая палатка",
      "description": "Надёжная палатка для ваших приключений на природе",
      "price": 200.00,
      "imageUrl": "~/images/...",
      "category": "Tents",
      "categoryId": 1,
      "isAvailable": true,
      "createdDate": "2025-12-08T20:00:00Z"
    }
  ],
  "totalCount": 6,
  "page": 1,
  "pageSize": 12,
  "totalPages": 1
}
```

### GET /api/ApiProducts/{id}
Получить товар по ID.

**Query Parameters:**
- `culture` (optional) - язык для локализации

### POST /api/ApiProducts
Создать новый товар (требует авторизации).

**Request Body:**
```json
{
  "name": "New Product",
  "description": "Product description",
  "price": 99.99,
  "imageUrl": "~/images/...",
  "category": "Tents",
  "categoryId": 1,
  "isAvailable": true
}
```

### PUT /api/ApiProducts/{id}
Обновить товар (требует авторизации).

### DELETE /api/ApiProducts/{id}
Удалить товар (требует авторизации).

---

## Переводы

### GET /api/ApiTranslations
Получить список переводов.

**Query Parameters:**
- `scope` (optional) - область (например, "Product")
- `culture` (optional) - язык
- `key` (optional) - поиск по ключу

### GET /api/ApiTranslations/{id}
Получить перевод по ID.

### POST /api/ApiTranslations
Создать новый перевод (требует авторизации).

**Request Body:**
```json
{
  "key": "Product_1_Name",
  "culture": "ru-RU",
  "value": "Белая палатка",
  "scope": "Product"
}
```

### PUT /api/ApiTranslations/{id}
Обновить перевод (требует авторизации).

### DELETE /api/ApiTranslations/{id}
Удалить перевод (требует авторизации).

---

## Корзина

Все методы корзины требуют авторизации.

### GET /api/ApiCart
Получить содержимое корзины.

**Query Parameters:**
- `culture` (optional) - язык для локализации товаров

### POST /api/ApiCart
Добавить товар в корзину.

**Request Body:**
```json
{
  "productId": 1,
  "quantity": 2
}
```

### PUT /api/ApiCart/{id}
Обновить количество товара в корзине.

**Request Body:**
```json
2
```

### DELETE /api/ApiCart/{id}
Удалить товар из корзины.

### DELETE /api/ApiCart/clear
Очистить корзину.

---

## Использование из jQuery

```javascript
// Подключить скрипт
<script src="~/js/api-client.js"></script>

// Вход
ApiClient.login('test@example.com', 'password123')
    .then(response => {
        console.log('Logged in:', response);
    });

// Получить товары
ApiClient.getProducts('Tents', null, 1, 12, 'ru-RU')
    .then(data => {
        console.log('Products:', data.products);
    });

// Добавить в корзину
ApiClient.addToCart(1, 2)
    .then(response => {
        console.log('Added to cart');
    });
```

---

## Использование из серверного приложения

```csharp
// В контроллере или сервисе
public class MyController : Controller
{
    private readonly ApiClientService _apiClient;

    public MyController(ApiClientService apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index()
    {
        // Установить токен (если нужно)
        _apiClient.SetToken("your-jwt-token");

        // Получить товары
        var products = await _apiClient.GetAsync<ProductsResponse>("/ApiProducts?culture=ru-RU");
        
        return View(products);
    }
}
```

---

## Обработка ошибок

Все API endpoints возвращают стандартные HTTP коды:
- `200 OK` - успешный запрос
- `201 Created` - ресурс создан
- `400 Bad Request` - неверный запрос
- `401 Unauthorized` - требуется авторизация
- `404 Not Found` - ресурс не найден
- `500 Internal Server Error` - ошибка сервера

В случае ошибки возвращается JSON:
```json
{
  "error": {
    "message": "Error message",
    "details": "Error details",
    "path": "/api/endpoint",
    "timestamp": "2025-12-08T20:00:00Z"
  }
}
```

