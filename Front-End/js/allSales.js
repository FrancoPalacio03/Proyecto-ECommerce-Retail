const url = "https://localhost:7021/api/SaleControler/";
const productURL = "https://localhost:7021/api/ProductControler/";

document.addEventListener('DOMContentLoaded', () => {
    const today = new Date().toISOString().split('T')[0];
    const fromDateInput = document.getElementById('from-date');
    const toDateInput = document.getElementById('to-date');

    fromDateInput.value = today;
    toDateInput.value = today;
    fromDateInput.max = today;
    toDateInput.max = today;

    loadSales(today, today);

    fromDateInput.addEventListener('change', () => {
        const fromDate = fromDateInput.value;
        const toDate = toDateInput.value;
        if (toDate < fromDate) {
            toDateInput.value = fromDate;
        }
        loadSales(fromDate, toDate);
    });

    toDateInput.addEventListener('change', () => {
        const fromDate = fromDateInput.value;
        const toDate = toDateInput.value;
        if (toDate < fromDate) {
            fromDateInput.value = toDate;
        }
        loadSales(fromDate, toDate);
    });
});

async function loadSaleDetails(saleData) {
    try {
        const response = await fetch(url + saleData.id);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const sale = await response.json();
        renderCartDetails(sale);
    } catch (error) {
        console.error("Error fetching sales data: ", error);
    }
}

async function loadSales(fromDate, toDate) {
    const noResults = document.getElementById('no-results');
    noResults.style.display = 'none';
    try {
        const response = await fetch(`${url}?from=${fromDate}&to=${toDate}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        const sales = await response.json();
        if (sales.length === 0) {
            noResults.style.display = 'block';
        }
        renderSales(sales.reverse());
    } catch (error) {
        console.error("Error fetching sales data: ", error);
    }
}

function renderSales(sales) {
    const saleContainer = document.getElementById('cards-container');
    saleContainer.innerHTML = '';
    
    sales.forEach(sale => {  
        const saleCard = `
            <div class="card-container" data-sale='${JSON.stringify(sale)}'>
                <div class="card-details">
                    <h1 class="card-title">SALE ID:${sale.id}</h1>
                    <div class="pricing">
                        <p class="price">Total Payed: $${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(sale.totalPay)}</p>       
                        <p class="price">Products total quantity: ${sale.totalQuantity}</p>
                    </div>
                    <p class="price">Date: ${new Date(sale.date).toLocaleDateString()}</p>
                </div>
            </div>
        `;
        saleContainer.insertAdjacentHTML('beforeend', saleCard);
    });

    document.querySelectorAll('.card-container').forEach(card => {
        card.addEventListener('click', (event) => {
            const saleData = JSON.parse(card.getAttribute('data-sale'));
            loadSaleDetails(saleData);
        });
    });
}

async function renderCartDetails(sale) {
    const tableBody = document.querySelector('#cart-details-body');
    tableBody.innerHTML = '';
    let itemCount = 0;
    
    for (const product of sale.products) {
        const response = await fetch(productURL + product.productId);
        const productObtainer = await response.json();
        itemCount++;
        const row = `
            <tr>
                <td>${itemCount.toString().padStart(2, '0')}</td>
                <td>${productObtainer.name}</td>
                <td>$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(product.price)}</td>
                <td>${product.quantity}</td>
                <td class="text-end">$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(product.price * product.quantity)}</td>
            </tr>
        `;
        tableBody.insertAdjacentHTML('beforeend', row);
    }

    document.getElementById('subtotal-details').textContent = `$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(sale.subtotal)}`;
    document.getElementById('discount-details').textContent = `-$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(sale.totalDiscount)}`;
    document.getElementById('tax-details').textContent = `$${new Intl.NumberFormat('en-US', { maximumFractionDigits: 2 }).format(sale.taxes)}`;
    document.getElementById('total-details').textContent = `$${new Intl.NumberFormat('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(sale.totalPay)}`;

    const modal = new bootstrap.Modal(document.getElementById('saleDetailsModal'));
    modal.show();
}
