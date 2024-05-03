window.addEventListener('DOMContentLoaded', function () {
    const precios = Array.from(document.getElementsByClassName('precioTotalProducto')).map(a => a.textContent);
    const preciosMap = precios.map(p => Number(p.replace(',', '.')));
    var precioTot = 0;
    preciosMap.forEach(m => precioTot += m);
    document.getElementById('precioTotal').textContent = `Precio total: ${precioTot.toFixed(2).toString().replace('.', ',')}€`;
});