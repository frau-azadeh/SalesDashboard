let invoiceItemsList = [];

document.addEventListener("DOMContentLoaded", () => {
    loadCustomers();
    loadProducts();

    const invoiceForm = document.getElementById("invoiceForm");
    const productSelect = document.getElementById("productSelect");
    const customerSelect = document.getElementById("customerSelect");
    const quantityInput = document.getElementById("quantity");
    const invoiceItemsTable = document.getElementById("invoiceItems");

    // ثبت نهایی فاکتور
    invoiceForm.addEventListener("submit", async (e) => {
        e.preventDefault();

        const customerId = parseInt(customerSelect.value);
        if (!customerId || invoiceItemsList.length === 0) {
            showToast("لطفاً مشتری و اقلام را وارد کنید", "#f59e0b");
            return;
        }

        const productIds = invoiceItemsList.map(p => p.productId);
        const quantities = invoiceItemsList.map(p => p.quantity);

        try {
            const response = await axios.post("/api/invoice", {
                customerId,
                productIds,
                quantities
            });

            showToast(response.data.message || "فاکتور ثبت شد", "green");
            setTimeout(() => location.reload(), 2000);
        } catch (err) {
            const msg = err.response?.data || err.message || "خطا در ثبت فاکتور";
            showToast("❌ " + msg, "red");
        }
    });

    // نمایش قیمت از data-price
    productSelect.addEventListener("change", () => {
        const selectedOption = productSelect.options[productSelect.selectedIndex];
        const unitPrice = selectedOption.getAttribute("data-price");
        document.getElementById("unitPrice").value = unitPrice ? parseInt(unitPrice).toLocaleString() : "";
    });

    // افزودن محصول به فاکتور
    window.addToInvoice = () => {
        const productId = parseInt(productSelect.value);
        const productName = productSelect.options[productSelect.selectedIndex].text;
        const unitPrice = parseInt(productSelect.options[productSelect.selectedIndex].getAttribute("data-price"));
        const quantity = parseInt(quantityInput.value);

        if (!productId || !quantity || quantity < 1) {
            showToast("محصول یا تعداد معتبر نیست", "#f59e0b");
            return;
        }

        invoiceItemsList.push({ productId, quantity, unitPrice });

        invoiceItemsTable.innerHTML += `
            <tr class="text-right border-t">
                <td class="px-4 py-2">${productName}</td>
                <td class="px-4 py-2">${quantity}</td>
                <td class="px-4 py-2">${unitPrice.toLocaleString()} تومان</td>
                <td class="px-4 py-2">${(unitPrice * quantity).toLocaleString()} تومان</td>
            </tr>
        `;

        quantityInput.value = "";
        showToast("✅ محصول اضافه شد", "green");
    };
});

// بارگذاری مشتری‌ها
function loadCustomers() {
    axios.get("/api/customersapi")
        .then(res => {
            const select = document.getElementById("customerSelect");
            select.innerHTML = '<option value="">انتخاب مشتری</option>';
            res.data.forEach(c => {
                select.innerHTML += `<option value="${c.Id}">${c.FullName}</option>`;
            });
        })
        .catch(() => showToast("❌ خطا در بارگذاری مشتری‌ها", "red"));
}

// بارگذاری محصولات
function loadProducts() {
    axios.get("/api/productsapi")
        .then(res => {
            const select = document.getElementById("productSelect");
            select.innerHTML = '<option value="">انتخاب محصول</option>';
            res.data.forEach(p => {
                select.innerHTML += `<option value="${p.Id}" data-price="${p.Price}">${p.Name}</option>`;
            });
        })
        .catch(() => showToast("❌ خطا در بارگذاری محصولات", "red"));
}

// پیام هشدار
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
