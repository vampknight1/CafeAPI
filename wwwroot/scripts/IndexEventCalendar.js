$(document).ready(function () {
    var events = [];
    googleCalendarApiKey: 'AIzaSyDcnW6WejpTOCffshGDDb4neIrXVUA1EAE';
    var eventsIDHolyday = 'id.indonesian#holiday@group.v.calendar.google.com';

    $.ajax({
        type: "POST",
        url: "/EventCalendar/LoadData/",
        success: function (data) {
            $.each(data, function (i, v) {
                events.push({
                    title: v.subject,
                    description: v.description,
                    start: moment(v.startEvent),
                    end: v.endEvent != null ? moment(v.endEvent) : null,
                    color: v.themeColor,
                    allDay: v.isFullDay
                });
            })

            GenerateCalender(events);
        },
        error: function (error) {
            alert('Failed to Load Calendar Events...');
        }
    })

    function GenerateCalender(events) {
        $('#calender').fullCalendar('destroy');
        $('#calender').fullCalendar({
            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay,agenda'
            },
            eventLimit: true,
            eventColor: '#78CD51',
            events: events,
            editable: true,
            droppable: true,    // this allows things to be dropped onto the calendar
            eventClick: function (calEvent, jsEvent, view) {
                $('#myModal #eventTitle').text(calEvent.title);
                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Start : </b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($('<p/>').html('<b>End : </b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }
                $description.append($('<p/>').html('<b>Description : </b>' + calEvent.description));
                $('#myModal #pDetails').empty().html($description);

                $('#myModal').modal();
            }
        })
    }
})