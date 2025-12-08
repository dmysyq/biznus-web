/**
 * Система всплывающих уведомлений (Toast)
 */
(function() {
    'use strict';

    // Создаём контейнер для toast-уведомлений, если его ещё нет
    if (!document.getElementById('toast-container')) {
        var container = document.createElement('div');
        container.id = 'toast-container';
        container.className = 'toast-container';
        document.body.appendChild(container);
    }

    window.showToast = function(message, type = 'success') {
        var container = document.getElementById('toast-container');
        var toast = document.createElement('div');
        toast.className = 'toast toast-' + type;
        
        var icon = type === 'success' ? '✓' : type === 'error' ? '✕' : 'ℹ';
        toast.innerHTML = '<span class="toast-icon">' + icon + '</span><span class="toast-message">' + message + '</span>';
        
        container.appendChild(toast);
        
        // Анимация появления
        setTimeout(function() {
            toast.classList.add('show');
        }, 10);
        
        // Автоматическое скрытие через 3 секунды
        setTimeout(function() {
            toast.classList.remove('show');
            setTimeout(function() {
                if (toast.parentNode) {
                    toast.parentNode.removeChild(toast);
                }
            }, 300);
        }, 3000);
    };
})();

