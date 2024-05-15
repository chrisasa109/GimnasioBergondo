window.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.btn-visualizar-perfil').forEach(b => {
        b.addEventListener('click', VisualizarPerfil);
    });
    document.querySelectorAll('.btn-comprobar-contrato').forEach(b => {
        b.addEventListener('click', ComprobarContrato);
    });
    document.querySelectorAll('.btn-modificar-usuario').forEach(b => {
        b.addEventListener('click', ModificarPerfil);
    });
});
function VisualizarPerfil() {
    var idUsuario = this.getAttribute('data-idUsuario');
    fetch(`/Usuario/Detalles/${idUsuario}`, {
        method: 'GET'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(response.status);
            }
            window.location.href = `/Usuario/Detalles/${idUsuario}`
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}
function ComprobarContrato() {
    var idUsuario = this.getAttribute('data-idUsuario');
    fetch('/Contrato/ComprobarContrato?IdUsuario=' + idUsuario, {
        method: 'POST'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(response.status);
            }
            return response.text();
        })
        .then(data => {
            document.getElementById('texto-status-contrato').innerHTML = data;
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}
function ModificarPerfil() {
    var idUsuario = this.getAttribute('data-idUsuario');

    fetch(`/Usuario/Modificacion?IdUsuario=${idUsuario}`, {
        method: 'GET'
    })
        .then(response => {
            if (response.ok) {
                // Redirigir a la página de modificación de perfil
                window.location.href = `/Usuario/Modificacion/${idUsuario}`;
            } else {
                console.error('Error en la respuesta de la solicitud fetch');
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}
