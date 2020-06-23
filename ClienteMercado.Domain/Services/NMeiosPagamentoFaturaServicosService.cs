using System.Collections.Generic;
using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;

namespace ClienteMercado.Domain.Services
{
    public class NMeiosPagamentoFaturaServicosService
    {
        DMeiosPagamentoFaturaServicosRepository dmeiospagamentofaturaservicos = new DMeiosPagamentoFaturaServicosRepository();

        public List<meios_pagamento_fatura_servicos> ListaDeMeiosPagamento()
        {
            return dmeiospagamentofaturaservicos.ListaDeMeiosPagamento();
        }
    }
}
