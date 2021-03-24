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
            obj['descricao_DepartamentoEmpresaCliente'] = $("#inNomeDepto").val();
            //obj['ativoInativo_DepartamentoEmpresaCliente'] = $("#inValidade").val();

            $.ajax({
                type: "POST",
                url: "/DepartamentosEmpresa/GravarRegistro",
                traditional: true,
                async: false,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(obj),
                success: function (data) {
                    $.unblockUI();

                    debugger;

                    if (data.status == "ok") {
                        var idRegistroGerado = data.idRegistroGerado;

                        //--------------------------------------------------------------------------
                        //GRAVAR IMAGEM
                        if ($('#inNomeImagem').val() != "") {
                            //Gravando a imagem
                            $.ajax({
                                type: "POST",
                                url: "/DepartamentosEmpresa/GravarImagemLogoMarcaDepto",
                                dataType: "json",
                                data: {
                                    'idDepto': idRegistroGerado, 'imagem': $("#logoMarcaDepto").attr("src"), 'nome': $("#logoMarcaDepto").attr("nome"), 'extensao': $("#logoMarcaDepto").attr("extensao")
                                },
                                success: function (data) {
                                    $.unblockUI();

                                    if (data.status == "Nok") {
                                        swal({ title: "O arquivo selecionado não possui um formato válido!", text: "Os seguintes formatos são aceitos: .jpg .jpeg .png", type: "warning", confirmButtonColor: "#337ab7" });
                                    }
                                    else {
                                        new PNotify({
                                            title: 'SUCESSO!',
                                            text: 'IMAGEM do DEPARTAMETO GRAVADA com SUCESSO!!',
                                            type: 'success',
                                            styling: 'bootstrap3',
                                            icons: 'bootstrap3',
                                            addclass: 'customsuccess',
                                            animateSpeed: 'fast',
                                            mouseReset: true,
                                            Buttons: { closer: true }
                                        });
                                    }
                                },
                                error: function (result) {
                                    $.unblockUI();

                                    swal({ title: "Ocorreu algum erro na gravação da IMAGEM! \nTente novamente.", type: "error", confirmButtonColor: "#337ab7" });
                                }
                            });
                        }
                        //--------------------------------------------------------------------------

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

                        //REDIRECIONAR PARA TELA DE EDIÇÃO
                        window.location.href = "/Company/DepartamentosEmpresa/AlterarDados?id=" + idRegistroGerado;
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
            obj['iDEC'] = $("#inIEC").val();
            obj['descricao_DepartamentoEmpresaCliente'] = $("#inNomeDepto").val();
            //obj['ativoInativo_DepartamentoEmpresaCliente'] = $("#inValidade").val();

            $.ajax({
                type: "POST",
                url: "/DepartamentosEmpresa/AtualizarRegistro",
                traditional: true,
                async: false,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(obj),
                success: function (data) {
                    $.unblockUI();

                    debugger;

                    if (data.status == "ok") {
                        var idRegistroAtualizado = data.idRegistroAtualizado;

                        //--------------------------------------------------------------------------
                        //GRAVAR IMAGEM
                        if ($('#inNomeImagem').val() != "") {
                            //Gravando a imagem
                            $.ajax({
                                type: "POST",
                                url: "/DepartamentosEmpresa/GravarImagemLogoMarcaDepto",
                                dataType: "json",
                                data: {
                                    'idDepto': idRegistroAtualizado, 'imagem': $("#logoMarcaDepto").attr("src"), 'nome': $("#logoMarcaDepto").attr("nome"), 'extensao': $("#logoMarcaDepto").attr("extensao")
                                },
                                success: function (data) {
                                    $.unblockUI();

                                    if (data.status == "Nok") {
                                        swal({ title: "O arquivo selecionado não possui um formato válido!", text: "Os seguintes formatos são aceitos: .jpg .jpeg .png", type: "warning", confirmButtonColor: "#337ab7" });
                                    }
                                    else {
                                        new PNotify({
                                            title: 'SUCESSO!',
                                            text: 'IMAGEM do DEPARTAMENTO GRAVADA com SUCESSO!!',
                                            type: 'success',
                                            styling: 'bootstrap3',
                                            icons: 'bootstrap3',
                                            addclass: 'customsuccess',
                                            animateSpeed: 'fast',
                                            mouseReset: true,
                                            Buttons: { closer: true }
                                        });
                                    }
                                },
                                error: function (result) {
                                    $.unblockUI();

                                    swal({ title: "Ocorreu algum erro na gravação da IMAGEM! \nTente novamente.", type: "error", confirmButtonColor: "#337ab7" });
                                }
                            });
                        }
                        //--------------------------------------------------------------------------

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
            $('#gridDeptos').bootgrid('reload');
        }
        else {
            swal({ title: "ATENÇÃO:\n\nDigite ALGO no campo do FILTRO para pesquisar.", type: "warning", confirmButtonColor: "#337ab7" });
        }
    });

    //=======================================================
    //Carrega a imagem
    $("#fileupload").change(function (event) {
        debugger;

        var reader = new FileReader();

        $(reader).load(function (event) {

            $("#logoMarcaDepto").attr("src", event.target.result);
            var extension = event.target.result.replace(/^.*\./, '');
            //console.log(extension);
        });

        reader.readAsDataURL(event.target.files[0]);

        $("#logoMarcaDepto").attr("extensao", event.target.files[0].type);
        $("#logoMarcaDepto").attr("nome", event.target.files[0].name);

        $('#inNomeImagem').val(event.target.files[0].name);
    });
    //=======================================================
});