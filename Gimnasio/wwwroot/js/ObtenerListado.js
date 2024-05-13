window.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('.LinkListaUsuariosActividad').forEach(link => {
        link.addEventListener('click', function () {
            var idActividad = this.getAttribute('data-idActividad');
            var filas = Array.from(this.closest('.filaTabla').querySelectorAll('td')).splice(0, 3);
            var valores = filas.map(f => f.textContent);
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
                    document.getElementById('descripcionActividad').textContent = valores[0];
                    document.getElementById('fechaActividad').textContent = valores[1];
                    document.getElementById('horaActividad').textContent = valores[2];
                })
                .catch(error => {
                    console.error(error);
                    document.getElementById('listaUsuarios').innerHTML = "<p class='text-danger'>No se han podido cargar los datos</p>";
                });
        });
    });
});