window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Uspesna operacija", { timeOut: 5000 });
    }
    if (type === "error") {
        toastr.error(message, "Neuspela operacija", { timeOut: 5000 });
    }
}