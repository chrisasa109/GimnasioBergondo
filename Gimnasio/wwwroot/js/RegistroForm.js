window.addEventListener("DOMContentLoaded", () => {
    document.getElementById('FormularioUsuario').addEventListener("submit", validarFormularioNuevoUsuario);
});

function validarFormularioNuevoUsuario(evento) {
    evento.preventDefault();
    var arrayElementos = Array.from(this.elements);
    arrayElementos.splice(4, 1);
    arrayElementos.splice(10, 2);

    let resultadosValidacion = [];

    arrayElementos.forEach(elemento => {
        mensajeError(elemento, null);
        if (elemento.value == "") {
            mensajeError(elemento, "El campo no puede quedar vacío.");
            resultadosValidacion.push(false);
        } else {
            let resultado = false;
            switch (elemento.id) {
                case "Nombre":
                case "Apellidos":
                case "Direccion":
                case "Poblacion":
                    resultado = validarCamposTexto(elemento);
                    break;
                case "DNI":
                    resultado = validarDNI(elemento);
                    break;
                case "Telefono":
                    resultado = validarTelefono(elemento);
                    break;
                case "Email":
                    resultado = validarEmail(elemento);
                    break;
                case "FechaNacimiento":
                    resultado = validarFechaNacimiento(elemento);
                    break;
                case "Password":
                    resultado = validarPassword(elemento);
                    break;
                case "ConfirmPassword":
                    resultado = validarConfirmarPassword(elemento);
                    break;
                default:
                    break;
            }
            resultadosValidacion.push(resultado);
        }
    });
    const todosValidos = resultadosValidacion.every(resultado => resultado === true);

    if (todosValidos) {
        document.getElementById('FormularioUsuario').submit();
    }
}
function validarDNI(input) {
    if (/^\d{8}[A-Za-z]$/.test(input.value)) {
        validacionCorrecta(input)
        return true;
    } else {
        mensajeError(input, "El formato del DNI es incorrecto. Son 8 números y una letra");
        return false;
    }
}
function validarTelefono(input) {
    if (/^\d{9}$/.test(input.value)) {
        validacionCorrecta(input)
        return true;
    } else {
        mensajeError(input, "El formato del teléfono es incorrecto. Deben ser de 6 a 14 dígitos");
        return false;
    }
}
function validarCamposTexto(input) {
    var val = false;
    switch (input.id) {
        case "Nombre":
            if (input.value.length >= 3 && input.value.length <= 50) {
                validacionCorrecta(input);
                val = true;
            } else {
                mensajeError(input, "La longitud es min 3 y max 50")
            }
            break;
        case "Apellidos":
        case "Direccion":
            if (input.value.length >= 6 && input.value.length <= 50) {
                validacionCorrecta(input);
                val = true;
            } else {
                mensajeError(input, "La longitud es min 6 y max 50");
            }
            break;
        case "Poblacion":
            if (input.value.length >= 2 && input.value.length <= 50) {
                validacionCorrecta(input);
                val = true;
            } else {
                mensajeError(input, "La longitud es min 2 y max 50")
            }
            break;
    }
    return val;
}
function validarFechaNacimiento(input) {
    var fecha = new Date(input.value);
    var hoy = new Date();

    if (isNaN(fecha.getTime())) {
        mensajeError(input, "Por favor, ingresa una fecha válida.");
        return false;
    } else if (fecha.getTime() >= hoy.getTime()) {
        mensajeError(input, "La fecha de nacimiento no puede ser igual o posterior a la fecha actual.");
        return false;
    } else {
        validacionCorrecta(input);
        return true;
    }
}
function validacionCorrecta(input) {
    input.classList.add('is-valid');
}
function validarConfirmarPassword(input) {
    if (input.value == document.getElementById('Password').value) {
        validacionCorrecta(input);
        return true;
    } else {
        mensajeError(input, "Las contraseñas no coinciden.");
        return false;
    }
}
function validarEmail(input) {
    if (/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(input.value)) {
        validacionCorrecta(input);
        return true;
    } else {
        mensajeError(input, "El formato del email es incorrecto.");
        return false;
    }
}
function validarPassword(input) {
    if (/^(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).{10,20}$/.test(input.value)) {
        validacionCorrecta(input);
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