$(document).ready(function () {

    // Trumbowyg

    $('#text-editor').trumbowyg({
        lang: 'tr',
        btns: [
            ['viewHTML'],
            ['undo', 'redo'], // Only supported in Blink browsers
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['insertImage'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen'],
            ['foreColor', 'backColor'],
            ['emoji'],
            ['fontfamily'],
            ['fontsize']
        ],
        plugins: {
            colors: {
                foreColorList: [
                    'ff0000', '00ff00', '0000ff', '54e346'
                ],
                backColorList: [
                    '000', '333', '555'
                ],
                displayAsList: false
            }
        }
    });

    // Trumbowyg

    // Select2

    $('#categoryList').select2({
        theme: 'bootstrap4',
        placeholder: "L�tfen bir kategori se�iniz...",
        allowClear: true
    });

    // Select2


    // jQuery UI - DatePicker

    $(function () {
        $("#datepicker").datepicker({
            closeText: "kapat",
            prevText: "&#x3C;geri",
            nextText: "ileri&#x3e",
            currentText: "bug�n",
            monthNames: ["Ocak", "�ubat", "Mart", "Nisan", "May�s", "Haziran",
                "Temmuz", "A�ustos", "Eyl�l", "Ekim", "Kas�m", "Aral�k"],
            monthNamesShort: ["Oca", "�ub", "Mar", "Nis", "May", "Haz",
                "Tem", "A�u", "Eyl", "Eki", "Kas", "Ara"],
            dayNames: ["Pazar", "Pazartesi", "Sal�", "�ar�amba", "Per�embe", "Cuma", "Cumartesi"],
            dayNamesShort: ["Pz", "Pt", "Sa", "�a", "Pe", "Cu", "Ct"],
            dayNamesMin: ["Pz", "Pt", "Sa", "�a", "Pe", "Cu", "Ct"],
            weekHeader: "Hf",
            dateFormat: "dd.mm.yy",
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: "",
            duration: 1000,
            showAnim: "drop",
            showOptions: { direction: "down" },
            minDate: -3,
            maxDate: +3
        });
    });


    // jQuery UI - DatePicker
});