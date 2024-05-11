window.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('.LinkListaUsuariosActividad').forEach(link => {
        link.addEventListener('click', function () {
            var idActividad = this.getAttribute('data-idActividad');

            fetch('/UsuarioActividad/ListaUsuarios?idActividad=' + idActividad, {
                method: 'POST'
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Error al cargar la vista parcial');
                    }
                    return response.text();
                })
                .then(data => {
                    document.getElementById('listaUsuarios').innerHTML = data;
                })
                .catch(error => {
                    console.error(error.message);
                });
        });
    });
});