document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    fetch('/Actividad/IndexCalendar')
        .then(response => response.json())
        .then(data => {
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'timeGridWeek',
                headerToolbar: {
                    left: 'prev,next',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                events: data,
                //Evento para mostrar la información en un alert(probando eventos)
                eventClick: function (info) {
                    
                    var title = info.event.title;
                    var start = info.event.start;
                    var end = info.event.end;

                    alert('Detalles del Evento:\n\nTítulo: ' + title + '\nInicio: ' + start + '\nFin: ' + end);
                }
            });
            calendar.render();
        })
        .catch(error => {
            console.error('Error al cargar eventos:', error);
        });
});