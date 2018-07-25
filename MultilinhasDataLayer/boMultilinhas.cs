using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Runtime.Caching;

namespace MultilinhasDataLayer
{
    public class boMultilinhas
    {
        public const string schema = "DB2PTUSER.";
        System.Runtime.Caching.ObjectCache cache = MemoryCache.Default;
        public DateTime DataOperacao_Fechas(ABUtil.ABCommandArgs AbArgs, string balcao)
        {
            DateTime fechasdt = new DateTime();
            try
            {
                //WriteLog.Log(System.Diagnostics.TraceLevel.Info, PrecarioComissionamentoObjects.LogTypeName.TAT2Request, "DataOperacao_Fechas  - FECHAS", AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                //Vai lêr à tabela

                OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MASTERDB2LOCAL"].ConnectionString);
                DataSet ds = new DataSet();

                try
                {
                    string query = "SELECT FE_COD_BALCAO, FE_OPER FROM FECHAS WHERE FE_COD_BALCAO = " + balcao;
                    OdbcDataAdapter ad = new OdbcDataAdapter(query, connection); //Tabela sistema DATAS
                    ad.Fill(ds);
                }
                finally
                {
                    connection.Close();
                }

                //WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, PrecarioComissionamentoObjects.LogTypeName.TAT2Request, "Retun value count: " + ds.Tables[0].Rows.Count, AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                string dtacol = ds.Tables[0].Rows[0][1].ToString(); //coluna 1 da 1a linha

                //retorna data
                DateTime.TryParseExact(dtacol, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechasdt);
                return fechasdt;

            }
            catch (Exception ex)
            {
                //WriteLog.Log(System.Diagnostics.TraceLevel.Error, MultilinhasObjects.LogTypeName.TAT2Request, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                DataTable dt = new DataTable();

                return fechasdt;
            }
        }

        public DataTable GetProdutos(string connection, ABUtil.ABCommandArgs AbArgs)
        {
            try
            {
                DataTable Produtos = cache["Produtos"] as DataTable;

                //WriteLog.Log(System.Diagnostics.TraceLevel.Info, PrecarioComissionamentoObjects.LogTypeName.TAT2Request, "GetProdutos  - TB196 ", AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                //Vai lêr à tabela
                if (Produtos == null)
                {
                    OdbcConnection con = new OdbcConnection(connection);
                    DataSet ds = new DataSet();

                    try
                    {
                        OdbcDataAdapter ad = new OdbcDataAdapter("SELECT CELEMTAB1, GELEM30, NELEMC01, NELEMC02 FROM TB196 where NELEMC01 != '' AND NELEMC13 = 'S'  order by NELEMC01", con); //Tabela geral TB196
                        ad.Fill(ds);
                    }
                    finally
                    {
                        con.Close();
                    }

                    //WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, PrecarioComissionamentoObjects.LogTypeName.TAT2Request, "Setting cache for [Produtos]", AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    //Set Cache
                    System.Runtime.Caching.CacheItemPolicy policy = new System.Runtime.Caching.CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(1);
                    cache.Set("Produtos", ds.Tables[0], policy);

                    //WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, PrecarioComissionamentoObjects.LogTypeName.TAT2Request, "Retun value count: " + ds.Tables[0].Rows.Count, AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    return ds.Tables[0];
                }
                //Devolver valor em cache
                else
                {
                    //WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, PrecarioComissionamentoObjects.LogTypeName.TAT2Request, "Cache found for [Produtos] : " + Produtos.Rows.Count, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    return Produtos;
                }
            }
            catch (Exception ex)
            {
                //WriteLog.Log(System.Diagnostics.TraceLevel.Error, MultilinhasObjects.LogTypeName.TAT2Request, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                DataTable dt = new DataTable();

                return dt;
            }
        }

        public string GetSubProdDescriptionByCode(string productCode, string subProductCode, string connection, ABUtil.ABCommandArgs AbArgs)
        {
            WriteLog.Log(System.Diagnostics.TraceLevel.Info, MultilinhasObjects.LogTypeName.Internal, "GetSubProdDescriptionByCode", AbArgs.USERNT, AbArgs.SN_HOSTNAME);

            string subproduto = "";
            DataTable produtos = GetProdutos(connection, AbArgs);
            if (produtos != null && produtos.Rows.Count != 0)
            {
                //Encontra row com descrição do produto
                DataRow[] drs = produtos.Select("CELEMTAB1 = '" + productCode.ToUpper() + subProductCode.ToUpper() + "'");
                if (drs.Count() > 0)
                {
                    //Select first or default
                    subproduto = drs[0][1].ToString();
                }
            }
            return subproduto;
        }

        public List<string> GetSubProdByProdCode(string productCode, string connection, ABUtil.ABCommandArgs AbArgs)
        {
            List<string> subprods = new List<string>();
            WriteLog.Log(System.Diagnostics.TraceLevel.Info, MultilinhasObjects.LogTypeName.Internal, "GetSubProdByProdCode", AbArgs.USERNT, AbArgs.SN_HOSTNAME);

            DataTable produtos = GetProdutos(connection, AbArgs);
            if (produtos != null && produtos.Rows.Count != 0)
            {
                //Encontra row com subprodutos
                DataRow[] drs = produtos.Select("NELEMC01 = '" + productCode.ToUpper() + "'");

                for (int i = 0; i < drs.Length; i++)
                {
                    //adiciona apenas subproduto
                    string dr = drs[i]["NELEMC02"].ToString();
                    subprods.Add(dr);
                }

            }
            return subprods;
        }

        //FOR debug
        public List<long> SearchDOCliente(string cliente)
        {
            List<long> response = new List<long>();
            response.Add(33412325317);
            response.Add(33412325317);

            return response;
        }

        public List<string> SearchSubProdutML(string cliente)
        {
            List<string> response = new List<string>();
            response.Add("01");
            response.Add("02");

            return response;
        }

        public List<string> SearchSubProdutDescriptionML(string subprod)
        {
            List<string> response = new List<string>();
            if (subprod == "01")
            {
                response.Add("ML - Base");
            }
            if (subprod == "02")
            {
                response.Add("ML - Avancado");
            }

            return response;
        }

       

        public LM33_ContratoML SearchML03(int _cliente, string _idworkflow)
        {
            return new LM33_ContratoML()
            {
                Cliente = _cliente,
                datainiciocontrato = Convert.ToDateTime("2018-08-01"),
                datafimcontrato = Convert.ToDateTime("2019-09-10"),
                dataProcessamento = Convert.ToDateTime("2018-08-21"),
                dataproximacobranca = Convert.ToDateTime("2018-09-21"),
                datarenovacao = Convert.ToDateTime("2019-08-01"),
                Descritivo = "ML - Base",
                EstadoContrato = "PENDENTE",
                graumorosidade = 9999,
                idmultilinha = 000002,
                idproposta = string.IsNullOrEmpty(_idworkflow) ? "12351" : _idworkflow,
                indicadorAcaoCancelamento = false,
                indicadorAcaoEnvioCartas = false,
                indicadorrenovacao = false,
                limiteglobalmultilinha = Convert.ToDecimal(4000000000.00),
                ncontado = "33412325317",
                ndiasincumprimento = 70,
                Nome = "CLIENTE 01",
                PeriocidadeCobranca = MultilinhasObjects.ML_Objectos.GetPeriocidade()[0].Code,
                prazocontrato = 1,
                prazorenovacao = 1,
                Produtoml = "LM",
                Subprodutoml = "01",
                sublimiteriscoAssinatura = Convert.ToDecimal(30001.00),
                sublimiteriscoFinanceiro = Convert.ToDecimal(30002.00),
                sublimitriscoComercial = Convert.ToDecimal(30003.00),
                tipologiaRiscoA = "A",
                tipologiaRiscoC = "C",
                tipologiaRiscoF = "F",
                valorimpostocomabert = Convert.ToDecimal(0.10),
                valorimpostocomgestcontrato = Convert.ToDecimal(0.10),
                comissaoabertura = Convert.ToDecimal(2.10),
                comissaogestaocontrato = Convert.ToDecimal(2.10),
                baseincidenciacomabert = 2,
                baseincidenciacomgestcontrato = 2,
                
               
                produtosRiscoF = new List<LM33_ContratoML.ProdutoRiscoF>()
                {
                    new LM33_ContratoML.ProdutoRiscoF
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Descobertos.descFamProd,
                        tipologia = "F",
                    },
                    new LM33_ContratoML.ProdutoRiscoF
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd,
                        tipologia = "F",
                    },
                     new LM33_ContratoML.ProdutoRiscoF
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                        tipologia = "F",
                    },
                },

                 ProdutosRiscoAssinatura = new List<LM33_ContratoML.ProdutosRiscoA>()
                {

                      new LM33_ContratoML.ProdutosRiscoA
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_CreditoDocumentarioImportação.descFamProd,
                        tipologia = "A",
                    },
                    new LM33_ContratoML.ProdutosRiscoA
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancAvalesBancarios.descFamProd,
                        tipologia = "A",
                    },
                 },
                 produtosRiscoC = new List<LM33_ContratoML.ProdutoRiscoC>()
                 {

                    new LM33_ContratoML.ProdutoRiscoC
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_Letras.descFamProd,
                        tipologia = "C",
                    },
                     new LM33_ContratoML.ProdutoRiscoC
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                        tipologia = "C",
                    }
                  }

            };
        }

        public LM34_SublimitesML SearchML04(int cliente, int idMult, int idSim)
        {
            return new LM34_SublimitesML()
            {
                Cliente = cliente,
                Descritivo = "ML - Base",
                EstadoContrato = "PENDENTE",
                idmultilinha = idMult,
                idsimulacaoml = idSim,
                limiteglobalmultilinha = 4000000000,
                Nome = "CLIENTE 01",
                Produtoml = "LM",
                ProdutosRiscoAssinatura = new List<LM34_SublimitesML.ProdutosRisco>()
                {
                    new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_CreditoDocumentarioImportação.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_CreditoDocumentarioImportação.codeFamProd,
                        tipologia = "A",
                    },
                    new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancAvalesBancarios.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancAvalesBancarios.codeFamProd,
                        tipologia = "A",
                    },
                 },
                produtosRiscoC = new List<LM34_SublimitesML.ProdutosRisco>()
                {

                    new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_Letras.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_Letras.codeFamProd,
                        tipologia = "C",
                    },
                     new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                        tipologia = "C",
                    },
                   
                },
                produtosRiscoF = new List<LM34_SublimitesML.ProdutosRisco>()
                {

                    new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Descobertos.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Descobertos.codeFamProd,
                        tipologia = "F",
                    },
                    new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.codeFamProd,
                        tipologia = "F",
                    },
                     new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                        codfamiliaproduto =  ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.codeFamProd,
                        tipologia = "F",
                    },
                },
                sublimiteriscoAssinatura = 30001,
                sublimiteriscoFinanceiro = 30002,
                sublimitriscoComercial = 30003,
                Subprodutoml = "01"

            };
       }
       
    }
}

