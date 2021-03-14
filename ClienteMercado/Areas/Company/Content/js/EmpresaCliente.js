$(document).ready(function () {
    var url1 = $('#Url1').val();
    var url2 = $('#Url2').val();

    //=================================================================
    //CONSULTA os DADOS e CARREGA o GRID ao entrar na página
    var grid = $('#gridEmpresasClientes').bootgrid({
        ajax: true,
        navigation: 0, //CABEÇALHO: 2 - Exibe só RODAPÉ
        columnSelection: false, //SELETOR de COLUNA: True - Habilita / False - Desabilita
        url: "/PedidosClientes/BuscarListaEmpresasClientes",
        post: function () {
            /* PARÂMETROS a serem enviados na REQUISIÇÃO AJAX */
            debugger;

            return {
                'idPedido': $('#inIdPed').val()
            };
        },
        selection: true,
        multiSelect: true,
        rowSelect: true,
        keepSelection: true,
        formatters: {
            "link0": function (column, row) {
                return "<input type='checkbox' name='checkbox' id='" + row.idProdutoPedido + "' class='itemcotado' title='' value='" + row.idProdutoPedido + "'>";
            },
            "link1": function (column, row) {
                return row.itemPedido;
            },
            "link2": function (column, row) {
                return row.quantidadeItemPedido;
            },
            "link3": function (column, row) {
                return row.valorUnitarioItemPedido;
            },
            "link4": function (column, row) {
                return row.totalProdutoComprado;
            },
            "link5": function (column, row) {
                return row.dataEntregaItemPedido;
            },
            "link6": function (column, row) {
                return row.motivoNaoEntregaDotemPedido;
            }
        }
    }).on("loaded.rs.jquery.bootgrid", function () {
        /* Executa depois que os dados são carregados e renderizados */

    });
    //=================================================================

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
            obj['cnpj_EmpresaCliente'] = $("#inCNPJEmpresa").val();
            obj['razaoSocial_EmpresaCliente'] = $("#inRazaoSocialEmpresa").val();
            obj['nomeFantasia_EmpresaCliente'] = $("#inNomeFantasiaEmpresa").val();
            obj['email1_EmpresaCliente'] = $("#inEmail1Empresa").val();
            obj['telefone1_EmpresaCliente'] = $("#inTelefone1Empresa").val();
            obj['pais_EmpresaCliente'] = $("#inListaDePaises").val();
            obj['cepEndereco_EmpresaCliente'] = $("#inCepEmpresa").val();
            obj['endereco_EmpresaCliente'] = $("#inEnderecoEmpresa").val();
            obj['complementoEndereco_EmpresaCliente'] = $("#inComplementoEndEmpresa").val();
            obj['bairro_EmpresaCliente'] = $("#inBairroEmpresa").val();
            obj['cidade_EmpresaCliente'] = $("#inCidadeEmpresa").val();
            obj['uf_EmpresaCliente'] = $("#inListaDeEstados").val();

            $.ajax({
                type: "POST",
                url: "/EmpresaCliente/GravarRegistro",
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
                        window.location.href = "/Company/EmpresaCliente/AlterarDados?id=" + idRegistroGerado;
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
            obj['iEC'] = $('#inIEC').val();
            obj['cnpj_EmpresaCliente'] = $("#inCNPJEmpresa").val();
            obj['razaoSocial_EmpresaCliente'] = $("#inRazaoSocialEmpresa").val();
            obj['nomeFantasia_EmpresaCliente'] = $("#inNomeFantasiaEmpresa").val();
            obj['email1_EmpresaCliente'] = $("#inEmail1Empresa").val();
            obj['telefone1_EmpresaCliente'] = $("#inTelefone1Empresa").val();
            obj['pais_EmpresaCliente'] = $("#inListaDePaises").val();
            obj['cepEndereco_EmpresaCliente'] = $("#inCepEmpresa").val();
            obj['endereco_EmpresaCliente'] = $("#inEnderecoEmpresa").val();
            obj['complementoEndereco_EmpresaCliente'] = $("#inComplementoEndEmpresa").val();
            obj['bairro_EmpresaCliente'] = $("#inBairroEmpresa").val();
            obj['cidade_EmpresaCliente'] = $("#inCidadeEmpresa").val();
            obj['uf_EmpresaCliente'] = $("#inListaDeEstados").val();

            $.ajax({
                type: "POST",
                url: "/EmpresaCliente/AtualizarRegistro",
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