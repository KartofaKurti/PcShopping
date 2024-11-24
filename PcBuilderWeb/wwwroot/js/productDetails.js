document.addEventListener("DOMContentLoaded", function () {
    const currentPage = new URLSearchParams(window.location.search).get('page');
    const productId = document.getElementById('backToProducts').getAttribute('data-product-id');

    if (currentPage) {
        localStorage.setItem('returnPage', currentPage);
        localStorage.setItem('productId', productId);
    }

    document.getElementById('backToProducts')?.addEventListener('click', function () {
        const page = localStorage.getItem('returnPage') || 1; 
        const productId = localStorage.getItem('productId'); 

        window.location.href = `/Product/Index?page=${page}&productId=${productId}`;
    });
});
