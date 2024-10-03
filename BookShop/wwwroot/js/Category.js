
$(document).ready(function () {
    $('.js-delete').on('click', function (e) {
        e.preventDefault();
        var id = $(this).data('id');

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Admin/Category/Delete/${id}`,
                    type: 'delete',
                    success: function (data) {
                        if (data.success) {
                            swal.fire(
                                'Deleted!',
                                data.message,
                                'success'
                            ).then(() => {
                                // Optionally remove the row from the table or reload the page
                                window.location.reload(); // Reload the page
                            });
                        }
                        else {
                            swal.fire(
                                'Error!',
                                data.message,
                                'error'
                            );
                        }

                    }
                });
            }
        });
    });
});

