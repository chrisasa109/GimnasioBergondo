const esEnteroPositivo = valor => Number.isInteger(valor) && valor >= 0;
function validarCampos(elemento) {
    switch (elemento.id) {
        case "TarifaID":
        case "UsuarioId":
            return esEnteroPositivo(Number(elemento.value));
            break;
        case "FechaInicio":
            return valFechaInicio(elemento);
            break;
        case "FechaFin":
            return valFechaFin(elemento);
            break;
        case "Comentarios":
            return elemento.value.length < 500;
            break;
        case "FormaPago":
            var op = Array("TRANSFERENCIA", "TARJETA", "EFECTIVO");
            return op.includes(elemento.value);
            break;
        case "condiciones-contrato":
            if (!elemento.checked) { document.getElementById('errorCheckbox').textContent = "Debes de aceptar los términos y condiciones." }
            return elemento.checked;
            break;
        default:
            return false;
            break;
    }
}
function valFechaInicio(elemento) {
    var dateC = formatearFecha(new Date(elemento.value));
    var fechaJS = formatearFecha(new Date());
    return dateC == fechaJS;
}
function formatearFecha(fechaDate, plus = 1) {
    var año = fechaDate.getFullYear();
    var mes = (fechaDate.getMonth() + plus).toString().padStart(2, '0');
    var dia = fechaDate.getDate().toString().padStart(2, '0');
    var fechaFormateada = año + '-' + mes + '-' + dia;
    return fechaFormateada;
}
function valFechaFin(elemento) {
    var dateC = formatearFecha(new Date(elemento.value));
    var fechaJS = formatearFecha(new Date(), 2);
    return dateC == fechaJS;
}