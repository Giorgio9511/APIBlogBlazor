window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operación correcta", { timeOut: 10000 });
    }
    if (type === "error") {
        toastr.error(message, "Operación fallida", { timeOut: 10000 });
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success Notification',
            message,
            'success'
        );
    }
    if (type === "error") {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        );
    }
}

window.MostrarModalConfirmacionBorrado = () => {
    const modal = document.getElementById('modalConfirmacionBorrado');
    const bsModal = new bootstrap.Modal(modal);
    bsModal.show();
}

window.OcultarModalConfirmacionBorrado = () => {
    const modal = document.getElementById('modalConfirmacionBorrado');
    const bsModal = bootstrap.Modal.getInstance(modal);
    bsModal.hide();
}