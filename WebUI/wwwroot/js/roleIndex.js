$(document).ready(function () {

    /* DataTables start here. */

    const dataTable = $('#rolesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Role/GetAllRoles/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#rolesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const rolesResult = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(rolesResult);
                            if (rolesResult.Roles) {
                                const articlesArray = [];
                                $.each(rolesResult.Roles,
                                    function (index, role) {
                                        const newTableRow = dataTable.row.add([
                                            role.Id,
                                            role.Name
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr('name', `row_${role.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#rolesTable').fadeIn(1400);
                            } else {
                                toastr.error(`Beklenmeyen bir hata olu�tu`, '��lem Ba�ar�s�z!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#rolesTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut de�il",
            "sInfo": "_TOTAL_ kay�ttan _START_ - _END_ aras�ndaki kay�tlar g�steriliyor",
            "sInfoEmpty": "Kay�t yok",
            "sInfoFiltered": "(_MAX_ kay�t i�erisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kay�t g�ster",
            "sLoadingRecords": "Y�kleniyor...",
            "sProcessing": "��leniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "E�le�en kay�t bulunamad�",
            "oPaginate": {
                "sFirst": "�lk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "�nceki"
            },
            "oAria": {
                "sSortAscending": ": artan s�tun s�ralamas�n� aktifle�tir",
                "sSortDescending": ": azalan s�tun s�ralamas�n� aktifle�tir"
            },
            "select": {
                "rows": {
                    "_": "%d kay�t se�ildi",
                    "0": "",
                    "1": "1 kay�t se�ildi"
                }
            }
        }
    });
});