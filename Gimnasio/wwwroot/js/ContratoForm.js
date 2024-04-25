window.addEventListener("DOMContentLoaded", () => {
    document.forms[0].addEventListener("submit", validarFormulario);
});
function validarFormulario(evento) {
    evento.preventDefault();
    const arrayElementos = Array.from(this.elements);
    arrayElementos.splice(3, 1);
    arrayElementos.splice(4, 1);
    arrayElementos.splice(6, 2);
    //console.log(arrayElementos);

    let resultadosValidacion = [];
    arrayElementos.forEach(elemento => {
        let resultado = validarCampos(elemento);
        resultadosValidacion.push(resultado);
    });
    const todosValidos = resultadosValidacion.every(resultado => resultado === true);
    if (todosValidos) {document.forms[0].submit() }
}
