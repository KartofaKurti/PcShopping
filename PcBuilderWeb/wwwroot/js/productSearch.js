$(document).ready(function () {
    let currentPage = 1;
    const pageSize = 10;

    function performSearch() {
        const name = $('#searchName').val();
        const manufacturerId = $('#manufacturerFilter').val();
        const categoryId = $('#categoryFilter').val();
        const minPrice = $('#minPrice').val();
        const maxPrice = $('#maxPrice').val();

        $.ajax({
            url: '/api/products/search',  // Call to the API controller
            method: 'GET',
            data: {
                name: name,
                manufacturerId: manufacturerId,
                categoryId: categoryId,
                minPrice: minPrice,
                maxPrice: maxPrice,
                page: currentPage,
                pageSize: pageSize
            },
            success: function (data) {
                updateProductList(data);
                updatePagination(data);
            },
            error: function () {
                alert('Error fetching search results.');
            }
        });
    }

    function updateProductList(data) {
        const productContainer = $('#productList');
        productContainer.empty();

        if (data.products.length > 0) {
            data.products.forEach(product => {
                productContainer.append(`
                    <div class="product-item">
                        <img src="${product.imageUrl}" alt="${product.productName}" />
                        <h3>${product.productName}</h3>
                        <p>${product.productDescription}</p>
                        <p>Price: $${product.productPrice}</p>
                    </div>
                `);
            });
        } else {
            productContainer.append('<p>No products found.</p>');
        }
    }

    function updatePagination(data) {
        const paginationContainer = $('#pagination');
        paginationContainer.empty();

        for (let i = 1; i <= data.totalPages; i++) {
            const activeClass = i === data.currentPage ? 'active' : '';
            paginationContainer.append(`
                <li class="page-item ${activeClass}">
                    <a class="page-link" href="#" onclick="changePage(${i})">${i}</a>
                </li>
            `);
        }
    }

    window.changePage = function (page) {
        currentPage = page;
        performSearch();
    };

    $('#searchForm').on('submit', function (e) {
        e.preventDefault();
        currentPage = 1;
        performSearch();
    });

    performSearch();
});
