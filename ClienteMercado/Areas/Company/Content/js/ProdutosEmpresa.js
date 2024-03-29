﻿$(document).ready(function () {
    var url1 = $('#Url1').val();
    var url2 = $('#Url2').val();
    var url3 = $('#Url3').val();

    $(".data").mask("99/99/9999");
    $('.monetario').mask('000.000.000.000.000,00', { reverse: true });

    if ($('#inSDp').val() != "")
        $('#inListaSubDepto').val($('#inSDp').val());
    if ($('#inFb').val() != "")
        $('#inListaFabricantes').val($('#inFb').val());
    if ($('#inProm').val() != "")
        $('#inListaPromo').val($('#inProm').val());
    if ($('#inOps').val() != "")
        $('#inListaOpcoes').val($('#inOps').val());

    //BOTÃO NOVO CADASTRO
    $(document).on("click", "#btn-cadastrar", function () {
        debugger;

        //REDIRECIONA NOVO CADASTRO
        window.location.href = url1;
    });

    //BOTÃO EXCLUIR
    $(document).on("click", "#btn-excluir", function () {
        debugger;

        swal({ title: "ATENÇÃO:\n\nFUNCIONALIDE em CONSTRUÇÃO!!!", type: "warning", confirmButtonColor: "#337ab7" });
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
            obj['descricao_ProdutoEmpresaCliente'] = $("#inNomeProduto").val();
            obj['tipoEmbalagem_ProdutoEmpresaCliente'] = $("#inEmbalagem").val();
            obj['pesoEmbalagem_ProdutoEmpresaCliente'] = $("#inPeso").val();
            obj['unidadePesoEmbalagem_ProdutoEmpresaCliente'] = $("#inUnidade").val();
            obj['valorVenda_ProdutoEmpresaCliente'] = $("#inVlrVenda").val();
            obj['id_SubDepartamentoEmpresaCliente'] = $("#inListaSubDepto").val();
            obj['id_EmpresaFabricantesMarcas'] = $("#inListaFabricantes").val();
            obj['id_PromocaoVendaEmpresaCliente'] = $("#inListaPromo").val();
            obj['ativoInativo_ProdutoEmpresaCliente'] = $("#inListaOpcoes").val();            

            $.ajax({
                type: "POST",
                url: "/ProdutosEmpresa/GravarRegistro",
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
                        window.location.href = "/Company/ProdutosEmpresa/AlterarDados?id=" + idRegistroGerado;
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
            obj['iPEC'] = $('#inIdP').val();
            obj['descricao_ProdutoEmpresaCliente'] = $("#inNomeProduto").val();
            obj['tipoEmbalagem_ProdutoEmpresaCliente'] = $("#inEmbalagem").val();
            obj['pesoEmbalagem_ProdutoEmpresaCliente'] = $("#inPeso").val();
            obj['unidadePesoEmbalagem_ProdutoEmpresaCliente'] = $("#inUnidade").val();
            obj['valorVenda_ProdutoEmpresaCliente'] = $("#inVlrVenda").val();
            obj['id_SubDepartamentoEmpresaCliente'] = $("#inListaSubDepto").val();
            obj['id_EmpresaFabricantesMarcas'] = $("#inListaFabricantes").val();
            obj['id_PromocaoVendaEmpresaCliente'] = $("#inListaPromo").val();
            obj['ativoInativo_ProdutoEmpresaCliente'] = $("#inListaOpcoes").val();   

            $.ajax({
                type: "POST",
                url: "/ProdutosEmpresa/AtualizarRegistro",
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

    //BOTÃO PESQUISAR
    $(document).on("click", "#btn-pesquisar", function () {
        debugger;

        if ($('#inPesquisar').val() != "") {
            $('#gridProdutosEmpresa').bootgrid('reload');
        }
        else {
            swal({ title: "ATENÇÃO:\n\nDigite ALGO no campo do FILTRO para pesquisar.", type: "warning", confirmButtonColor: "#337ab7" });
        }
    });
});