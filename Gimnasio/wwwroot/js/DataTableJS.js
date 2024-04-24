//Cargar el siguiente script después de todo el documento
//Los links de referencia van en el layout
window.addEventListener("DOMContentLoaded", () => {
    new DataTable('#formatDataTable', {
        responsive: true,
        rowReorder: {
            selector: 'td:nth-child(2)'
        },
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_",
            "infoEmpty": "Mostrando 0 de 0",
            "infoFiltered": "(Filtrando de _MAX_ Entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": ">",
                "previous": "<"
            }
        }
    });
});
