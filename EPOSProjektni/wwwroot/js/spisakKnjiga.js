
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/books/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "naziv", "width": "20%" },
            { "data": "autor", "width": "20%" },
            { "data": "godina", "width": "20%" },
            { "data": "brojStrana", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Books/Update?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:90px;'>
                           Azuriraj
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:90px;'
                            onclick=Delete('/books/Delete?id='+${data})>
                            Obrisi
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "Nema podataka o knjigama"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Da li ste sigurni da zelite da obrisete zapis?",
        text: "Jednom kada obrisete, necete biti vise u mogucnosti da povratite dati zapis",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}