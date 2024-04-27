window.addEventListener("DOMContentLoaded", () => {
    document.forms[0].addEventListener("submit", validarFormulario);
});
function validarFormulario(evento) {
    evento.preventDefault();
    var arrayElementos = Array.from(this.elements);
    arrayElementos.splice(1, 1);
    arrayElementos.splice(5, 1);
    arrayElementos.splice(9, 2);
    console.log(arrayElementos)
    let resultadosValidacion = [];

    arrayElementos.forEach(elemento => {
        if (elemento.type != "file") {
        mensajeError(elemento, null);
        }
        if (elemento.value == "" && elemento.type != "file") {
            mensajeError(elemento, "El campo no puede quedar vacío.");
            resultadosValidacion.push(false);
        } else {
            let resultado = validarElemento(elemento);
            resultadosValidacion.push(resultado);
        }
    });

    const todosValidos = resultadosValidacion.every(resultado => resultado === true);

    if (todosValidos) {
        document.forms[0].submit();
    }
}