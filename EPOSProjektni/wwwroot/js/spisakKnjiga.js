
var dataTable;

$(document).ready(function () {
    loadDataTable(); //ucitavanje tabele
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/books/getall/", // podaci koje cemo upisivati u tabelu
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "naziv", "width": "15%" }, // prva kolona - promenljiva naziv
            { "data": "autor", "width": "15%" }, // druga kolona - promenljiva autor
            { "data": "godina", "width": "15%" }, // treca kolona - promenljiva godina izdanja
            { "data": "brojStrana", "width": "15%" }, // cetvrta kolona - promenljiva broj strana
            {
                "data": "slika",  // peta kolona - promenljiva slika
                "render": function (data) {
                    return '<img src="../image/' + data + '" class="avatar" width="140" height="100"/>'; // upisivanje slike
                    // sa putanje wwwroot/image/data u celiju tabele
                }, "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    // buttons azuriraj i obrisi
                    return `<div class="text-center">
                        <a href="/Books/Update?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:90px;'>
                           Ažuriraj  
                        </a> 
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:90px;'
                            onclick=Delete('/books/Delete?id='+${data})>
                            Obriši
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "Nema podataka o knjigama" // ukoliko tabela nema zapisa, prikazace se nema podataka o knjigama
        },
        "width": "100%"
    });
}

function Delete(url) {
    // ukoliko korisnik klikne na obrisi, pojavljuje se sweet-alert notifikacija sa sledecim podacima:
    swal({
        title: "Da li ste sigurni da želite da obrišete zapis?",
        text: "Jednom kada obrišete, nećete više biti u mogućnosti da povratite dati zapis!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => { // ukoliko korisnik potvrdi da zeli da obrise zapis
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) { // ukoliko je zapis uspesno obrisan iz baze
                        toastr.success(data.message); // notifikacija sa porukom uspeha
                        dataTable.ajax.reload(); // azuriranje tabele
                    }
                    else {
                        toastr.error(data.message); // u suprotnom poruka neuspeha
                    }
                }
            });
        }
    });
}