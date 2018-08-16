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
                WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.TAT2Request, "DataOperacao_Fechas  - FECHAS", AbArgs.USERNT, AbArgs.SN_HOSTNAME);

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

                WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.TAT2Request, "Retun value count: " + ds.Tables[0].Rows.Count, AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                string dtacol = ds.Tables[0].Rows[0][1].ToString(); //coluna 1 da 1a linha

                //retorna data
                DateTime.TryParseExact(dtacol, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechasdt);
                return fechasdt;

            }
            catch (Exception ex)
            {
               WriteLog.Log(System.Diagnostics.TraceLevel.Error, MultilinhasObjects.LogTypeName.TAT2Request, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                DataTable dt = new DataTable();

                return fechasdt;
            }
        }

        public DataTable GetProdutos(string connection, ABUtil.ABCommandArgs AbArgs)
        {
            try
            {
                DataTable Produtos = cache["Produtos"] as DataTable;

                WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.TAT2Request, "GetProdutos  - TB196 ", AbArgs.USERNT, AbArgs.SN_HOSTNAME);

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

                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.TAT2Request, "Setting cache for [Produtos]", AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    //Set Cache
                    System.Runtime.Caching.CacheItemPolicy policy = new System.Runtime.Caching.CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(1);
                    cache.Set("Produtos", ds.Tables[0], policy);

                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.TAT2Request, "Retun value count: " + ds.Tables[0].Rows.Count, AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    return ds.Tables[0];
                }
                //Devolver valor em cache
                else
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.TAT2Request, "Cache found for [Produtos] : " + Produtos.Rows.Count, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    return Produtos;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Log(System.Diagnostics.TraceLevel.Error, MultilinhasObjects.LogTypeName.TAT2Request, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                DataTable dt = new DataTable();

                return dt;
            }
        }

        public DataTable GetCodMensagens(ABUtil.ABCommandArgs AbArgs)
        {
            try
            {
                DataTable CodMsgs = cache["CodigosMensagens"] as DataTable;

                WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.TAT2Request, "CodigosMensagens LM  - SYT05L", AbArgs.USERNT, AbArgs.SN_HOSTNAME);


                if (CodMsgs == null)
                {
                    //Vai lêr à tabela

                    OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MASTERDB2LOCAL"].ConnectionString);
                    DataSet ds = new DataSet();

                    try
                    {
                        OdbcDataAdapter ad = new OdbcDataAdapter("SELECT CELEMTAB2, NELEMC01 FROM SYT05L WHERE CELEMTAB1 = 'LM' AND CELEMTAB3 = 'PO'", connection); //Tabela sistema SYT05
                        ad.Fill(ds);
                    }
                    finally
                    {
                        connection.Close();
                    }

                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.TAT2Request, "Setting cache for [CodigosMensagens]", AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    //Set Cache
                    CacheItemPolicy policy = new CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(1);
                    cache.Set("CodigosMensagens", ds.Tables[0], policy);

                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.TAT2Request, "Retun value count: " + ds.Tables[0].Rows.Count, AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    return ds.Tables[0];
                }
                //Devolver valor em cache
                else
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.TAT2Request, "Cache found for [CodigosMensagens] : " + CodMsgs.Rows.Count, AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    return CodMsgs;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.TAT2Request, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
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

        public string GetMsgErroTATDescription(string msg, ABUtil.ABCommandArgs Abargs)
        {
            string outmsg = msg;

            try
            {
                WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.Internal, "GetMsgErroTATDescription", Abargs.USERNT, Abargs.SN_HOSTNAME);

                //Tabela de codigos de mensagem
                DataTable CodMsgs = GetCodMensagens(Abargs);

                if (CodMsgs != null && CodMsgs.Rows.Count > 0)
                {
                    //Encontra rows com descrição do codigo de erro
                    DataRow[] drs = CodMsgs.Select("CELEMTAB2 = '" + msg.PadLeft(3, '0') + "'");
                    if (drs.Count() > 0)
                    {
                        //Select first or default
                        outmsg = drs[0][1].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.TAT2Request, ex, Abargs.USERNT, Abargs.SN_HOSTNAME);
            }

            return outmsg;
        }

        //FOR debug
        //public List<long> SearchDOCliente(string cliente)
        //{
        //    List<long> response = new List<long>();
        //    response.Add(33412325317);
        //    response.Add(33412325317);

        //    return response;
        //}

        //public List<string> SearchSubProdutML(string cliente)
        //{
        //    List<string> response = new List<string>();
        //    response.Add("01");
        //    response.Add("02");

        //    return response;
        //}

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

        public LM31_CatalogoProdutoML SearchLM31(string prod, int subprod)
        {
            return new LM31_CatalogoProdutoML()
            {
                NDiasIncumprimento = 2,
                IndRenovacao = "N",
                NDiasPreAviso = 60,
                PrazoRenovacao = 3,
                NumeroMinimoProdutos = 5,
                
                produtosF = new List<LM31_CatalogoProdutoML.ProdutoRisco>()
                {
                    new LM31_CatalogoProdutoML.ProdutoRisco
                    {
                        familia = ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd,
                        produto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FirstOrDefault(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd).produto,
                        subproduto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FirstOrDefault(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd).subproduto,
                        tipologia = "F",
                        descritivo = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FirstOrDefault(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd).descricao,
                    },
                     new LM31_CatalogoProdutoML.ProdutoRisco
                    {
                        familia = ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd,
                        produto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd).produto,
                        subproduto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd).subproduto,
                        tipologia = "F",
                        descritivo = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd).descricao,
                    },
                      new LM31_CatalogoProdutoML.ProdutoRisco
                    {
                        familia = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd,
                        produto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd).produto,
                        subproduto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd).subproduto,
                        tipologia = "F",
                        descritivo = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd).descricao,
                    },
                      new LM31_CatalogoProdutoML.ProdutoRisco
                    {
                        familia = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd,
                        produto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FirstOrDefault(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd).produto,
                        subproduto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FirstOrDefault(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd).subproduto,
                        tipologia = "F",
                        descritivo = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FirstOrDefault(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd).descricao,
                    },
                       new LM31_CatalogoProdutoML.ProdutoRisco
                    {
                        familia = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd,
                        produto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd).produto,
                        subproduto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd).subproduto,
                        tipologia = "F",
                        descritivo = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd).descricao,
                    },
                },
                produtosA = new List<LM31_CatalogoProdutoML.ProdutoRisco>()
                {
                    new LM31_CatalogoProdutoML.ProdutoRisco
                    {
                        familia = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancTécnicas.descFamProd,
                        produto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancTécnicas.descFamProd).produto,
                        subproduto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancTécnicas.descFamProd).subproduto,
                        tipologia = "A",
                        descritivo = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA).FindLast(x => x.familiaProduto == ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancTécnicas.descFamProd).descricao,
                    }
                }
                
               
            };
        }

        public LM33_ContratoML SearchML03(int _cliente, string _idworkflow)
        {
            return new LM33_ContratoML()
            {
                Cliente = _cliente.ToString(),
                datainiciocontrato = Convert.ToDateTime("2018-08-01"),
                datafimcontrato = Convert.ToDateTime("2019-09-10"),
                dataProcessamento = Convert.ToDateTime("2018-08-21"),
                dataproximacobrancagestcontrato = Convert.ToDateTime("2018-09-21"),
                datarenovacao = Convert.ToDateTime("2019-08-01"),
                Descritivo = "ML - Base",
                EstadoContrato = "PENDENTE",
                graumorosidade = 999,
                idmultilinha = "401258852001",
                idproposta = string.IsNullOrEmpty(_idworkflow) ? "12351" : _idworkflow,
                indicadorAcaoCancelamento = false,
                indicadorAcaoEnvioCartas = false,
                IndRenovacao = false,
                limiteglobalmultilinha = Convert.ToDecimal(4000000000.00),
                ncontado = "33412325317",
                NDiasIncumprimento = 70,
                Nome = "CLIENTE 01",
                PeriocidadeCobrancagestcontrato = MultilinhasObjects.ML_Objectos.GetPeriocidade()[0].Code,
                prazocontrato = 1,
                PrazoRenovacao = 1,
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
                NMinutaContrato = 001,
                NumeroMinimoProdutos = 5,
                NDiasPreAviso = 60,
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

        public LM34_SublimitesML SearchML04(int cliente, string idMult, string idSim)
        {
            return new LM34_SublimitesML()
            {
                Cliente = cliente,
                Descritivo = "ML - Base",
                EstadoContrato = "PENDENTE",
                idmultilinha = idMult.ToString(),
                idsimulacaoml = idSim.ToString(),
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
                        sublimitecomprometido = 0.00M
                    },
                    new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancAvalesBancarios.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancAvalesBancarios.codeFamProd,
                        tipologia = "A",
                        sublimitecomprometido = 0.00M
                    },
                 },
                produtosRiscoC = new List<LM34_SublimitesML.ProdutosRisco>()
                {

                    new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_Letras.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_Letras.codeFamProd,
                        tipologia = "C",
                        sublimitecomprometido = 0.00M
                    },
                     new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                        tipologia = "C",
                        sublimitecomprometido = 0.00M
                    },
                   
                },
                produtosRiscoF = new List<LM34_SublimitesML.ProdutosRisco>()
                {

                    new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Descobertos.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Descobertos.codeFamProd,
                        tipologia = "F",
                        sublimitecomprometido = 0.00M
                    },
                    new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd,
                        codfamiliaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.codeFamProd,
                        tipologia = "F",
                        sublimitecomprometido = 0.00M
                    },
                     new LM34_SublimitesML.ProdutosRisco
                    {
                        familiaproduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                        codfamiliaproduto =  ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.codeFamProd,
                        tipologia = "F",
                        sublimitecomprometido = 0.00M
                    },
                },
                sublimiteriscoAssinatura = 30001,
                sublimiteriscoFinanceiro = 30002,
                sublimitriscoComercial = 30003,
                Subprodutoml = "01"

            };
       }

        public LM35_AssociacaoContasDO SearchML35(int cliente, int idMult)
        {
            return new LM35_AssociacaoContasDO()
            {
                Cliente = cliente,
                idmultilinha = idMult,
                Lista = new List<listaContaDO>
                {
                    new listaContaDO
                    {
                        Associado = false,
                        DataAssociada = DateTime.Now,
                        NumContaDO = "1234536789101"
                    }
                }
            };
        }

        public LM37_SimulacaoMl SearchML37(int cliente, string idMult)
        {
            return new LM37_SimulacaoMl()
            {
                Cliente = cliente,
                idmultilinha = idMult,
                Balcao = 810,
                dataSimulacao = DateTime.Now,
                Descritivo = "ML - BASE",
                EstadoContrato = ML_Objectos.GetEstadosDoCatalogo()[0].Description,
                idsimulacaoml = "1212142342",
                limiteglobalmultilinha = 10000,
                ncontado = "81045845878",
                Nome = "NOME 1",
                Produtoml = "LM",
                Subprodutoml = "01",
                tipoSimulacao = ML_Objectos.GetTiposSimulacao()[0].Code,
                sublimiteriscoAssinatura = 100000,
                sublimiteriscoFinanceiro = 100000,
                sublimitriscoComercial = 100000,
                limiteglobalmultilinhaTotal = 100000,
                sublimiteriscoFinanceiroTotal = 100000,
                sublimitriscoComercialTotal = 100000,
                sublimiteriscoAssinaturaTotal = 100000,
                SimulacaoSublimites = new List<LM37_SimulacaoMl.simulacaoSublimites>
                {
                   new LM37_SimulacaoMl.simulacaoSublimites
                   {
                       CodigoTipologia = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA)[0].codfamiliaProduto.ToString(),
                       cons_Balcao = 823.ToString(),
                       cons_Cliente = 1231241.ToString(),
                       cons_DataSimulacao = DateTime.Now,
                       cons_idMultilinha = 12345678909.ToString(),
                       cons_idSimulacao = 000000001.ToString(),
                       cons_limiteML = 10000,
                       cons_limiteRA = 10000,
                       cons_limiteRC = 10000,
                       cons_limiteRF = 10000,
                       cons_ProdSub = 2005.ToString(),
                       FamiliaProduto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA)[0].familiaProduto.ToString(),
                       cons_utilizador = "BDASP",
                       ExposicaoAtual = 90000,
                       preco = true,
                       SublimiteComprometido = 95000,
                       SublimiteContratado = 95000,
                       TipologiaRisco = "A",
                       
                   },
                   new LM37_SimulacaoMl.simulacaoSublimites
                   {
                       CodigoTipologia = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF)[2].codfamiliaProduto.ToString(),
                       cons_Balcao = 823.ToString(),
                       cons_Cliente = 1231241.ToString(),
                       cons_DataSimulacao = DateTime.Now,
                       cons_idMultilinha = 12345678909.ToString(),
                       cons_idSimulacao = 000000001.ToString(),
                       cons_limiteML = 10000,
                       cons_limiteRA = 10000,
                       cons_limiteRC = 10000,
                       cons_limiteRF = 10000,
                       cons_ProdSub = 2005.ToString(),
                       FamiliaProduto = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF)[2].familiaProduto.ToString(),
                       cons_utilizador = "BDASP",
                       ExposicaoAtual = 90000,
                       preco = true,
                       SublimiteComprometido = 74000,
                       SublimiteContratado = 74000,
                       TipologiaRisco = "F",
                   },
                }
            };
        }

        public LM38_HistoricoAlteracoes SearchLM38(int client, string idMult)
        {
            return new LM38_HistoricoAlteracoes()
            {
                Cliente = client,
                idmultilinha = idMult,
                Nome = "JOSÉ MANUEL",
                HistoricoAlteracoes = new List<LM38_HistoricoAlteracoes.historicoAlteracoes>()
                {
                    new LM38_HistoricoAlteracoes.historicoAlteracoes
                    {
                        Alteracao = "Sublimite",
                        campoAlterado = "TO DO",
                        dataProcessamento = DateTime.Now.Date,
                        dataValorAlteracao = DateTime.Now.Date,
                        description = "Sublimite",
                        idAlteracao = 1.ToString(),
                        nContratoProduto = "84532456540",
                        TipoAlteracao = "Condição Particular",
                        utilizador = "BDAPS",
                        valorAnterior = "2000",
                        valorPosterior = "560000",

                    },
                     new LM38_HistoricoAlteracoes.historicoAlteracoes
                    {
                        Alteracao = "Sublimite",
                        campoAlterado = "Prazo",
                        dataProcessamento = DateTime.Now.Date,
                        dataValorAlteracao = DateTime.Now.Date,
                        description = "Sublimite",
                        idAlteracao = 1.ToString(),
                        nContratoProduto = "84532456540",
                        TipoAlteracao = "Condição Geral",
                        utilizador = "BDAPS",
                        valorAnterior = "2000",
                        valorPosterior = "560000",

                    }
                },

            };
        }
    }
}

