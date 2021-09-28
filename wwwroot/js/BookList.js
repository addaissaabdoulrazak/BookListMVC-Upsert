
var dataTable;

$(Document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#DT_load').dataTable({

        "ajax": {
            "url": "/Books/Getall/",
            "type": "GET",
            "datatype": "json"
        },

        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "author", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class= "text-center">
                             <a href="/Books/Upsert?id=${data}" class= "btn btn-success text-white" style="cusor:pointer; width=70%;">Edit</a>
                         &nbsp;
                             <a  class= "btn btn-danger text-white" style="cusor:pointer; width=70%;" onclick=Delete('/Books/Delete?id='+${data})>Delete</a>

                            </div>`
                }, "width": "40%",
            }



        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"






    });
}
function Delete(url) {
    //copie Coller du code Swet-Alert
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function(data){
                    if (data.success) {

                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        )
                        datatable.ajax.reload();
                    }
                else {
                    toastr.error(data.message);
                }
            }





            })



}

function Delete(url) {

    //utilisation de sweet-Alert
   
            $.ajax({

                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {

                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        )
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }

                }
            });
        }
    });

}