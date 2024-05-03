document.addEventListener('DOMContentLoaded', () => {
    document.querySelector('.btn-agregar-producto-carrito').addEventListener('click', async function () {
        var elemento = document.getElementById('cantidadProducto');
        var cantidad = elemento.value;
        var idProducto = elemento.getAttribute('data-producto-id');
        //Hacer el fetch para añadir el producto al carrito en la base de datos
       // const reponse = await fetch('.....')
    });
    document.querySelectorAll('.btnAgregarProd').forEach(b => b.addEventListener('click', function () {
        //Poner el id del producto como atributo de la cantidad, así ambos datos se guardan en un elemento
        var productoId = b.getAttribute('data-producto-id');
        var cantidadInput = document.getElementById('cantidadProducto');
        cantidadInput.setAttribute('data-producto-id', productoId);
    }));
});