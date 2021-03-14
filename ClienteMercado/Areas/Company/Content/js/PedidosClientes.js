$(document).ready(function () {
    var url1 = $('#Url1').val();
    var url2 = $('#Url2').val();
    var url3 = $('#Url3').val();

    $(".data").mask("99/99/9999");
    $('.monetario').mask('000.000.000.000.000,00', { reverse: true });

    if ($('#inEntr').val() != "")
        $('#inListaOpcoes').val($('#inEntr').val());

    //---------------------------------------------------------------------------
    //===================================================================================
    //CONSULTA os DADOS e CARREGA o GRID ao entrar na página
    var grid = $('#gridItensPedido').bootgrid({
        ajax: true,
        navigation: 0, //CABEÇALHO: 2 - Exibe só RODAPÉ
        columnSelection: false, //SELETOR de COLUNA: True - Habilita / False - Desabilita
        url: "/PedidosClientes/BuscarListaDeProdutosDoPedido",
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
        ///* Executa depois que os dados são carregados e renderizados */

        ////PARTICIPAR da CENTRAL de COMPRAS
        //grid.find(".command-participar").on("click", function (e) {

        //    var codCC = $(this).attr("data-row-id");

        //    swal({
        //        title: "ATENÇÃO!!\n\n",
        //        text: "Deseja participar desta Central de Compras?",
        //        type: "warning",
        //        showCancelButton: true,
        //        confirmButtonColor: "#337ab7",
        //        confirmButtonText: "Sim",
        //        cancelButtonText: "Não",
        //        closeOnConfirm: true,
        //    },
        //        function (isConfirm) {
        //            if (isConfirm) {
        //                debugger;

        //                $.ajax({
        //                    type: "POST",
        //                    url: "/CentraisDeCompras/SolicitarParticipacaoNaCC",
        //                    dataType: 'json',
        //                    data: {
        //                        'cCC': codCC
        //                    },
        //                    success: function (data) {
        //                        debugger;

        //                        if (data.participacaoSolicitada == "Ok") {
        //                            debugger;

        //                            swal.close(); //FECHA SWAL MSG

        //                            //EXIBE MSG LATERAL À DIREITA SUPERIOR
        //                            new PNotify({
        //                                title: 'Sucesso!',
        //                                text: 'SOLICITAÇÃO de PARTICIPAÇÃO ENVIADA!\n\nAguarde a LIBERAÇÃO.',
        //                                type: 'success',
        //                                styling: 'bootstrap3',
        //                                icons: 'bootstrap3',
        //                                addclass: 'customsuccess',
        //                                animateSpeed: 'fast',
        //                                mouseReset: true,
        //                                Buttons: {
        //                                    closer: true
        //                                }
        //                            });

        //                            $('#gridCentraisDeComprasDoSistema').bootgrid('reload');
        //                        }
        //                    }
        //                });
        //            }
        //            else {
        //                $('#gridCentraisDeComprasDoSistema').bootgrid('reload');
        //            }
        //        });

        //});
    });
    //---------------------------------------------------------------------------

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

    ////BOTÃO DE GRAVAÇÃO DO NOVO REGISTRO --> TESTAR
    //$("#btn-gravar").click(function (e) {
    //    debugger;

    //    //Validando o formulário
    //    var qtdCamposVazios = 0;
    //    var $inputsObrigatorios = $('.obg');
    //    $inputsObrigatorios.each(function () {
    //        $(this).css({ "border": "1px solid #ccc", "padding": "2px" });
    //        if ($(this).hasClass("obg")) {
    //            if ($(this).val() == "" || parseFloat($(this).val().replace(".", "").replace(".", "").replace(".", "").replace(",", ".")) == 0) {
    //                $(this).css({ "border": "1px solid #F00", "padding": "2px" });
    //                qtdCamposVazios++;
    //            }
    //        }
    //    });

    //    if (qtdCamposVazios == 0) {
    //        var cont = 0;
    //        var msg = "";
    //        var obj = {};

    //        //Obtendo o valores
    //        obj['cnpj_EmpresaCliente'] = $("#inVlrPedido").val();
    //        obj['razaoSocial_EmpresaCliente'] = $("#inPedEntregue").val();
    //        obj['nomeFantasia_EmpresaCliente'] = $("#inPedDataEntrega").val();
    //        obj['email1_EmpresaCliente'] = $("#inUsuEntrega").val();

    //        $.ajax({
    //            type: "POST",
    //            url: "/PedidosClientes/GravarRegistro",
    //            traditional: true,
    //            async: false,
    //            dataType: "json",
    //            contentType: "application/json",
    //            data: JSON.stringify(obj),
    //            success: function (data) {
    //                $.unblockUI();

    //                debugger;

    //                if (data.status == "ok") {
    //                    new PNotify({
    //                        title: 'SUCESSO!',
    //                        text: 'DADOS GRAVADOS com SUCESSO!!',
    //                        type: 'success',
    //                        styling: 'bootstrap3',
    //                        icons: 'bootstrap3',
    //                        addclass: 'customsuccess',
    //                        animateSpeed: 'fast',
    //                        mouseReset: true,
    //                        Buttons: { closer: true }
    //                    });

    //                    var idRegistroGerado = data.idRegistroGerado;

    //                    //REDIRECIONAR PARA TELA DE EDIÇÃO
    //                    window.location.href = "/Company/PedidosClientes/AlterarDados?id=" + idRegistroGerado;
    //                }
    //                else {
    //                    swal({ title: "Ocorreu algum erro na gravação!\nTente novamente.", type: "error", confirmButtonColor: "#337ab7" });
    //                }
    //            }
    //        });
    //    }
    //    else {
    //        swal({ title: "ATENÇÃO!!", text: "Os campos EM VERMELHO devem ser preenchidos!", type: "warning", confirmButtonColor: "#337ab7" },
    //            function () {
    //                $('#qtdaDois').focus();
    //            }
    //        );
    //    }
    //});

    //============================================================================
    //AUCOMPLETE USUÁRIOS FUNCIONÁRIOS da EMPRESA CLIENTE
    $("#inUsuEntrega").autocomplete({
        source: function (request, response) {
            debugger;
            var termo = $("#inUsuEntrega").val();

            $.ajax({
                type: "POST",
                url: "/PedidosClientes/BuscarListaUsuariosFuncionariosEmpresaCliente",
                dataType: "json",
                data: { 'term': termo },
                success: function (data) {
                    debugger;

                    response($.map(data, function (item) {
                        return {
                            label: item.nome_UsuarioEmpresaCliente,
                            value: item.id_UsuarioEmpresaCliente
                        };
                    }));
                }
            });
        },
        select: function (event, ui) {
            debugger;

            $("#inUsuEntrega").val(ui.item.label);
            $("#inUsuEntrega_").val(ui.item.label);
            $("#inUsuEntrega").hide();
            $("#inUsuEntrega_").show();
            $("#inIdUsuEntrega").val(ui.item.value);

        }, minLength: 3,
    });
    //============================================================================

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

            //Obtendo o valores
            obj['idPedidoCliente'] = $('#inIdPed').val();
            obj['valorPedido_PedidoClienteEmpresaCliente'] = $("#inVlrPedido").val();
            obj['pedidoEntregue_PedidoClienteEmpresaCliente'] = $("#inListaOpcoes").val();
            obj['dataEntregaPedido_ClienteEmpresaCliente'] = $("#inPedDataEntrega").val();
            obj['idUsuarioEmpresaEntregou_ClienteEmpresaCliente'] = $("#inIdUsuEntrega").val();

            $.ajax({
                type: "POST",
                url: "/PedidosClientes/AtualizarRegistro",
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

function verificaCampo() {
    $('#inIdUsuEntrega').val('');
    $('#inUsuEntrega').val('');
    $('#inUsuEntrega_').val('');
    $('#inUsuEntrega_').hide();
    $('#inUsuEntrega').show();
    $('#inUsuEntrega').focus();
}
