function validarElemento(elemento) {
    switch (elemento.id) {
        case "Nombre":
        case "Apellidos":
        case "Direccion":
        case "Poblacion":
            return validarCamposTexto(elemento);
        case "DNI":
            return validarDNI(elemento);
        case "Telefono":
            return validarTelefono(elemento);
        case "Email":
            return validarEmail(elemento);
        case "FechaNacimiento":
            return validarFechaNacimiento(elemento);
        case "Password":
            return validarPassword(elemento);
        case "ConfirmPassword":
            return validarConfirmarPassword(elemento);
        case "images":
            return validarImagen(elemento);
        default:
            return true;
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
function validarImagen(input) {
    var val = false;
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    if (input.files.length > 0) {
        var file = input.files[0];
        if (!allowedExtensions.exec(file.name)) {
            mensajeErrorImagen(input, 'Por favor, seleccione un archivo de imagen con formato JPEG o PNG.');
            input.value = '';
        } else if (file.size > 5 * 1024 * 1024) {
            mensajeErrorImagen(input, 'El tamaño del archivo no puede ser mayor a 5 MB.');
            input.value = '';
        } else {
            mensajeErrorImagen(input, null);
            validacionCorrecta(input);
            val = true;
        }
    } else {
        val = true;
    }
    return val;
}
function mensajeErrorImagen(input, mensaje) {
    input.parentNode.nextElementSibling.textContent = mensaje;
    if (mensaje != null) {
        input.classList.add('is-invalid');
    } else {
        input.classList.remove('is-invalid');
    }
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
    input.classList.remove('is-invalid');
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
    if (input.id == "FechaNacimiento") {
        input.nextElementSibling.nextElementSibling.textContent = mensaje;
    } else {
        input.nextElementSibling.textContent = mensaje;
    }
    
    if (mensaje != null) {
        input.classList.add('is-invalid');
    } else {
        input.classList.remove('is-invalid');
    }
}