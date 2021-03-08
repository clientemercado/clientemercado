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

    //BOTÃO DE GRAVAÇÃO DO NOVO REGISTRO --> TESTAR
    $("#btn-gravar").click(function (e) {
        debugger;

        //Validando o formulário
        var qtdCamposVazios = 0;
        var $inputsObrigatorios = $('.obrigatorio');
        $inputsObrigatorios.each(function () {
            $(this).css({ "border": "1px solid #ccc", "padding": "2px" });
            if ($(this).hasClass("obrigatorio")) {
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

            ////Obtendo o valores
            //obj['cenCusCodigo'] = $("#CenCusCodigo").val();
            //obj['frenteServIndiceFilho'] = $("#FreSerIndice").val();
            //obj['frenteServFilho'] = $("#freSerCodigo").val();
            //obj['orcCodItemContrato'] = $("#orcCodigo").val();
            //obj['indiceItemContrato'] = $("#OrcSerIndice").val();
            //obj['codServItemContrato'] = $("#orcSerCodigo").val();
            //obj['iteConEmpCodigoItemContrato'] = $("#iteConEmpCodigo").val();
            //obj['saldoApurado'] = $("#qtdaUm").val();
            //obj['quantidadeNew'] = $("#qtdaDois").val();
            //obj['descAtivPrincPai'] = $("#descAtividadePrincipal").val();
            //obj['unidAtivPrinc'] = $("#unidDois").val();
            //obj['fatorXPrinc'] = $("#fatorX").val();
            //obj['previstoAtivPrinc'] = $("#hHPrevDois").val().replace(".", ",");
            //obj['ConEmpCodigo'] = $("#conEmpCodigo").val();
            //obj['AtiCodigoPai'] = $("#freSerCodigoPai").val();

            $.ajax({
                type: "POST",
                url: "/MeiosPagamentoEmpresa/GravarRegistro",
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
                        window.location.href = url3;
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
        var $inputsObrigatorios = $('.obrigatorio');
        $inputsObrigatorios.each(function () {
            $(this).css({ "border": "1px solid #ccc", "padding": "2px" });
            if ($(this).hasClass("obrigatorio")) {
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

            ////Obtendo o valores
            //obj['cenCusCodigo'] = $("#CenCusCodigo").val();
            //obj['frenteServIndiceFilho'] = $("#FreSerIndice").val();
            //obj['frenteServFilho'] = $("#freSerCodigo").val();
            //obj['orcCodItemContrato'] = $("#orcCodigo").val();
            //obj['indiceItemContrato'] = $("#OrcSerIndice").val();
            //obj['codServItemContrato'] = $("#orcSerCodigo").val();
            //obj['iteConEmpCodigoItemContrato'] = $("#iteConEmpCodigo").val();
            //obj['saldoApurado'] = $("#qtdaUm").val();
            //obj['quantidadeNew'] = $("#qtdaDois").val();
            //obj['descAtivPrincPai'] = $("#descAtividadePrincipal").val();
            //obj['unidAtivPrinc'] = $("#unidDois").val();
            //obj['fatorXPrinc'] = $("#fatorX").val();
            //obj['previstoAtivPrinc'] = $("#hHPrevDois").val().replace(".", ",");
            //obj['ConEmpCodigo'] = $("#conEmpCodigo").val();
            //obj['AtiCodigoPai'] = $("#freSerCodigoPai").val();

            $.ajax({
                type: "POST",
                url: "/MeiosPagamentoEmpresa/AtualizarRegistro",
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