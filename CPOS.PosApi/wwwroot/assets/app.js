const storageKey = "cpos.pos.session";

const state = {
    token: "",
    user: null,
    tables: [],
    groups: [],
    items: [],
    units: [],
    stores: [],
    billTypes: [],
    paymentMethods: [],
    defaultAgentId: 0,
    status: "all",
    groupId: 0,
    currentBill: null,
    selectedTable: null,
    editingItem: null,
    selectedUnitId: 0,
    selectedSalesTypeId: 0,
    selectedPaymentId: 0,
    componentTab: "notes",
    componentOptions: {
        notes: [],
        adds: []
    },
    selectedComponents: []
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
    els.directBillButton = document.getElementById("directBillButton");
    els.groupsList = document.getElementById("groupsList");
    els.itemsList = document.getElementById("itemsList");
    els.itemsCount = document.getElementById("itemsCount");
    els.barcodeInput = document.getElementById("barcodeInput");
    els.barcodeAddButton = document.getElementById("barcodeAddButton");
    els.itemSearchInput = document.getElementById("itemSearchInput");
    els.billTitle = document.getElementById("billTitle");
    els.billMeta = document.getElementById("billMeta");
    els.billItems = document.getElementById("billItems");
    els.billTotal = document.getElementById("billTotal");
    els.billDiscount = document.getElementById("billDiscount");
    els.billPure = document.getElementById("billPure");
    els.changeBillTypeButton = document.getElementById("changeBillTypeButton");
    els.sendOrderButton = document.getElementById("sendOrderButton");
    els.billTypeDialog = document.getElementById("billTypeDialog");
    els.billTypeOptions = document.getElementById("billTypeOptions");
    els.cancelBillTypeButton = document.getElementById("cancelBillTypeButton");
    els.closeBillTypeButton = document.getElementById("closeBillTypeButton");
    els.paymentDialog = document.getElementById("paymentDialog");
    els.paymentAmountText = document.getElementById("paymentAmountText");
    els.paymentOptions = document.getElementById("paymentOptions");
    els.paymentTreasuryText = document.getElementById("paymentTreasuryText");
    els.cancelPaymentButton = document.getElementById("cancelPaymentButton");
    els.closePaymentButton = document.getElementById("closePaymentButton");
    els.confirmPaymentButton = document.getElementById("confirmPaymentButton");
    els.itemEditDialog = document.getElementById("itemEditDialog");
    els.itemEditTitle = document.getElementById("itemEditTitle");
    els.editUnitButtons = document.getElementById("editUnitButtons");
    els.editSalesTypeButtons = document.getElementById("editSalesTypeButtons");
    els.editNotesInput = document.getElementById("editNotesInput");
    els.openComponentsButton = document.getElementById("openComponentsButton");
    els.componentDialog = document.getElementById("componentDialog");
    els.componentDialogTitle = document.getElementById("componentDialogTitle");
    els.closeComponentDialogButton = document.getElementById("closeComponentDialogButton");
    els.cancelComponentDialogButton = document.getElementById("cancelComponentDialogButton");
    els.componentNotesTab = document.getElementById("componentNotesTab");
    els.componentAddsTab = document.getElementById("componentAddsTab");
    els.componentOptions = document.getElementById("componentOptions");
    els.selectedComponents = document.getElementById("selectedComponents");
    els.clearComponentsButton = document.getElementById("clearComponentsButton");
    els.saveItemEditButton = document.getElementById("saveItemEditButton");
    els.cancelItemEditButton = document.getElementById("cancelItemEditButton");
    els.closeItemEditButton = document.getElementById("closeItemEditButton");
    els.toast = document.getElementById("toast");
}

function bindEvents() {
    els.loginForm.addEventListener("submit", handleLogin);
    els.refreshButton.addEventListener("click", refreshAll);
    els.logoutButton.addEventListener("click", logout);
    els.directBillButton.addEventListener("click", openDirectBill);
    els.barcodeAddButton.addEventListener("click", addBarcodeItem);
    els.barcodeInput.addEventListener("keydown", event => {
        if (event.key === "Enter") {
            event.preventDefault();
            addBarcodeItem();
        }
    });
    els.itemSearchInput.addEventListener("input", renderItems);
    els.changeBillTypeButton.addEventListener("click", openBillTypeDialog);
    els.sendOrderButton.addEventListener("click", saveBill);
    els.cancelBillTypeButton.addEventListener("click", closeBillTypeDialog);
    els.closeBillTypeButton.addEventListener("click", closeBillTypeDialog);
    els.cancelPaymentButton.addEventListener("click", closePaymentDialog);
    els.closePaymentButton.addEventListener("click", closePaymentDialog);
    els.confirmPaymentButton.addEventListener("click", confirmPaymentAndSave);
    els.saveItemEditButton.addEventListener("click", saveItemEdit);
    els.cancelItemEditButton.addEventListener("click", closeItemEdit);
    els.closeItemEditButton.addEventListener("click", closeItemEdit);
    els.openComponentsButton.addEventListener("click", openComponentDialog);
    els.closeComponentDialogButton.addEventListener("click", closeComponentDialog);
    els.cancelComponentDialogButton.addEventListener("click", closeComponentDialog);
    els.componentNotesTab.addEventListener("click", () => setComponentTab("notes"));
    els.componentAddsTab.addEventListener("click", () => setComponentTab("adds"));
    els.clearComponentsButton.addEventListener("click", clearItemComponents);

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

    if (state.user.canUseSalesPriceInfo === undefined ||
        state.user.canSellWholesale === undefined ||
        state.user.canSellWholesale2 === undefined ||
        state.user.isMinSalesPriceEnabled === undefined) {
        localStorage.removeItem(storageKey);
        state.token = "";
        state.user = null;
        showLogin();
        els.loginMessage.textContent = "تم تحديث الصلاحيات، الرجاء تسجيل الدخول مرة أخرى";
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
    syncUserPermissions();
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
    state.billTypes = data.billTypes || [];
    state.paymentMethods = data.paymentMethods || [];
    state.defaultAgentId = Number(data.defaultAgentId || 0);

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
    const editable = canEditCurrentBill();
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
        button.disabled = !editable || !unit;
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
        els.barcodeInput.focus();
        showToast("تم فتح الفاتورة", "success");
        await loadTables();
    } catch (error) {
        showToast(error.message || "تعذر فتح الفاتورة", "error");
    }
}

async function openDirectBill() {
    if (!canOpenDirectBill()) {
        showToast("ليس لديك صلاحية فتح فاتورة بدون طاولة", "error");
        return;
    }

    try {
        const data = await request("/api/bills/open-direct", {
            method: "POST",
            body: {
                userId: state.user.userId,
                billTypeId: getDefaultDirectBillTypeId(),
                agentId: state.user.agentId || 0
            }
        });

        state.selectedTable = null;
        state.currentBill = data.bill;
        renderTables();
        renderBill();
        renderItems();
        els.barcodeInput.focus();
        showToast("تم فتح فاتورة بدون طاولة", "success");
    } catch (error) {
        showToast(error.message || "تعذر فتح فاتورة بدون طاولة", "error");
    }
}

async function addItem(item) {
    if (!state.currentBill) return;
    if (!ensureBillEditable()) return;

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

async function addBarcodeItem() {
    if (!state.currentBill) {
        showToast("اختر طاولة قبل قراءة الباركود", "error");
        els.barcodeInput.focus();
        return;
    }

    if (!ensureBillEditable()) return;

    const barcode = els.barcodeInput.value.trim();
    if (!barcode) {
        els.barcodeInput.focus();
        return;
    }

    const store = state.stores[0];
    if (!store) {
        showToast("لا يوجد مخزن متاح للإضافة", "error");
        return;
    }

    els.barcodeAddButton.disabled = true;
    try {
        const unit = await request(`/api/pos/items/by-barcode/${encodeURIComponent(barcode)}`);
        if (!unit || !unit.unitItemId || !unit.itemId) {
            showToast("لم يتم العثور على صنف لهذا الباركود", "error");
            return;
        }

        state.currentBill = await request(`/api/bills/${state.currentBill.transactionId}/items`, {
            method: "POST",
            body: {
                itemId: unit.itemId,
                storeId: store.storeId,
                unitItemId: unit.unitItemId,
                barcode: unit.barcode || barcode,
                quantity: 1,
                price: unit.price,
                salesTypeId: 0
            }
        });

        els.barcodeInput.value = "";
        renderBill();
        showToast(`تمت إضافة ${unit.itemName || "الصنف"}`, "success");
    } catch (error) {
        const message = error.message === "خطأ 404" ? "لم يتم العثور على صنف لهذا الباركود" : error.message;
        showToast(message || "تعذر إضافة الصنف بالباركود", "error");
    } finally {
        syncBillEntryControls();
        if (canEditCurrentBill()) els.barcodeInput.focus();
    }
}

async function changeQty(detailId, value) {
    if (!ensureBillEditable()) return;

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
    if (!ensureBillEditable()) return;

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
    if (!state.currentBill || !state.currentBill.tableId) return;
    if (!ensureBillEditable()) return;

    const buttonText = els.sendOrderButton.textContent;
    els.sendOrderButton.disabled = true;
    els.sendOrderButton.textContent = "جاري الإرسال";

    try {
        await request(`/api/bills/${state.currentBill.transactionId}/send-order`, {
            method: "POST",
            body: { tableId: state.currentBill.tableId }
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

async function saveBill() {
    if (!state.currentBill) return;
    if (!ensureBillCanBeSaved()) return;

    const items = state.currentBill.items || [];
    if (items.length === 0) {
        showToast("لا توجد أصناف في الفاتورة الحالية", "error");
        return;
    }

    if (shouldAskForPaymentMethod() && state.paymentMethods.length > 1) {
        openPaymentDialog();
        return;
    }

    await submitBillSave(getDefaultPaymentMethod());
}

async function submitBillSave(paymentMethod) {
    const buttonText = els.sendOrderButton.textContent;
    els.sendOrderButton.disabled = true;
    els.sendOrderButton.textContent = "جاري الحفظ";

    try {
        const body = buildSaveBillRequest(paymentMethod);
        const result = await request(`/api/bills/${state.currentBill.transactionId}/save`, {
            method: "POST",
            body
        });

        els.sendOrderButton.textContent = buttonText;
        closePaymentDialog();
        resetCurrentBill();
        await loadTables();
        renderItems();
        showToast(result.message || "تم حفظ الفاتورة", "success");
    } catch (error) {
        els.sendOrderButton.textContent = buttonText;
        renderBill();
        showToast(error.message || "تعذر حفظ الفاتورة", "error");
    }
}

function shouldAskForPaymentMethod() {
    const bill = state.currentBill;
    if (!bill) return false;
    if (state.defaultAgentId > 0 && Number(bill.agentId || 0) > 0 && Number(bill.agentId) !== Number(state.defaultAgentId)) return false;

    return !(bill.tableId && !isCurrentTableCash(bill));
}

function getDefaultPaymentMethod() {
    return state.paymentMethods.find(method => Number(method.paymentId) === 1) || {
        paymentId: 1,
        paymentName: "نقدا",
        treasuryId: state.user?.treasuryId || 0,
        treasuryName: ""
    };
}

function buildSaveBillRequest(paymentMethod) {
    const payment = paymentMethod || getDefaultPaymentMethod();
    const body = {
        payId: Number(payment.paymentId || 1)
    };

    const treasuryId = Number(payment.treasuryId || state.user?.treasuryId || 0);
    if (treasuryId > 0) {
        body.treasuryId = treasuryId;
    }

    return body;
}

function openPaymentDialog() {
    const bill = state.currentBill;
    if (!bill) return;

    const defaultPayment = getDefaultPaymentMethod();
    state.selectedPaymentId = Number(defaultPayment.paymentId || 1);
    els.paymentAmountText.textContent = formatNumber(bill.pure || 0);
    renderPaymentOptions();
    els.paymentDialog.classList.remove("hidden");
}

function closePaymentDialog() {
    state.selectedPaymentId = 0;
    els.paymentDialog.classList.add("hidden");
    els.paymentOptions.innerHTML = "";
    els.paymentTreasuryText.textContent = "-";
}

function renderPaymentOptions() {
    els.paymentOptions.innerHTML = "";

    const methods = state.paymentMethods.length > 0 ? state.paymentMethods : [getDefaultPaymentMethod()];
    if (!methods.some(method => Number(method.paymentId) === Number(state.selectedPaymentId))) {
        state.selectedPaymentId = Number(methods[0].paymentId || 1);
    }

    methods.forEach(method => {
        const paymentId = Number(method.paymentId || 0);
        const button = document.createElement("button");
        button.type = "button";
        button.className = "payment-option";
        if (paymentId === Number(state.selectedPaymentId)) button.classList.add("active");
        button.innerHTML = `
            <strong>${escapeHtml(method.paymentName || "نقدا")}</strong>
            <small>${escapeHtml(method.treasuryName || "الخزينة الافتراضية")}</small>
        `;
        button.addEventListener("click", () => {
            state.selectedPaymentId = paymentId;
            renderPaymentOptions();
        });
        els.paymentOptions.appendChild(button);
    });

    const selected = getSelectedPaymentMethod();
    const lockText = selected?.isLocked ? " / مقفلة" : "";
    els.paymentTreasuryText.textContent = selected?.treasuryName ? `${selected.treasuryName}${lockText}` : "الخزينة الافتراضية";
}

function getSelectedPaymentMethod() {
    return state.paymentMethods.find(method => Number(method.paymentId) === Number(state.selectedPaymentId)) || getDefaultPaymentMethod();
}

async function confirmPaymentAndSave() {
    if (!state.currentBill) return;
    if (!ensureBillCanBeSaved()) return;

    setBusy(els.paymentDialog, true);
    try {
        await submitBillSave(getSelectedPaymentMethod());
    } finally {
        setBusy(els.paymentDialog, false);
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
        els.billItems.textContent = canOpenDirectBill() ? "اختر طاولة أو افتح فاتورة بدون طاولة" : "اختر طاولة لفتح الفاتورة";
        els.changeBillTypeButton.disabled = true;
        els.sendOrderButton.disabled = true;
        syncBillEntryControls();
        setTotals();
        return;
    }

    const editable = canEditCurrentBill();
    els.billTitle.textContent = getBillTitle(bill);
    els.billMeta.innerHTML = buildBillMetaHtml(bill);
    els.changeBillTypeButton.disabled = !editable;
    els.sendOrderButton.textContent = getSaveButtonText(bill);
    els.sendOrderButton.disabled = !canSaveCurrentBill() || (bill.items || []).length === 0;
    syncBillEntryControls();

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
            row.querySelectorAll("button").forEach(button => {
                button.disabled = !editable;
            });
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

function canEditCurrentBill() {
    const bill = state.currentBill;
    return !!bill && !bill.isVoid && !bill.isPaid && !bill.isDepended && !bill.isOrdered;
}

function canSaveCurrentBill() {
    const bill = state.currentBill;
    return !!bill && !bill.isVoid && !bill.isPaid && !bill.isDepended;
}

function ensureBillEditable() {
    if (canEditCurrentBill()) return true;

    showToast(getBillLockedMessage(state.currentBill), "error");
    renderBill();
    renderItems();
    return false;
}

function ensureBillCanBeSaved() {
    if (canSaveCurrentBill()) return true;

    showToast(getBillSaveLockedMessage(state.currentBill), "error");
    renderBill();
    renderItems();
    return false;
}

function syncBillEntryControls() {
    const editable = canEditCurrentBill();
    els.barcodeInput.disabled = !editable;
    els.barcodeAddButton.disabled = !editable;
}

function getBillLockedMessage(bill) {
    if (!bill) return "اختر طاولة قبل الإجراء";
    if (bill.isVoid) return "الفاتورة ملغية ولا يمكن تعديلها";
    if (bill.isPaid) return "الفاتورة مدفوعة ولا يمكن تعديلها";
    if (bill.isDepended || bill.isOrdered) return "تم إرسال أو اعتماد الطلب ولا يمكن تعديله";
    return "الفاتورة غير قابلة للتعديل حاليا";
}

function getBillSaveLockedMessage(bill) {
    if (!bill) return "اختر طاولة أو افتح فاتورة قبل الحفظ";
    if (bill.isVoid) return "الفاتورة ملغية ولا يمكن حفظها";
    if (bill.isPaid) return "الفاتورة مدفوعة مسبقاً";
    if (bill.isDepended) return "الفاتورة معتمدة ولا يمكن تعديلها";
    return "الفاتورة غير قابلة للحفظ حالياً";
}

function getBillStatusText(bill) {
    if (bill.isVoid) return "ملغية";
    if (bill.isPaid) return "مدفوعة";
    if (bill.isDepended && bill.isOrdered) return "طلب مرسل ومعتمد";
    if (bill.isOrdered) return "طلب مرسل";
    if (bill.isDepended) return "معتمدة";
    return "قيد الإدخال";
}

function getBillStatusClass(bill) {
    if (bill.isVoid) return "status-void";
    if (bill.isPaid) return "status-paid";
    if (bill.isDepended || bill.isOrdered) return "status-locked";
    return "status-editable";
}

function getSaveButtonText(bill) {
    if (!bill) return "إرسال الطلب";
    if (bill.tableId && !isCurrentTableCash(bill)) return "إرسال الطلب";
    if (Number(bill.billTypeId || 0) === 3) return "إنتقال إلى التسليم";
    return "حفظ / إنهاء";
}

function isCurrentTableCash(bill) {
    if (!bill?.tableId) return false;
    if (bill.tableIsCash === true) return true;

    const table = state.tables.find(item => Number(item.tableId) === Number(bill.tableId));
    return Boolean(table?.isCash);
}

function getBillTitle(bill) {
    if (bill.tableName) return bill.tableName;
    if (state.selectedTable?.tableName) return state.selectedTable.tableName;
    return "فاتورة بدون طاولة";
}

function buildBillMetaHtml(bill) {
    return `
        <span>رقم يومي: ${bill.dailyBillNumber || "-"}</span>
        <span>رقم آلي: ${bill.salesBillId || bill.transactionId}</span>
        <span>نوع الفاتورة: ${escapeHtml(getBillTypeName(bill.billTypeId))}</span>
        <span class="bill-status ${getBillStatusClass(bill)}">${getBillStatusText(bill)}</span>
    `;
}

function getBillTypeName(billTypeId) {
    const typeId = Number(billTypeId || 0);
    const billType = state.billTypes.find(type => Number(type.billTypeId) === typeId);
    return billType?.billTypeName || (typeId ? `نوع ${typeId}` : "-");
}

function getDefaultDirectBillTypeId() {
    if (state.billTypes.some(type => Number(type.billTypeId) === 1)) return 1;

    const nonOrderType = state.billTypes.find(type => Number(type.billTypeId) !== 3);
    if (nonOrderType) return Number(nonOrderType.billTypeId);

    return Number(state.billTypes[0]?.billTypeId || 1);
}

function canOpenDirectBill() {
    return state.user?.canUseSalesPriceInfo === true;
}

function syncUserPermissions() {
    els.directBillButton.classList.toggle("hidden", !canOpenDirectBill());
}

function openBillTypeDialog() {
    if (!state.currentBill) {
        showToast("اختر فاتورة قبل تغيير النوع", "error");
        return;
    }

    if (!ensureBillEditable()) return;

    renderBillTypeOptions();
    els.billTypeDialog.classList.remove("hidden");
}

function renderBillTypeOptions() {
    els.billTypeOptions.innerHTML = "";

    if (state.billTypes.length === 0) {
        els.billTypeOptions.classList.add("empty-state");
        els.billTypeOptions.textContent = "لا توجد أنواع فواتير";
        return;
    }

    els.billTypeOptions.classList.remove("empty-state");
    const currentTypeId = Number(state.currentBill?.billTypeId || 0);
    state.billTypes.forEach(type => {
        const typeId = Number(type.billTypeId || 0);
        const button = document.createElement("button");
        button.className = "bill-type-option";
        if (typeId === currentTypeId) button.classList.add("active");
        button.type = "button";
        button.disabled = typeId === currentTypeId;
        button.textContent = type.billTypeName || `نوع ${typeId}`;
        button.addEventListener("click", () => updateBillType(typeId));
        els.billTypeOptions.appendChild(button);
    });
}

function closeBillTypeDialog() {
    els.billTypeDialog.classList.add("hidden");
    els.billTypeOptions.innerHTML = "";
}

async function updateBillType(typeId) {
    if (!state.currentBill) return;
    if (!ensureBillEditable()) return;

    const currentTypeId = Number(state.currentBill.billTypeId || 0);
    const orderBillTypeId = 3;
    const isMovingToOrFromOrder = currentTypeId === orderBillTypeId || Number(typeId) === orderBillTypeId;
    if (isMovingToOrFromOrder && (state.currentBill.items || []).length > 0) {
        showToast("لتحويل الفاتورة إلى طلبية أو من طلبية يجب حذف كل الأصناف الموجودة بها أولًا", "error");
        return;
    }

    setBusy(els.billTypeDialog, true);
    try {
        state.currentBill = await request(`/api/bills/${state.currentBill.transactionId}/type`, {
            method: "PATCH",
            body: { billTypeId: typeId }
        });

        closeBillTypeDialog();
        renderBill();
        renderItems();
        showToast("تم تغيير نوع الفاتورة", "success");
    } catch (error) {
        showToast(error.message || "تعذر تغيير نوع الفاتورة", "error");
    } finally {
        setBusy(els.billTypeDialog, false);
    }
}

function getDefaultUnit(itemId) {
    const units = state.units.filter(unit => unit.itemId === itemId);
    return units.find(unit => unit.isDefault) || units[0] || null;
}

function openItemEdit(item) {
    if (!ensureBillEditable()) return;

    const units = state.units.filter(unit => unit.itemId === item.itemId);
    if (units.length === 0) {
        showToast("لا توجد وحدات لهذا الصنف", "error");
        return;
    }

    state.editingItem = item;
    state.selectedUnitId = Number(item.unitId || 0);
    state.selectedSalesTypeId = 0;
    state.componentTab = "notes";
    state.componentOptions = { notes: [], adds: [] };
    state.selectedComponents = [];

    els.itemEditTitle.textContent = item.itemName || "تعديل الصنف";
    renderUnitButtons(units);
    els.editNotesInput.value = item.notes || "";
    renderSalesTypeButtons();
    els.itemEditDialog.classList.remove("hidden");
    const activeUnitButton = els.editUnitButtons.querySelector(".unit-button.active");
    if (activeUnitButton) activeUnitButton.focus();
}

function closeItemEdit() {
    closeComponentDialog();
    state.editingItem = null;
    state.selectedUnitId = 0;
    state.selectedSalesTypeId = 0;
    state.componentTab = "notes";
    state.componentOptions = { notes: [], adds: [] };
    state.selectedComponents = [];
    els.itemEditDialog.classList.add("hidden");
    els.editUnitButtons.innerHTML = "";
    els.editSalesTypeButtons.innerHTML = "";
    els.editNotesInput.value = "";
    renderComponentOptions();
    renderSelectedComponents();
}

async function saveItemEdit() {
    const item = state.editingItem;
    if (!item) return;
    if (!ensureBillEditable()) return;

    const unitId = Number(state.selectedUnitId || 0);
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
                salesTypeId: state.selectedSalesTypeId || 0
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

function renderUnitButtons(units) {
    els.editUnitButtons.innerHTML = "";

    if (!units || units.length === 0) return;

    if (!units.some(unit => Number(unit.unitId) === Number(state.selectedUnitId))) {
        state.selectedUnitId = Number(units[0].unitId || 0);
    }

    units.forEach(unit => {
        const button = document.createElement("button");
        button.type = "button";
        button.className = "unit-button";
        if (Number(unit.unitId) === Number(state.selectedUnitId)) button.classList.add("active");
        button.innerHTML = `
            <strong>${escapeHtml(unit.unitName || "")}</strong>
            <small>${formatNumber(unit.price)}</small>
        `;
        button.addEventListener("click", () => {
            state.selectedUnitId = Number(unit.unitId || 0);
            renderUnitButtons(units);
        });
        els.editUnitButtons.appendChild(button);
    });
}

function openComponentDialog() {
    const item = state.editingItem;
    if (!item) return;

    state.componentTab = "notes";
    state.componentOptions = { notes: [], adds: [] };
    state.selectedComponents = [];
    els.componentDialogTitle.textContent = item.itemName ? `ملاحظات وإضافات ${item.itemName}` : "ملاحظات وإضافات الصنف";
    renderComponentTabs();
    renderComponentOptions();
    renderSelectedComponents();
    els.componentDialog.classList.remove("hidden");
    loadItemComponentData(item);
}

function closeComponentDialog() {
    els.componentDialog.classList.add("hidden");
}

function getAvailableSalesTypes() {
    const types = [{ id: 0, name: "مبيعات قطاعي" }];
    if (state.user?.isMinSalesPriceEnabled) {
        if (state.user?.canSellWholesale) {
            types.push({ id: 1, name: "مبيعات جملة" });
        }

        if (state.user?.canSellWholesale2) {
            types.push({ id: 2, name: "مبيعات جملة الجملة" });
        }
    }

    return types;
}

function renderSalesTypeButtons() {
    els.editSalesTypeButtons.innerHTML = "";

    getAvailableSalesTypes().forEach(type => {
        const button = document.createElement("button");
        button.type = "button";
        button.className = "sales-type-button";
        if (Number(type.id) === Number(state.selectedSalesTypeId)) button.classList.add("active");
        button.textContent = type.name;
        button.addEventListener("click", () => {
            state.selectedSalesTypeId = type.id;
            renderSalesTypeButtons();
        });
        els.editSalesTypeButtons.appendChild(button);
    });
}

function renderComponentTabs() {
    els.componentNotesTab.classList.toggle("active", state.componentTab === "notes");
    els.componentAddsTab.classList.toggle("active", state.componentTab === "adds");
}

function setComponentTab(tabName) {
    state.componentTab = tabName;
    renderComponentTabs();
    renderComponentOptions();
}

async function loadItemComponentData(item) {
    try {
        els.componentOptions.textContent = "جاري تحميل الخيارات";
        els.selectedComponents.textContent = "جاري تحميل المحدد";

        const [notes, adds, selected] = await Promise.all([
            request(`/api/bills/items/${item.detailId}/component-options?isAdd=false`),
            request(`/api/bills/items/${item.detailId}/component-options?isAdd=true`),
            request(`/api/bills/items/${item.detailId}/components`)
        ]);

        state.componentOptions = {
            notes: notes || [],
            adds: adds || []
        };
        state.selectedComponents = selected || [];
        renderComponentOptions();
        renderSelectedComponents();
    } catch (error) {
        showToast(error.message || "تعذر تحميل الملاحظات والإضافات", "error");
        state.componentOptions = { notes: [], adds: [] };
        state.selectedComponents = [];
        renderComponentOptions();
        renderSelectedComponents();
    }
}

function renderComponentOptions() {
    const options = state.componentTab === "adds" ? state.componentOptions.adds : state.componentOptions.notes;
    els.componentOptions.innerHTML = "";

    if (!options || options.length === 0) {
        els.componentOptions.className = "component-options empty-state";
        els.componentOptions.textContent = state.componentTab === "adds" ? "لا توجد إضافات" : "لا توجد ملاحظات";
        return;
    }

    els.componentOptions.className = "component-options";
    options.forEach(option => {
        const button = document.createElement("button");
        button.type = "button";
        button.className = option.isAdd ? "component-option add" : "component-option note";
        button.innerHTML = `
            <span>${escapeHtml(option.componentName)}</span>
            <small>${formatNumber(option.price)}</small>
        `;
        button.addEventListener("click", () => addItemComponent(option.componentId));
        els.componentOptions.appendChild(button);
    });
}

function renderSelectedComponents() {
    els.selectedComponents.innerHTML = "";
    els.clearComponentsButton.disabled = state.selectedComponents.length === 0;

    if (state.selectedComponents.length === 0) {
        els.selectedComponents.className = "selected-components empty-state";
        els.selectedComponents.textContent = "لا توجد ملاحظات أو إضافات";
        return;
    }

    els.selectedComponents.className = "selected-components";
    state.selectedComponents.forEach(component => {
        const row = document.createElement("div");
        row.className = "selected-component-row";
        row.innerHTML = `
            <strong>${escapeHtml(component.componentName)}</strong>
            <span>${formatNumber(component.quantity)}</span>
            <div>
                <button type="button" data-action="plus">+</button>
                <button type="button" data-action="minus">-</button>
                <button type="button" data-action="delete">حذف</button>
            </div>
        `;

        row.querySelector('[data-action="plus"]').addEventListener("click", () => changeItemComponentQuantity(component.lineId, 1));
        row.querySelector('[data-action="minus"]').disabled = Number(component.quantity || 0) <= 1;
        row.querySelector('[data-action="minus"]').addEventListener("click", () => changeItemComponentQuantity(component.lineId, -1));
        row.querySelector('[data-action="delete"]').addEventListener("click", () => deleteItemComponent(component.lineId));
        els.selectedComponents.appendChild(row);
    });
}

async function addItemComponent(componentId) {
    const item = state.editingItem;
    if (!item || !ensureBillEditable()) return;

    setBusy(els.componentDialog, true);
    try {
        state.selectedComponents = await request(`/api/bills/items/${item.detailId}/components`, {
            method: "POST",
            body: { componentId }
        });
        await refreshCurrentBill();
        renderSelectedComponents();
        showToast("تمت إضافة الخيار للصنف", "success");
    } catch (error) {
        showToast(error.message || "تعذر إضافة الخيار للصنف", "error");
    } finally {
        setBusy(els.componentDialog, false);
        renderComponentOptions();
        renderSelectedComponents();
    }
}

async function changeItemComponentQuantity(componentLineId, changeBy) {
    const item = state.editingItem;
    if (!item || !ensureBillEditable()) return;

    setBusy(els.componentDialog, true);
    try {
        state.selectedComponents = await request(`/api/bills/items/${item.detailId}/components/${componentLineId}/qty`, {
            method: "PATCH",
            body: { changeBy }
        });
        await refreshCurrentBill();
        renderSelectedComponents();
    } catch (error) {
        showToast(error.message || "تعذر تعديل كمية الخيار", "error");
    } finally {
        setBusy(els.componentDialog, false);
        renderComponentOptions();
        renderSelectedComponents();
    }
}

async function deleteItemComponent(componentLineId) {
    const item = state.editingItem;
    if (!item || !ensureBillEditable()) return;

    setBusy(els.componentDialog, true);
    try {
        state.selectedComponents = await request(`/api/bills/items/${item.detailId}/components/${componentLineId}`, {
            method: "DELETE"
        });
        await refreshCurrentBill();
        renderSelectedComponents();
    } catch (error) {
        showToast(error.message || "تعذر حذف الخيار", "error");
    } finally {
        setBusy(els.componentDialog, false);
        renderComponentOptions();
        renderSelectedComponents();
    }
}

async function clearItemComponents() {
    const item = state.editingItem;
    if (!item || !ensureBillEditable()) return;
    if (state.selectedComponents.length === 0) return;

    setBusy(els.componentDialog, true);
    try {
        state.selectedComponents = await request(`/api/bills/items/${item.detailId}/components`, {
            method: "DELETE"
        });
        await refreshCurrentBill();
        renderSelectedComponents();
        showToast("تم إلغاء كل الخيارات", "success");
    } catch (error) {
        showToast(error.message || "تعذر إلغاء الخيارات", "error");
    } finally {
        setBusy(els.componentDialog, false);
        renderComponentOptions();
        renderSelectedComponents();
    }
}

async function refreshCurrentBill() {
    if (!state.currentBill?.transactionId) return;
    state.currentBill = await request(`/api/bills/${state.currentBill.transactionId}`);
    renderBill();
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
