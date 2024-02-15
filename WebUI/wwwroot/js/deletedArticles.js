$(document).ready(function () {

    /* DataTables start here. */

    const dataTable = $('#deletedArticlesTable').DataTable({
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
                        url: '/Admin/Article/GetAllDeletedArticles/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#deletedArticlesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const articleResult = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(articleResult);
                            if (articleResult.Data.ResultStatus === 1) {
                                let categoriesArray = [];
                                $.each(articleResult.Data.Articles.$values,
                                    function (index, article) {
                                        const newArticle = getJsonNetObject(article, articleResult.Data.Articles.$values);
                                        let newCategory = getJsonNetObject(newArticle.Category, newArticle);
                                        if (newCategory !== null) {
                                            categoriesArray.push(newCategory);
                                        }
                                        if (newCategory === null) {
                                            newCategory = categoriesArray.find((category) => {
                                                return category.$id === newArticle.Category.$ref;
                                            });
                                        }
                                        const newTableRow = dataTable.row.add([
                                            newArticle.Id,
                                            newCategory.Name,
                                            newArticle.Title,
                                            `<img src="/img/${newArticle.Thumbnail}" alt="${newArticle.Title}" class="my-image-table" />`,
                                            `${convertShortDate(newArticle.Date)}`,
                                            newArticle.ViewCount,
                                            newArticle.CommentCount,
                                            `${newArticle.IsActive ? "Evet" : "Hayýr"}`,
                                            `${newArticle.IsDeleted ? "Evet" : "Hayýr"}`,
                                            `${convertShortDate(newArticle.CreatedDate)}`,
                                            newArticle.CreatedByName,
                                            `${convertShortDate(newArticle.ModifiedDate)}`,
                                            newArticle.ModifiedByName,
                                            `
                                <button class="btn btn-primary btn-sm btn-undo" data-id="${newArticle.Id}"><span class="fas fa-undo"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${newArticle.Id}"><span class="fas fa-minus-circle"></span></button>
                                            `
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr('name', `row_${newArticle.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#deletedArticlesTable').fadeIn(1400);
                            } else {
                                toastr.error(`${articleResult.Data.Message}`, 'Ýþlem Baþarýsýz!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#deletedArticlesTable').fadeIn(1000);
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

    /* Ajax POST / HardDeleting a Article starts from here */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="row_${id}"]`);
            const articleTitle = tableRow.find('td:eq(2)').text();
            Swal.fire({
                title: 'Kalýcý olarak silmek istediðinize emin misiniz?',
                text: `${articleTitle} baþlýklý makale kalýcý olarak silinicektir!`,
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
                        data: { articleId: id },
                        url: '/Admin/Article/HardDelete/',
                        success: function (data) {
                            const articleResult = jQuery.parseJSON(data);
                            if (articleResult.ResultStatus === 1) {
                                Swal.fire(
                                    'Silindi!',
                                    `${articleResult.Message}`,
                                    'success'
                                );

                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Baþarýsýz Ýþlem!',
                                    text: `${articleResult.Message}`,
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

    /* Ajax POST / HardDeleting a Article ends here */

    /* Ajax POST / UndoDeleting a Article starts from here */

    $(document).on('click',
        '.btn-undo',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="row_${id}"]`);
            let articleTitle = tableRow.find('td:eq(2)').text();
            Swal.fire({
                title: 'Arþivden geri getirmek istediðinize emin misiniz?',
                text: `${articleTitle} baþlýklý makale arþivden geri getirilecektir!`,
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
                        data: { articleId: id },
                        url: '/Admin/Article/UndoDelete/',
                        success: function (data) {
                            const articleUndoDeleteResult = jQuery.parseJSON(data);
                            console.log(articleUndoDeleteResult);
                            if (articleUndoDeleteResult.ResultStatus === 1) {
                                Swal.fire(
                                    'Arþivden Geri Getirildi!',
                                    `${articleUndoDeleteResult.Message}`,
                                    'success'
                                );

                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Baþarýsýz Ýþlem!',
                                    text: `${articleUndoDeleteResult.Message}`,
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
    /* Ajax POST / UndoDeleting a Article ends here */

});