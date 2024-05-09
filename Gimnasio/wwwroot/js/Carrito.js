document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('.iCantidad').forEach(input => {
        precioProducto.call(input);
        input.addEventListener('input', precioProducto);
        input.addEventListener('change', precioProductoChange);
    });

    document.querySelectorAll('.eliminar-carrito').forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            const carritoId = this.getAttribute('data-carrito-id');
            eliminarElementoDelCarrito(carritoId);
        });
    });

    document.getElementById('GuardarCambios').addEventListener('click', function (e) {
        e.preventDefault();
        const idsCarrito = Array.from(document.querySelectorAll('.eliminar-carrito')).map(a => a.getAttribute('data-carrito-id'));
        const cantidadInputs = Array.from(document.querySelectorAll('.iCantidad')).map(a => a.value);
        const envio = {
            idsCarrito: idsCarrito,
            cantidades: cantidadInputs
        };
        console.log(envio)
        fetch('/Carrito/GuardarCambios', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(envio)
        })
            .then(() => location.reload());
    });

    document.querySelector('.btn-realizar-pedido').addEventListener('click', async function () {
        const metodoPagoSelect = document.getElementById('metodoPago');
        const metodoPago = metodoPagoSelect.value;

        if (['TRANSFERENCIA', 'TARJETA', 'EFECTIVO'].includes(metodoPago)) {
            try {
                const response = await fetch('/DetallePedido/ProcesarPedido', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(metodoPago)
                });

                if (response.ok) {
                    window.location.href="Pedido";
                } else {
                    console.error("Error al procesar el pedido:", response.statusText);
                }
            } catch (error) {
                console.error("Error en la solicitud fetch:", error);
            }
        } else {
            document.getElementById('errorSelect').textContent = "Debes seleccionar uno de los tres métodos de pago.";
        }
    });

});

function precioProducto() {
    var cantidad = parseInt(this.value);
    if (isNaN(cantidad)) {cantidad=1 }
    const precioProductoElement = this.closest('.card-body').querySelector('.precioProducto');
    const precioFormateado = parseFloat(precioProductoElement.innerText.replace(',', '.'));
    const precioTotalProducto = cantidad * precioFormateado;
    const precioMostrar = precioTotalProducto.toFixed(2).replace('.', ',') + ' €';
    const elementoPrecioTotalProducto = this.closest('.card-body').querySelector('.precioTotalProducto');
    elementoPrecioTotalProducto.innerText = precioMostrar;
    calcularPrecioTotalCarrito();
}

function precioProductoChange() {
    const cantidad = this.value;
    if (cantidad == "" || cantidad<1 || cantidad==null) {
        this.value = 1;
    } else{
        this.value = parseInt(cantidad)
    }
    precioProducto.call(this);
}

function calcularPrecioTotalCarrito() {
    const preciosTotales = document.querySelectorAll('.precioTotalProducto');
    let precioTotalCarrito = 0;

    preciosTotales.forEach(precioTotal => {
        precioTotalCarrito += parseFloat(precioTotal.innerText.replace(',', '.'));
    });

    document.getElementById('precioTotalCarrito').innerText = `${precioTotalCarrito.toFixed(2).replace('.', ',')} €`;
}

function eliminarElementoDelCarrito(carritoId) {
    fetch(`/Carrito/Eliminar/${carritoId}`, {
        method: 'DELETE'
    })
        .then(response => {
            if (response.ok) {
                location.reload();
            }
        });
}