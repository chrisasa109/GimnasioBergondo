document.addEventListener("DOMContentLoaded", () => {
    document.querySelector('#FormularioInicioSesion').addEventListener("submit", validarFormularioInicioSesion);
});

function validarFormularioInicioSesion(evento) {
    evento.preventDefault();
    if (validarEmail(Array.from(this.elements)[0]) && validarPassword(Array.from(this.elements)[1])) { document.querySelector('#FormularioInicioSesion').submit() }
}

function validarEmail(input) {
    mensajeError(input, null);
    if (/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(input.value)) {
        return true;
    } else {
        mensajeError(input, "El formato del email es incorrecto.");
        return false;
    }
}
function validarPassword(input) {
    mensajeError(input, null);
    if (/^(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).{10,20}$/.test(input.value)) {
        return true;
    } else {
        mensajeError(input, "El formato de la contraseña es incorrecto.");
        return false;
    }
}
function mensajeError(input, mensaje) {
    input.nextElementSibling.textContent = mensaje;
    if (mensaje != null) {
        input.classList.add('is-invalid');
    } else {
        input.classList.remove('is-invalid');
    }
}