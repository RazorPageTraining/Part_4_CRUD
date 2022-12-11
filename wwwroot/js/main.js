(function ($)
{
    var t = $('#data').DataTable({
        pageLength: 5,
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "SEMUA"]],
        "order": [[ $('th.sort-this').index(), 'desc' ]],
        "columnDefs": [ 
            {
                "searchable": false,
                "orderable": false,
                "targets": "dont-sort"
            },
        ],
    });

    t.on( 'order.dt search.dt', function () {
        t.column(0, {search:'applied', order:'applied'}).nodes().each( function (cell, i) {
            cell.innerHTML = i+1;
        } );
    }).draw(); 
})(jQuery);

function ConfirmDelete()
{
    return confirm("Are you sure you want to delete?");
}
