// Плавная прокрутка для якорных ссылок
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();

        const targetId = this.getAttribute('href');
        if (targetId === '#') return;

        const targetElement = document.querySelector(targetId);
        if (targetElement) {
            targetElement.scrollIntoView({
                behavior: 'smooth'
            });
        }
    });
});

// Мобильное меню (можно добавить при необходимости)
function setupMobileMenu() {
    const menuToggle = document.createElement('button');
    menuToggle.className = 'mobile-menu-toggle';
    menuToggle.innerHTML = '<span></span><span></span><span></span>';

    const nav = document.querySelector('.navbar .container');
    if (nav) {
        nav.prepend(menuToggle);

        menuToggle.addEventListener('click', () => {
            document.body.classList.toggle('mobile-menu-open');
        });
    }
}

// Инициализация при загрузке
document.addEventListener('DOMContentLoaded', () => {
    setupMobileMenu();
});