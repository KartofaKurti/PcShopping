$(document).ready(function () {
    $('#searchForm').on('submit', function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            url: $(this).attr('action'),
            type: 'GET',
            data: formData,
            success: function (result) {
                $('#searchResultsContainer').html($(result).find('#searchResultsContainer').html());
                $('#paginationControls').html($(result).find('#paginationControls').html());
            },
            error: function () {
                alert('An error occurred while searching.');
            }
        });
    });
});