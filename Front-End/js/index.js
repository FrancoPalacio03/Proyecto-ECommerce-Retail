let currentPage = 1;
const productsPerPage = 6;

document.addEventListener('DOMContentLoaded', () => {
  loadProducts(currentPage);

  document.getElementById('apply-filters-btn').addEventListener('click', () => {
    loadProducts(currentPage);
  });

  document.getElementById('search-form').addEventListener('submit', (e) => {
    e.preventDefault();
    currentPage = 1;
    loadProducts(currentPage);
  });

  document.getElementById('prev-page').addEventListener('click', () => {
    if (currentPage > 1) {
      currentPage--;
      loadProducts(currentPage);
    }
  });

  document.getElementById('next-page').addEventListener('click', () => {
    currentPage++;
    loadProducts(currentPage);
  });
});

async function loadProducts(page) {
  let selectedCategories = [];
  document.querySelectorAll('.category-checkbox:checked').forEach(checkbox => {
    selectedCategories.push(checkbox.value);
    console.log(selectedCategories)
  });

  let categoriesQueryString = ''
  if(selectedCategories!=0){
    categoriesQueryString = selectedCategories.map(category => `categories=${category}`).join('&');
  }
  const searchQuery = document.getElementById('search-input').value.trim();
  const offset = (page - 1) * productsPerPage;
  const url = searchQuery
    ? `https://localhost:7021/api/ProductControler?${categoriesQueryString}&name=${encodeURIComponent(searchQuery)}&limit=${productsPerPage}&offset=${offset}`
    : `https://localhost:7021/api/ProductControler?${categoriesQueryString}&limit=${productsPerPage}&offset=${offset}`;

  console.log(url)
  const loader = document.getElementById('loader');
  const noResults = document.getElementById('no-results');
  const productsContainer = document.getElementById('products-container');
  const paginationContainer = document.getElementById('pagination-container');

  loader.style.display = 'block';
  noResults.style.display = 'none';
  paginationContainer.style.display = 'block';

  try {
    const response = await fetch(url);
    const products = await response.json();

    loader.style.display = 'none';
    productsContainer.innerHTML = '';

    if (products.length > 0) {
      renderProducts(products);
      document.getElementById('page-info').textContent = `Page ${page}`;
      document.getElementById('prev-page').disabled = (page === 1);
    } else {
      if (page > 1) {
        currentPage--;
        loadProducts(currentPage);
      } else {
        noResults.style.display = 'block';
        paginationContainer.style.display = 'none';       
      }
      
    }
  } catch (error) {
    console.error('Error fetching products:', error);
    loader.style.display = 'none';
  }
}

function renderProducts(products) {
  const productContainer = document.getElementById('products-container');
  productContainer.innerHTML = '';
  products.forEach(product => {
    const productCard = `
      <div class="product-container" data-product='${JSON.stringify(product)}'>
        <img src="${product.imageUrl}" alt="${product.name}" class="product-image">
        <div class="product-details">
          <h1 class="product-title">${product.name}</h1>
          <div class="pricing">
            <strong>Precio Total:</strong>
            <p class="price">$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(product.price)}</p>
            <strong>Precio Especial Con Descuento:</strong>
            <p class="price">$${new Intl.NumberFormat('en-ES', { maximumSignificantDigits: 3 }).format(product.price - ((product.discount * product.price) / 100))}</p>
          </div>
          <button class="delivery-button">Llega mañana</button>
        </div>
      </div>
    `;
    productContainer.insertAdjacentHTML('beforeend', productCard);
  });

  document.querySelectorAll('.product-container').forEach(card => {
    card.addEventListener('click', (event) => {
      const productData = JSON.parse(card.getAttribute('data-product'));
      showProductDetails(productData);
    });
  });
}
