document.addEventListener("DOMContentLoaded", function () {
    // Retrieve the page and productId from the URL
    const currentPage = new URLSearchParams(window.location.search).get('page');
    const productId = document.getElementById('backToProducts').getAttribute('data-product-id');

    if (currentPage) {
        // Store the page and productId in localStorage
        localStorage.setItem('returnPage', currentPage);
        localStorage.setItem('productId', productId);
    }

    // Handle the back button click
    document.getElementById('backToProducts')?.addEventListener('click', function () {
        const page = localStorage.getItem('returnPage') || 1; // Default to page 1
        const productId = localStorage.getItem('productId'); // Retrieve the stored productId

        // Redirect to Product Index with the page and optionally the productId
        window.location.href = `/Product/Index?page=${page}&productId=${productId}`;
    });
});
