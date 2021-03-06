$(document).ready(function () {
    var url1 = $('#Url1').val();
    var url2 = $('#Url2').val();
    var url3 = $('#Url3').val();

    //BOTÃO NOVO CADASTRO
    $(document).on("click", "#btn-cadastrar", function () {
        debugger;

        //REDIRECIONA NOVO CADASTRO
        window.location.href = url1;
    });

    //BOTÃO VOLTAR PÁGINA ANTERIOR
    $(document).on("click", "#btn-voltar", function () {
        debugger;

        window.location.href = url2;
    });

    //BOTÃO DE GRAVAÇÃO DO NOVO REGISTRO
    $("#btn-gravar").click(function (e) {
        debugger;

        window.location.href = url3;
    });
});