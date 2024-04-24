window.addEventListener("DOMContentLoaded", () => {
    document.forms[0].addEventListener("submit", validarFormulario);
});
function validarFormulario(evento) {
    evento.preventDefault();
    var arrayElementos = Array.from(this.elements);
    arrayElementos.splice(0, 1);
    arrayElementos.splice(4, 1);
    arrayElementos.splice(8, 2);
    console.log(arrayElementos)
    let resultadosValidacion = [];

    arrayElementos.forEach(elemento => {
        mensajeError(elemento, null);
        if (elemento.value == "") {
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