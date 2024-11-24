document.querySelectorAll('[data-bs-target="#deleteModal"]').forEach(button => {
    button.addEventListener('click', function () {
        const productId = this.getAttribute('data-product-id');
        const confirmButton = document.getElementById('confirmDeleteButton');
        confirmButton.setAttribute('data-product-id', productId);
    });
});

document.getElementById('confirmDeleteButton').addEventListener('click', function () {
    const productId = this.getAttribute('data-product-id');

    fetch(`/api/ProductApi/DeleteProduct/${productId}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then(response => {
            if (response.ok) {
                const deleteModal = document.getElementById('deleteModal');
                const modalInstance = bootstrap.Modal.getInstance(deleteModal);
                modalInstance.hide();

                window.location.href = '/Product/Index';
            } else {
                return response.json().then(data => {
                    console.error("Failed to delete product:", data.message || 'Unknown error.');
                });
            }
        })
        .catch(error => {
            console.error("Error deleting product:", error);
        });
});