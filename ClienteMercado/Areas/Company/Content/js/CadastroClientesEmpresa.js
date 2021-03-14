$(document).ready(function () {
    debugger;

    var url1 = $('#Url1').val();
    var url2 = $('#Url2').val();

    if ($('#inUF').val() != "")
        $('#inListaDeEstados').val($('#inUF').val());

    if ($('#inPais').val() != "")
        $('#inListaDePaises').val($('#inPais').val());

    //MÁSCARA CNPJ
    $(".cnpj").keydown(function () {
        $(".cnpj").mask("99.999.999/9999-99");
    });

    //MÁSCARA TELEFONE
    $(".telefone").keydown(function () {
        $(".telefone").mask("(00) 00000-0009");
    });

    $(".cep").mask("99.999-999");
    $(".cpf").mask("999.999.999-99");

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

    //BOTÃO DE GRAVAÇÃO DO NOVO REGISTRO --> TESTAR
    $("#btn-gravar").click(function (e) {
        debugger;

        //Validando o formulário
        var qtdCamposVazios = 0;
        var $inputsObrigatorios = $('.obg');
        $inputsObrigatorios.each(function () {
            $(this).css({ "border": "1px solid #ccc", "padding": "2px" });
            if ($(this).hasClass("obg")) {
                if ($(this).val() == "" || parseFloat($(this).val().replace(".", "").replace(".", "").replace(".", "").replace(",", ".")) == 0) {
                    $(this).css({ "border": "1px solid #F00", "padding": "2px" });
                    qtdCamposVazios++;
                }
            }
        });

        if (qtdCamposVazios == 0) {
            var cont = 0;
            var msg = "";
            var obj = {};

            //Obtendo o valores
            obj['cpf_UsuarioEmpresaCliente'] = $("#inCpfUsu").val();
            obj['nome_UsuarioEmpresaCliente'] = $("#inNomeUsu").val();
            obj['eMail1_UsuarioEmpresaCliente'] = $("#inEmail1Usu").val();
            obj['telefone1_UsuarioEmpresaCliente'] = $("#inTelefone1Usu").val();
            obj['pais_UsuarioEmpresaCliente'] = $("#inListaDePaises").val();
            obj['cepEndereco_UsuarioEmpresaCliente'] = $("#inCepUsu").val();
            obj['endereco_UsuarioEmpresaCliente'] = $("#inEnderecoUsu").val();
            obj['complementoEndereco_UsuarioEmpresaCliente'] = $("#inComplementoEndUsu").val();
            obj['bairro_UsuarioEmpresaCliente'] = $("#inBairroUsu").val();
            obj['cidade_UsuarioEmpresaCliente'] = $("#inCidadeUsu").val();
            obj['uf_UsuarioEmpresaCliente'] = $("#inListaDeEstados").val();

            $.ajax({
                type: "POST",
                url: "/CadastroClientesEmpresa/GravarRegistro",
                traditional: true,
                async: false,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(obj),
                success: function (data) {
                    $.unblockUI();

                    debugger;

                    if (data.status == "ok") {
                        new PNotify({
                            title: 'SUCESSO!',
                            text: 'DADOS GRAVADOS com SUCESSO!!',
                            type: 'success',
                            styling: 'bootstrap3',
                            icons: 'bootstrap3',
                            addclass: 'customsuccess',
                            animateSpeed: 'fast',
                            mouseReset: true,
                            Buttons: { closer: true }
                        });

                        var idRegistroGerado = data.idRegistroGerado;

                        //REDIRECIONAR PARA TELA DE EDIÇÃO
                        window.location.href = "/Company/CadastroClientesEmpresa/AlterarDados?id=" + idRegistroGerado;
                    }
                    else {
                        swal({ title: "Ocorreu algum erro na gravação!\nTente novamente.", type: "error", confirmButtonColor: "#337ab7" });
                    }
                }
            });
        }
        else {
            swal({ title: "ATENÇÃO!!", text: "Os campos EM VERMELHO devem ser preenchidos!", type: "warning", confirmButtonColor: "#337ab7" },
                function () {
                    $('#qtdaDois').focus();
                }
            );
        }
    });

    //BOTÃO DE ATUALIZAÇÃO DOS DADOS EDITADOS DO REGISTRO --> TESTAR
    $("#btn-alterar").click(function (e) {
        debugger;

        //Validando o formulário
        var qtdCamposVazios = 0;
        var $inputsObrigatorios = $('.obg');
        $inputsObrigatorios.each(function () {
            $(this).css({ "border": "1px solid #ccc", "padding": "2px" });
            if ($(this).hasClass("obg")) {
                if ($(this).val() == "" || parseFloat($(this).val().replace(".", "").replace(".", "").replace(".", "").replace(",", ".")) == 0) {
                    $(this).css({ "border": "1px solid #F00", "padding": "2px" });
                    qtdCamposVazios++;
                }
            }
        });

        if (qtdCamposVazios == 0) {
            var cont = 0;
            var msg = "";
            var obj = {};

            //Obtendo o valores
            obj['id_UsuarioEmpresaCliente'] = $("#inIEC").val();
            obj['cpf_UsuarioEmpresaCliente'] = $("#inCpfUsu").val();
            obj['nome_UsuarioEmpresaCliente'] = $("#inNomeUsu").val();
            obj['eMail1_UsuarioEmpresaCliente'] = $("#inEmail1Usu").val();
            obj['telefone1_UsuarioEmpresaCliente'] = $("#inTelefone1Usu").val();
            obj['pais_UsuarioEmpresaCliente'] = $("#inListaDePaises").val();
            obj['cepEndereco_UsuarioEmpresaCliente'] = $("#inCepUsu").val();
            obj['endereco_UsuarioEmpresaCliente'] = $("#inEnderecoUsu").val();
            obj['complementoEndereco_UsuarioEmpresaCliente'] = $("#inComplementoEndUsu").val();
            obj['bairro_UsuarioEmpresaCliente'] = $("#inBairroUsu").val();
            obj['cidade_UsuarioEmpresaCliente'] = $("#inCidadeUsu").val();
            obj['uf_UsuarioEmpresaCliente'] = $("#inListaDeEstados").val();

            $.ajax({
                type: "POST",
                url: "/CadastroClientesEmpresa/AtualizarRegistro",
                traditional: true,
                async: false,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(obj),
                success: function (data) {
                    $.unblockUI();

                    debugger;

                    if (data.status == "ok") {
                        new PNotify({
                            title: 'SUCESSO!',
                            text: 'DADOS ATUALIZADOS com SUCESSO!!',
                            type: 'success',
                            styling: 'bootstrap3',
                            icons: 'bootstrap3',
                            addclass: 'customsuccess',
                            animateSpeed: 'fast',
                            mouseReset: true,
                            Buttons: { closer: true }
                        });

                    }
                    else {
                        swal({ title: "Ocorreu algum erro na gravação!\nTente novamente.", type: "error", confirmButtonColor: "#337ab7" });
                    }
                }
            });
        }
        else {
            swal({ title: "ATENÇÃO!!", text: "Os campos EM VERMELHO devem ser preenchidos!", type: "warning", confirmButtonColor: "#337ab7" },
                function () {
                    $('#qtdaDois').focus();
                }
            );
        }
    });
});

function opLnk(id) {
    debugger;

    //REDIRECIONAR PARA TELA DE EDIÇÃO
    window.location.href = "/Company/CadastroClientesEmpresa/AlterarDados?id=" + idRegistroGerado;
}