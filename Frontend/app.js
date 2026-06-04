const API_BASE_URL = "https://localhost:7273/api";
const DTO_INFO_MESSAGE = "Bu işlem için gerekli bilgiler backend DTO yapısına göre doldurulmalıdır.";

function getToken() {
    return localStorage.getItem("token");
}

function showResult(elementId, data) {
    const element = document.getElementById(elementId);

    if (!element) {
        return;
    }

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

function escapeHtml(value) {
    return String(value ?? "")
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}

function normalizeEndpoint(endpointInput) {
    const endpoint = endpointInput.trim();

    if (!endpoint) {
        return "";
    }

    return endpoint.startsWith("/") ? endpoint : `/${endpoint}`;
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
            updateUserStatus();
            renderByAuthState();
            showResult("loginResult", "Giriş başarılı. Token kaydedildi.");
        } else {
            showResult("loginResult", data);
        }
    } catch (error) {
        showResult("loginResult", error);
    }
}

async function register() {
    const firstName = document.getElementById("registerFirstName").value;
    const lastName = document.getElementById("registerLastName").value;
    const email = document.getElementById("registerEmail").value;
    const password = document.getElementById("registerPassword").value;
    const phoneNumber = document.getElementById("registerPhoneNumber").value;

    try {
        const data = await apiRequest("/Auth/register", "POST", {
            firstName: firstName,
            lastName: lastName,
            email: email,
            password: password,
            phoneNumber: phoneNumber || null
        });

        showResult("registerResult", data);
    } catch (error) {
        showResult("registerResult", error);
    }
}

function logout() {
    localStorage.removeItem("token");
    closeProfileMenu();
    closeSettingsModal();
    updateUserStatus();
    renderByAuthState();
    showResult("loginResult", "Çıkış yapıldı. Token silindi.");
}

function setLocalMessage(elementId, message) {
    const result = document.getElementById(elementId);

    if (!result) {
        return;
    }

    result.className = "local-result empty";
    result.textContent = message;
}

function setLocalHtml(elementId, html) {
    const result = document.getElementById(elementId);

    if (!result) {
        return;
    }

    result.className = "local-result";
    result.innerHTML = html;
}

function toggleForm(formId) {
    const form = document.getElementById(formId);

    if (form) {
        form.classList.toggle("hidden");
    }
}

function showDtoInfo(targetId = null) {
    setLocalMessage(targetId || getDefaultDtoResultId(), DTO_INFO_MESSAGE);
}

function getDefaultDtoResultId() {
    const userSection = document.getElementById("userSection");

    if (userSection && !userSection.classList.contains("hidden")) {
        return "cardResult";
    }

    return "busLineResult";
}

function showReportInfo() {
    setLocalMessage("reportResult", "Rapor işlemleri için rapor endpointleri geliştirici test alanından veya rapor ekranından çağrılmalıdır.");
}

function renderAdminMessage(message) {
    setLocalMessage("busLineResult", message);
}

function renderAdminTable(type, items, targetId) {
    const result = document.getElementById(targetId);

    if (!result) {
        return;
    }

    if (!Array.isArray(items) || items.length === 0) {
        setLocalMessage(targetId, type === "stations" ? "Kayıtlı durak bulunamadı." : "Kayıt bulunamadı.");
        return;
    }

    result.className = "local-result";

    if (type === "busLines") {
        const rows = items.map(function (line) {
            return `
                <tr>
                    <td>${escapeHtml(line.lineCode)}</td>
                    <td>${escapeHtml(line.lineName)}</td>
                    <td>${escapeHtml(line.description || "-")}</td>
                    <td><span class="pill">${line.isActive ? "Aktif" : "Pasif"}</span></td>
                </tr>
            `;
        }).join("");

        const cards = items.map(function (line) {
            return `
                <article class="data-card">
                    <h3>${escapeHtml(line.lineCode)} - ${escapeHtml(line.lineName)}</h3>
                    <p>${escapeHtml(line.description || "Açıklama yok")}</p>
                    <span class="pill">${line.isActive ? "Aktif" : "Pasif"}</span>
                </article>
            `;
        }).join("");

        result.innerHTML = `
            <table class="data-table">
                <thead>
                    <tr>
                        <th>Hat Kodu</th>
                        <th>Hat Adı</th>
                        <th>Açıklama</th>
                        <th>Durum</th>
                    </tr>
                </thead>
                <tbody>${rows}</tbody>
            </table>
            <div class="mobile-list">${cards}</div>
        `;
        return;
    }

    const stationRows = items.map(normalizeStation);
    const hasKnownStationField = stationRows.some(function (station) {
        return station.id || station.name || station.description || station.location || station.latitude || station.longitude || station.createdAt || station.isActive !== null;
    });

    if (!hasKnownStationField) {
        result.innerHTML = `<pre class="code-result">${escapeHtml(JSON.stringify(items, null, 2))}</pre>`;
        return;
    }

    const rows = stationRows.map(function (station) {
        return `
            <tr>
                <td>${escapeHtml(station.id || "-")}</td>
                <td>${escapeHtml(station.name || "-")}</td>
                <td>${escapeHtml(station.location || "-")}</td>
                <td>${escapeHtml(station.description || "-")}</td>
                <td>${escapeHtml(formatCoordinate(station.latitude))}</td>
                <td>${escapeHtml(formatCoordinate(station.longitude))}</td>
                <td>${renderStatusPill(station.isActive)}</td>
                <td>${escapeHtml(formatDate(station.createdAt))}</td>
            </tr>
        `;
    }).join("");

    const cards = stationRows.map(function (station) {
        return `
            <article class="data-card">
                <h3>${escapeHtml(station.name || "Durak")}</h3>
                <p><strong>ID:</strong> ${escapeHtml(station.id || "-")}</p>
                <p><strong>Konum:</strong> ${escapeHtml(station.location || "-")}</p>
                <p><strong>Açıklama:</strong> ${escapeHtml(station.description || "-")}</p>
                <p><strong>Enlem:</strong> ${escapeHtml(formatCoordinate(station.latitude))}</p>
                <p><strong>Boylam:</strong> ${escapeHtml(formatCoordinate(station.longitude))}</p>
                <p><strong>Oluşturulma:</strong> ${escapeHtml(formatDate(station.createdAt))}</p>
                ${renderStatusPill(station.isActive)}
            </article>
        `;
    }).join("");

    result.innerHTML = `
        <table class="data-table">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Durak Adı</th>
                    <th>Konum / Açıklama</th>
                    <th>Açıklama</th>
                    <th>Enlem</th>
                    <th>Boylam</th>
                    <th>Durum</th>
                    <th>Oluşturulma</th>
                </tr>
            </thead>
            <tbody>${rows}</tbody>
        </table>
        <div class="mobile-list">${cards}</div>
    `;
}

function normalizeStation(station) {
    return {
        id: station.stationId ?? station.id ?? "",
        name: station.stationName ?? station.name ?? station.stopName ?? "",
        description: station.description ?? "",
        location: station.location ?? station.district ?? "",
        latitude: station.latitude ?? "",
        longitude: station.longitude ?? "",
        isActive: typeof station.isActive === "boolean" ? station.isActive : null,
        createdAt: station.createdAt ?? ""
    };
}

function renderStatusPill(isActive) {
    if (isActive === null) {
        return `<span class="pill">Bilinmiyor</span>`;
    }

    return `<span class="pill">${isActive ? "Aktif" : "Pasif"}</span>`;
}

function formatCoordinate(value) {
    return value === null || value === undefined || value === "" ? "-" : value;
}

function formatDate(value) {
    if (!value) {
        return "-";
    }

    const date = new Date(value);

    if (Number.isNaN(date.getTime())) {
        return value;
    }

    return date.toLocaleDateString("tr-TR");
}

function renderSimpleCards(targetId, title, items, fields) {
    const result = document.getElementById(targetId);

    if (!result) {
        return;
    }

    if (!Array.isArray(items) || items.length === 0) {
        setLocalMessage(targetId, "Kayıt bulunamadı.");
        return;
    }

    const cards = items.map(function (item) {
        const rows = fields.map(function (field) {
            return `<p><strong>${field.label}:</strong> ${escapeHtml(item[field.key] ?? "-")}</p>`;
        }).join("");

        return `
            <article class="data-card">
                <h3>${escapeHtml(item[fields[0].key] ?? title)}</h3>
                ${rows}
            </article>
        `;
    }).join("");

    result.className = "local-result";
    result.innerHTML = `<div class="mobile-list always-visible">${cards}</div>`;
}

async function getAdminReports() {
    try {
        setLocalMessage("reportResult", "Raporlar yükleniyor...");

        const reportRequests = [
            {
                title: "Dashboard Özeti",
                endpoint: "/Reports/user-dashboard",
                type: "dashboard"
            },
            {
                title: "Günlük Gelir Raporu",
                endpoint: "/Reports/daily-revenue",
                type: "dailyRevenue"
            },
            {
                title: "En Çok Kullanılan Hatlar",
                endpoint: "/Reports/most-used-lines",
                type: "mostUsedLines"
            }
        ];

        const reportResults = [];

        for (const request of reportRequests) {
            try {
                const data = await apiRequest(request.endpoint);
                reportResults.push({
                    title: request.title,
                    type: request.type,
                    data: data,
                    error: null
                });
            } catch (error) {
                reportResults.push({
                    title: request.title,
                    type: request.type,
                    data: null,
                    error: error.message
                });
            }
        }

        if (reportResults.every(function (report) { return report.error; })) {
            setLocalMessage("reportResult", `Raporlar yüklenirken hata oluştu: ${reportResults[0].error || "Rapor endpointleri backend tarafında kontrol edilmelidir."}`);
            return;
        }

        renderReports(reportResults);
    } catch (error) {
        setLocalMessage("reportResult", `Raporlar yüklenirken hata oluştu: ${error.message || "Rapor endpointleri backend tarafında kontrol edilmelidir."}`);
    }
}

function renderReports(reportResults) {
    const sections = reportResults.map(function (report) {
        if (report.error) {
            return `
                <section class="report-section">
                    <h3>${escapeHtml(report.title)}</h3>
                    <p class="message">Raporlar yüklenirken hata oluştu: ${escapeHtml(report.error)}</p>
                </section>
            `;
        }

        const rows = Array.isArray(report.data) ? report.data : [];

        if (rows.length === 0) {
            return `
                <section class="report-section">
                    <h3>${escapeHtml(report.title)}</h3>
                    <p class="message">Bu rapor için kayıt bulunamadı.</p>
                </section>
            `;
        }

        if (report.type === "dashboard") {
            return renderDashboardReport(report.title, rows);
        }

        if (report.type === "dailyRevenue") {
            return renderDailyRevenueReport(report.title, rows);
        }

        if (report.type === "mostUsedLines") {
            return renderMostUsedLinesReport(report.title, rows);
        }

        return renderUnknownReport(report.title, rows);
    }).join("");

    setLocalHtml("reportResult", `<div class="report-grid">${sections}</div>`);
}

function renderDashboardReport(title, rows) {
    const cards = rows.map(function (item) {
        return `
            <article class="data-card">
                <h3>${escapeHtml(item.fullName || "Kullanıcı")}</h3>
                <p><strong>E-posta:</strong> ${escapeHtml(item.email || "-")}</p>
                <p><strong>Kart No:</strong> ${escapeHtml(item.cardNumber || "-")}</p>
                <p><strong>Kart Tipi:</strong> ${escapeHtml(item.cardTypeName || "-")}</p>
                <p><strong>Bakiye:</strong> ${escapeHtml(item.balance ?? "-")}</p>
                <p><strong>Durum:</strong> ${escapeHtml(item.cardStatus || "-")}</p>
            </article>
        `;
    }).join("");

    return `
        <section class="report-section">
            <h3>${escapeHtml(title)}</h3>
            <div class="mobile-list always-visible">${cards}</div>
        </section>
    `;
}

function renderDailyRevenueReport(title, rows) {
    const tableRows = rows.map(function (item) {
        return `
            <tr>
                <td>${escapeHtml(formatDate(item.revenueDate))}</td>
                <td>${escapeHtml(item.paymentCount ?? "-")}</td>
                <td>${escapeHtml(item.totalRevenue ?? "-")}</td>
            </tr>
        `;
    }).join("");

    return `
        <section class="report-section">
            <h3>${escapeHtml(title)}</h3>
            <table class="data-table">
                <thead>
                    <tr>
                        <th>Tarih</th>
                        <th>Ödeme Sayısı</th>
                        <th>Toplam Gelir</th>
                    </tr>
                </thead>
                <tbody>${tableRows}</tbody>
            </table>
        </section>
    `;
}

function renderMostUsedLinesReport(title, rows) {
    const tableRows = rows.map(function (item) {
        return `
            <tr>
                <td>${escapeHtml(item.lineCode || "-")}</td>
                <td>${escapeHtml(item.lineName || "-")}</td>
                <td>${escapeHtml(item.tripCount ?? "-")}</td>
                <td>${escapeHtml(item.totalFareAmount ?? "-")}</td>
            </tr>
        `;
    }).join("");

    return `
        <section class="report-section">
            <h3>${escapeHtml(title)}</h3>
            <table class="data-table">
                <thead>
                    <tr>
                        <th>Hat Kodu</th>
                        <th>Hat Adı</th>
                        <th>Yolculuk Sayısı</th>
                        <th>Toplam Ücret</th>
                    </tr>
                </thead>
                <tbody>${tableRows}</tbody>
            </table>
        </section>
    `;
}

function renderUnknownReport(title, rows) {
    return `
        <section class="report-section">
            <h3>${escapeHtml(title)}</h3>
            <pre class="code-result">${escapeHtml(JSON.stringify(rows, null, 2))}</pre>
        </section>
    `;
}

async function getBusLines() {
    try {
        setLocalMessage("busLineResult", "Hatlar yükleniyor...");
        const data = await apiRequest("/Admin/bus-lines");
        renderAdminTable("busLines", data, "busLineResult");
    } catch (error) {
        setLocalMessage("busLineResult", error.message);
    }
}

async function createBusLine() {
    const lineCode = document.getElementById("newLineCode").value;
    const lineName = document.getElementById("newLineName").value;
    const description = document.getElementById("newLineDescription").value;

    try {
        setLocalMessage("busLineResult", "Yeni hat kaydediliyor...");
        await apiRequest("/Admin/bus-lines", "POST", {
            lineCode: lineCode,
            lineName: lineName,
            description: description || null
        });
        await getBusLines();
    } catch (error) {
        setLocalMessage("busLineResult", error.message);
    }
}

async function getStations() {
    try {
        setLocalMessage("stationResult", "Duraklar yükleniyor...");
        const data = await apiRequest("/Admin/stations");
        renderAdminTable("stations", data, "stationResult");
    } catch (error) {
        setLocalMessage("stationResult", `Duraklar yüklenirken hata oluştu: ${error.message}`);
    }
}

async function createStation() {
    const stationName = document.getElementById("newStationName").value;
    const district = document.getElementById("newStationDistrict").value;

    try {
        setLocalMessage("stationResult", "Yeni durak kaydediliyor...");
        await apiRequest("/Admin/stations", "POST", {
            stationName: stationName,
            district: district || null
        });
        await getStations();
    } catch (error) {
        setLocalMessage("stationResult", error.message);
    }
}

async function getMyCards() {
    try {
        setLocalMessage("cardResult", "Kartlar yükleniyor...");
        const data = await apiRequest("/Cards/my");
        renderSimpleCards("cardResult", "Kart", data, [
            { key: "cardNumber", label: "Kart numarası" },
            { key: "cardTypeName", label: "Kart tipi" },
            { key: "balance", label: "Bakiye" },
            { key: "status", label: "Durum" }
        ]);
    } catch (error) {
        setLocalMessage("cardResult", error.message);
    }
}

async function getSubscriptionPlans() {
    try {
        setLocalMessage("subscriptionLostResult", "Abonman planları yükleniyor...");
        const data = await apiRequest("/Subscriptions/plans");
        renderSimpleCards("subscriptionLostResult", "Abonman", data, [
            { key: "name", label: "Plan adı" },
            { key: "cardTypeName", label: "Kart tipi" },
            { key: "price", label: "Ücret" },
            { key: "rideCount", label: "Biniş hakkı" },
            { key: "validityDays", label: "Geçerlilik günü" }
        ]);
    } catch (error) {
        setLocalMessage("subscriptionLostResult", error.message);
    }
}

function toggleDeveloperPanel() {
    const content = document.getElementById("developerContent");
    const button = document.getElementById("developerToggleButton");

    if (!content || !button) {
        return;
    }

    const isOpening = content.classList.contains("collapsed");
    content.classList.toggle("collapsed");
    button.textContent = isOpening ? "Geliştirici Test Alanını Kapat" : "Geliştirici Test Alanını Aç";
}

async function customRequest() {
    const method = document.getElementById("customMethod").value;
    const endpoint = normalizeEndpoint(document.getElementById("customEndpoint").value);
    const bodyText = document.getElementById("customBody").value.trim();

    if (!endpoint) {
        showResult("customResult", "Endpoint alanı boş olamaz.");
        return;
    }

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

async function customGet() {
    const endpoint = normalizeEndpoint(document.getElementById("manualEndpoint").value);

    if (!endpoint) {
        showResult("manualResult", "Endpoint alanı boş olamaz.");
        return;
    }

    try {
        const data = await apiRequest(endpoint);
        showResult("manualResult", data);
    } catch (error) {
        showResult("manualResult", error);
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

function getUserInfoFromToken() {
    const token = getToken();
    const payload = token ? parseJwt(token) : null;
    const role = token ? getRoleFromToken(token) : null;

    return {
        token: token,
        payload: payload,
        role: role,
        roleText: getRoleText(role),
        email: payload ? (payload.email || payload.sub || "") : "",
        name: payload ? (payload.name || payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || "") : ""
    };
}

function getRoleText(role) {
    if (!role) {
        return "Misafir";
    }

    if (isAdminRole(role)) {
        return "Admin";
    }

    return "Kullanıcı";
}

function isAdminRole(role) {
    if (Array.isArray(role)) {
        return role.some(function (item) {
            return item && item.toString().toLowerCase() === "admin";
        });
    }

    return role && role.toString().toLowerCase() === "admin";
}

function updateUserStatus() {
    const status = document.getElementById("userStatus");
    const token = getToken();
    const userInfo = getUserInfoFromToken();

    if (!status) {
        return;
    }

    if (!token) {
        status.textContent = "Giriş yapılmadı";
        status.className = "status guest";
    } else if (isAdminRole(userInfo.role)) {
        status.textContent = "Admin olarak giriş yapıldı";
        status.className = "status admin";
    } else {
        status.textContent = "Kullanıcı olarak giriş yapıldı";
        status.className = "status user";
    }

    updateProfileArea(userInfo);
    updateSettingsInfo(userInfo);
}

function renderByAuthState() {
    const userInfo = getUserInfoFromToken();
    const isLoggedIn = Boolean(userInfo.token);
    const isAdmin = isLoggedIn && isAdminRole(userInfo.role);

    setSectionVisible("guestSection", !isLoggedIn);
    setSectionVisible("adminSection", isLoggedIn && isAdmin);
    setSectionVisible("userSection", isLoggedIn && !isAdmin);
    setSectionVisible("resultSection", isLoggedIn && !isAdmin);

    if (!isLoggedIn) {
        resetDeveloperPanel();
    }
}

function setSectionVisible(sectionId, visible) {
    const section = document.getElementById(sectionId);

    if (section) {
        section.classList.toggle("hidden", !visible);
    }
}

function resetDeveloperPanel() {
    const content = document.getElementById("developerContent");
    const button = document.getElementById("developerToggleButton");

    if (content) {
        content.classList.add("collapsed");
    }

    if (button) {
        button.textContent = "Geliştirici Test Alanını Aç";
    }
}

function updateProfileArea(userInfo) {
    const avatar = document.getElementById("profileAvatar");
    const roleText = document.getElementById("profileRole");
    const emailText = document.getElementById("profileEmail");
    const summary = document.getElementById("profileSummary");
    const logoutButton = document.getElementById("dropdownLogoutButton");

    if (!avatar || !roleText || !emailText || !summary || !logoutButton) {
        return;
    }

    if (!userInfo.token) {
        avatar.textContent = "M";
        roleText.textContent = "Misafir";
        emailText.textContent = "Oturum yok";
        summary.innerHTML = `
            <strong>Misafir kullanıcı</strong>
            <p>Giriş yaparak işlemlere erişebilirsiniz.</p>
        `;
        logoutButton.style.display = "none";
        return;
    }

    const visibleName = userInfo.name || userInfo.email || userInfo.roleText;
    avatar.textContent = visibleName.charAt(0).toUpperCase();
    roleText.textContent = userInfo.roleText;
    emailText.textContent = userInfo.email || "E-posta bilgisi yok";
    summary.innerHTML = `
        <strong>Rol bilgisi: ${escapeHtml(userInfo.roleText)}</strong>
        <p>${escapeHtml(userInfo.email || "E-posta token içinde bulunamadı.")}</p>
    `;
    logoutButton.style.display = "block";
}

function toggleProfileMenu(event) {
    event.stopPropagation();
    const dropdown = document.getElementById("profileDropdown");

    if (dropdown) {
        dropdown.classList.toggle("open");
    }
}

function closeProfileMenu() {
    const dropdown = document.getElementById("profileDropdown");

    if (dropdown) {
        dropdown.classList.remove("open");
    }
}

function openSettingsModal() {
    closeProfileMenu();
    updateSettingsInfo(getUserInfoFromToken());

    const modal = document.getElementById("settingsModal");
    if (modal) {
        modal.classList.add("open");
    }
}

function closeSettingsModal() {
    const modal = document.getElementById("settingsModal");
    if (modal) {
        modal.classList.remove("open");
    }
}

function updateSettingsInfo(userInfo) {
    const role = document.getElementById("settingsRole");
    const tokenStatus = document.getElementById("settingsTokenStatus");
    const apiUrl = document.getElementById("settingsApiUrl");

    if (role) {
        role.textContent = userInfo.roleText;
    }

    if (tokenStatus) {
        tokenStatus.textContent = userInfo.token ? "Aktif" : "Yok";
    }

    if (apiUrl) {
        apiUrl.textContent = API_BASE_URL;
    }
}

function setTheme(theme) {
    const selectedTheme = theme === "dark" ? "dark" : "light";
    localStorage.setItem("theme", selectedTheme);
    applyTheme(selectedTheme);
}

function applyTheme(theme) {
    const selectedTheme = theme === "dark" ? "dark" : "light";
    document.body.classList.toggle("dark-theme", selectedTheme === "dark");

    const lightButton = document.getElementById("lightThemeButton");
    const darkButton = document.getElementById("darkThemeButton");

    if (lightButton && darkButton) {
        lightButton.classList.toggle("active", selectedTheme === "light");
        darkButton.classList.toggle("active", selectedTheme === "dark");
    }
}

function loadTheme() {
    const savedTheme = localStorage.getItem("theme") || "light";
    applyTheme(savedTheme);
}

document.addEventListener("click", function (event) {
    const profileMenu = document.querySelector(".profile-menu");
    const modal = document.getElementById("settingsModal");

    if (profileMenu && !profileMenu.contains(event.target)) {
        closeProfileMenu();
    }

    if (modal && event.target === modal) {
        closeSettingsModal();
    }
});

document.addEventListener("keydown", function (event) {
    if (event.key === "Escape") {
        closeProfileMenu();
        closeSettingsModal();
    }
});

loadTheme();
updateUserStatus();
renderByAuthState();
