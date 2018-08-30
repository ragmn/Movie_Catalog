$(function () {

    $(document).ready(function () {
        $("#success-alert").hide();
        getData();
        $("#example_filter").append("<i id='add' title='ADD' class='fa fa-plus color-green hand-cursor fa-lg' aria-hidden='true' style='padding-left: 10px;'></i>");
    });

    var getData = function () {
        table = $('#example').DataTable({
            "processing": true,
            //"serverSide": true,
            //"orderMulti": false, // for disable multiple column at once
            "ajax": "../Movie/GetData",
            "dataSrc": 'data',
            "columns": [
                { "data": "movieID", "name": "Movie ID", "autoWidth": true, className: 'movieId' },
                { "data": "movieTitle", "name": "Movie Title", "autoWidth": true },
                { "data": "description", "name": "Description", "autoWidth": true },
                { "data": "imageURL", "name": "Image URL", "autoWidth": true },
                { "data": "releaseYear", "name": "Release Year", "autoWidth": true },
                { "data": "genre", "name": "Genre", "autoWidth": true },
                { "data": null, "orderable": false, "defaultContent": "<i title='EDIT' class='fas fa-edit color-green hand-cursor fa-lg edit' aria-hidden='true'></i><i title='DELETE' class='fas fa-trash-alt color-red fa-lg left-padding-8px hand-cursor delete'></i>", "sortable": false }

            ]
        });
    };


    $(document).on("click", "#add", function (element) {
        $("#title").val("");
        $("#url").val("");
        $("#desc").val("");
        $("#year").val("");
        $("#genre").val("");
        $('#myModalEdit').modal('show');
        $('#btnUpdate').text("ADD");
        $('#btnUpdate').attr("id", "btnAdd");
        $("#editModalLabel").text("ADD MOVIE");
    });

    $(document).on("click", "#btnAdd", function (element) {
        $.ajax({
            url: "../Movie/AddMovie",
            type: "POST",
            data:
                {
                    "data": JSON.stringify({
                        "MovieID": "{id}",
                        "description": $("#desc").val(),
                        "genre": $("#genre").val(),
                        "imageURL": $("#url").val(),
                        "movieID": "{id}",
                        "movieTitle": $("#title").val(),
                        "releaseYear": $("#year").val()
                    }),
                },
            success: function (data) {
                $('#myModalEdit').modal('hide');
                $("#success-alert").fadeTo(6000, 500).slideUp(500, function () {
                    $("#success-alert").slideUp(500)
                });
                table.ajax.reload(null, false);
            },
            error: function (err) {
                debugger;
            }
        });
    });




    $(document).on("click", ".edit", function (element) {
        $('#btnAdd').attr("id", "btnUpdate");
        $('#btnUpdate').text("UPDATE");
        $("#editModalLabel").text("EDIT MOVIE");
        $('#myModalEdit').find("#btnUpdate").attr("data-id", $(this).closest("tr").find(".movieId").text());
        var id = $(this).closest("tr").find(".movieId").text();
        $.ajax({
            url: "../Movie/GetMovie",
            type: "GET",
            data:
                {
                    'id': id
                },
            success: function (data) {
                $("#title").val(data.movieTitle);
                $("#url").val(data.imageURL);
                $("#desc").val(data.description);
                $("#year").val(data.releaseYear);
                $("#genre").val(data.genre);
                $('#myModalEdit').modal('show');
            },
            error: function (err) {
                debugger;
            }
        });
    });

    $(document).on("click", ".delete", function (element) {
        //$('#myModalDelete').find(".modal-content").find(".modal-body").text("Are you sure you want to delete?")
        //$("#btnDelete").show();
        $('#myModalDelete').find("#btnDelete").attr("data-id", $(this).closest("tr").find(".movieId").text());
        $('#myModalDelete').modal('show');
    });

    $(document).on("click", "#btnDelete", function (element) {
        var id = $(this).attr("data-id");
        $.ajax({
            url: "../Movie/Delete",
            type: "POST",
            data:
                {
                    'id': id
                },
            success: function (data) {
                $('#myModalDelete').modal('hide');
                $("#success-alert").fadeTo(6000, 500).slideUp(500, function () {
                    $("#success-alert").slideUp(500)
                });
                table.ajax.reload(null, false);
            },
            error: function (err) {
            }
        });
    });

    $(document).on("click", "#btnUpdate", function () {
        var id = $(this).attr("data-id");
        $.ajax({
            url: "../Movie/UpdateMovie",
            type: "POST",
            data:
                {
                    "data": JSON.stringify({
                        "description": $("#desc").val(),
                        "genre": $("#genre").val(),
                        "imageURL": $("#url").val(),
                        "movieID": id,
                        "movieTitle": $("#title").val(),
                        "releaseYear": $("#year").val()
                    }),
                    "movieId": id
                },
            success: function (data) {
                $('#myModalEdit').modal('hide');
                $("#success-alert").fadeTo(6000, 500).slideUp(500, function () {
                    $("#success-alert").slideUp(500)
                });
                table.ajax.reload(null, false);
            },
            error: function (err) {
            }
        });
    });
});
