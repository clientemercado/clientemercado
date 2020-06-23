using System;
using System.Collections.Generic;
using System.Linq;
using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;

namespace ClienteMercado.Infra.Repositories
{
    public class DGruposAtividadesEmpresaProfissionalRepository : RepositoryBase<grupo_atividades_empresa>
    {
        //Busca os Grupos de Atividades para montagem da Cotação
        public List<grupo_atividades_empresa> ListaGruposAtividadesEmpresaProfissional()
        {
                return _contexto.grupo_atividades_empresa.OrderBy(m => m.DESCRICAO_ATIVIDADE).ToList();
        }

        //Consultar dados do Grupo de Atividade registrado para a Empresa
        public grupo_atividades_empresa ConsultarDadosDoGrupoDeAtividadesDaEmpresa(grupo_atividades_empresa obj)
        {
                grupo_atividades_empresa atividadesEmpresa = 
                    _contexto.grupo_atividades_empresa.FirstOrDefault(m => m.ID_GRUPO_ATIVIDADES.Equals(obj.ID_GRUPO_ATIVIDADES));

                return atividadesEmpresa;
        }

        //CONSULTAR DADOS do RAMO de ATIVIDADES
        public grupo_atividades_empresa ConsultarDadosDoRamoDeAtividadeDaEmpesaPeloID(int iD_GRUPO_ATIVIDADES)
        {
            grupo_atividades_empresa dadosDoRamoDeAtividade = 
                _contexto.grupo_atividades_empresa.FirstOrDefault(m => (m.ID_GRUPO_ATIVIDADES == iD_GRUPO_ATIVIDADES));

            return dadosDoRamoDeAtividade;
        }

        //BUSCA DADOS sobre o GRUPO de ATIVIDADES
        public grupo_atividades_empresa ConsultarDadosGeraisSobreOGrupoDeAtividades(int iD_GRUPO_ATIVIDADES)
        {
            grupo_atividades_empresa dadosGA = 
                _contexto.grupo_atividades_empresa.FirstOrDefault(m => (m.ID_GRUPO_ATIVIDADES == iD_GRUPO_ATIVIDADES));

            return dadosGA;
        }

        //Buscar lista de CATEGORIAS ATACADISTAS
        public List<grupo_atividades_empresa> CarregarListaDeCategoriasAtacadistas()
        {
            List<grupo_atividades_empresa> listaCategoriaAtacadistas = 
                _contexto.grupo_atividades_empresa.Where(m => ((m.ID_CLASSIFICACAO_EMPRESA == 1) && (m.ID_GRUPO_ATIVIDADES > 61))).ToList(); //DEPOIS RETIRAR ESSA SENTENÇA && (m.ID_GRUPO_ATIVIDADES > 61)

            return listaCategoriaAtacadistas;
        }

        //Buscar lista de CATEGORIAS VAREJISTAS
        public List<grupo_atividades_empresa> CarregarListaDeCategoriasVarejistas()
        {
            List<grupo_atividades_empresa> listaCategoriaVrejistas =
                _contexto.grupo_atividades_empresa.Where(m => (m.ID_CLASSIFICACAO_EMPRESA == 2)).ToList();

            return listaCategoriaVrejistas;
        }
    }
}
