document.querySelectorAll('.toggleVisibility').forEach(button => {
    button.addEventListener('click', function (event) {
        event.preventDefault();

        const productId = this.getAttribute('data-product-id');

        fetch(`/api/products/togglevisibility/${productId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    if (button.textContent === 'Hide') {
                        button.textContent = 'Show';
                        button.classList.remove('btn-danger');
                        button.classList.add('btn-success');
                    } else {
                        button.textContent = 'Hide';
                        button.classList.remove('btn-success');
                        button.classList.add('btn-danger');
                    }
                } else {
                    console.error("Error toggling product visibility:", data.message);
                }
            })
            .catch(error => {
                console.error("Error:", error);
            });
    });
});