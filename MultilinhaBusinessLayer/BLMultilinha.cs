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
                    string nConta = string.Format("{0}-{1}{2}{3}", a.cbalcao_l, a.cproduto_l, a.cnumecta_l, a.cdigicta_l);
                    lst.Add(nConta);
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
                obj.prazorenovacao = Convert.ToInt32(response.output.przrenov);
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

            return msgOut;
        }
    }
}
