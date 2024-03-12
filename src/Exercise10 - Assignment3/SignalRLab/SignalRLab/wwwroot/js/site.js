$(() => {
    LoadProdData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadProducts", function () {
        LoadProdData();
    });
    LoadProdData();

    function LoadProdData() {
        var tr = '';
        $.ajax({
            url: '/Products/GetProducts',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                        <td> ${v.proName} </td>
                        <td> ${v.category} </td>
                        <td> ${v.unitPrice} </td>
                        <td> ${v.stockQty} </td>
                        <td>
                            <a href='../Products/Edit?id=${v.proId}'> Edit </a>
                            <a href='../Products/Details?id=${v.proId}'> Details </a>
                            <a href='../Products/Delete?id=${v.proId}'> Delete </a>
                        </td>
                        </tr>`;
                });
                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});
