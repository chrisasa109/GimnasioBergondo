window.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('.btn-editar-producto').forEach(b => {
        b.addEventListener('click', function () {
            var idProducto = this.getAttribute('data-idProducto');
            fetch('/Producto/Editar?idProducto=' + idProducto, {
                method: 'GET'
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(response.status);
                    }
                    return response.text();
                })
                .then(data => {
                    document.getElementById('formularioEditarProducto').innerHTML = data;
                })
        });
    });
});