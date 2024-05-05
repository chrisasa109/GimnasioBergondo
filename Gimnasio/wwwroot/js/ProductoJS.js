document.addEventListener('DOMContentLoaded', () => {
    document.querySelector('.btn-agregar-producto-carrito').addEventListener('click', async function () {
        const cantidadInput = document.getElementById('cantidadProducto');
        const cantidad = cantidadInput.value;
        const idProducto = cantidadInput.getAttribute('data-producto-id');

        const data = {
            idProducto: idProducto,
            Cantidad: cantidad
        };

        console.log(data);

        try {
            const response = await fetch('Carrito/Agregar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (response.ok) {
                // Si la respuesta es exitosa (código de estado 200 OK), muestra el mensaje de éxito
                alert('Producto agregado al carrito correctamente.');
            } else {
                // Si la respuesta es un error (código de estado diferente de 200 OK), muestra el mensaje de error
                throw new Error('Hubo un problema al agregar el producto al carrito. Código de estado: ' + response.status);
            }
        } catch (error) {
            // Captura cualquier error de red o problema al procesar la solicitud
            console.error('Error al procesar la solicitud:', error.message);
            alert('Hubo un error al procesar la solicitud: ' + error.message);
        }
    });



    document.querySelectorAll('.btnAgregarProd').forEach(b => b.addEventListener('click', function () {
        // Poner el id del producto como atributo de la cantidad, así ambos datos se guardan en un elemento
        var productoId = b.getAttribute('data-producto-id');
        var cantidadInput = document.getElementById('cantidadProducto');
        cantidadInput.setAttribute('data-producto-id', productoId);
    }));
});
