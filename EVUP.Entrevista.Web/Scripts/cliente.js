function deleteCliente(id) {
    if (confirm("Você tem certeza que gostaria de excluir este registro?")) {
        $.ajax({
            method: "POST",
            url: "/Cliente/Delete/" + id,
            success: function (data) {
                $("#tblCliente tbody > tr").remove();
                $.each(data, function (i, cliente) {
                    $("#tblCliente tbody").append(
                        "<tr>" +
                        "   <td>" + (cliente.Nome ?? '') + "</td>" +
                        "   <td>" + (cliente.Telefone ?? '') + "</td>" +
                        "   <td>" + (cliente.Endereco ?? '') + "</td>" +
                        "   <td>" + (cliente.Email ?? '') + "</td>" +
                        "   <td>" + (cliente.Cidade ?? '') + "</td>" +
                        "   <td>" + (cliente.Genero ?? '') + "</td>" +
                        "   <td>" +
                        "       <a href='/Cliente/Edit/" + cliente.Id + "'>Editar</a> |" +
                        "       <button type=\"button\" class=\"btn btn-link\" data-item=\"" + cliente.Id + "\">Excluir</button>" +
                        "   </td>" +
                        "</tr>"
                    );
                });
            },
            error: function (data) {
                alert("Houve um erro na pesquisa.");
            }
        });
    }
}