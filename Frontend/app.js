const API_BASE_URL = "https://localhost:7273/api";

function getToken() {
    return localStorage.getItem("token");
}

function showResult(elementId, data) {
    const element = document.getElementById(elementId);

    if (data instanceof Error) {
        element.textContent = data.message;
        return;
    }

    if (typeof data === "string") {
        element.textContent = data;
        return;
    }

    element.textContent = JSON.stringify(data, null, 2);
}

async function apiRequest(endpoint, method = "GET", body = null) {
    const headers = {
        "Content-Type": "application/json"
    };

    const token = getToken();

    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    const options = {
        method: method,
        headers: headers
    };

    if (body) {
        options.body = JSON.stringify(body);
    }

    const response = await fetch(`${API_BASE_URL}${endpoint}`, options);

    const text = await response.text();

    let data;

    try {
        data = text ? JSON.parse(text) : "";
    } catch {
        data = text;
    }

    if (!response.ok) {
        throw new Error(
            typeof data === "string"
                ? data
                : JSON.stringify(data, null, 2)
        );
    }

    return data;
}

async function login() {
    const email = document.getElementById("loginEmail").value;
    const password = document.getElementById("loginPassword").value;

    try {
        const data = await apiRequest("/Auth/login", "POST", {
            email: email,
            password: password
        });

        const token = data.token || data.accessToken || data.jwtToken;

        if (token) {
            localStorage.setItem("token", token);
            showResult("loginResult", "Giriş başarılı. Token kaydedildi.");
        } else {
            showResult("loginResult", data);
        }
    } catch (error) {
        showResult("loginResult", error);
    }
}

async function register() {
    const fullName = document.getElementById("registerFullName").value;
    const email = document.getElementById("registerEmail").value;
    const password = document.getElementById("registerPassword").value;

    try {
        const data = await apiRequest("/Auth/register", "POST", {
            fullName: fullName,
            email: email,
            password: password
        });

        showResult("registerResult", data);
    } catch (error) {
        showResult("registerResult", error);
    }
}

function logout() {
    localStorage.removeItem("token");
    showResult("loginResult", "Çıkış yapıldı. Token silindi.");
}

async function getBusLines() {
    try {
        const data = await apiRequest("/Admin/bus-lines");
        showResult("adminResult", data);
    } catch (error) {
        showResult("adminResult", error);
    }
}

async function getStations() {
    try {
        const data = await apiRequest("/Admin/stations");
        showResult("adminResult", data);
    } catch (error) {
        showResult("adminResult", error);
    }
}

async function customRequest() {
    const method = document.getElementById("customMethod").value;
    const endpointInput = document.getElementById("customEndpoint").value;
    const bodyText = document.getElementById("customBody").value.trim();

    const endpoint = endpointInput.startsWith("/") ? endpointInput : `/${endpointInput}`;

    let body = null;

    if (bodyText && method !== "GET") {
        try {
            body = JSON.parse(bodyText);
        } catch {
            showResult("customResult", "JSON formatı hatalı. Lütfen body alanını kontrol et.");
            return;
        }
    }

    try {
        const data = await apiRequest(endpoint, method, body);
        showResult("customResult", data);
    } catch (error) {
        showResult("customResult", error);
    }
}
function parseJwt(token) {
    try {
        const base64Url = token.split(".")[1];
        const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
        const jsonPayload = decodeURIComponent(
            atob(base64)
                .split("")
                .map(function (c) {
                    return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
                })
                .join("")
        );

        return JSON.parse(jsonPayload);
    } catch {
        return null;
    }
}

function getRoleFromToken(token) {
    const payload = parseJwt(token);

    if (!payload) {
        return null;
    }

    return (
        payload.role ||
        payload.roles ||
        payload.Role ||
        payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    );
}

function updateUserStatus() {
    const status = document.getElementById("userStatus");
    const token = getToken();

    if (!token) {
        status.textContent = "Giriş yapılmadı";
        status.className = "status guest";
        return;
    }

    const role = getRoleFromToken(token);

    if (role && role.toString().toLowerCase() === "admin") {
        status.textContent = "Admin olarak giriş yapıldı";
        status.className = "status admin";
    } else {
        status.textContent = "Kullanıcı olarak giriş yapıldı";
        status.className = "status user";
    }
}

updateUserStatus();