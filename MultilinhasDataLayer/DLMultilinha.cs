using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MultilinhasDataLayer
{
    public class DLMultilinha
    {
        int atempt = 1;
        BCDWSProxy.CL55ConsultaRelacoesRequest CL55 = new BCDWSProxy.CL55ConsultaRelacoesRequest();

        public BCDWSProxy.CL55Transaction CL55Request(ABUtil.ABCommandArgs AbArgs, int numerocliente, string accao, string tipoacesso, string tipoConsulta)
        {
            BCDWSProxy.CL55Transaction response = new BCDWSProxy.CL55Transaction();

            CL55.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
            CL55.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
            CL55.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
            CL55.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;

            CL55.input = new BCDWSProxy.CL55Input();
            CL55.input.caccao = accao;
            CL55.input.zcliente = numerocliente.ToString().PadLeft(7,'0');
            CL55.input.ctipcon = Convert.ToInt64(tipoConsulta);
            CL55.input.cacesso = tipoacesso;
            CL55.input.pedido_dados = false;


            BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
            bool bRetry = false;
            atempt = 0;
            do
            {
                try
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapRequest, CL55.input.SerializeToString(), AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    response = client.CL55ConsultaRelacoes(CL55.BarclaysBankAccountSettings, CL55.input);

                    string sresponse = response.SerializeToString();

                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapResponse, sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    atempt++;
                }
                catch (Exception ex)
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Error, MultilinhasObjects.LogTypeName.WsSoapRequest, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    response.Erro = new BCDWSProxy.TransactionError();
                    response.Erro.MensagemErro = tratamentoExcepcoes(ex, AbArgs, out bRetry);

                }
            } while (bRetry && atempt <= 1);
            return response;
        }

        BCDWSProxy.LM31CATALOGOMLRequest LM31 = new BCDWSProxy.LM31CATALOGOMLRequest();
        public BCDWSProxy.LM31Transaction LM31Request(ABUtil.ABCommandArgs AbArgs, LM31_CatalogoProdutoML _lm31, string accao)
        {
            BCDWSProxy.LM31Transaction response = new BCDWSProxy.LM31Transaction();

            LM31.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
            LM31.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
            LM31.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
            LM31.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;

            LM31.input = new BCDWSProxy.LM31Input();
            LM31.input.caccao = accao;
            LM31.input.pedido_dados = false;
            LM31.input.cprodutoml = _lm31.ProductCode;
            LM31.input.csubprodml = _lm31.SubProdutoCode;
            //LM31.input.C = _lm31.SubProductDescription;
            LM31.input.dtinicomer = _lm31.DataInicioComercializacao.ToString("yyyyMMdd");
            LM31.input.dtfimcomer = _lm31.DataFimComercializacao.ToString("yyyyMMdd"); ;
            LM31.input.qprzminml = _lm31.PrazoMinimo;
            LM31.input.qprzmaxml = _lm31.PrazoMaximo;
            LM31.input.qminprod = _lm31.NumeroMinimoProdutos;
            LM31.input.mlimminml = _lm31.LimiteMinimoCredito;
            LM31.input.mlimmaxml = _lm31.LimiteMaximoCredito;
            LM31.input.iestado = ML_Objectos.GetEstadosDoCatalogo().FirstOrDefault(x => x.Description.ToUpper() == _lm31.Estado.ToUpper()).Code;
            LM31.input.qdiaincum = _lm31.NDiasIncumprimento;
            LM31.input.irenovac = _lm31.IndRenovacao.ToString();
            LM31.input.qperigest = Convert.ToInt32(_lm31.PeriocidadeCobranca);
            LM31.input.qprzrenov = _lm31.PrazoRenovacao;
            LM31.input.qdiapaviso = _lm31.NDiasPreAviso;
            LM31.input.dtversao = _lm31.DataVersao.ToString("yyyyMMdd");

            List<BCDWSProxy.LM31Row1> lstRow1 = new List<BCDWSProxy.LM31Row1>();

            if (_lm31.produtosF != null)
            {
                foreach (var f in _lm31.produtosF)
                {
                    BCDWSProxy.LM31Row1 _row1 = new BCDWSProxy.LM31Row1();
                    _row1.caccao = accao;
                    _row1.pedido_dados = false;
                    _row1.l_cproduto_l = f.produto;
                    _row1.l_tiporisco_l = f.tipologia;
                    _row1.l_famiprod_l = f.familia.PadLeft(3, '0');
                    _row1.l_csubprod_l = f.subproduto;

                    lstRow1.Add(_row1);
                }
            }

            if (_lm31.produtosA != null)
            {
                foreach (var a in _lm31.produtosA)
                {
                    BCDWSProxy.LM31Row1 _row1 = new BCDWSProxy.LM31Row1();
                    _row1.caccao = accao;
                    _row1.pedido_dados = false;
                    _row1.l_cproduto_l = a.produto;
                    _row1.l_tiporisco_l = a.tipologia;
                    _row1.l_famiprod_l = a.familia.PadLeft(3, '0');
                    _row1.l_csubprod_l = a.subproduto;

                    lstRow1.Add(_row1);
                }
            }

            if (_lm31.produtosC != null)
            {
                foreach (var c in _lm31.produtosC)
                {
                    BCDWSProxy.LM31Row1 _row1 = new BCDWSProxy.LM31Row1();
                    _row1.caccao = accao;
                    _row1.pedido_dados = false;
                    _row1.l_cproduto_l = c.produto;
                    _row1.l_tiporisco_l = c.tipologia;
                    _row1.l_famiprod_l = c.familia.PadLeft(3, '0');
                    _row1.l_csubprod_l = c.subproduto;

                    lstRow1.Add(_row1);
                }
            }

            LM31.input.Row1 = lstRow1.ToArray();
            BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
            bool bRetry = false;
            atempt = 0;
            do
            {
                try
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapRequest, LM31.input.SerializeToString(), AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    response = client.LM31CATALOGOML(LM31.BarclaysBankAccountSettings, LM31.input);
                    string sresponse = response.SerializeToString();

                    WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.WsSoapRequest, sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    atempt++;
                }
                catch (Exception ex)
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.WsSoapResponse, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    response.Erro = new BCDWSProxy.TransactionError();
                    response.Erro.MensagemErro = tratamentoExcepcoes(ex, AbArgs, out bRetry);
                }
            } while (bRetry && atempt <= 1);

            return response;
        }

        BCDWSProxy.LM32APROVACOESMLRequest LM32 = new BCDWSProxy.LM32APROVACOESMLRequest();
        public BCDWSProxy.LM32Transaction LM32Request(ABUtil.ABCommandArgs AbArgs, LM32_PedidosContratoML _lm32, string accao)
        {
            BCDWSProxy.LM32Transaction response = new BCDWSProxy.LM32Transaction();

            LM32.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
            LM32.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
            LM32.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
            LM32.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;

            LM32.input = new BCDWSProxy.LM32Input();
            LM32.input.btn_accept = _lm32.btnAccept ? "S" : "N";
            LM32.input.btn_reject = _lm32.btnReject ? "S" : "N";
            LM32.input.caccao = accao;
            LM32.input.cbalcao = _lm32.nBalcao.ToString();
            LM32.input.cbalcao_cidctrml = _lm32.txtidmultilinha_balcao;
            LM32.input.cdigictaml = _lm32.idmultilinha.Length > 10 ? _lm32.idmultilinha.ToString().Substring(8, 1) : "" ;
            LM32.input.cnumectaml = _lm32.idmultilinha.Length > 10 ? _lm32.idmultilinha.ToString().Substring(2, 6) : "";
            LM32.input.cprodutoml = _lm32.idmultilinha.Length > 10 ? _lm32.idmultilinha.ToString().Substring(0, 2) : "";
            LM32.input.cprodml = _lm32.ProductCode;
            LM32.input.csubprodml = _lm32.SubProdutoCode;
            LM32.input.gbalcao = _lm32.gBalcao;
            LM32.input.gcliente = _lm32.Nome;
            LM32.input.gdescml = _lm32.SubProductDescription;
            LM32.input.pedido_dados = false;
            LM32.input.tppedido = _lm32.TipoPedido;
            LM32.input.zcliente = _lm32.Cliente.ToString();
            

            BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
            bool bRetry = false;
            atempt = 0;
            do
            {
                try
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapRequest, LM32.input.SerializeToString(), AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    response = client.LM32APROVACOESML(LM32.BarclaysBankAccountSettings, LM32.input);
                    string sresponse = response.SerializeToString();

                    WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.WsSoapRequest, sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    atempt++;
                }
                catch (Exception ex)
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.WsSoapResponse, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    response.Erro = new BCDWSProxy.TransactionError();
                    response.Erro.MensagemErro = tratamentoExcepcoes(ex, AbArgs, out bRetry);
                }
            } while (bRetry && atempt <= 1);


            return response;

        }

        BCDWSProxy.LM33CONTRATOMLRequest LM33 = new BCDWSProxy.LM33CONTRATOMLRequest();
        public BCDWSProxy.LM33Transaction LM33Request(ABUtil.ABCommandArgs AbArgs, LM33_ContratoML _lm33, string accao, string acesso)
        {
            BCDWSProxy.LM33Transaction response = new BCDWSProxy.LM33Transaction();

            LM33.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
            LM33.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
            LM33.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
            LM33.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;

            LM33.input = new BCDWSProxy.LM33Input();
            LM33.input.caccao = accao;
            LM33.input.pedido_dados = false;
            LM33.input.acesso = acesso;
            LM33.input.cbalcaoml = string.IsNullOrEmpty(_lm33.idmultilinha) ? "" : _lm33.idmultilinha.ToString().Substring(0, 3);
            LM33.input.cprodutoml = _lm33.Produtoml;
            LM33.input.cnumectaml = string.IsNullOrEmpty(_lm33.idmultilinha) ? "" : _lm33.idmultilinha.ToString().Substring(5, 6);
            LM33.input.cdigictaml = string.IsNullOrEmpty(_lm33.idmultilinha) ? "" : _lm33.idmultilinha.ToString().Substring(11, 1);
            LM33.input.ccontado = _lm33.ncontado;
            LM33.input.csubprodml = _lm33.Subprodutoml;
            LM33.input.dtfimcont = _lm33.datafimcontrato.ToString("yyyyMMdd");
            LM33.input.dtinicont = _lm33.datainiciocontrato.ToString("yyyyMMdd");
            LM33.input.dtprocess = _lm33.dataProcessamento.ToString("yyyyMMdd");
            LM33.input.dproxgest = _lm33.dataproximacobrancagestcontrato.ToString("yyyyMMdd");
            LM33.input.dproxrenov = _lm33.dataproximacobrancagestrenovacao.ToString("yyyyMMdd");
            LM33.input.dtrenov = _lm33.datarenovacao.ToString("yyyyMMdd");
            LM33.input.iestado = _lm33.EstadoContrato != null ? ML_Objectos.GetEstadosDoCatalogo().FirstOrDefault(x => x.Description.ToUpper() == _lm33.EstadoContrato.ToUpper()).Code : "";
            LM33.input.qgrau = _lm33.graumorosidade.ToString();
            LM33.input.irenovac = _lm33.IndRenovacao == true ? "S" : "N";
            LM33.input.idwf = _lm33.idproposta;
            LM33.input.idenuncia = _lm33.indicadorAcaoCancelamento == true ? "S" : "N";
            LM33.input.ienviocarta = _lm33.indicadorAcaoEnvioCartas == true ? "S" : "N";
            LM33.input.mlimglobal = _lm33.limiteglobalmultilinha;
            LM33.input.mlimassin = _lm33.sublimiteriscoAssinatura;
            LM33.input.mlimcomer = _lm33.sublimitriscoComercial;
            LM33.input.mlimfinan = _lm33.sublimiteriscoFinanceiro;
            LM33.input.qdiapaviso = _lm33.NDiasPreAviso;
            LM33.input.qdiasincum = _lm33.NDiasIncumprimento;
            LM33.input.zversao = _lm33.NMinutaContrato;
            LM33.input.qperigest = Convert.ToInt64(_lm33.PeriocidadeCobrancagestcontrato);
            LM33.input.qperirenov = Convert.ToInt64(_lm33.PeriocidadeCobrancagestRenovacao);
            LM33.input.qprzcont = _lm33.prazocontrato;
            LM33.input.qprzrenov = _lm33.PrazoRenovacao;
            //LM33.input.tplriscass = _lm33.tipologiaRiscoA;
            //LM33.input.tplrisccom = _lm33.tipologiaRiscoC;
            //LM33.input.tplriscfin = _lm33.tipologiaRiscoF;
            LM33.input.comissabe = _lm33.comissaoabertura;
            LM33.input.comissgct = _lm33.comissaogestaocontrato;
            LM33.input.lmcv3301_comissren = _lm33.comissaorenovacao;
            LM33.input.vicomissabe = _lm33.valorimpostocomabert;
            LM33.input.vicomissgct = _lm33.valorimpostocomgestcontrato;
            LM33.input.lmcv3301_vicomissren = _lm33.valorimpostocomgestrenovacao;
            LM33.input.zcliente = _lm33.Cliente.ToString();
            LM33.input.bicomissabe = _lm33.baseincidenciacomabert;
            LM33.input.bicomissgct = _lm33.baseincidenciacomgestcontrato;
            LM33.input.lmcv3301_bicomissren = _lm33.baseincidenciacomgestrenovacao;
            LM33.input.qminprod = _lm33.NumeroMinimoProdutos;
   

            List<BCDWSProxy.LM33Row1> lstRow1 = new List<BCDWSProxy.LM33Row1>();
           

            foreach (var f in _lm33.produtosRiscoF)
            {
                BCDWSProxy.LM33Row1 _row1 = new BCDWSProxy.LM33Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.cproduto_l_l = f.prodsubproduto.Substring(0,2);
                _row1.irisco_l_l = f.tipologia;
                _row1.cfamprod_l_l = f.familiaproduto;
                _row1.csubprod_l_l = f.prodsubproduto.Substring(2,2);
            }

            foreach (var a in _lm33.ProdutosRiscoAssinatura)
            {
                BCDWSProxy.LM33Row1 _row1 = new BCDWSProxy.LM33Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.cproduto_l_l = a.prodsubproduto.Substring(0, 2);
                _row1.irisco_l_l = a.tipologia;
                _row1.cfamprod_l_l = a.familiaproduto;
                _row1.csubprod_l_l = a.prodsubproduto.Substring(2, 2);
            }

            foreach (var c in _lm33.produtosRiscoC)
            {
                BCDWSProxy.LM33Row1 _row1 = new BCDWSProxy.LM33Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.cproduto_l_l = c.prodsubproduto.Substring(0, 2);
                _row1.irisco_l_l = c.tipologia;
                _row1.cfamprod_l_l = c.familiaproduto;
                _row1.csubprod_l_l = c.prodsubproduto.Substring(2, 2);
            }

            LM33.input.Row1 = lstRow1.ToArray();
            BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
            bool bRetry = false;
            atempt = 0;
            do
            {
                try
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapRequest, LM33.input.SerializeToString(), AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    response = client.LM33CONTRATOML(LM33.BarclaysBankAccountSettings, LM33.input);
                    string sresponse = response.SerializeToString();

                    WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.WsSoapRequest, sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    atempt++;
                }
                catch (Exception ex)
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.WsSoapResponse, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    response.Erro = new BCDWSProxy.TransactionError();
                    response.Erro.MensagemErro = tratamentoExcepcoes(ex, AbArgs, out bRetry);

                }
            } while (bRetry && atempt <= 1);

            return response;
        }

        BCDWSProxy.LM34SUBLIMITESMLRequest LM34 = new BCDWSProxy.LM34SUBLIMITESMLRequest();
        public BCDWSProxy.LM34Transaction LM34Request(ABUtil.ABCommandArgs AbArgs, LM34_SublimitesML _LM34, string accao)
        {
            BCDWSProxy.LM34Transaction response = new BCDWSProxy.LM34Transaction();

            LM34.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
            LM34.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
            LM34.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
            LM34.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;

            LM34.input = new BCDWSProxy.LM34Input();
            LM34.input.caccao = accao;
            LM34.input.pedido_dados = false;

            LM34.input.cbalcao = string.IsNullOrEmpty(_LM34.idmultilinha.ToString()) ? "" : _LM34.idmultilinha.ToString().Substring(0, 3);
            LM34.input.cprod = _LM34.Produtoml;
            LM34.input.cta = string.IsNullOrEmpty(_LM34.idmultilinha.ToString()) ? "" : _LM34.idmultilinha.ToString().Substring(5, 6);
            LM34.input.dgt = string.IsNullOrEmpty(_LM34.idmultilinha.ToString()) ? "" : _LM34.idmultilinha.ToString().Substring(11, 1);
            LM34.input.cbalcaodo = string.IsNullOrEmpty(_LM34.ncontado) ? "" : _LM34.ncontado.Split('-')[0];
            LM34.input.cproddo = string.IsNullOrEmpty(_LM34.ncontado) ? "" : _LM34.ncontado.Split('-')[1].Replace("-", "").Trim().Substring(0, 2);
            LM34.input.ctado = string.IsNullOrEmpty(_LM34.ncontado) ? "" : _LM34.ncontado.Split('-')[1].Replace("-", "").Trim().Substring(2, 6);
            LM34.input.dgtdo = string.IsNullOrEmpty(_LM34.ncontado) ? "" : _LM34.ncontado.Split('-')[1].Replace("-", "").Trim().Substring(8, 1);
            LM34.input.csubprdml = _LM34.Subprodutoml;
            LM34.input.estctr = _LM34.EstadoContrato;
            LM34.input.limgloml = _LM34.limiteglobalmultilinha;
            LM34.input.limrisass = _LM34.sublimiteriscoAssinatura;
            LM34.input.limriscom = _LM34.sublimitriscoComercial;
            LM34.input.limrisfin = _LM34.sublimiteriscoFinanceiro;
            LM34.input.zcliente = _LM34.Cliente.ToString();
            LM34.input.indsimul = _LM34.idSimulacao.ToString();
            LM34.input.idwrkflw = _LM34.idSimulacao.ToString();

            List<BCDWSProxy.LM34Row1> lstRow1 = new List<BCDWSProxy.LM34Row1>();

            foreach (var f in _LM34.produtosRiscoF)
            {
                BCDWSProxy.LM34Row1 _row1 = new BCDWSProxy.LM34Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.codprod_f_l = f.prodsubproduto.Substring(0, 2);
                _row1.codtplo_f_l = f.tipologia;
                _row1.famprod_f_l = f.familiaproduto;
                _row1.subprod_f_l = f.prodsubproduto.Substring(2, 2);
            }

            foreach (var a in _LM34.ProdutosRiscoAssinatura)
            {
                BCDWSProxy.LM34Row1 _row1 = new BCDWSProxy.LM34Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.codprod_a_l = a.prodsubproduto.Substring(0, 2);
                _row1.codtplo_a_l = a.tipologia;
                _row1.famprod_a_l = a.familiaproduto;
                _row1.subprod_a_l = a.prodsubproduto.Substring(2, 2);
            }

            foreach (var c in _LM34.produtosRiscoC)
            {
                BCDWSProxy.LM34Row1 _row1 = new BCDWSProxy.LM34Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.codprod_c_l = c.prodsubproduto.Substring(0, 2); ;
                _row1.codtplo_c_l = c.tipologia;
                _row1.famprod_c_l = c.familiaproduto;
                _row1.subprod_c_l = c.prodsubproduto.Substring(2, 2);
            }

            LM34.input.Row1 = lstRow1.ToArray();
            BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
            bool bRetry = false;
            atempt = 0;
            do
            {
                try
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapRequest, LM34.input.SerializeToString(), AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    response = client.LM34SUBLIMITESML(LM34.BarclaysBankAccountSettings, LM34.input);
                    string sresponse = response.SerializeToString();

                    WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.WsSoapRequest, sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    atempt++;
                }
                catch (Exception ex)
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.WsSoapResponse, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    response.Erro = new BCDWSProxy.TransactionError();
                    response.Erro.MensagemErro = tratamentoExcepcoes(ex, AbArgs, out bRetry);

                }
            } while (bRetry && atempt <= 1);

            return response;
        }

        BCDWSProxy.LM36SUBPRODUTOSMLRequest LM36 = new BCDWSProxy.LM36SUBPRODUTOSMLRequest();
        public BCDWSProxy.LM36Transaction LM36Request(ABUtil.ABCommandArgs AbArgs, LM36_ContratosProduto _LM36, string accao)
        {
            BCDWSProxy.LM36Transaction response = new BCDWSProxy.LM36Transaction();

            LM36.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
            LM36.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
            LM36.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
            LM36.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;
           

            LM36.input = new BCDWSProxy.LM36Input();
            LM36.input.pedido_dados = false;
            LM36.input.caccao = accao;

            LM36.input.cbalcao = string.IsNullOrEmpty(_LM36.idmultilinha.ToString()) ? "" : _LM36.idmultilinha.ToString().Substring(0, 3);
            LM36.input.cprodml = _LM36.Produtoml;
            LM36.input.ccta = string.IsNullOrEmpty(_LM36.idmultilinha.ToString()) ? "" : _LM36.idmultilinha.ToString().Substring(5, 6);
            LM36.input.cdgt = string.IsNullOrEmpty(_LM36.idmultilinha.ToString()) ? "" : _LM36.idmultilinha.ToString().Substring(11, 1);
            LM36.input.csubprdml = _LM36.Subprodutoml;
            LM36.input.gdescml = _LM36.Descritivo;
            LM36.input.mlmglbml = _LM36.limiteglobalmultilinha;
            LM36.input.msublmra = _LM36.sublimiteriscoAssinatura;
            LM36.input.msublmrc = _LM36.sublimitriscoComercial;
            LM36.input.msublmrf = _LM36.sublimiteriscoFinanceiro;
            LM36.input.zcliente = _LM36.Cliente.ToString();
            LM36.input.gdescml = _LM36.Nome;
            LM36.input.cgraumorml = _LM36.GrauMorosidade.ToString();
            LM36.input.ntprisco = _LM36.TipologiaRisco;
            LM36.input.ntplprod = _LM36.FamiliaProduto;
            LM36.input.ndpdml = _LM36.DPD.ToString();

            List<BCDWSProxy.LM36Row1> lstRow1 = new List<BCDWSProxy.LM36Row1>();

            foreach (var f in _LM36.ContratosProdutos)
            {
                BCDWSProxy.LM36Row1 _row1 = new BCDWSProxy.LM36Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.cgraumor_l = f.GrauMorosidade;
                _row1.cidctrprod_l = f.NContratoProduto;
                _row1.cprodsubp_l = f.SubProduto;
                _row1.mexpoatual_l = f.ExposicaoAtual;
                _row1.mvlcompr_l = f.ValorComprometido;
                _row1.ndpd_l = f.DPD.ToString();
                _row1.nestctrprd_l = f.EstadoContratoProduto;
                _row1.ntplprod_l = f.FamiliaProduto;
                _row1.ntprisco_l = f.TipoRisco;
                _row1.lmcv3700_mvlcontr_l = f.ValorContratado;
                
            }

            LM36.input.Row1 = lstRow1.ToArray();
            BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
            bool bRetry = false;
            atempt = 0;
            do
            {
                try
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapRequest, LM36.input.SerializeToString(), AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    response = client.LM36SUBPRODUTOSML(LM36.BarclaysBankAccountSettings, LM36.input);
                    string sresponse = response.SerializeToString();

                    WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.WsSoapRequest, sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    atempt++;
                }
                catch (Exception ex)
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.WsSoapResponse, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    response.Erro = new BCDWSProxy.TransactionError();
                    response.Erro.MensagemErro = tratamentoExcepcoes(ex, AbArgs, out bRetry);

                }
            } while (bRetry && atempt <= 1);

            return response;
        }

        BCDWSProxy.LM37SIMULACAOMLRequest LM37 = new BCDWSProxy.LM37SIMULACAOMLRequest();
        public BCDWSProxy.LM37Transaction LM37Request(ABUtil.ABCommandArgs AbArgs, LM37_SimulacaoMl _LM37, string accao)
        {
            BCDWSProxy.LM37Transaction response = new BCDWSProxy.LM37Transaction();

            LM37.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
            LM37.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
            LM37.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
            LM37.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;
         

            LM37.input = new BCDWSProxy.LM37Input();
            LM37.input.pedido_dados = false;
            LM37.input.caccao = accao;

            LM37.input.cbalcao = string.IsNullOrEmpty(_LM37.idmultilinha.ToString()) ? "" : _LM37.idmultilinha.ToString().Substring(0, 3);
            LM37.input.lcmv3700_cprod = _LM37.Produtoml;
            LM37.input.ccta = string.IsNullOrEmpty(_LM37.idmultilinha.ToString()) ? "" : _LM37.idmultilinha.ToString().Substring(5, 6);
            LM37.input.cdgt = string.IsNullOrEmpty(_LM37.idmultilinha.ToString()) ? "" : _LM37.idmultilinha.ToString().Substring(11, 1);
            LM37.input.csubprdml = _LM37.Subprodutoml;
            LM37.input.gdescml = _LM37.Descritivo;
            LM37.input.mlmglbmlatual = _LM37.limiteglobalmultilinha;
            LM37.input.msublmraatual = _LM37.sublimiteriscoAssinatura;
            LM37.input.msublmrcatual = _LM37.sublimitriscoComercial;
            LM37.input.msublmrfatual = _LM37.sublimiteriscoFinanceiro;
       
            LM37.input.mlmglbmlnovo = _LM37.limiteglobalmultilinhaNovo;
            LM37.input.mlmglbmltotal = _LM37.limiteglobalmultilinhaTotal;
            LM37.input.msublmranovo = _LM37.sublimiteriscoAssinaturaNovo;
            LM37.input.msublmratotal = _LM37.sublimiteriscoAssinaturaTotal;
            LM37.input.msublmrcnovo = _LM37.sublimitriscoComercialNovo;
            LM37.input.msublmrctotal = _LM37.sublimitriscoComercialTotal;
            LM37.input.msublmrfnovo = _LM37.sublimiteriscoFinanceiroNovo;
            LM37.input.msublmrftotal = _LM37.sublimiteriscoFinanceiroTotal;
            LM37.input.zcliente = _LM37.Cliente.ToString();
            LM37.input.gdescml = _LM37.Nome;
            LM37.input.ctpsimulml = _LM37.tipoSimulacao;
           

            List<BCDWSProxy.LM37Row1> lstRow1 = new List<BCDWSProxy.LM37Row1>();

            foreach (var f in _LM37.SimulacaoSublimites)
            {
                BCDWSProxy.LM37Row1 _row1 = new BCDWSProxy.LM37Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.ntplprod_l = f.FamiliaProduto;
                _row1.ntprisco_l = f.TipologiaRisco;
                _row1.ccodtpl_l = f.CodigoTipologia; //com 4
                _row1.mpexpoatual_l = f.ExposicaoAtual;
                _row1.mpsublmcompnovo_l = f.SublimiteComprometidoNovo;
                _row1.msublmcompatual_l = f.SublimiteComprometido;
                _row1.msublmcontr_l = f.SublimiteContratado;
                _row1.nindpreco_l = f.preco ? "S" : "N";

                _row1.cons_cbalcao_l = f.cons_Balcao;
                _row1.cons_ccta_l = string.IsNullOrEmpty(f.cons_idMultilinha.ToString()) ? "" : f.cons_idMultilinha.ToString().Substring(5, 6);
                _row1.cons_cdgt_l = string.IsNullOrEmpty(f.cons_idMultilinha.ToString()) ? "" : f.cons_idMultilinha.ToString().Substring(11, 1);
                _row1.cons_cidsimulml_l = f.cons_idSimulacao.ToString();
                _row1.cons_cprod_l = string.IsNullOrEmpty(f.cons_idMultilinha.ToString()) ? "" : f.cons_idMultilinha.ToString().Substring(3, 2);
                _row1.cons_cprodsubp_l = f.cons_ProdSub;
                _row1.cons_cutulcria_l = f.cons_utilizador;
                _row1.cons_dsimulml_l = f.cons_DataSimulacao.ToString("yyyyMMdd");
                _row1.cons_mlmglbml_l = f.cons_limiteML;
                _row1.cons_msublmra_l = f.cons_limiteRA;
                _row1.cons_msublmra_l = f.cons_limiteRA;
                _row1.cons_msublmrc_l = f.cons_limiteRC;
                _row1.cons_msublmrf_l = f.cons_limiteRF;
                _row1.cons_zcliente_l = f.cons_Cliente;


            }

            LM37.input.Row1 = lstRow1.ToArray();
            BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
            bool bRetry = false;
            atempt = 0;
            do
            {
                try
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapRequest, LM37.input.SerializeToString(), AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    response = client.LM37SIMULACAOML(LM37.BarclaysBankAccountSettings, LM37.input);
                    string sresponse = response.SerializeToString();

                    WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.WsSoapRequest, sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    atempt++;
                }
                catch (Exception ex)
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.WsSoapResponse, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    response.Erro = new BCDWSProxy.TransactionError();
                    response.Erro.MensagemErro = tratamentoExcepcoes(ex, AbArgs, out bRetry);

                }
            } while (bRetry && atempt <= 1);

            return response;
        }

        BCDWSProxy.LM38HISTORICOMLRequest LM38 = new BCDWSProxy.LM38HISTORICOMLRequest();
        public BCDWSProxy.LM38Transaction LM38Request(ABUtil.ABCommandArgs AbArgs, LM38_HistoricoAlteracoes _LM38, string accao)
        {
            BCDWSProxy.LM38Transaction response = new BCDWSProxy.LM38Transaction();

            LM38.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
            LM38.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
            LM38.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
            LM38.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;
           

            LM38.input = new BCDWSProxy.LM38Input();
            LM38.input.pedido_dados = false;
            LM38.input.caccao = accao;

            LM38.input.cbalcao = string.IsNullOrEmpty(_LM38.idmultilinha.ToString()) ? "" : _LM38.idmultilinha.ToString().Substring(0, 3);
            LM38.input.cprod = string.IsNullOrEmpty(_LM38.idmultilinha.ToString()) ? "" : _LM38.idmultilinha.ToString().Substring(3, 2);
            LM38.input.cta = string.IsNullOrEmpty(_LM38.idmultilinha.ToString()) ? "" : _LM38.idmultilinha.ToString().Substring(5, 6);
            LM38.input.dgt = string.IsNullOrEmpty(_LM38.idmultilinha.ToString()) ? "" : _LM38.idmultilinha.ToString().Substring(11, 1); 
            LM38.input.zcliente = _LM38.Cliente.ToString();
            //LM38.input. = _LM38.Nome;


            List<BCDWSProxy.LM38Row1> lstRow1 = new List<BCDWSProxy.LM38Row1>();

            foreach (var f in _LM38.HistoricoAlteracoes)
            {
                BCDWSProxy.LM38Row1 _row1 = new BCDWSProxy.LM38Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.dprocess_l = f.dataProcessamento.ToString("yyyyMMdd");
                _row1.dtaltera_l = f.dataValorAlteracao.ToString("yyyyMMdd");
               // _row1.n = f.nContratoProduto;
                _row1.vanterior_l = f.valorAnterior;
                _row1.gtipo_l = f.TipoAlteracao;
                _row1.zaltera_l = Convert.ToInt64(f.idAlteracao);
                _row1.gtipo_l = f.campoAlterado; 
                _row1.vposterior_l = f.valorPosterior;
                _row1.cutulmod_l = f.utilizador;
            }

            //LM38.input.Row = lstRow1.ToArray();
            BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
            bool bRetry = false;
            atempt = 0;
            do
            {
                try
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapRequest, LM38.input.SerializeToString(), AbArgs.USERNT, AbArgs.SN_HOSTNAME);

                    response = client.LM38HISTORICOML(LM38.BarclaysBankAccountSettings, LM38.input);
                    string sresponse = response.SerializeToString();

                    WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.WsSoapRequest, sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    atempt++;
                }
                catch (Exception ex)
                {
                    WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.WsSoapResponse, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                    response.Erro = new BCDWSProxy.TransactionError();
                    response.Erro.MensagemErro = tratamentoExcepcoes(ex, AbArgs, out bRetry);

                }
            } while (bRetry && atempt <= 1);

            return response;
        }

        internal static bool SelectLogonUser(ABUtil.ABCommandArgs AbArgs)
        {
            BCDWSProxy.SelectLogonRequest selectLogon = new BCDWSProxy.SelectLogonRequest();
            BCDWSProxy.SelectLogonResponse response = new BCDWSProxy.SelectLogonResponse();
            string selectLogonResult = "";
            bool bLogonOK = false;

            selectLogon.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
            selectLogon.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
            selectLogon.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
            selectLogon.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;

            BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
            try
            {
                selectLogonResult = client.SelectLogon(selectLogon.BarclaysBankAccountSettings, AbArgs.CUTILIZA, 0);
                response.SelectLogonResult = selectLogonResult;

                string sresponse = response.SerializeToString();
                string _sresponse = sresponse.Replace("'1'", "1");

                bLogonOK = response.SelectLogonResult.Contains("status=\"0\"");

                WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapResponse, _sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);

            }
            catch (Exception ex)
            {
                WriteLog.Log(System.Diagnostics.TraceLevel.Error, MultilinhasObjects.LogTypeName.WsSoapRequest, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                response = null;
                
            }

            return bLogonOK;

        }

        //ENQUANTO NAO SAO INSERIDAS NA SYT05
        public static string tratamentoExcepcoes(Exception ex, ABUtil.ABCommandArgs AbArgs, out bool bRetry)
        {
            string mensagem = "";
            bRetry = false;
            try
            {
                System.ServiceModel.FaultCode fault = ex.GetType().GetProperty("Code").GetValue(ex, null) as System.ServiceModel.FaultCode;

                switch (fault.Name)
                {
                    case "0002":
                        mensagem = MultilinhasObjects.Constantes.Mensagens.UtilizadorSessaoInvalida;
                        if (!bRetry)
                            bRetry = SelectLogonUser(AbArgs);
                        break;
                    case "0066":
                        mensagem = MultilinhasObjects.Constantes.Mensagens.BalcaoInativo;
                        break;
                    case "0012":
                        mensagem = MultilinhasObjects.Constantes.Mensagens.UserRequesterInvalido;
                        break;
                    case "0025":
                        mensagem = MultilinhasObjects.Constantes.Mensagens.UtilizadorSemAcesso;
                        break;
                    case "9999":
                        if (ex.Message.Contains("0006 - Resposta vazia"))
                            mensagem = MultilinhasObjects.Constantes.Mensagens.SistemaIndisponivel;
                        else
                        {
                            mensagem = ex.Message;
                        }
                        break;
                    default:
                        mensagem = ex.Message;
                        break;
                }
            }
            catch
            {
            }

            return mensagem;
        }
    }
}
