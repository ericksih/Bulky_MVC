﻿
var dataTale;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'title' },
            { data: 'isbn' },
            { data: 'listPrice' },
            { data: 'author' },
            { data: 'category.name' },
            { data: 'description' },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="btn-group">
                            <a href="/admin/product/upsert?id=${data}" class="btn btn-sm btn-outline-primary mx-2">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-sm btn-outline-danger mx-2">
                                <i class="bi bi-trash-fill"></i>
                            </a>
                        </div>
                    `;
                },
                orderable: false
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
               $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}



