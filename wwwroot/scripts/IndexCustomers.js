$(document).ready(function () {
    $.ajaxSetup({ cache: false });

    var oTable = $('#myTable').DataTable({
        ajax: {
            url: '/Customers/LoadData',
            type: 'POST',
            datatype: 'json',
            processing: true,
            serverSide: true
        },
        columns: [
            {
                data: 'id', 'width': '11%', 'render': function (data) {
                    return '<a class="btn btn-info" href="/Customers/Edit/' + data + '">Edit</a>'
                        + '<a class="btn btn-danger" onclick="return confirm(Are you sure wants to delete @item.CustomerName?);" href="/Customers/Delete/' + data + '">Del</a>'
                        + '<a asp-action="Edit" asp-route-id="' + data + '" class="btn btn-primary">Edit</a>'
                    //+ '<a asp-action="Delete" asp-route-id="@item.ID"
                    //    class="btn btn-danger"
                    //    onclick="return confirm('Are you sure wants to delete @item.CustomerName?');">Delete
                    //</a>'
                }
            },
            { data: 'tableCode', 'autoWidth': true },
            { data: 'customerName', 'autoWidth': true },
            { data: 'customerAddress', 'autoWidth': true },
            { data: 'cityName', 'autoWidth': true },
            { data: 'customerPhone', 'autoWidth': true },
        ],
        pagingType: 'full_numbers',
        info: true,
        sScrollX: true,
        sScrollY: '55vh',
        sScrollCollapse: true,
        width: '100%'
    });


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
            $dialog = $('<div class="modal popupWindow" role="dialog" style="background-color: White;" style="overflow:auto"></div>')
            //$dialog = $('<div class="popupWindow" style="overflow:auto"></div>')
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