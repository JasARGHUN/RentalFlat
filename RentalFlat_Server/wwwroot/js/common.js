window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Success!");
    }
    if (type === "error") {
        toastr.error(message, "Failed!");
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success!',
            message,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire(
            'Failed!',
            message,
            'error'
        )
    }
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}