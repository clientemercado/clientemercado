$(document).ready(function () {
    var url1 = $('#Url1').val();
    var url2 = $('#Url2').val();

    //BOTÃO NOVO CADASTRO
    $(document).on("click", "#btn-cadastrar", function () {
        debugger;

        //REDIRECIONA NOVO CADASTRO
        window.location.href = url1;
    });

    //BOTÃO VOLTAR PÁGINA ANTERIOR
    $(document).on("click", "#inVoltarPagina", function () {
        debugger;

        window.location.href = url2;
    });
});