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
                slotMinTime: '08:00:00',
                slotMaxTime: '21:00:00',
                events: data,
                
                eventClick: function (info) {
                    var title = info.event.title;
                    var start = FullCalendar.formatDate(info.event.start, {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit'
                    });
                    var end = FullCalendar.formatDate(info.event.end, {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit'
                    });

                    alert('Detalles de la actividad:\n\nTítulo: ' + title + '\nInicio: ' + start + '\nFin: ' + end);
                }
            });
            calendar.render();
        })
        .catch(error => {
            console.error('Error al cargar eventos:', error);
        });
});