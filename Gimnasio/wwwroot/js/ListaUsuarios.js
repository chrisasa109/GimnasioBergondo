window.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".btn-adjudicar-rol").forEach(b => {
        b.addEventListener("click", adjudicarUsuario);
    });
    document.querySelector("#btn-guardar-rol").addEventListener("click", adjudicarRol);
});
function adjudicarUsuario() {
    var idUsuario = this.getAttribute('data-idUsuario');
    document.getElementById('rolSeleccionado').setAttribute('data-IdUsuario', idUsuario);
}
async function adjudicarRol(evento) {
    evento.preventDefault();
    var idUsuario = document.getElementById('rolSeleccionado').getAttribute('data-IdUsuario');
    var rolFronted = document.getElementById('rolSeleccionado').value;
    if (['ADMINISTRADOR', 'CLIENTE', 'TRABAJADOR'].includes(rolFronted)) {
        var data = {
            idUsuario: idUsuario,
            rolFronted: rolFronted
        }
        try {
            const response = await fetch('/Usuario/AsignarRol', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (response.ok) {
                window.location.href = "ListaUsuarios";
            } else {
                console.error("Error al procesar cambiar el rol:", response.statusText);
            }
        } catch (error) {
            console.error("Error en la solicitud fetch:", error);
        }
    } else {
        document.getElementById('errorSelect').textContent = "Debes seleccionar uno de los tres roles.";
    }
}