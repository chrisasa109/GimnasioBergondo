//Para el toggle de la barra de navegación
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("menu-toggle").addEventListener("click", function () {
        document.getElementById("navbarSupportedContent").classList.toggle("show");
    });
});
