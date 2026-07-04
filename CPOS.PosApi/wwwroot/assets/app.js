const storageKey = "cpos.pos.session";

const state = {
    token: "",
    user: null,
    tables: [],
    groups: [],
    items: [],
    units: [],
    stores: [],
    status: "all",
    groupId: 0,
    currentBill: null,
    selectedTable: null,
    editingItem: null
};

const els = {};

document.addEventListener("DOMContentLoaded", () => {
    bindElements();
    bindEvents();
    restoreSession();
});

function bindElements() {
    els.loginView = document.getElementById("loginView");
    els.appView = document.getElementById("appView");
    els.loginForm = document.getElementById("loginForm");
    els.passwordInput = document.getElementById("passwordInput");
    els.loginMessage = document.getElementById("loginMessage");
    els.sessionInfo = document.getElementById("sessionInfo");
    els.refreshButton = document.getElementById("refreshButton");
    els.logoutButton = document.getElementById("logoutButton");
    els.tablesList = document.getElementById("tablesList");
    els.tablesCount = document.getElementById("tablesCount");
    els.groupsList = document.getElementById("groupsList");
    els.itemsList = document.getElementById("itemsList");
    els.itemsCount = document.getElementById("itemsCount");
    els.itemSearchInput = document.getElementById("itemSearchInput");
    els.billTitle = document.getElementById("billTitle");
    els.billMeta = document.getElementById("billMeta");
    els.billItems = document.getElementById("billItems");
    els.billTotal = document.getElementById("billTotal");
    els.billDiscount = document.getElementById("billDiscount");
    els.billPure = document.getElementById("billPure");
    els.sendOrderButton = document.getElementById("sendOrderButton");
    els.itemEditDialog = document.getElementById("itemEditDialog");
    els.itemEditTitle = document.getElementById("itemEditTitle");
    els.editUnitSelect = document.getElementById("editUnitSelect");
    els.editNotesInput = document.getElementById("editNotesInput");
    els.saveItemEditButton = document.getElementById("saveItemEditButton");
    els.cancelItemEditButton = document.getElementById("cancelItemEditButton");
    els.closeItemEditButton = document.getElementById("closeItemEditButton");
    els.toast = document.getElementById("toast");
}

function bindEvents() {
    els.loginForm.addEventListener("submit", handleLogin);
    els.refreshButton.addEventListener("click", refreshAll);
    els.logoutButton.addEventListener("click", logout);
    els.itemSearchInput.addEventListener("input", renderItems);
    els.sendOrderButton.addEventListener("click", sendOrder);
    els.saveItemEditButton.addEventListener("click", saveItemEdit);
    els.cancelItemEditButton.addEventListener("click", closeItemEdit);
    els.closeItemEditButton.addEventListener("click", closeItemEdit);

    document.querySelectorAll(".segmented button").forEach(button => {
        button.addEventListener("click", () => {
            document.querySelectorAll(".segmented button").forEach(item => item.classList.remove("active"));
            button.classList.add("active");
            state.status = button.dataset.status || "all";
            loadTables();
        });
    });
}

async function handleLogin(event) {
    event.preventDefault();
    els.loginMessage.textContent = "";

    const password = els.passwordInput.value.trim();
    if (!password) {
        els.loginMessage.textContent = "أدخل كلمة المرور";
        return;
    }

    setBusy(els.loginForm, true);
    try {
        const user = await request("/api/auth/login", {
            method: "POST",
            body: { password },
            skipAuth: true
        });

        if (!user.token) {
            els.loginMessage.textContent = "لم يتم إصدار توكن للدخول";
            return;
        }

        state.token = user.token;
        state.user = user;
        localStorage.setItem(storageKey, JSON.stringify({ token: state.token, user: state.user }));
        showApp();
        await refreshAll();
    } catch (error) {
        els.loginMessage.textContent = error.message || "تعذر تسجيل الدخول";
    } finally {
        setBusy(els.loginForm, false);
    }
}

function restoreSession() {
    const saved = localStorage.getItem(storageKey);
    if (!saved) {
        showLogin();
        return;
    }

    try {
        const session = JSON.parse(saved);
        state.token = session.token || "";
        state.user = session.user || null;
    } catch {
        localStorage.removeItem(storageKey);
    }

    if (!state.token || !state.user) {
        showLogin();
        return;
    }

    showApp();
    refreshAll();
}

function showLogin() {
    els.loginView.classList.remove("hidden");
    els.appView.classList.add("hidden");
    els.passwordInput.focus();
}

function showApp() {
    els.loginView.classList.add("hidden");
    els.appView.classList.remove("hidden");
    els.sessionInfo.textContent = state.user?.userName ? `المستخدم: ${state.user.userName}` : "";
}

async function refreshAll() {
    try {
        await Promise.all([loadBootstrap(), loadTables()]);
    } catch (error) {
        showToast(error.message || "تعذر تحديث البيانات", "error");
    }
}

async function loadBootstrap() {
    const data = await request("/api/pos/bootstrap");
    state.groups = data.groups || [];
    state.items = data.items || [];
    state.units = data.units || [];
    state.stores = data.stores || [];

    if (!state.groupId && state.groups.length > 0) {
        state.groupId = state.groups[0].groupId;
    }

    renderGroups();
    renderItems();
}

async function loadTables() {
    const data = await request(`/api/tables?status=${encodeURIComponent(state.status)}&includeLayout=false`);
    state.tables = data.tables || [];
    renderTables();
}

function renderTables() {
    els.tablesCount.textContent = state.tables.length.toString();
    els.tablesList.innerHTML = "";

    if (state.tables.length === 0) {
        els.tablesList.classList.add("empty-state");
        els.tablesList.textContent = "لا توجد طاولات";
        return;
    }

    els.tablesList.classList.remove("empty-state");
    state.tables.forEach(table => {
        const button = document.createElement("button");
        button.className = "table-button";
        if (state.selectedTable?.tableId === table.tableId) button.classList.add("active");
        button.type = "button";
        button.innerHTML = `
            <div class="table-name">
                <strong>${escapeHtml(table.tableName || `طاولة ${table.tableId}`)}</strong>
                <span>${table.tableId}</span>
            </div>
            <div class="table-status ${table.isBusy ? "status-open" : "status-free"}">
                ${table.isBusy ? "مفتوحة" : "شاغرة"}${table.flateName ? ` - ${escapeHtml(table.flateName)}` : ""}
            </div>
        `;
        button.addEventListener("click", () => openTableBill(table));
        els.tablesList.appendChild(button);
    });
}

function renderGroups() {
    els.groupsList.innerHTML = "";

    state.groups.forEach(group => {
        const button = document.createElement("button");
        button.className = "group-button";
        if (group.groupId === state.groupId) button.classList.add("active");
        button.type = "button";
        button.textContent = group.groupName || `مجموعة ${group.groupId}`;
        if (group.backgroundColor) button.style.backgroundColor = group.backgroundColor;
        if (group.foregroundColor) button.style.color = group.foregroundColor;
        button.addEventListener("click", () => {
            state.groupId = group.groupId;
            renderGroups();
            renderItems();
        });
        els.groupsList.appendChild(button);
    });
}

function renderItems() {
    const search = els.itemSearchInput.value.trim().toLowerCase();
    const filtered = state.items.filter(item => {
        const byGroup = !state.groupId || item.groupId === state.groupId;
        const name = `${item.salesName || ""} ${item.itemName || ""}`.toLowerCase();
        return byGroup && (!search || name.includes(search));
    });

    els.itemsCount.textContent = filtered.length.toString();
    els.itemsList.innerHTML = "";

    if (filtered.length === 0) {
        els.itemsList.classList.add("empty-state");
        els.itemsList.textContent = "لا توجد أصناف";
        return;
    }

    els.itemsList.classList.remove("empty-state");
    filtered.forEach(item => {
        const unit = getDefaultUnit(item.itemId);
        const button = document.createElement("button");
        button.className = "item-button";
        button.type = "button";
        button.disabled = !state.currentBill || !unit;
        button.innerHTML = `
            <strong>${escapeHtml(item.salesName || item.itemName || `صنف ${item.itemId}`)}</strong>
            <div class="item-price">${unit ? formatNumber(unit.price) : "بدون وحدة"}</div>
        `;
        if (item.backgroundColor) button.style.backgroundColor = item.backgroundColor;
        if (item.foregroundColor) button.style.color = item.foregroundColor;
        button.addEventListener("click", () => addItem(item));
        els.itemsList.appendChild(button);
    });
}

async function openTableBill(table) {
    state.selectedTable = table;
    renderTables();

    try {
        const data = await request(`/api/tables/${table.tableId}/open-bill`, {
            method: "POST",
            body: {
                userId: state.user.userId,
                billTypeId: 3,
                agentId: state.user.agentId || 0
            }
        });

        state.currentBill = data.bill;
        renderBill();
        renderItems();
        showToast("تم فتح الفاتورة", "success");
        await loadTables();
    } catch (error) {
        showToast(error.message || "تعذر فتح الفاتورة", "error");
    }
}

async function addItem(item) {
    if (!state.currentBill) return;

    const unit = getDefaultUnit(item.itemId);
    const store = state.stores[0];
    if (!unit || !store) {
        showToast("لا توجد وحدة أو مخزن للصنف", "error");
        return;
    }

    try {
        state.currentBill = await request(`/api/bills/${state.currentBill.transactionId}/items`, {
            method: "POST",
            body: {
                itemId: item.itemId,
                storeId: store.storeId,
                unitItemId: unit.unitItemId,
                barcode: unit.barcode || "",
                quantity: 1,
                price: unit.price,
                salesTypeId: 0
            }
        });

        renderBill();
    } catch (error) {
        showToast(error.message || "تعذر إضافة الصنف", "error");
    }
}

async function changeQty(detailId, value) {
    try {
        state.currentBill = await request(`/api/bills/items/${detailId}/qty`, {
            method: "PATCH",
            body: { changeBy: value }
        });
        renderBill();
    } catch (error) {
        showToast(error.message || "تعذر تعديل الكمية", "error");
    }
}

async function deleteItem(detailId) {
    try {
        state.currentBill = await request(`/api/bills/items/${detailId}`, {
            method: "DELETE"
        });
        renderBill();
    } catch (error) {
        showToast(error.message || "تعذر حذف الصنف", "error");
    }
}

async function sendOrder() {
    if (!state.currentBill || !state.selectedTable) return;

    const buttonText = els.sendOrderButton.textContent;
    els.sendOrderButton.disabled = true;
    els.sendOrderButton.textContent = "جاري الإرسال";

    try {
        await request(`/api/bills/${state.currentBill.transactionId}/send-order`, {
            method: "POST",
            body: { tableId: state.selectedTable.tableId }
        });
        els.sendOrderButton.textContent = buttonText;
        resetCurrentBill();
        await loadTables();
        renderItems();
        showToast("تم إرسال الطلب", "success");
    } catch (error) {
        els.sendOrderButton.textContent = buttonText;
        renderBill();
        showToast(error.message || "تعذر إرسال الطلب", "error");
    }
}

function resetCurrentBill() {
    state.currentBill = null;
    state.selectedTable = null;
    renderTables();
    renderBill();
}

function renderBill() {
    const bill = state.currentBill;
    if (!bill) {
        els.billTitle.textContent = "الفاتورة";
        els.billMeta.textContent = "لم يتم اختيار طاولة";
        els.billItems.className = "bill-items empty-state";
        els.billItems.textContent = "اختر طاولة لفتح الفاتورة";
        els.sendOrderButton.disabled = true;
        setTotals();
        return;
    }

    els.billTitle.textContent = bill.tableName || state.selectedTable?.tableName || "الفاتورة";
    els.billMeta.textContent = `رقم يومي: ${bill.dailyBillNumber || "-"} - رقم آلي: ${bill.salesBillId || bill.transactionId}`;
    els.sendOrderButton.disabled = (bill.items || []).length === 0;

    const items = bill.items || [];
    if (items.length === 0) {
        els.billItems.className = "bill-items empty-state";
        els.billItems.textContent = "الفاتورة فارغة";
    } else {
        els.billItems.className = "bill-items";
        els.billItems.innerHTML = "";
        items.forEach(item => {
            const row = document.createElement("div");
            row.className = "bill-row";
            row.innerHTML = `
                <div>
                    <div class="bill-item-name">${escapeHtml(item.itemName)}</div>
                    <div class="bill-item-sub">${escapeHtml(item.unitName)} - ${formatNumber(item.price)}</div>
                    ${item.notes ? `<div class="bill-item-notes">${escapeHtml(item.notes)}</div>` : ""}
                </div>
                <div class="qty-control">
                    <button type="button" data-action="plus">+</button>
                    <span>${formatNumber(item.quantity)}</span>
                    <button type="button" data-action="minus">-</button>
                </div>
                <div class="row-actions">
                    <button class="edit-item" type="button">تعديل</button>
                    <button class="delete-item" type="button">حذف</button>
                </div>
            `;
            row.querySelector('[data-action="plus"]').addEventListener("click", () => changeQty(item.detailId, 1));
            row.querySelector('[data-action="minus"]').addEventListener("click", () => changeQty(item.detailId, -1));
            row.querySelector(".edit-item").addEventListener("click", () => openItemEdit(item));
            row.querySelector(".delete-item").addEventListener("click", () => deleteItem(item.detailId));
            els.billItems.appendChild(row);
        });
    }

    setTotals(bill);
}

function setTotals(bill) {
    els.billTotal.textContent = formatNumber(bill?.total || 0);
    els.billDiscount.textContent = formatNumber(bill?.discount || 0);
    els.billPure.textContent = formatNumber(bill?.pure || 0);
}

function getDefaultUnit(itemId) {
    const units = state.units.filter(unit => unit.itemId === itemId);
    return units.find(unit => unit.isDefault) || units[0] || null;
}

function openItemEdit(item) {
    const units = state.units.filter(unit => unit.itemId === item.itemId);
    if (units.length === 0) {
        showToast("لا توجد وحدات لهذا الصنف", "error");
        return;
    }

    state.editingItem = item;
    els.itemEditTitle.textContent = item.itemName || "تعديل الصنف";
    els.editUnitSelect.innerHTML = "";

    units.forEach(unit => {
        const option = document.createElement("option");
        option.value = unit.unitId;
        option.textContent = `${unit.unitName} - ${formatNumber(unit.price)}`;
        if (unit.unitId === item.unitId) option.selected = true;
        els.editUnitSelect.appendChild(option);
    });

    els.editNotesInput.value = item.notes || "";
    els.itemEditDialog.classList.remove("hidden");
    els.editUnitSelect.focus();
}

function closeItemEdit() {
    state.editingItem = null;
    els.itemEditDialog.classList.add("hidden");
    els.editUnitSelect.innerHTML = "";
    els.editNotesInput.value = "";
}

async function saveItemEdit() {
    const item = state.editingItem;
    if (!item) return;

    const unitId = Number(els.editUnitSelect.value || 0);
    if (!unitId) {
        showToast("اختر الوحدة", "error");
        return;
    }

    setBusy(els.itemEditDialog, true);
    try {
        state.currentBill = await request(`/api/bills/items/${item.detailId}/details`, {
            method: "PATCH",
            body: {
                unitId,
                notes: els.editNotesInput.value.trim(),
                salesTypeId: 0
            }
        });

        closeItemEdit();
        renderBill();
        showToast("تم تعديل الصنف", "success");
    } catch (error) {
        showToast(error.message || "تعذر تعديل الصنف", "error");
    } finally {
        setBusy(els.itemEditDialog, false);
    }
}

async function request(url, options = {}) {
    const init = {
        method: options.method || "GET",
        headers: {
            Accept: "application/json"
        }
    };

    if (!options.skipAuth && state.token) {
        init.headers.Authorization = `Bearer ${state.token}`;
    }

    if (options.body !== undefined) {
        init.headers["Content-Type"] = "application/json";
        init.body = JSON.stringify(options.body);
    }

    const response = await fetch(url, init);
    const text = await response.text();
    const data = text ? safeJson(text) : null;

    if (response.status === 401 && !options.skipAuth) {
        logout();
        throw new Error("انتهت الجلسة");
    }

    if (!response.ok) {
        throw new Error(data?.message || `خطأ ${response.status}`);
    }

    return data;
}

function safeJson(text) {
    try {
        return JSON.parse(text);
    } catch {
        return null;
    }
}

function logout() {
    state.token = "";
    state.user = null;
    state.currentBill = null;
    state.selectedTable = null;
    localStorage.removeItem(storageKey);
    showLogin();
}

function setBusy(element, busy) {
    Array.from(element.querySelectorAll("button, input, select, textarea")).forEach(item => {
        item.disabled = busy;
    });
}

function showToast(text, type = "") {
    els.toast.textContent = text;
    els.toast.className = `toast ${type}`.trim();
    window.clearTimeout(showToast.timer);
    showToast.timer = window.setTimeout(() => {
        els.toast.classList.add("hidden");
    }, 2600);
}

function formatNumber(value) {
    const number = Number(value || 0);
    return number.toLocaleString("en-US", {
        minimumFractionDigits: Number.isInteger(number) ? 0 : 3,
        maximumFractionDigits: 3
    });
}

function escapeHtml(value) {
    return String(value ?? "")
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}
