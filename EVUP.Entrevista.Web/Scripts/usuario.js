function deleteCliente(id) {
    if (confirm("Você tem certeza que gostaria de excluir este registro?")) {
        $.ajax({
            method: "POST",
            url: "/Usuario/Delete/" + id,
            success: function (data) {
                $("#tblUsuario tbody > tr").remove();
                $.each(data, function (i, usuario) {
                    $("#tblUsuario tbody").append(
                        "<tr>" +
                        "   <td>" + (usuario.Nome ?? '') + "</td>" +
                        "   <td>" + (usuario.Login ?? '') + "</td>" +
                        "   <td>" +
                        "       <a href='/Usuario/Edit/" + usuario.Id + "'>Editar</a> |" +
                        "       <button type=\"button\" class=\"btn btn-link\" data-item=\"" + usuario.Id + "\">Excluir</button>" +
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