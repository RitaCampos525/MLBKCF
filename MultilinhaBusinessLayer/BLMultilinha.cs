using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilinhaBusinessLayer
{
    
    public class BLMultilinha : IBLMultilinha
    {
        MultilinhasDataLayer.DLMultilinha dl = new MultilinhasDataLayer.DLMultilinha();

        public MensagemOutput<List<string>> CL55Request(int nCliente, ABUtil.ABCommandArgs abargs )
        {
            MensagemOutput<List<string>> msgOut = new MensagemOutput<List<string>>();
            MultilinhasDataLayer.BCDWSProxy.CL55Transaction response = dl.CL55Request(abargs, nCliente, "V", "1", "1");

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            List<string> lst = new List<string>();
            if (response.Row1 != null)
            {
                foreach (var a in response.Row1)
                {
                    if (a.ctitular_l.Equals("1") && a.ztitular_l.Equals("00")) //1 titular e 1 interveniente
                    {
                        string nConta = string.Format("{0}-{1}{2}{3}", a.cbalcao_l, a.cproduto_l, a.cnumecta_l, a.cdigicta_l);
                        lst.Add(nConta);
                    }
                }
            }

            msgOut.ResultResult = lst;
            return msgOut;

        }

        public MensagemOutput<LM31_CatalogoProdutoML> LM31Request(LM31_CatalogoProdutoML _lm31 , ABUtil.ABCommandArgs abargs, string accao)
        {
            MensagemOutput<LM31_CatalogoProdutoML> msgOut = new MensagemOutput<LM31_CatalogoProdutoML>();
            MultilinhasDataLayer.BCDWSProxy.LM31Transaction response = dl.LM31Request(abargs, _lm31, accao );

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            LM31_CatalogoProdutoML obj = new LM31_CatalogoProdutoML();
            if (response.output != null)
            {
                obj.DataFimComercializacao = Convert.ToDateTime(response.output.dficomer);
                obj.DataInicioComercializacao = Convert.ToDateTime(response.output.dincomer);
                obj.Estado = response.output.estparm;
                obj.IndRenovacao = response.output.idrenov;
                obj.LimiteMaximoCredito = response.output.lmtmaxcr;
                obj.LimiteMaximoCredito = response.output.lmtmincr;
                obj.NDiasIncumprimento = Convert.ToInt32(response.output.nmdiainc);
                obj.NDiasPreAviso = Convert.ToInt32(response.output.ndpreavi);
                obj.NumeroMinimoProdutos = Convert.ToInt32(response.output.nmprdat);
                obj.PeriocidadeCobranca = response.output.percobcom;
                obj.PrazoMaximo = Convert.ToInt32(response.output.przmax);
                obj.PrazoMinimo = Convert.ToInt32(response.output.przmin);
                obj.PrazoRenovacao = Convert.ToInt32(response.output.przrenov);
                obj.ProductCode = response.output.cprdml;
                obj.SubProdutoCode = response.output.csubprdml;
                obj.SubProductDescription = response.output.cdescprd;

                obj.produtosA = new List<LM31_CatalogoProdutoML.ProdutoRisco>();
                obj.produtosC = new List<LM31_CatalogoProdutoML.ProdutoRisco>();
                obj.produtosF = new List<LM31_CatalogoProdutoML.ProdutoRisco>();
                foreach (var a in response.row1)
                {
                    if (a.codprod_a_l != null && a.codprod_a_l != "")
                    {
                        obj.produtosA.Add(new LM31_CatalogoProdutoML.ProdutoRisco
                        {
                            descritivo = a.subprod_a_l, //TO DO ir a tat buscar descritivo
                            familia = a.famprod_a_l,
                            produto = a.codprod_a_l,
                            subproduto = a.subprod_a_l,
                            tipologia = a.codtplo_a_l
                        });
                    }
                    if (a.codprod_c_l != null && a.codprod_c_l != "")
                    {
                        obj.produtosC.Add(new LM31_CatalogoProdutoML.ProdutoRisco
                        {
                            descritivo = a.subprod_c_l, //TO DO ir a tat buscar descritivo
                            familia = a.famprod_c_l,
                            produto = a.codprod_c_l,
                            subproduto = a.subprod_c_l,
                            tipologia = a.codtplo_c_l
                        });
                    }
                    if (a.codprod_f_l != null && a.codprod_f_l != "")
                    {
                        obj.produtosF.Add(new LM31_CatalogoProdutoML.ProdutoRisco
                        {
                            descritivo = a.subprod_f_l, //TO DO ir a tat buscar descritivo
                            familia = a.famprod_f_l,
                            produto = a.codprod_f_l,
                            subproduto = a.subprod_f_l,
                            tipologia = a.codtplo_f_l
                        });
                    }
                }

                obj.tipologiaRiscoA = obj.produtosA.Count() > 0 ? "A" : "";
                obj.tipologiaRiscoF = obj.produtosF.Count() > 0 ? "F" : "";
                obj.tipologiaRiscoC = obj.produtosC.Count() > 0 ? "C" : "";
            }

            msgOut.ResultResult = obj;
            return msgOut;

        }

        public MensagemOutput<LM33_ContratoML> LM33Request(LM33_ContratoML _lm33, ABUtil.ABCommandArgs abargs, string accao)
        {
            MensagemOutput<LM33_ContratoML> msgOut = new MensagemOutput<LM33_ContratoML>();
            MultilinhasDataLayer.BCDWSProxy.LM33Transaction response = dl.LM33Request(abargs, _lm33, accao);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            LM33_ContratoML obj = new LM33_ContratoML();
            if (response.output != null)
            {
                obj.baseincidenciacomabert = response.output.basincabert;
                obj.baseincidenciacomgestcontrato = response.output.basincctr;
                obj.baseincidenciacomgestrenovacao = response.output.basincrenov;
                obj.Cliente = response.output.zcliente;
                obj.comissaoabertura = response.output.vrcomabert;
                obj.comissaogestaocontrato = response.output.vrcomgestc;
                obj.comissaorenovacao = response.output.vrcomrenov;
                obj.datafimcontrato = Convert.ToDateTime(response.output.dtfimctr);
                obj.datainiciocontrato = Convert.ToDateTime(response.output.dtinctr);
                obj.dataProcessamento = Convert.ToDateTime(response.output.dtprocess);
                obj.dataproximacobrancagestcontrato = Convert.ToDateTime(response.output.dtpxcobcom);
                obj.dataproximacobrancagestrenovacao = Convert.ToDateTime(response.output.dtpxcobrenov);
                obj.datarenovacao = Convert.ToDateTime(response.output.dtrenov);
                //obj.Descritivo = response.output.su
                obj.EstadoContrato = response.output.estctr;
                obj.graumorosidade = Convert.ToInt32(response.output.grmoros);
                obj.idmultilinha = string.Format("{0}{1}{2}{3}", response.output.cbalcao.ToString(), response.output.cprod.ToString(), response.output.cta.ToString(), response.output.dgt.ToString());
                obj.IndRenovacao = response.output.idrenov == "S" ? true : false;
                obj.limiteglobalmultilinha = response.output.limgloml;
                obj.ncontado = string.Format("{0}{1}{2}{3}", response.output.cbalcaodo.ToString(), response.output.cproddo.ToString(), response.output.ctado.ToString(), response.output.dgtdo.ToString());
                obj.NDiasIncumprimento = Convert.ToInt32(response.output.nmdincinb);
                obj.NDiasPreAviso = Convert.ToInt32(response.output.ndpreavi);
                obj.NMinutaContrato = Convert.ToInt32(response.output.nmincontrato);
                //obj.Nome = response.output.n
                obj.NumeroMinimoProdutos = Convert.ToInt32(response.output.nmprdat);
                obj.PeriocidadeCobrancagestcontrato = response.output.percobcom;
                obj.PeriocidadeCobrancagestRenovacao = response.output.percobrenov;
                obj.prazocontrato = Convert.ToInt32(response.output.przctr);
                obj.PrazoRenovacao = Convert.ToInt32(response.output.przrenov);
                obj.Produtoml = response.output.cprod;
                obj.sublimiteriscoAssinatura = response.output.limrisass;
                obj.sublimiteriscoFinanceiro = response.output.limrisfin;
                obj.sublimitriscoComercial = response.output.limriscom;
                obj.Subprodutoml = response.output.csubprdml;
                obj.tipologiaRiscoA = response.output.tplriscass;
                obj.tipologiaRiscoC = response.output.tplrisccom;
                obj.tipologiaRiscoF = response.output.tplriscfin;
                obj.valorimpostocomabert = response.output.vrcomabert;
                obj.valorimpostocomgestcontrato = response.output.vrcomgestc;
                obj.valorimpostocomgestrenovacao = response.output.vrcomrenov;
                
                obj.ProdutosRiscoAssinatura = new List<LM33_ContratoML.ProdutosRiscoA>();
                obj.produtosRiscoC = new List<LM33_ContratoML.ProdutoRiscoC>();
                obj.produtosRiscoF = new List<LM33_ContratoML.ProdutoRiscoF>();

                //listas
                foreach (var a in response.row1)
                {
                    if (a.codprod_a_l != null && a.codprod_a_l != "")
                    {
                        obj.ProdutosRiscoAssinatura.Add(new LM33_ContratoML.ProdutosRiscoA
                        {
                            descritivo = a.subprod_a_l, //TO DO ir a tat buscar descritivo
                            familiaproduto = a.famprod_a_l,
                            prodsubproduto = string.Concat(a.codprod_a_l, a.subprod_a_l),
                            tipologia = a.codtplo_a_l
                            //zSeq = 
                        });
                    }
                    if (a.codprod_f_l != null && a.codprod_f_l != "")
                    {
                        obj.produtosRiscoF.Add(new LM33_ContratoML.ProdutoRiscoF
                        {
                            descritivo = a.subprod_f_l, //TO DO ir a tat buscar descritivo
                            familiaproduto = a.famprod_f_l,
                            prodsubproduto = string.Concat(a.codprod_f_l, a.subprod_f_l),
                            tipologia = a.codtplo_f_l
                            //zSeq = 
                        });
                    }
                    if (a.codprod_c_l != null && a.codprod_c_l != "")
                    {
                        obj.produtosRiscoC.Add(new LM33_ContratoML.ProdutoRiscoC
                        {
                            descritivo = a.subprod_c_l, //TO DO ir a tat buscar descritivo
                            familiaproduto = a.famprod_c_l,
                            prodsubproduto = string.Concat(a.codprod_c_l, a.subprod_c_l),
                            tipologia = a.codtplo_c_l
                            //zSeq = 
                        });
                    }
                }
            }
            msgOut.ResultResult = obj;
            return msgOut;
        }

        public  MensagemOutput<LM34_SublimitesML> LM34Request(LM34_SublimitesML _LM34, ABUtil.ABCommandArgs abargs, string accao)
        {
            MensagemOutput<LM34_SublimitesML> msgOut = new MensagemOutput<LM34_SublimitesML>();
            MultilinhasDataLayer.BCDWSProxy.LM34Transaction response = dl.LM34Request(abargs, _LM34, accao);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            LM34_SublimitesML obj = new LM34_SublimitesML();
            if (response.output != null)
            {
                int cliente = 0;
                Int32.TryParse(response.output.zcliente, out cliente);
                obj.Cliente = cliente;
                obj.Descritivo = response.output.cprod; //IR A TAT
                obj.EstadoContrato = response.output.estctr;
                obj.idmultilinha = string.Format("{0}{1}{2}{3}", response.output.cbalcao, response.output.cprod, response.output.cta, response.output.dgt);
                obj.idsimulacaoml = response.output.idwrkflw;
                obj.limiteglobalmultilinha = response.output.limgloml;
                obj.ncontado = string.Format("{0}{1}{2}{3}", response.output.cbalcaodo.ToString(), response.output.cproddo.ToString(), response.output.ctado.ToString(), response.output.dgtdo.ToString());
                //obj.Nome =
                obj.Produtoml = response.output.cprod;
                obj.sublimiteriscoAssinatura = response.output.limrisass;
                obj.sublimiteriscoFinanceiro = response.output.limrisfin;
                obj.sublimitriscoComercial = response.output.limriscom;
                obj.Subprodutoml = response.output.csubprdml;

                //listas
                foreach (var a in response.row1)
                {
                    if (a.codprod_a_l != null && a.codprod_a_l != "")
                    {
                        obj.ProdutosRiscoAssinatura.Add(new LM34_SublimitesML.ProdutosRisco
                        {
                            descritivo = a.subprod_a_l, //TO DO ir a tat buscar descritivo
                            familiaproduto = a.famprod_a_l,
                            prodsubproduto = string.Concat(a.codprod_a_l, a.subprod_a_l),
                            tipologia = a.codtplo_a_l,
                            //sublimitecomprometido = a //em FALTA
                            //zSeq = 
                        });
                    }
                    if (a.codprod_f_l != null && a.codprod_f_l != "")
                    {
                        obj.produtosRiscoF.Add(new LM34_SublimitesML.ProdutosRisco
                        {
                            descritivo = a.subprod_f_l, //TO DO ir a tat buscar descritivo
                            familiaproduto = a.famprod_f_l,
                            prodsubproduto = string.Concat(a.codprod_f_l, a.subprod_f_l),
                            tipologia = a.codtplo_f_l
                            //sublimitecomprometido = a //em FALTA
                            //zSeq = 
                        });
                    }
                    if (a.codprod_c_l != null && a.codprod_c_l != "")
                    {
                        obj.produtosRiscoC.Add(new LM34_SublimitesML.ProdutosRisco
                        {
                            descritivo = a.subprod_c_l, //TO DO ir a tat buscar descritivo
                            familiaproduto = a.famprod_c_l,
                            prodsubproduto = string.Concat(a.codprod_c_l, a.subprod_c_l),
                            tipologia = a.codtplo_c_l
                            //sublimitecomprometido = a //em FALTA
                            //zSeq = 
                        });
                    }
                }
            }
            msgOut.ResultResult = obj;
            return msgOut;
        }

        public MensagemOutput<LM36_ContratosProduto> LM36Request(LM36_ContratosProduto _LM36, ABUtil.ABCommandArgs abargs, string accao)
        {
            MensagemOutput<LM36_ContratosProduto> msgOut = new MensagemOutput<LM36_ContratosProduto>();
            MultilinhasDataLayer.BCDWSProxy.LM36Transaction response = dl.LM36Request(abargs, _LM36, accao);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            LM36_ContratosProduto obj = new LM36_ContratosProduto();
            if (response.output != null)
            {
                int cliente = 0;
                Int32.TryParse(response.output.zcliente, out cliente);
                obj.Cliente = cliente;
                obj.Descritivo = response.output.gdescml;
                int DPD = 0;
                Int32.TryParse(response.output.ndpdml, out DPD);
                obj.DPD = DPD;
                //obj.EstadoContratoProduto = response.output.e
                obj.FamiliaProduto = response.output.ntplprod;
                int graumorosidade = 0;
                Int32.TryParse(response.output.cgraumorml, out graumorosidade);
                obj.GrauMorosidade = graumorosidade;
                obj.idmultilinha = string.Format("{0}{1}{2}{3}", response.output.cbalcao, response.output.cprodml, response.output.ccta, response.output.cdgt);
                obj.limiteglobalmultilinha = response.output.mlmglbml;
                obj.Nome = response.output.gliente;
                obj.Produtoml = response.output.cprodml;
                obj.sublimiteriscoAssinatura = response.output.msublmra;
                obj.sublimiteriscoFinanceiro = response.output.msublmrf;
                obj.sublimitriscoComercial = response.output.msublmrc;
                obj.Subprodutoml = response.output.csubprdml;
                obj.TipologiaRisco = response.output.ntprisco;

                //listas
                foreach (var a in response.row1)
                {
                    if (a.cprodsubp_l != null)
                    {
                        LM36_ContratosProduto.ContratosProduto ctr = new LM36_ContratosProduto.ContratosProduto();

                        int dpd = 0;
                        Int32.TryParse(a.ndpd_l, out dpd);
                        ctr.DPD = dpd;
                        ctr.EstadoContratoProduto = a.nestctrprd_l;
                        ctr.ExposicaoAtual = a.mexpoatual_l;
                        ctr.FamiliaProduto = a.ntplprod_l;
                        ctr.GrauMorosidade = a.cgraumor_l;
                        ctr.NContratoProduto = a.cidctrprod_l;
                        ctr.SubProduto = a.cprodsubp_l; // com 4
                        ctr.TipoRisco = a.ntprisco_l;
                        ctr.ValorComprometido = a.mvlcompr_l;
                        ctr.ValorContratado = a.lmcv3700_mvlcontr_l;
                        ctr.ExposicaoAtual = a.mexpoatual_l;


                        obj.ContratosProdutos.Add(ctr);
                    };
                }
            }
            msgOut.ResultResult = obj;
            return msgOut;
        }

        public MensagemOutput<LM37_SimulacaoMl> LM37Request(LM37_SimulacaoMl _LM37, ABUtil.ABCommandArgs abargs, string accao)
        {
            MensagemOutput<LM37_SimulacaoMl> msgOut = new MensagemOutput<LM37_SimulacaoMl>();
            MultilinhasDataLayer.BCDWSProxy.LM37Transaction response = dl.LM37Request(abargs, _LM37, accao);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            LM37_SimulacaoMl obj = new LM37_SimulacaoMl();
            if (response.output != null)
            {
                int balcao = 0;
                Int32.TryParse(response.output.cbalcao, out balcao);
                obj.Balcao = balcao;
                int client = 0;
                int.TryParse(response.output.zcliente, out client);
                obj.Cliente = client;
                obj.Nome = response.output.gcliente;

                obj.idmultilinha = string.Format("{0}{1}{2}{3}", response.output.cbalcao, response.output.lcmv3701_cprod, response.output.ccta, response.output.cdgt);
                obj.idsimulacaoml = response.output.cidsimulml;
                obj.limiteglobalmultilinha = response.output.mlmglbmlatual;
                obj.limiteglobalmultilinhaNovo = response.output.mlmglbmlnovo;
                obj.limiteglobalmultilinhaTotal = response.output.mlmglbmltotal;
                obj.Produtoml = response.output.lcmv3701_cprod;
                obj.Subprodutoml = response.output.csubprdml;
                obj.sublimiteriscoAssinatura = response.output.msublmraatual;
                obj.sublimiteriscoAssinaturaNovo = response.output.msublmranovo;
                obj.sublimiteriscoAssinaturaTotal = response.output.msublmratotal;
                obj.sublimiteriscoFinanceiro = response.output.msublmrfatual;
                obj.sublimiteriscoFinanceiroNovo = response.output.msublmrfnovo;
                obj.sublimiteriscoFinanceiroTotal = response.output.msublmrftotal;
                obj.sublimitriscoComercial = response.output.msublmrcatual;
                obj.sublimitriscoComercialNovo = response.output.msublmrcnovo;
                obj.sublimitriscoComercialTotal = response.output.msublmrctotal;
                //obj.tipoSimulacao

                //listas
                foreach (var a in response.row1)
                {
                    if (a.ntplprod_l != null)
                    {
                        LM37_SimulacaoMl.simulacaoSublimites sim = new LM37_SimulacaoMl.simulacaoSublimites();
                        sim.cons_Balcao = a.cons_cbalcao_l;
                        sim.cons_Cliente = a.cons_zcliente_l;
                        DateTime dat;
                        DateTime.TryParseExact(a.cons_dsimulml_l, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dat);
                        sim.cons_DataSimulacao = dat;
                        sim.cons_idMultilinha = string.Format("{0}{1}{2}{3}", a.cons_cbalcao_l, a.cons_cprod_l, a.cons_ccta_l, a.cons_cdgt_l);
                        sim.cons_idSimulacao = a.cons_cidsimulml_l;
                        sim.cons_limiteML = a.cons_mlmglbml_l;
                        sim.cons_limiteRA = a.cons_msublmra_l;
                        sim.cons_limiteRC = a.cons_msublmrc_l;
                        sim.cons_limiteRF = a.cons_msublmrf_l;
                        sim.cons_utilizador = a.cons_cutulcria_l;
                        sim.SublimiteComprometido = a.msublmcompatual_l;
                        sim.SublimiteComprometidoNovo = a.mpsublmcompnovo_l;
                        sim.SublimiteContratado = a.msublmcontr_l;
                        sim.TipologiaRisco = a.ntprisco_l;
                        sim.ExposicaoAtual = a.mpexpoatual_l;
                        sim.FamiliaProduto = a.ntplprod_l;
                        sim.preco = a.nindpreco_l != "S" ? false: true;
                        sim.produto = a.ccodtpl_l; // COM 4

                        obj.SimulacaoSublimites.Add(sim);

                    }
                }
            }

            msgOut.ResultResult = obj;
            return msgOut;
        }

        public MensagemOutput<LM38_HistoricoAlteracoes> LM38Request(LM38_HistoricoAlteracoes _LM38, ABUtil.ABCommandArgs abargs, string accao)
        {
            MensagemOutput<LM38_HistoricoAlteracoes> msgOut = new MensagemOutput<LM38_HistoricoAlteracoes>();
            MultilinhasDataLayer.BCDWSProxy.LM38Transaction response = dl.LM38Request(abargs, _LM38, accao);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            LM38_HistoricoAlteracoes obj = new LM38_HistoricoAlteracoes();
            if (response.output != null)
            {
                int client = 0;
                int.TryParse(response.output.zcliente, out client);
                obj.Cliente = client;
                obj.idmultilinha = string.Format("{0}{1}{2}{3}", response.output.cbalcao, response.output.cprod, response.output.cta, response.output.dgt);
                //obj.Nome = response.output.g

                //listas
                foreach (var a in response.row1)
                {
                    if (a.ncontrato_l != null)
                    {
                        LM38_HistoricoAlteracoes.historicoAlteracoes his = new LM38_HistoricoAlteracoes.historicoAlteracoes();
                        his.campoAlterado = a.galtera_l;

                        DateTime dat;
                        DateTime.TryParseExact(a.dprocess_l, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dat);
                        his.dataProcessamento = dat;

                        DateTime datV;
                        DateTime.TryParseExact(a.dtaltera_l, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out datV);
                        his.dataValorAlteracao = datV;

                        //his.description = a.d
                        his.idAlteracao = a.idaltera_l;
                        his.nContratoProduto = a.ncontrato_l;
                        his.TipoAlteracao = a.gtipo_l;
                        //his.utilizador = a TO DO
                        his.valorAnterior = a.vanterior_l;
                        //his.valorPosterior = a. TO DO

                        obj.HistoricoAlteracoes.Add(his);
                    }
                }
            }
            msgOut.ResultResult = obj;
            return msgOut;
        }
    }
}
