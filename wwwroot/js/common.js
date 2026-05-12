const API_BASE_URL = 'https://fashionshopapi-e8enh8bvd9edenae.southeastasia-01.azurewebsites.net/api';

async function loadNavbar() {
    const container = document.getElementById('navbar-container');
    if (!container) return; 

    try {
        const response = await fetch('/navbar.html');
        const navbarHtml = await response.text();
        container.innerHTML = navbarHtml;
        updateNavbarUI();
    } catch (error) {
        console.error("Navbar load error:", error);
    }
}

async function loadAdminNav() {
    const container = document.getElementById('admin-nav-container');
    if (!container) return;

    try {
        const response = await fetch('/admin/adminNav.html');
        const navHtml = await response.text();
        container.innerHTML = navHtml;
    } catch (error) {
        console.error("Admin nav load error:", error);
    }
}

function updateNavbarUI() {
    const userJson = localStorage.getItem('user');
    const userInfoDiv = document.getElementById('userInfo');
    const cartCountSpan = document.getElementById('cartCount');

    const cart = JSON.parse(localStorage.getItem('cart')) || {};
    let count = 0;
    Object.values(cart).forEach(item => count += item.quantity);
    if (cartCountSpan) cartCountSpan.innerText = count;

    if (!userInfoDiv) return;

    if (userJson) {
        const user = JSON.parse(userJson);
        let html = `Hello, <a href="/profile.html">${user.fullName}</a> | `;
        if (user.role === 'admin') {
            html += `<a href="/admin/dashboard.html" style="color:var(--c-accent)">Admin</a> | `;
        }
        html += `<a href="#" onclick="logout()">Sign Out</a>`;
        userInfoDiv.innerHTML = html;
    } else {
        userInfoDiv.innerHTML = `<a href="/login.html">Sign In</a> | <a href="/register.html">Register</a>`;
    }
}

function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    window.location.href = '/index.html';
}

function formatCurrency(amount) {
    return (amount || 0).toLocaleString('vi-VN') + ' ₫';
}