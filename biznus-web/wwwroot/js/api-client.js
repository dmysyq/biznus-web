/**
 * Клиент для работы с Web API через jQuery
 */

const ApiClient = {
    baseUrl: '/api',
    token: null,

    /**
     * Установить JWT токен
     */
    setToken(token) {
        this.token = token;
        localStorage.setItem('jwt_token', token);
    },

    /**
     * Получить JWT токен из localStorage
     */
    getToken() {
        if (!this.token) {
            this.token = localStorage.getItem('jwt_token');
        }
        return this.token;
    },

    /**
     * Удалить токен
     */
    clearToken() {
        this.token = null;
        localStorage.removeItem('jwt_token');
    },

    /**
     * Выполнить AJAX запрос
     */
    async request(url, options = {}) {
        const token = this.getToken();
        const defaultOptions = {
            url: `${this.baseUrl}${url}`,
            contentType: 'application/json',
            dataType: 'json',
            beforeSend: function(xhr) {
                if (token) {
                    xhr.setRequestHeader('Authorization', `Bearer ${token}`);
                }
            }
        };

        const mergedOptions = { ...defaultOptions, ...options };
        
        if (mergedOptions.data && typeof mergedOptions.data === 'object') {
            mergedOptions.data = JSON.stringify(mergedOptions.data);
        }

        return $.ajax(mergedOptions)
            .fail(function(xhr) {
                if (xhr.status === 401) {
                    // Токен истёк или невалидный
                    ApiClient.clearToken();
                    if (window.location.pathname !== '/Account/Login') {
                        window.location.href = '/Account/Login';
                    }
                }
            });
    },

    /**
     * Аутентификация
     */
    async login(email, password) {
        const response = await this.request('/ApiAuth/login', {
            method: 'POST',
            data: { email, password }
        });
        
        if (response.token) {
            this.setToken(response.token);
        }
        
        return response;
    },

    async register(firstName, lastName, email, password) {
        const response = await this.request('/ApiAuth/register', {
            method: 'POST',
            data: { firstName, lastName, email, password }
        });
        
        if (response.token) {
            this.setToken(response.token);
        }
        
        return response;
    },

    async getCurrentUser() {
        return await this.request('/ApiAuth/me', {
            method: 'GET'
        });
    },

    /**
     * Товары
     */
    async getProducts(category = null, search = null, page = 1, pageSize = 12, culture = null) {
        const params = new URLSearchParams();
        if (category) params.append('category', category);
        if (search) params.append('search', search);
        params.append('page', page);
        params.append('pageSize', pageSize);
        if (culture) params.append('culture', culture);

        return await this.request(`/ApiProducts?${params.toString()}`, {
            method: 'GET'
        });
    },

    async getProduct(id, culture = null) {
        const params = culture ? `?culture=${culture}` : '';
        return await this.request(`/ApiProducts/${id}${params}`, {
            method: 'GET'
        });
    },

    /**
     * Переводы
     */
    async getTranslations(scope = null, culture = null, key = null) {
        const params = new URLSearchParams();
        if (scope) params.append('scope', scope);
        if (culture) params.append('culture', culture);
        if (key) params.append('key', key);

        return await this.request(`/ApiTranslations?${params.toString()}`, {
            method: 'GET'
        });
    },

    /**
     * Корзина
     */
    async getCart(culture = null) {
        const params = culture ? `?culture=${culture}` : '';
        return await this.request(`/ApiCart${params}`, {
            method: 'GET'
        });
    },

    async addToCart(productId, quantity = 1) {
        return await this.request('/ApiCart', {
            method: 'POST',
            data: { productId, quantity }
        });
    },

    async updateCartItem(itemId, quantity) {
        return await this.request(`/ApiCart/${itemId}`, {
            method: 'PUT',
            data: quantity
        });
    },

    async removeFromCart(itemId) {
        return await this.request(`/ApiCart/${itemId}`, {
            method: 'DELETE'
        });
    },

    async clearCart() {
        return await this.request('/ApiCart/clear', {
            method: 'DELETE'
        });
    }
};

// Примеры использования:

/*
// Вход
ApiClient.login('test@example.com', 'password123')
    .then(response => {
        console.log('Logged in:', response);
    })
    .catch(error => {
        console.error('Login failed:', error);
    });

// Получить товары
ApiClient.getProducts('Tents', null, 1, 12, 'ru-RU')
    .then(data => {
        console.log('Products:', data);
    });

// Добавить в корзину
ApiClient.addToCart(1, 2)
    .then(response => {
        console.log('Added to cart:', response);
    });

// Получить корзину
ApiClient.getCart('ru-RU')
    .then(cart => {
        console.log('Cart:', cart);
    });
*/

