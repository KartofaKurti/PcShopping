document.querySelectorAll('.hardDeleteButton').forEach(button => {
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
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {   
                const deleteModal = document.getElementById('deleteModal');
                const modalInstance = bootstrap.Modal.getInstance(deleteModal);
                modalInstance.hide();

                window.location.href = '/Product/Index';
            } else {
                console.error("Error deleting product:", data.message);
            }
        })
        .catch(error => {
            console.error("Error:", error);
        });
});
