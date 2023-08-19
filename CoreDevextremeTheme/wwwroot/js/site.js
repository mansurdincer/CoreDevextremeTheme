// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function showPopup() {
    $.ajax({
        type: "GET",
        url: "/Views/Shared/_ModalPopup.cshtml",
        success: function (data) {
            $('#modalPopup').html(data);
            $('#modalPopup').modal('show');
        }
    });
}

function AddItem(Table) {
    var table = document.getElementById(Table);
    var rows = table.getElementsByTagName('tr');
    var lastRow = rows[rows.length - 1];
    var newRow = table.insertRow();
    newRow.innerHTML = lastRow.innerHTML;

    var inputs = newRow.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {

        if (inputs[i].id.endsWith("Id")) {
            inputs[i].setAttribute("data-val-id", "ValidateNever");
        }

        var id = inputs[i].id;
        id = id.replace(/_(\d+)_/, "_" + (rows.length - 2) + "_");
        id = id.replace(/\[(\d+)\]/, "[" + (rows.length - 2) + "]");
        id = id.replace(/-(\d+)/, "-" + (rows.length) - 2);
        inputs[i].id = id;
        inputs[i].name = inputs[i].name.replace(/\[(\d+)\]/, "[" + (rows.length - 2) + "]");
        inputs[i].value = "";
    }
}

//generic remove
function RemoveItem(button, model) {
    var table = $(button).closest('table');
    //var tableName = $(table).attr('id');
    var tr = $(button).closest('tr');
    var rows = $(table).find('tbody tr');

    if (rows.length > 1) {
        $(tr).remove();
        //rows = $(table).find('tbody tr');
    }
    else {
        var inputs = $(tr).find('input');
        for (var i = 0; i < inputs.length; i++) {
            $(inputs[i]).val('');
        }
    }

    var itemIndex = -1;

    $(table).find('tr').each(function () {
        var this_row = $(this);
        var input_names = this_row.find('input').map(function () {
            return $(this).attr('name');
        }).get();

        for (var i = 0; i < input_names.length; i++) {
            var match = input_names[i].match(/\.([^.[]+)/);
            if (match !== null) {
                var propertyName = match[1];
                this_row.find('input[name$=".' + propertyName + '"]').attr('name', model + '[' + itemIndex + '].' + propertyName);
                //bu satır olmadan da çalışıyor ama id'lerdeki indexler yanlış görüntüleniyor. bu yüzden ekledim. 
                this_row.find('input[id$="__' + propertyName + '"]').attr('id', model + '_' + itemIndex + '__' + propertyName);
            }
        }
        itemIndex++;
    });
}

//üstteki fonksiyonun generic olmayan hali. örnek olsun diye bıraktım
function RemoveItem1(button, model) {
    var table = $(button).closest('table');
    var tr = $(button).closest('tr');
    var rows = $(table).find('tbody tr');
    if (rows.length > 1) {
        $(tr).remove();
    }

    var itemIndex = -1;
    $('#ChildTable tr').each(function () {
        var this_row = $(this);
        console.log(itemIndex);
        this_row.find('input[name$=".Id"]').attr('name', model + '[' + itemIndex + '].Id');
        this_row.find('input[name$=".Number"]').attr('name', model + '[' + itemIndex + '].Number');
        this_row.find('input[name$=".Name"]').attr('name', model + '[' + itemIndex + '].Name');

        this_row.find('input[id$="__Id"]').attr('id', model + '_' + itemIndex + '__Id');
        this_row.find('input[id$="__Number"]').attr('id', model + '_' + itemIndex + '__Number');
        this_row.find('input[id$="__Name"]').attr('id', model + '_' + itemIndex + '__Name');

        itemIndex++;
    });

}

function sortBySequence(rows, sequenceIndex) {
    rows.sort((a, b) => {
        const aValue = parseInt(a.querySelectorAll('input')[sequenceIndex].value);
        const bValue = parseInt(b.querySelectorAll('input')[sequenceIndex].value);
        return aValue - bValue;
    });
}

document.addEventListener('DOMContentLoaded', function () {
    const table = document.querySelector('table');
    if (!table) return;

    const headers = table.querySelectorAll('th');
    const sequenceIndex = Array.from(headers).findIndex(header => header.textContent.trim().match(/^(Sıra|Sequence)$/));

    if (sequenceIndex === -1) return;

    const tbody = table.querySelector('tbody');
    const rows = Array.from(tbody.getElementsByTagName('tr'));

    sortBySequence(rows, sequenceIndex);
    rows.forEach(row => tbody.appendChild(row));

    const button = document.querySelector('#sort-button');
    if (button) button.addEventListener('click', () => {
        sortBySequence(rows, sequenceIndex);
        rows.forEach(row => tbody.appendChild(row));
    });
});

function sortTableBySequence(tableId) {
    const tbody = document.querySelector(`#${tableId} tbody`);
    const rows = Array.from(tbody.getElementsByTagName('tr'));

    sortBySequence(rows, 0);
    rows.forEach(row => tbody.appendChild(row));
}

