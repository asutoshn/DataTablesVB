﻿@Code
    ViewData("Title") = "ShowGrid"
    Layout = Nothing
End Code


<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>ShowGrid</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link href="~/Content/bootstrap.css" rel="stylesheet" />

    <link href="https://cdn.datatables.net/1.10.15/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/responsive/2.1.1/css/responsive.bootstrap.min.css" rel="stylesheet" />

    <script src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.15/js/dataTables.bootstrap4.min.js "></script>

    <script>
        $(document).ready(function () {
            $("#demoGrid").DataTable({

                "processing": true, // for show progress bar
                "serverSide": true, // for process server side
                "filter": true, // this is for disable filter (search box)
                "orderMulti": false, // for disable multiple column at once
                "pageLength": 5,

                "ajax": {
                    "url": "/Demo/LoadData",
                    "type": "POST",
                    "datatype": "json"
                },

                "columnDefs":
                [{
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [7],
                    "searchable": false,
                    "orderable": false
                },
                {
                    "targets": [8],
                    "searchable": false,
                    "orderable": false
                },
                {
                    "targets": [9],
                    "searchable": false,
                    "orderable": false
                }],

                "columns": [
                      { "data": "CustomerID", "name": "CustomerID", "autoWidth": true },
                      { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
                      { "data": "ContactName", "title": "ContactName", "name": "ContactName", "autoWidth": true },
                      { "data": "ContactTitle", "name": "ContactTitle", "autoWidth": true },
                      { "data": "City", "name": "City", "autoWidth": true },
                      { "data": "PostalCode", "name": "PostalCode", "autoWidth": true },
                      { "data": "Country", "name": "Country", "autoWidth": true },
                      { "data": "Phone", "name": "Phone", "title": "Status", "autoWidth": true },

                      {
                          "render": function (data, type, full, meta) {
                              return '<a class="btn btn-info" href="/Demo/Edit/' + full.CustomerID + '">Edit</a>';
                          }
                      },

                       {
                           data: null, render: function (data, type, row) {
                               return "<a href='#' class='btn btn-danger' onclick=DeleteData('" + row.CustomerID + "'); >Delete</a>";
                           }
                       },

                ]

            });
        });
    </script>

    <script>

        function DeleteData(CustomerID) {
            if (confirm("Are you sure you want to delete ...?")) {
                Delete(CustomerID);
            }
            else {
                return false;
            }
        }


        function Delete(CustomerID) {
            var url = '@Url.Content("~/")' + "Demo/DeleteCustomer";
            $.post(url, { ID: CustomerID }, function (data) {
                if (data == "Deleted") {
                    alert("Delete Customer !");
                    oTable = $('#demoGrid').DataTable();
                    oTable.draw();
                }
                else {
                    alert("Something Went Wrong!");
                }
            });
        }
    </script>

</head>
<body>
    <div class="container">
        <br />
        <div style="width:90%; margin:0 auto;">
            <table id="demoGrid" class="table table-striped table-bordered dt-responsive nowrap" width="100%" cellspacing="0">
                <thead>
                    <tr>
                        <th>CustomerID</th>
                        <th>CompanyName</th>
                        <th>ContactName</th>
                        <th>ContactTitle</th>
                        <th>City</th>
                        <th>PostalCode</th>
                        <th>Country</th>
                        <th>Phone</th>
                        <th>Edit</th>
                        <th>Delete</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
</body>
</html>

