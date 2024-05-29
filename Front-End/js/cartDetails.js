document.getElementById('sale-btn').addEventListener('click', function() {
    // Obtener referencia al cuerpo de la tabla
    const tableBody = document.querySelector('#cart-details-body');
    
    // Limpiar el contenido anterior de la tabla
    tableBody.innerHTML = '';

    let subTotal = 0;
    let totalDiscount = 0;
    let itemCount = 0;

    // Iterar sobre los productos en el carrito y agregar filas a la tabla
    cartProducts.forEach(item => {
        itemCount++;
        const total = item.product.price * item.quantity;
        subTotal += total;
        totalDiscount += (item.product.discount * item.product.price / 100) * item.quantity;

        const row = `
            <tr>
                <td>${itemCount.toString().padStart(2, '0')}</td>
                <td>${item.product.name}</td>
                <td>$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(item.product.price)}</td>
                <td>${item.quantity}</td>
                <td class="text-end">$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(total)}</td>
            </tr>
        `;
        tableBody.insertAdjacentHTML('beforeend', row);
    });

    // Calcular impuestos y totales
    const tax = subTotal * 0.21;
    const total = subTotal + tax - totalDiscount;

    // Actualizar los valores en la tabla
    document.getElementById('subtotal-details').textContent = `$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(subTotal)}`;
    document.getElementById('discount-details').textContent = `-$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(totalDiscount)}`;
    document.getElementById('tax-details').textContent = `$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(tax)}`;
    document.getElementById('total-details').textContent = `$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(total)}`;

    // Mostrar el modal
    const modal = new bootstrap.Modal(document.getElementById('cartDetailsModal'));
    modal.show();
});

// Agregar evento al botón "Check out"
document.getElementById('checkout-btn').addEventListener('click', function() {


    // Crear objeto con la lista de productos y el total a pagar
    const saleDetails = {
        products: cartProducts.map(item => ({
            id: item.product.id,
            quantity: item.quantity
        })),
        total: parseFloat(document.getElementById('total-details').textContent.replace('$', ''))
    };

    // Imprimir el objeto en la consola
    console.log(saleDetails);
});


