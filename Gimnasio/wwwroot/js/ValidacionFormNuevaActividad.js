window.addEventListener("DOMContentLoaded", () => {
    document.forms[0].addEventListener('submit', validarCamposForm);
});

function validarCamposForm(evento) {
    evento.preventDefault();
    const elementos = Array.from(document.forms[0].getElementsByClassName('form-control'));
    let resultadosValidacion = [];
    elementos.forEach(elemento => {
        mensajeError(elemento);
        let resultado = validarCampo(elemento);
        resultadosValidacion.push(resultado);
    });
    const todosValidos = resultadosValidacion.every(resultado => resultado === true);
    if (todosValidos) { document.forms[0].submit() }
}

function validarCampo(elemento) {
    switch (elemento.id) {
        case "Descripcion":
            return validarDescripcion(elemento);
            break;
        case "Duracion":
            return validarDuracion(elemento);
            break;
        case "CapacidadMaxima":
            return validarCapacidadMaxima(elemento);
            break;
        case "FechaHora":
            return validarFechaHora(elemento);
            break;
        default:
            break;
    }
}
function mensajeError(elemento, mensaje = null) {
    if (elemento.id == "Duracion" || elemento.id == "FechaHora" || elemento.id =="CapacidadMaxima") {
        elemento.nextElementSibling.nextElementSibling.textContent = mensaje;
    } else {
        elemento.nextElementSibling.textContent = mensaje;
    }
}
function validarDescripcion(elemento) {
    var val = true;
    if (elemento.value.length < 6 || elemento.value.length > 50) {
        mensajeError(elemento, "La descripción de la actividad debe de tener entre 6 y 50 caracteres.");
        val = false;
    }
    return val;
}
function validarDuracion(elemento) {
    var val = true;
    if (!/^\d{2}:\d{2}$/.test(elemento.value)) {
        mensajeError(elemento, "El formato no es correcto");
        val = false;
    }
    return val;
}
function validarCapacidadMaxima(elemento) {
    var val = true;
    var num = Number(elemento.value);
    if (num <= 0 || !Number.isInteger(num)) {
        mensajeError(elemento, "La duración debe de ser un número entero positivo.");
        val = false;
    }
    return val;
}
function validarFechaHora(elemento) {
    var val = true;
    console.log(elemento.value)
    if (!/^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/.test(elemento.value)) {
        mensajeError(elemento, "El formato es incorrecto.")
        val = false;
    }
    return val;
}