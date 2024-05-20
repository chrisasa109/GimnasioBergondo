window.addEventListener("DOMContentLoaded", () => {

    document.querySelectorAll('.btn-nuevaAct').forEach(a => a.addEventListener('click', gestionIdActividad))

    var DTable = new DataTable('#formatDataTable');
    //Evento propio de la API de DataTables. La función se ejecuta a coninuación de pintar nuevos datos de tablas -> evento draw
    //el evento page ejecutaría una función en el cambio de página pero con los datos antiguos
    DTable.on('draw', function () {
        this.querySelectorAll('.btn-nuevaAct').forEach(a => a.addEventListener('click', gestionIdActividad));
    });
    
    document.querySelectorAll('.btn-agregar-actividad-usuario').forEach(b => {
        b.addEventListener('click', agregarActividad);
    });

});
async function agregarActividad () {
    const txtarea = document.querySelector('#notaNuevaActividad');
    if (validarNota.call(txtarea)) {
        const nota = txtarea.value;
        const actividad = txtarea.getAttribute('data-IdActividad');
        const data = {
            ActividadId: actividad,
            Notas: nota
        }
        console.log(data);
        try {
            const response = await fetch('UsuarioActividad/Agregar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            if (response.ok) {
                window.location.href = 'UsuarioActividad';
            } else {
                throw new Error('Hubo un problema al apuntarse a la actividad. Código de estado: ' + response.status);
            }
        } catch (error) {
            console.error(error);
        }
    } else {
        alert("No te has podido apuntar a la tarea. El comentario no puede superar los 500 caracteres.");
    }
}
function validarNota() {
    const elementoNota = document.getElementById('errorNota');
    if (this.value.length > 500) {
        elementoNota.innerText = "El comentario no puede ser de más de 500 caracteres";
        return false;
    } else {
        elementoNota.innerText = "";
        return true;
    }
}
function gestionIdActividad(a) {
    var actividad = this.getAttribute('data-IdActividad');
    document.querySelector('#notaNuevaActividad').setAttribute("data-IdActividad", actividad);
}