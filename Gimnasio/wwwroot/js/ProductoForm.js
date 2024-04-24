/**Eventos para el input:file*/
const dropContainer = document.getElementById("dropcontainer");
const fileInput = document.getElementById("images");

dropContainer.addEventListener("dragover", (e) => {
    e.preventDefault();
}, false);

dropContainer.addEventListener("dragenter", () => {
    dropContainer.classList.add("drag-active");
});

dropContainer.addEventListener("dragleave", () => {
    dropContainer.classList.remove("drag-active");
});

dropContainer.addEventListener("drop", (e) => {
    e.preventDefault()
    dropContainer.classList.remove("drag-active");
    fileInput.files = e.dataTransfer.files;
});
/**Validación del formulario de crear un producto*/
document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("FormularioNuevoProducto").addEventListener("submit", validarForm)
});
function validarForm(evento) {
    evento.preventDefault();
    var arrayElementos = Array.from(this.elements);
    arrayElementos.splice(3, 1);
    arrayElementos.splice(4, 2);
    var validacion = [validarNombre(arrayElementos[0]), validarPrecio(arrayElementos[1]), validarStock(arrayElementos[2]), validarImagen(arrayElementos[3])];
    if (validacion.every(resultado => resultado === true)) {
        document.getElementById('FormularioNuevoProducto').submit();
    }
}
function validacionCorrecta(input) {
    input.classList.remove('is-invalid');
    input.classList.add('is-valid');
}
function mensajeError(input, mensaje) {
    input.nextElementSibling.textContent = mensaje;
    if (mensaje != null) {
        input.classList.add('is-invalid');
    } else {
        input.classList.remove('is-invalid');
    }
}
function validarNombre(input) {
    var val = false;
    if (input.value.length >= 6 && input.value.length <= 50) {
        validacionCorrecta(input);
        val = true;
    } else {
        mensajeError(input, "El nombre del producto debe de tener entre 6 y 50 caracteres.");
    }
    return val;
}
function validarPrecio(input) {
    var val = false;
    if (input.value.length == 0) {
        mensajeError(input, "El campo no puede quedar vacío.");
    } else if (input.value < 0.00 || input.value > Number.MAX_VALUE) {
        mensajeError(input, "El precio debe ser un valor positivo sin superar el máximo.");
    } else if (contarDecimales(input.value) > 2) {
        mensajeError(input, "El precio no puede tener más de dos decimales.");
    } else {
        mensajeError(input, null);
        validacionCorrecta(input);
        val = true;
    }
    return val;
}
function contarDecimales(numero) {
    var numeroComoCadena = numero.toString();
    if (numeroComoCadena.indexOf(',') !== -1) {
        return numeroComoCadena.split(',')[1].length;
    } else if (numeroComoCadena.indexOf('.') !== -1) {
        return numeroComoCadena.split('.')[1].length;
    } else {
        return 0;
    }
}
function mensajeErrorStock(input, mensaje) {
    input.nextElementSibling.nextElementSibling.textContent = mensaje;
    if (mensaje != null) {
        input.classList.add('is-invalid');
    } else {
        input.classList.remove('is-invalid');
    }
}
function validarStock(input) {
    var val = false;
    if (input.value == "") {
        mensajeErrorStock(input, "El campo no puede quedar vacío.");
    } else if (input.value < 0) {
        mensajeErrorStock(input, "El stock no puede ser un número negativo.");
    } else if (Number.isInteger(input.value)) {
        mensajeErrorStock(input, "El valor debe de ser un número entero.")
    } else {
        mensajeErrorStock(input, null);
        validacionCorrecta(input);
        val = true;
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
        }else {
            mensajeErrorImagen(input, null);
            validacionCorrecta(input);
            val = true;
        }
    } else {
        mensajeErrorImagen(input, 'Por favor, seleccione una imagen.');
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