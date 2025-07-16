let invoiceItems = [];

document.addEventListener("DOMContentLoaded", () => {
    loadCustomers();
    loadProducts();

    // هنگام ثبت فرم
    document.getElementById("invoiceForm").addEventListener("submit", function (e) {
        e.preventDefault();
        submitInvoice();
    });

    // هنگام تغییر محصول، قیمت نمایش بده
    document.getElementById("productSelect").addEventListener("change", function () {
        const selected = this.options[this.selectedIndex];
        const price = selected.getAttribute("data-price");
        document.getElementById("unitPrice").value = price || "";
    });
});

// بارگذاری مشتری‌ها
function loadCustomers() {
    axios.get("/api/customers") // فرض بر اینه این api ساخته شده
        .then(res => {
            const select = document.getElementById("customerSelect");
            res.data.forEach(c => {
                select.innerHTML += `<option value="${c.id}">${c.name}</option>`;
            });
        })
        .catch(err => showToast("❌ خطا در بارگذاری مشتری", "red"));
}

// بارگذاری محصولات
function loadProducts() {
    axios.get("/api/products") // فرض بر اینه این api ساخته شده
        .then(res => {
            const select = document.getElementById("productSelect");
            res.data.forEach(p => {
                select.innerHTML += `<option value="${p.id}" data-price="${p.price}">${p.name}</option>`;
            });
        })
        .catch(err => showToast("❌ خطا در بارگذاری محصولات", "red"));
}

// افزودن به فاکتور
function addToInvoice() {
    const productSelect = document.getElementById("productSelect");
    const quantityInput = document.getElementById("quantity");
    const unitPriceInput = document.getElementById("unitPrice");

    const productId = productSelect.value;
    const productName = productSelect.options[productSelect.selectedIndex].text;
    const unitPrice = parseInt(unitPriceInput.value);
    const quantity = parseInt(quantityInput.value);

    if (!productId || quantity < 1) {
        showToast("لطفاً محصول و تعداد معتبر وارد کنید", "#f59e0b"); // زرد
        return;
    }

    invoiceItems.push({ productId, quantity, unitPrice });

    document.getElementById("invoiceItems").innerHTML += `
        <tr class="text-right">
            <td class="px-4 py-2">${productName}</td>
            <td class="px-4 py-2">${quantity}</td>
            <td class="px-4 py-2">${unitPrice.toLocaleString()} تومان</td>
            <td class="px-4 py-2">${(quantity * unitPrice).toLocaleString()} تومان</td>
        </tr>
    `;

    quantityInput.value = "";
    showToast("✅ محصول اضافه شد", "green");
}

// ارسال به API
function submitInvoice() {
    const customerId = document.getElementById("customerSelect").value;

    if (!customerId || invoiceItems.length === 0) {
        showToast("لطفاً مشتری و اقلام را وارد کنید", "#f59e0b");
        return;
    }

    const payload = {
        customerId: parseInt(customerId),
        productIds: invoiceItems.map(i => parseInt(i.productId)),
        quantities: invoiceItems.map(i => parseInt(i.quantity))
    };

    axios.post("/api/invoice", payload)
        .then(res => {
            showToast("✅ فاکتور ثبت شد", "green");
            setTimeout(() => location.reload(), 2000);
        })
        .catch(err => {
            const msg = err.response?.data || "❌ خطا در ثبت فاکتور";
            showToast(msg, "red");
        });
}

// Toastify پیام‌ها
function showToast(text, color) {
    Toastify({
        text,
        duration: 3000,
        gravity: "top",
        position: "right",
        backgroundColor: color,
        className: "font-vazir text-sm"
    }).showToast();
}
