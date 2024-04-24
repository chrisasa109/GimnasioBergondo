document.addEventListener("DOMContentLoaded", () => {
    document.querySelector('#FormularioInicioSesion').addEventListener("submit", validarFormularioInicioSesion);
});

function validarFormularioInicioSesion(evento) {
    evento.preventDefault();
    if (validarEmail(Array.from(this.elements)[0]) && validarPassword(Array.from(this.elements)[1])) { document.querySelector('#FormularioInicioSesion').submit() }
}
