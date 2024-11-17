document.addEventListener('DOMContentLoaded', function () {

    const hardDeleteButton = document.getElementById("hardDeleteButton");
    const deleteModal = document.getElementById('deleteModal');
    const confirmDeleteButton = document.getElementById("confirmDeleteButton");


    if (hardDeleteButton && deleteModal && confirmDeleteButton) {


        hardDeleteButton.addEventListener("click", function () {
            const modal = new bootstrap.Modal(deleteModal);
            modal.show();
        });


        confirmDeleteButton.addEventListener("click", function () {
            const productId = "@Model.Id";


            fetch(`/api/products/${productId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        
                    } else {
                        console.error("Error:", data.message);
                    }
                })
                .catch(error => {
                    console.error("Error:", error);
                });
        });
    } else {
        console.error("Elements not found: Ensure all elements (buttons, modal) exist.");
    }
});
   
