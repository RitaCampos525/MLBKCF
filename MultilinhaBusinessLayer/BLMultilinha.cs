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

        public MensagemOutput<LM31_CatalogoProdutoML> LM31Request(LM31_CatalogoProdutoML _lm31 , ABUtil.ABCommandArgs abargs, string accao, bool pedido)
        {
            MensagemOutput<LM31_CatalogoProdutoML> msgOut = new MensagemOutput<LM31_CatalogoProdutoML>();
            MultilinhasDataLayer.BCDWSProxy.LM31Transaction response = dl.LM31Request(abargs, _lm31, accao, pedido);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";
            

            
            if (response.output != null)
            {
                LM31_CatalogoProdutoML obj = new LM31_CatalogoProdutoML();

                DateTime dtfimcomer;
                DateTime.TryParseExact(response.output.dtfimcomer, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtfimcomer);
                obj.DataFimComercializacao = dtfimcomer;

                DateTime dtinicomer;
                DateTime.TryParseExact(response.output.dtinicomer, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtinicomer);
                obj.DataInicioComercializacao = dtinicomer;

                obj.Estado = response.output.iestado;
                obj.IndRenovacao = response.output.irenovac;
                obj.LimiteMaximoCredito = response.output.mlimmaxml;
                obj.LimiteMinimoCredito = response.output.mlimminml;
                obj.NDiasIncumprimento =  Convert.ToInt32(response.output.qdiaincum);
                obj.NDiasPreAviso = Convert.ToInt32(response.output.qdiapaviso);
                obj.NumeroMinimoProdutos = Convert.ToInt32(response.output.qminprod);
                obj.PeriocidadeCobranca = response.output.qperigest.ToString();
                obj.PrazoMaximo = Convert.ToInt32(response.output.qprzmaxml);
                obj.PrazoMinimo = Convert.ToInt32(response.output.qprzminml);
                obj.PrazoRenovacao = Convert.ToInt32(response.output.qprzrenov);
                obj.ProductCode = response.output.cprodutoml;
                obj.SubProdutoCode = response.output.csubprodml;

                DateTime dtversao;
                DateTime.TryParseExact(response.output.dtversao, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtversao);
                obj.DataVersao = dtversao;
                //obj.SubProductDescription = response.output.cdescprd; TO DO IR A TAT

                obj.produtosA = new List<LM31_CatalogoProdutoML.ProdutoRisco>();
                obj.produtosC = new List<LM31_CatalogoProdutoML.ProdutoRisco>();
                obj.produtosF = new List<LM31_CatalogoProdutoML.ProdutoRisco>();
                foreach (var a in response.row1)
                {
                    if (a.l_cproduto_l != null && a.l_csubprod_l != "" && a.l_tiporisco_l == "A")
                    {
                        obj.produtosA.Add(new LM31_CatalogoProdutoML.ProdutoRisco
                        {
                            descritivo = a.l_csubprod_l, //TO DO ir a tat buscar descritivo
                            familia = a.l_famiprod_l,
                            produto = a.l_cproduto_l,
                            subproduto = a.l_csubprod_l,
                            tipologia = a.l_tiporisco_l
                        });
                    }
                    if (a.l_cproduto_l != null && a.l_csubprod_l != "" && a.l_tiporisco_l == "C")
                    {
                        obj.produtosC.Add(new LM31_CatalogoProdutoML.ProdutoRisco
                        {
                            descritivo = a.l_csubprod_l, //TO DO ir a tat buscar descritivo
                            familia = a.l_famiprod_l,
                            produto = a.l_cproduto_l,
                            subproduto = a.l_csubprod_l,
                            tipologia = a.l_tiporisco_l
                        });
                    }
                    if (a.l_cproduto_l != null && a.l_csubprod_l != "" && a.l_tiporisco_l == "F")
                    {
                        obj.produtosF.Add(new LM31_CatalogoProdutoML.ProdutoRisco
                        {
                            descritivo = a.l_csubprod_l, //TO DO ir a tat buscar descritivo
                            familia = a.l_famiprod_l,
                            produto = a.l_cproduto_l,
                            subproduto = a.l_csubprod_l,
                            tipologia = a.l_tiporisco_l
                        });
                    }
                }

                obj.tipologiaRiscoA = obj.produtosA.Count() > 0 ? "A" : "";
                obj.tipologiaRiscoF = obj.produtosF.Count() > 0 ? "F" : "";
                obj.tipologiaRiscoC = obj.produtosC.Count() > 0 ? "C" : "";

                msgOut.ResultResult = obj;
            }

          
            return msgOut;

        }

        public MensagemOutput<LM32_PedidosContratoML> LM32Request(LM32_PedidosContratoML _lm32, ABUtil.ABCommandArgs abargs, string accao, bool pedido)
        {
            MensagemOutput<LM32_PedidosContratoML> msgOut = new MensagemOutput<LM32_PedidosContratoML>();
            MultilinhasDataLayer.BCDWSProxy.LM32Transaction response = dl.LM32Request(abargs, _lm32, accao, pedido);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

           
            if (response.output != null)
            {
                LM32_PedidosContratoML obj = new LM32_PedidosContratoML();

                obj.btnAccept = response.output.btn_accept == "S"? true : false;
                obj.btnReject = response.output.btn_reject == "S" ? true: false;
                int _cliente;
                Int32.TryParse(response.output.zcliente, out _cliente);
                obj.Cliente = _cliente;
                obj.gBalcao = response.output.gbalcao;
                obj.idmultilinha = string.Concat(response.output.cprodml, response.output.cnumectaml, response.output.cdigictaml);
                int _balcao;
                Int32.TryParse(response.output.cbalcao, out _balcao);
                obj.nBalcao = _balcao;
                obj.Nome = response.output.gcliente;
                obj.ProductCode = response.output.cprodml;
                obj.SubProductDescription = response.output.gdescml;
                obj.SubProdutoCode = response.output.csubprodml;
                obj.TipoPedido = response.output.tppedido;
                obj.txtidmultilinha_balcao = response.output.cbalcao_cidctrml;

                obj.PedidosAprovacao = new List<LM32_PedidosContratoML.pedidoAprovacao>();
                if(response.row1.Count() > 0)
                {
                    foreach (var pd in response.row1)
                    {

                        LM32_PedidosContratoML.pedidoAprovacao pd1 = new LM32_PedidosContratoML.pedidoAprovacao();
                        pd1.descritivo = pd.lista_gdescml_l;
                        pd1.idcliente = Convert.ToInt32(pd.lista_zcliente_l);
                        pd1.idmultilinha =  string.Concat(pd.lista_cidctrml_l, pd.lista_cidctrml_l);
                        pd1.nBalcao = Convert.ToInt32(pd.lista_cbalcao_l);
                        pd1.produto = pd.lista_cprodutoml_l;
                        pd1.subProduto = pd.lista_csubprodml_l;
                        pd1.TipoPedido = pd.lista_tppedido_l;
                        pd1.utilizador = pd.lista_cutulcri_l;

                        obj.PedidosAprovacao.Add(pd1);
                    }
                }

                msgOut.ResultResult = obj;
            }
          
            return msgOut;
        }

        public MensagemOutput<LM33_ContratoML> LM33Request(LM33_ContratoML _lm33, ABUtil.ABCommandArgs abargs, string accao, string acesso, bool pedido)
        {
            MensagemOutput<LM33_ContratoML> msgOut = new MensagemOutput<LM33_ContratoML>();
            MultilinhasDataLayer.BCDWSProxy.LM33Transaction response = dl.LM33Request(abargs, _lm33, accao, acesso, pedido);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

           
            if (response.output != null)
            {
                LM33_ContratoML obj = new LM33_ContratoML();

                obj.baseincidenciacomabert = response.output.bicomissabe;
                obj.baseincidenciacomgestcontrato = response.output.bicomissgct;
                obj.baseincidenciacomgestrenovacao = response.output.bicomissren;
                obj.Cliente = response.output.zcliente;
                obj.comissaoabertura = response.output.comissabe;
                obj.comissaogestaocontrato = response.output.comissgct;
                obj.comissaorenovacao = response.output.comissren;

                DateTime dtfimcont;
                DateTime.TryParseExact(response.output.dtfimcont, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtfimcont);
                obj.datafimcontrato = dtfimcont;

                DateTime dtinicont;
                DateTime.TryParseExact(response.output.dtinicont, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtinicont);
                obj.datainiciocontrato = dtinicont;

                DateTime dtprocess;
                DateTime.TryParseExact(response.output.dtprocess, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtprocess);
                obj.dataProcessamento = dtprocess;

                DateTime dproxgest;
                DateTime.TryParseExact(response.output.dproxgest, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dproxgest);
                obj.dataproximacobrancagestcontrato = dproxgest;

                DateTime dproxrenov;
                DateTime.TryParseExact(response.output.dproxrenov, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dproxrenov);
                obj.dataproximacobrancagestrenovacao = dproxrenov;

                DateTime dtrenov;
                DateTime.TryParseExact(response.output.dtrenov, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtrenov);
                obj.datarenovacao = dtrenov;
                
                //obj.Descritivo = response.output.su
                obj.EstadoContrato = response.output.iestadoc;
                obj.graumorosidade = Convert.ToInt32(response.output.qgrau);
                obj.idmultilinha = response.output.idwf;
                obj.IndRenovacao = response.output.irenovac == "S" ? true : false;
                obj.indicadorAcaoCancelamento = response.output.idenuncia == "S" ? true : false ;
                obj.indicadorAcaoEnvioCartas = response.output.ienviocarta == "S" ? true : false;
                obj.limiteglobalmultilinha = response.output.mlimglobal;
                obj.ncontado = response.output.ccontado;
                obj.NDiasIncumprimento = Convert.ToInt32(response.output.qdiasincum);
                obj.NDiasPreAviso = Convert.ToInt32(response.output.qdiapaviso);
                obj.NMinutaContrato = Convert.ToInt32(response.output.zversao);
                //obj.Nome = response.output.n
                obj.NumeroMinimoProdutos = Convert.ToInt32(response.output.qminprod);
                obj.PeriocidadeCobrancagestcontrato = response.output.qperigest.ToString();
                obj.PeriocidadeCobrancagestRenovacao = response.output.qperirenov.ToString();
                obj.prazocontrato = Convert.ToInt32(response.output.qprzcont);
                obj.PrazoRenovacao = Convert.ToInt32(response.output.qprzrenov);
                obj.Produtoml = response.output.cprodutoml;
                obj.sublimiteriscoAssinatura = response.output.mlimassin;
                obj.sublimiteriscoFinanceiro = response.output.mlimfinan;
                obj.sublimitriscoComercial = response.output.mlimcomer;
                obj.Subprodutoml = response.output.cprodutoml;
                //obj.tipologiaRiscoA = response.output.tplriscass;
                //obj.tipologiaRiscoC = response.output.tplrisccom;
                //obj.tipologiaRiscoF = response.output.tplriscfin;
                obj.valorimpostocomabert = response.output.vicomissabe;
                obj.valorimpostocomgestcontrato = response.output.vicomissgct;
                obj.valorimpostocomgestrenovacao = response.output.vicomissren;
                
                obj.ProdutosRiscoAssinatura = new List<LM33_ContratoML.ProdutosRiscoA>();
                obj.produtosRiscoC = new List<LM33_ContratoML.ProdutoRiscoC>();
                obj.produtosRiscoF = new List<LM33_ContratoML.ProdutoRiscoF>();

                //listas
                foreach (var a in response.row1)
                {
                    if (a.l_cfamprod_l != null && a.l_cproduto_l != "")
                    {
                        obj.ProdutosRiscoAssinatura.Add(new LM33_ContratoML.ProdutosRiscoA
                        {
                            descritivo = a.l_csubprod_l, //TO DO ir a tat buscar descritivo
                            familiaproduto = a.l_cfamprod_l,
                            prodsubproduto = string.Concat(a.l_cproduto_l, a.l_csubprod_l),
                            tipologia = a.l_irisco_l
                            //zSeq = 
                        });
                    }
                    if (a.l_cfamprod_l != null && a.l_cproduto_l != "")
                    {
                        obj.produtosRiscoF.Add(new LM33_ContratoML.ProdutoRiscoF
                        {
                            descritivo = a.l_csubprod_l, //TO DO ir a tat buscar descritivo
                            familiaproduto = a.l_cfamprod_l,
                            prodsubproduto = string.Concat(a.l_cproduto_l, a.l_csubprod_l),
                            tipologia = a.l_irisco_l
                            //zSeq = 
                        });
                    }
                    if (a.l_cfamprod_l != null && a.l_cproduto_l != "")
                    {
                        obj.produtosRiscoC.Add(new LM33_ContratoML.ProdutoRiscoC
                        {
                            descritivo = a.l_csubprod_l, //TO DO ir a tat buscar descritivo
                            familiaproduto = a.l_cfamprod_l,
                            prodsubproduto = string.Concat(a.l_cproduto_l, a.l_csubprod_l),
                            tipologia = a.l_irisco_l
                            //zSeq = 
                        });
                    }
                }

                msgOut.ResultResult = obj;
            }
           
            return msgOut;
        }

        public  MensagemOutput<LM34_SublimitesML> LM34Request(LM34_SublimitesML _LM34, ABUtil.ABCommandArgs abargs, string accao, bool pedido)
        {
            MensagemOutput<LM34_SublimitesML> msgOut = new MensagemOutput<LM34_SublimitesML>();
            MultilinhasDataLayer.BCDWSProxy.LM34Transaction response = dl.LM34Request(abargs, _LM34, accao, pedido);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            
            if (response.output != null)
            {
                LM34_SublimitesML obj = new LM34_SublimitesML();

                int cliente = 0;
                Int32.TryParse(response.output.zcliente, out cliente);
                obj.Cliente = cliente;
                obj.Descritivo = response.output.cproduto; //IR A TAT
                obj.EstadoContrato = response.output.iestadoc;
                obj.idmultilinha = string.Format("{0}{1}{2}{3}", response.output.cbalcaoml, response.output.cproduto, response.output.cproduto, response.output.cdigictaml);
                obj.idSimulacao = response.output.idsimulacao.ToString();
                obj.limiteglobalmultilinha = response.output.mlimtotal;
                //obj.ncontado = string.Format("{0}{1}{2}{3}", response.output.cbalcaoml.ToString(), response.output.cprodutoml.ToString(), response.output.ct.ToString(), response.output.dgtdo.ToString());
                //obj.Nome =
                obj.Produtoml = response.output.cprodutoml;
                obj.sublimiteriscoAssinatura = response.output.mlimassin;
                obj.sublimiteriscoFinanceiro = response.output.mlimfinan;
                obj.sublimitriscoComercial = response.output.mlimcomer;
                obj.Subprodutoml = response.output.csubprod;

                //listas
                foreach (var a in response.row1)
                {
                    if (a.l_famprod_l != null && a.l_irisco_l != "")
                    {
                        obj.ProdutosRiscoAssinatura.Add(new LM34_SublimitesML.ProdutosRisco
                        {
                            codfamiliaproduto = Convert.ToInt32(a.l_famprod_l),
                            sublimiteAtual = a.l_mlimtotal_l,
                            sublimitecomprometido = a.l_mlimcomp_l,
                            sublimiteContratado = a.l_mlimcont_l,
                            tipologia = a.l_irisco_l,
                            
                        });
                    }
                    if (a.l_famprod_l != null && a.l_irisco_l != "")
                    {
                        obj.produtosRiscoF.Add(new LM34_SublimitesML.ProdutosRisco
                        {
                            codfamiliaproduto = Convert.ToInt32(a.l_famprod_l),
                            sublimiteAtual = a.l_mlimtotal_l,
                            sublimitecomprometido = a.l_mlimcomp_l,
                            sublimiteContratado = a.l_mlimcont_l,
                            tipologia = a.l_irisco_l,
                        });
                    }
                    if (a.l_famprod_l != null && a.l_irisco_l != "")
                    {
                        obj.produtosRiscoC.Add(new LM34_SublimitesML.ProdutosRisco
                        {
                            codfamiliaproduto = Convert.ToInt32(a.l_famprod_l),
                            sublimiteAtual = a.l_mlimtotal_l,
                            sublimitecomprometido = a.l_mlimcomp_l,
                            sublimiteContratado = a.l_mlimcont_l,
                            tipologia = a.l_irisco_l,
                        });
                    }
                }

                msgOut.ResultResult = obj;
            }
           
            return msgOut;
        }

        public MensagemOutput<LM35_AssociacaoContasDO> LM35Request(LM35_AssociacaoContasDO LM35, ABUtil.ABCommandArgs abargs, string accao, bool pedido)
        {
            MensagemOutput<LM35_AssociacaoContasDO> msgOut = new MensagemOutput<LM35_AssociacaoContasDO>();
            MultilinhasDataLayer.BCDWSProxy.LM35Transaction response = dl.LM35Request(abargs, LM35, accao, pedido);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";


            if (response.output != null)
            {
                LM35_AssociacaoContasDO obj = new LM35_AssociacaoContasDO();
                obj.Cliente = response.output.zcliente != null ? Convert.ToInt32(response.output.zcliente) : 0;
                //obj.DataAssociada = response.output.
                obj.ncontado = string.Concat(response.output.cbalcao, response.output.cproduto, response.output.cnumecta, response.output.cdigicta);
                obj.idmultilinha = string.Concat(response.output.cbalcaoml, response.output.cprodutoml, response.output.cnumectaml, response.output.cdigictaml);
                obj.Nome = response.output.gnome;
                obj.zSeq = response.output.zsequen;

                foreach(var a in response.row1)
                {
                    listaContaDO it = new listaContaDO();
                    if (a.l_cnumecta_l != null)
                    {
                        it.Associado = a.l_iassocia_l != "S" ? false : true;
                        DateTime dtiniass;
                        DateTime.TryParseExact(a.l_diniass_l, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtiniass);
                        DateTime dtfimass;
                        DateTime.TryParseExact(a.l_dfimass_l, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtfimass);
                        it.DataAssociada = dtiniass;
                        it.DataFimAssociacao = dtfimass;
                        it.NumContaDO = string.Concat(a.l_cbalcao_l, a.l_cproduto_l, a.l_cnumecta_l, a.l_cdigicta_l);
                        it.Moeda = a.l_cmoeda_l;
           
                    };

                    obj.Lista.Add(it);
                }
            }

            return msgOut;
        }

        public MensagemOutput<LM36_ContratosProduto> LM36Request(LM36_ContratosProduto _LM36, LM36_ContratosProduto.ContratosProduto rotLM36, ABUtil.ABCommandArgs abargs, string accao, bool pedido)
        {
            MensagemOutput<LM36_ContratosProduto> msgOut = new MensagemOutput<LM36_ContratosProduto>();
            MultilinhasDataLayer.BCDWSProxy.LM36Transaction response = dl.LM36Request(abargs, _LM36, rotLM36, accao, pedido);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            if (response.output != null)
            {
                LM36_ContratosProduto obj = new LM36_ContratosProduto();

                int cliente = 0;
                Int32.TryParse(response.output.zcliente, out cliente);
                obj.Cliente = cliente;
                obj.Descritivo = response.output.gdescml;

                obj.DPD = response.output.dpd == "N" ? false : true;
                //obj.EstadoContratoProduto = response.output.e
                obj.FamiliaProduto = response.output.cfamiprod;
                int graumorosidade = 0;
                Int32.TryParse(response.output.cgraumor, out graumorosidade);
                obj.GrauMorosidade = graumorosidade;
                obj.idmultilinha = string.Format("{0}{1}{2}{3}", response.output.cbalcao, response.output.cprodml, response.output.cnumectaml, response.output.cdigictaml);
                obj.limiteglobalmultilinha = response.output.mlimglobal;
                obj.Nome = response.output.gcliente;
                obj.Produtoml = response.output.cprodml;
                obj.sublimiteriscoAssinatura = response.output.mlimassin;
                obj.sublimiteriscoFinanceiro = response.output.mlimfinan;
                obj.sublimitriscoComercial = response.output.mlimcomer;
                obj.Subprodutoml = response.output.csubprodml;
                obj.TipologiaRisco = response.output.irisco;

                //listas
                foreach (var a in response.row1)
                {
                    if (a.lista_ccontrprod_l != null)
                    {
                        LM36_ContratosProduto.ContratosProduto ctr = new LM36_ContratosProduto.ContratosProduto();

                        int dpd = 0;
                        Int32.TryParse(response.output.dpd, out dpd);
                        ctr.DPD = dpd;
                        ctr.EstadoContratoProduto = a.lista_iestadocp_l;
                        ctr.ExposicaoAtual = a.lista_mexpoact_l;
                        ctr.FamiliaProduto = a.lista_cfamiprod_l;
                        ctr.GrauMorosidade = a.lista_cgraumor_l;
                        ctr.NContratoProduto = a.lista_ccontrprod_l;
                        ctr.SubProduto = a.lista_cprodsubpml_l; // com 4
                        ctr.TipoRisco = a.lista_irisco_l;
                        ctr.ValorComprometido = a.lista_mvlrcompr_l;
                        ctr.ValorContratado = a.lista_mvlrcontr_l;
                        ctr.ExposicaoAtual = a.lista_mexpoact_l;


                        obj.ContratosProdutos.Add(ctr);
                    };
                }
                msgOut.ResultResult = obj;
            }
          
            return msgOut;
        }

        public MensagemOutput<LM37_SimulacaoMl> LM37Request(LM37_SimulacaoMl _LM37, ABUtil.ABCommandArgs abargs, string accao, bool pedido)
        {
            MensagemOutput<LM37_SimulacaoMl> msgOut = new MensagemOutput<LM37_SimulacaoMl>();
            MultilinhasDataLayer.BCDWSProxy.LM37Transaction response = dl.LM37Request(abargs, _LM37, accao, pedido);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            
            if (response.output != null)
            {
                LM37_SimulacaoMl obj = new LM37_SimulacaoMl();

                int balcao = 0;
                Int32.TryParse(response.output.cbalcao, out balcao);
                obj.Balcao = balcao;
                int client = 0;
                int.TryParse(response.output.zcliente, out client);
                obj.Cliente = client;
                obj.Nome = response.output.gcliente;

                obj.idmultilinha = string.Format("{0}{1}{2}{3}", response.output.cbalcao, response.output.lcmv3701_cprod, response.output.ccta, response.output.cdgt);
                obj.idSimulacao = response.output.cidsimulml;
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
                obj.tipoSimulacao = response.output.ctpsimulml;

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
                        sim.CodigoTipologia = a.ccodtpl_l; // COM 4

                        obj.SimulacaoSublimites.Add(sim);

                    }
                }
                msgOut.ResultResult = obj;
            }
            return msgOut;
        }

        public MensagemOutput<LM38_HistoricoAlteracoes> LM38Request(LM38_HistoricoAlteracoes _LM38, ABUtil.ABCommandArgs abargs, string accao, bool pedido)
        {
            MensagemOutput<LM38_HistoricoAlteracoes> msgOut = new MensagemOutput<LM38_HistoricoAlteracoes>();
            MultilinhasDataLayer.BCDWSProxy.LM38Transaction response = dl.LM38Request(abargs, _LM38, accao, pedido);

            msgOut.erro = response.Erro != null ? response.Erro.CodigoErro : 999;
            msgOut.mensagem = response.Erro != null ? response.Erro.MensagemErro : "";

            
            if (response.output != null)
            {
                LM38_HistoricoAlteracoes obj = new LM38_HistoricoAlteracoes();
                int client = 0;
                int.TryParse(response.output.zcliente, out client);
                obj.Cliente = client;
                obj.idmultilinha = string.Format("{0}{1}{2}{3}", response.output.cbalcao, response.output.cprod, response.output.cta, response.output.dgt);
                //obj.Nome = response.output.gcliente; TO DO

                //listas
                foreach (var a in response.row1)
                {
                    if (a.zaltera_l != 0 && a.gtipo_l != null)
                    {
                        LM38_HistoricoAlteracoes.historicoAlteracoes his = new LM38_HistoricoAlteracoes.historicoAlteracoes();
                        his.campoAlterado = a.gtipo_l;

                        DateTime dat;
                        DateTime.TryParseExact(a.dprocess_l, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dat);
                        his.dataProcessamento = dat;

                        DateTime datV;
                        DateTime.TryParseExact(a.dtaltera_l, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out datV);
                        his.dataValorAlteracao = datV;

                        //his.description = a.d //Ir a TAT buscar descricao produto
                        his.idAlteracao = a.zaltera_l.ToString();
                        //his.nContratoProduto = a.ncontrato_l;
                        his.TipoAlteracao = a.gtipo_l;
                        his.utilizador = a.cutulmod_l;
                        his.valorAnterior = a.vanterior_l;
                        his.valorPosterior = a.vposterior_l;

                        obj.HistoricoAlteracoes.Add(his);
                    }
                }
                msgOut.ResultResult = obj;
            }
           
            return msgOut;
        }
    }
}
