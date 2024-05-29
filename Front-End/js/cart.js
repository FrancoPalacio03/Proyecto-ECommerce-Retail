const btnCart = document.querySelector('.container-cart-icon');
const containerCartProducts = document.querySelector('.container-cart-products');

btnCart.addEventListener('click', () => {
    containerCartProducts.classList.toggle('hidden-cart');
});

/* ========================= */

// Variable para almacenar los productos en el carrito
let cartProducts = [];

const saleBtn = document.querySelector('.sale-btn-container');
const valorTotal = document.querySelector('.total-pagar');
const countProducts = document.querySelector('#contador-productos');
const cartEmpty = document.querySelector('.cart-empty');
const cartTotal = document.querySelector('.cart-total');
const rowProduct = document.querySelector('.row-product');


function addToCart(product, quantity) {
    const infoProduct = {
        product : product, 
        quantity: parseInt(quantity), 
    };


    // Verificar si el producto ya existe en el carrito
    const exists = cartProducts.some(item => item.product.id === infoProduct.product.id);
    if (exists) {
        // Si el producto ya existe, aumentar la cantidad
        cartProducts.forEach(item => {
            if(item.product.id === infoProduct.product.id){
            item.quantity += infoProduct.quantity;
        }            
        });
    } else {
        // Si el producto no existe, añadirlo al carrito
        cartProducts.push(infoProduct);
    }

    // Actualizar la interfaz del carrito
    updateCart(); // Llamar a updateCart() para reflejar los cambios en la interfaz
}

// Función para eliminar un producto del carrito
function removeFromCart(title) {
    cartProducts = cartProducts.filter(item => item.product.name !== title); 
    updateCart();
}

// Función para mostrar el contenido del carrito en la interfaz
function updateCart() {
    console.log(cartProducts)
    if (cartProducts.length === 0) {
        cartEmpty.classList.remove('hidden');
        rowProduct.innerHTML = ''; 
        cartTotal.classList.add('hidden');
        saleBtn.classList.add('hidden')
    } else {
        cartEmpty.classList.add('hidden');
        cartTotal.classList.remove('hidden');
        saleBtn.classList.remove('hidden');

        let total = 0;
        let totalOfProducts = 0;

        rowProduct.innerHTML = '';
        cartProducts.forEach(productToShow => {

            const containerProduct = document.createElement('div');
            containerProduct.classList.add('cart-product');
            containerProduct.innerHTML = `
            <div class="info-cart-product">
            <span class="cantidad-producto-carrito">${productToShow.quantity}x </span>
            <p class="titulo-producto-carrito">${productToShow.product.name}</p>
            <span class="precio-producto-carrito">$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(productToShow.product.price)}  </span>
        </div>
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" class="icon-close cursor-pointer">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" data-title="${productToShow.product.Name}" />
        </svg>       
            `;
            rowProduct.appendChild(containerProduct);

            total += parseInt(productToShow.quantity) * parseFloat(productToShow.product.price);
        });

        valorTotal.innerText = `$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(total)}`;
            
        
        
    }
    countProducts.innerText = cartProducts.length;
}

rowProduct.addEventListener('click', e => {
    if (e.target.classList.contains('icon-close')) {
        const product = e.target.parentElement;
        const title = product.querySelector('p').textContent;
        console.log(product);
        removeFromCart(title);
    }
});



