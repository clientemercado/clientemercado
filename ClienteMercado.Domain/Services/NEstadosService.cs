using System.Collections.Generic;
using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;

namespace ClienteMercado.Domain.Services
{
    public class NEstadosService
    {
        DEstadosRepository destados = new DEstadosRepository();

        //Carrega a lista de Estados brasileiros
        public List<estados_empresa_usuario> ListaEstados()
        {
            return destados.ListaEstados();
        }

        //Busca dados do ESTADO - UF
        public estados_empresa_usuario ConsultarDadosDoEstado(int idEstado)
        {
            return destados.ConsultarDadosDoEstado(idEstado);
        }
    }
}
