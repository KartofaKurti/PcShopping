document.querySelectorAll('.toggleVisibility').forEach(button => {
    button.addEventListener('click', function (event) {
        event.preventDefault();
        const productId = this.getAttribute('data-product-id');
        console.log(productId);
        fetch(`/api/ProductApi/togglevisibility/${productId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    if (this.textContent === 'Hide') {
                        this.textContent = 'Show';
                        this.classList.remove('btn-danger');
                        this.classList.add('btn-success');
                    } else {
                        this.textContent = 'Hide';
                        this.classList.remove('btn-success');
                        this.classList.add('btn-danger');
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
