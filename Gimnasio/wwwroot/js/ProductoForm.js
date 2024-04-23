document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("FormularioNuevoProducto").addEventListener("submit", validarForm)
});
function validarForm(evento) {
    evento.preventDefault();
    var arrayElementos = Array.from(this.elements);
    arrayElementos.splice(3, 1);
    arrayElementos.splice(4, 2);
    validarNombre(arrayElementos[0])
}
function validacionCorrecta(input) {
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
    if (input.value.length >= 6 && input.value.length <= 50) {
        validacionCorrecta(input);
    } else {
        mensajeError(input, "El nombre del producto debe de tener entre 6 y 50 caracteres.");
    }
}
const dropContainer = document.getElementById("dropcontainer")
const fileInput = document.getElementById("images")

dropContainer.addEventListener("dragover", (e) => {
    e.preventDefault()
}, false);

dropContainer.addEventListener("dragenter", () => {
    dropContainer.classList.add("drag-active")
});

dropContainer.addEventListener("dragleave", () => {
    dropContainer.classList.remove("drag-active")
});

dropContainer.addEventListener("drop", (e) => {
    e.preventDefault()
    dropContainer.classList.remove("drag-active")
    fileInput.files = e.dataTransfer.files
});
