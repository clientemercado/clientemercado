using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClienteMercado.Infra.Repositories
{
    public class DEmpresasFabricantesMarcasRepository : RepositoryBase<empresas_fabricantes_marcas>
    {
        //Carrega a Lista de Fabricantes e Narcas registrados no bd, conforme o termo informado
        public List<ListaDeEmpresasFabricantesEMarcasViewModel> ListaDeFabricantesEMarcas(string term)
        {
            var query = "SELECT * FROM empresas_fabricantes_marcas WHERE DESCRICAO_EMPRESA_FABRICANTE_MARCAS LIKE '%" + term + "%'";

            var result = _contexto.Database.SqlQuery<ListaDeEmpresasFabricantesEMarcasViewModel>(query).ToList();
            return result;
        }

        //Gravar nova Empresa Fabricante ou Marcas
        public empresas_fabricantes_marcas GravarNovaEmpresaFabricanteEMarcas(empresas_fabricantes_marcas obj)
        {
            empresas_fabricantes_marcas gravarNovaEmpresaFabricanteOuMarcas = new empresas_fabricantes_marcas();

            //Verifica se a NOVA EMPRESA FABRICANTE ou MARCA já existe
            empresas_fabricantes_marcas dadosEmpresaOuMarca =
                _contexto.empresas_fabricantes_marcas.FirstOrDefault(m => (m.DESCRICAO_EMPRESA_FABRICANTE_MARCAS == obj.DESCRICAO_EMPRESA_FABRICANTE_MARCAS));

            if (dadosEmpresaOuMarca == null)
            {
                //Grava SE NÃO EXISTIR
                gravarNovaEmpresaFabricanteOuMarcas =
                    _contexto.empresas_fabricantes_marcas.Add(obj);
                _contexto.SaveChanges();
            }
            else
            {
                //Retorna os dados SE EXISTIR
                gravarNovaEmpresaFabricanteOuMarcas = dadosEmpresaOuMarca;
            }

            return gravarNovaEmpresaFabricanteOuMarcas;
        }

        //BUSCAR MARCA do PRODUTO
        public string ConsultarDescricaoDaEmpresaFabricanteOuMarca(int idFabricanteMarca)
        {
            empresas_fabricantes_marcas dadosDaEmpresaFabricante =
                _contexto.empresas_fabricantes_marcas.FirstOrDefault(m => (m.ID_CODIGO_EMPRESA_FABRICANTE_MARCAS == idFabricanteMarca));

            return dadosDaEmpresaFabricante.DESCRICAO_EMPRESA_FABRICANTE_MARCAS;
        }

        /// <summary>
        /// CONSULTAR DADOS EMPRESA FABRICANTE ou MARCA
        /// </summary>
        /// <returns></returns>
        public Empresa_FabricantesMarcas ConsultarDadosDaEmpresaFabricante(Empresa_FabricantesMarcas obj)
        {
            try
            {
                Empresa_FabricantesMarcas dadosEmpresaFabricante = 
                    _contexto.empresa_fabricanteMarcas.FirstOrDefault(m => (m.id_EmpresaFabricantesMarcas == obj.id_EmpresaFabricantesMarcas));

                return dadosEmpresaFabricante;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CARREGAR LISTA de EMPRESAS FABRICANTES e MARCAS
        /// </summary>
        /// <returns></returns>
        public List<Empresa_FabricantesMarcas> ListaGeralFabricantesEMarcas()
        {
            try
            {
                List<Empresa_FabricantesMarcas> listaFabricantesMarcas = 
                    _contexto.empresa_fabricanteMarcas.Where(m => (m.id_EmpresaFabricantesMarcas > 0)).ToList();

                return listaFabricantesMarcas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ALTERAR DADOS FABRICANTE MARCA
        /// </summary>
        /// <returns></returns>
        public void AlterarDadosEmpresaFabricanteMarca(Empresa_FabricantesMarcas obj)
        {
            try
            {
                Empresa_FabricantesMarcas dadosEmpresaFabricante =
                    _contexto.empresa_fabricanteMarcas.FirstOrDefault(m => (m.id_EmpresaFabricantesMarcas == obj.id_EmpresaFabricantesMarcas));

                if (dadosEmpresaFabricante != null)
                {
                    dadosEmpresaFabricante.descricao_EmpresaFabricantesMarcas = obj.descricao_EmpresaFabricantesMarcas;

                    _contexto.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// GRAVAR FABRICANTE MARCA
        /// </summary>
        /// <returns></returns>
        public Empresa_FabricantesMarcas GravarNovEmpresaFabricanteMarca(Empresa_FabricantesMarcas obj)
        {
            try
            {
                Empresa_FabricantesMarcas dadosNovEmpresaFabricanteMarca = _contexto.empresa_fabricanteMarcas.Add(obj);
                _contexto.SaveChanges();

                return dadosNovEmpresaFabricanteMarca;
            }
            catch (Exception e)
            {
                throw e;
            }

            throw new NotImplementedException();
        }

        //TRAZ MARCAS/FABRICANTES VINCULADAS AO PRODUTO
        public List<ListaDeEmpresasFabricantesEMarcasViewModel> ListaDeFabricantesEMarcasVinculadasAoProduto(string term, int codProduto)
        {
            List<ListaDeEmpresasFabricantesEMarcasViewModel> marcasEncontradas = new List<ListaDeEmpresasFabricantesEMarcasViewModel>();

            List<empresas_produtos_marcas> listaDeMarcasVinculadasAoProduto =
                _contexto.empresas_produtos_marcas.Where(m => (m.ID_CODIGO_PRODUTOS_SERVICOS_EMPRESAS_PROFISSIONAIS == codProduto)).ToList();

            if (listaDeMarcasVinculadasAoProduto.Count > 0)
            {
                int[] codMarcas = new int[listaDeMarcasVinculadasAoProduto.Count];

                for (int i = 0; i < listaDeMarcasVinculadasAoProduto.Count; i++)
                {
                    codMarcas[i] = listaDeMarcasVinculadasAoProduto[i].ID_CODIGO_EMPRESA_FABRICANTE_MARCAS;
                }

                string codigosMarcas = String.Join(", ", codMarcas);

                var query = "SELECT * FROM empresas_fabricantes_marcas WHERE ID_CODIGO_EMPRESA_FABRICANTE_MARCAS IN (" + codigosMarcas + ") AND DESCRICAO_EMPRESA_FABRICANTE_MARCAS LIKE '%" + term + "%'";
                marcasEncontradas = _contexto.Database.SqlQuery<ListaDeEmpresasFabricantesEMarcasViewModel>(query).ToList();
            }

            return marcasEncontradas;
        }

        //Consultar Empresas Fabricantes ou Marcas
        public empresas_fabricantes_marcas ConsultarEmpresaFabricanteOuMarca(int idFabricanteOuMarca)
        {
            empresas_fabricantes_marcas dadosEmpresaOuMarca =
                _contexto.empresas_fabricantes_marcas.FirstOrDefault(m => (m.ID_CODIGO_EMPRESA_FABRICANTE_MARCAS.Equals(idFabricanteOuMarca)));

            return dadosEmpresaOuMarca;
        }
    }
}
