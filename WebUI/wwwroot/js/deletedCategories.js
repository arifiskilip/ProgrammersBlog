$(document).ready(function () {

    /* DataTables start here. */

    const dataTable = $('#deletedCategoriesTable').DataTable({
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
                        url: '/Admin/Category/GetAllDeletedCategories/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#deletedCategoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const deletedCategories = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(deletedCategories);
                            if (deletedCategories.ResultStatus === 1) {
                                $.each(deletedCategories.Categories.$values,
                                    function (index, category) {
                                        const newTableRow = dataTable.row.add([
                                            category.Id,
                                            category.Name,
                                            category.Description,
                                            category.IsActive ? "Evet" : "Hayýr",
                                            category.IsDeleted ? "Evet" : "Hayýr",
                                            category.Note,
                                            convertShortDate(category.CreatedDate),
                                            category.CreatedByName,
                                            convertShortDate(category.ModifiedDate),
                                            category.ModifiedByName,
                                            `
                                <button class="btn btn-warning btn-sm btn-undo" data-id="${category.Id}"><span class="fas fa-undo"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${category.Id}"><span class="fas fa-minus-circle"></span></button>
                            `
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr('name', `row_${category.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#deletedCategoriesTable').fadeIn(1400);
                            } else {
                                toastr.error(`${deletedCategories.Categories.Message}`, 'Ýþlem Baþarýsýz!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#deletedCategoriesTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut deðil",
            "sInfo": "_TOTAL_ kayýttan _START_ - _END_ arasýndaki kayýtlar gösteriliyor",
            "sInfoEmpty": "Kayýt yok",
            "sInfoFiltered": "(_MAX_ kayýt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayýt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "Ýþleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eþleþen kayýt bulunamadý",
            "oPaginate": {
                "sFirst": "Ýlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sýralamasýný aktifleþtir",
                "sSortDescending": ": azalan sütun sýralamasýný aktifleþtir"
            },
            "select": {
                "rows": {
                    "_": "%d kayýt seçildi",
                    "0": "",
                    "1": "1 kayýt seçildi"
                }
            }
        }
    });

    /* DataTables end here */

    /* UndoDelete */

    $(document).on('click',
        '.btn-undo',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="row_${id}"]`);
            let categoryName = tableRow.find('td:eq(1)').text();
            Swal.fire({
                title: 'Arþivden geri getirmek istediðinize emin misiniz?',
                text: `${categoryName} adlý kategori arþivden geri getirilecektir!`,
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, arþivden geri getirmek istiyorum.',
                cancelButtonText: 'Hayýr, istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { categoryId: id },
                        url: '/Admin/Category/UndoDelete/',
                        success: function (data) {
                            const undoDeletedCategoryResult = jQuery.parseJSON(data);
                            console.log(undoDeletedCategoryResult);
                            if (undoDeletedCategoryResult.ResultStatus === 1) {
                                Swal.fire(
                                    'Arþivden Geri Getirildi!',
                                    `${undoDeletedCategoryResult.Message}`,
                                    'success'
                                );

                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Baþarýsýz Ýþlem!',
                                    text: `${undoDeletedCategoryResult.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!");
                        }
                    });
                }
            });
        });
    /* UndoDelete */
    /* HardDelete */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="row_${id}"]`);
            let categoryName = tableRow.find('td:eq(1)').text();
            Swal.fire({
                title: 'Kalýcý olarak silmek istediðinize emin misiniz?',
                text: `${categoryName} adlý kategori kalýcý olarak silinecektir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, kalýcý olarak silmek istiyorum.',
                cancelButtonText: 'Hayýr, istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { categoryId: id },
                        url: '/Admin/Category/HardDelete/',
                        success: function (data) {
                            const hardDeleteResult = jQuery.parseJSON(data);
                            console.log(hardDeleteResult);
                            if (hardDeleteResult.ResultStatus === 1) {
                                Swal.fire(
                                    'Kalýcý olarak silindi!',
                                    `${hardDeleteResult.Message}`,
                                    'success'
                                );

                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Baþarýsýz Ýþlem!',
                                    text: `${hardDeleteResult.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!");
                        }
                    });
                }
            });
        });
    /* HardDelete */
});