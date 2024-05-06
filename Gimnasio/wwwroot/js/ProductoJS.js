document.addEventListener('DOMContentLoaded', () => {
    document.querySelector('.btn-agregar-producto-carrito').addEventListener('click', async function () {
        const cantidadInput = document.getElementById('cantidadProducto');
        const cantidad = Number(cantidadInput.value);
        const idProducto = Number(cantidadInput.getAttribute('data-producto-id'));
        if (Number.isInteger(cantidad) && Number.isInteger(idProducto) && cantidad>0) {
            const data = {
                idProducto: idProducto,
                Cantidad: cantidad
            };

            try {
                const response = await fetch('Carrito/Agregar', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                });
                if (response.ok) {
                    window.location.href = 'Carrito';
                } else {
                    throw new Error('Hubo un problema al agregar el producto al carrito. Código de estado: ' + response.status);
                }
            } catch (error) {
                alert('Hubo un error al procesar la solicitud: ' + error.message);
            }
        } else {
            document.getElementById('errorCantidad').innerText = "La cantidad debe de ser un número entero positivo.";
        }
    });
    document.querySelectorAll('.btnAgregarProd').forEach(b => b.addEventListener('click', function () {
        //Poner el id del producto como atributo de la cantidad, así ambos datos se guardan en un elemento
        var productoId = b.getAttribute('data-producto-id');
        var cantidadInput = document.getElementById('cantidadProducto');
        cantidadInput.setAttribute('data-producto-id', productoId);
    }));
});