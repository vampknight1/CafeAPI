$(document).ready(function () {
    $.ajaxSetup({ cache: false });

   


    $('#btnCreate').on('click', function (e) {
        e.preventDefault();
        OpenPopup($(this).attr('href'));
    })

    $('#myTable').on('click', 'a.btn.btn-info', function (e) {
        e.preventDefault();
        OpenPopup($(this).attr('href'));
    })

    $('#myTable').on('click', 'a.btn.btn-danger', function (e) {
        e.preventDefault();
        OpenPopup($(this).attr('href'));
    })

        function OpenPopup(pageUrl) {
            var $pageContent = $('<div/>');
            $pageContent.load(pageUrl, function () {
                $('#myForm', $pageContent).removeData('validator');
                $('#myForm', $pageContent).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse('form');
            });
            $dialog = $('<div class="modal popupWindow" style="background-color: White;"></div>')
                .html($pageContent)
                .dialog({
                    show: true,
                    backdrop: 'static',
                    keyboard: false,
                    draggable: true,
                    autoOpen: false,
                    resizable: false,
                    model: true,
                    title: 'Confirmation',
                    height: 400,
                    width: 450,
                    show: { effect: 'drop', direction: "up" },
                    modal: true,
                    close: function () {
                        $dialog.dialog('destroy').remove();
                    }
                });

            $('.popupWindow').on('submit', '#myForm', function (e) {
                var url = $('#myForm')[0].action;
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: $('#myForm').serialize(),
                    success: function (data) {
                        if (data.status) {
                            $dialog.dialog('close');
                            oTable.ajax.reload();
                        }
                        alert('Success..!')
                    }
                })
                    .fail(function (xhr, status, errorThrown) {
                        alert('Sorry, there was a problem !');
                        console.log('Error: ' + errorThrown);
                        console.log('Status: ' + status);
                        console.dir(xhr);
                    });
                e.preventDefault();
            });

            $dialog.dialog('open');
    }
});