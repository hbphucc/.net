
async function loadNavbar() {
    try {
        const response = await fetch('/navbar.html');
        const navbarHtml = await response.text();
        document.getElementById('navbar-container').innerHTML = navbarHtml;
        updateNavbarUI();
    } catch (error) { console.error("Lỗi load navbar:", error); }
}

async function loadAdminNav() {
    try {
        const response = await fetch('/admin/adminNav.html');
        const navHtml = await response.text();
        document.getElementById('admin-nav-container').innerHTML = navHtml;
    } catch (error) { console.error("Lỗi load admin nav:", error); }
}


function updateNavbarUI() {
    const userJson = localStorage.getItem('user');
    const userInfoDiv = document.getElementById('userInfo');
    const cartCountSpan = document.getElementById('cartCount');

    const cart = JSON.parse(localStorage.getItem('cart')) || {};
    let count = Object.keys(cart).length;
    if (cartCountSpan) cartCountSpan.innerText = count;

    if (userJson) {
        const user = JSON.parse(userJson);
        let html = `Helllo, <a href="/profile.html">${user.fullName}</a> | `;
        if (user.role === 'admin') {
            html += `<a href="/admin/dashboard.html" style="color:var(--c-accent)">Admin</a> | `;
        }
        html += `<a href="#" onclick="logout()">Logout</a>`;
        userInfoDiv.innerHTML = html;
    } else {
        userInfoDiv.innerHTML = `<a href="/login.html">Login</a> | <a href="/register.html">Register</a>`;
    }
}

function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    window.location.href = '/index.html';
} 

function formatCurrency(amount) {
    return amount.toLocaleString('vi-VN') + ' ₫';
}

function formatCurrency(amount) {
    return amount.toLocaleString('en-US') + ' ₫';
}


function updateNavbarUI() {
    const userJson = localStorage.getItem('user');
    const userInfoDiv = document.getElementById('userInfo');
    const cartCountSpan = document.getElementById('cartCount');


    const cart = JSON.parse(localStorage.getItem('cart')) || {};
    let count = 0;
    Object.values(cart).forEach(item => count += item.quantity);
    if (cartCountSpan) cartCountSpan.innerText = count;

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