window.addEventListener("DOMContentLoaded", () => {
    document.forms[0].addEventListener("submit", validarFormulario);
    document.querySelector('.btn-aceptar-condiciones').addEventListener('click', confirmarCheck);
});
function validarFormulario(evento) {
    evento.preventDefault();
    const arrayElementos = Array.from(this.elements);
    arrayElementos.splice(3, 1);
    arrayElementos.splice(4, 1);
    arrayElementos.splice(7, 2);
    //console.log(arrayElementos);

    let resultadosValidacion = [];
    arrayElementos.forEach(elemento => {
        let resultado = validarCampos(elemento);
        resultadosValidacion.push(resultado);
    });
    const todosValidos = resultadosValidacion.every(resultado => resultado === true);
    console.log(resultadosValidacion)
    if (todosValidos) {document.forms[0].submit() }
}
function confirmarCheck() {
    document.getElementById('condiciones-contrato').checked = true;
}