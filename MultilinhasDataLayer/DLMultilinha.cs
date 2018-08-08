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
            LM31.input.cprdml = _lm31.ProductCode;
            LM31.input.csubprdml = _lm31.SubProdutoCode;
            LM31.input.cdescprd = _lm31.SubProductDescription;
            LM31.input.dincomer = _lm31.DataInicioComercializacao.ToString("yyyyMMdd");
            LM31.input.dficomer = _lm31.DataFimComercializacao.ToString("yyyyMMdd"); ;
            LM31.input.przmin = _lm31.PrazoMinimo.ToString();
            LM31.input.przmax = _lm31.PrazoMaximo.ToString();
            LM31.input.nmprdat = _lm31.NumeroMinimoProdutos.ToString();
            LM31.input.lmtmincr = _lm31.LimiteMinimoCredito;
            LM31.input.lmtmaxcr = _lm31.LimiteMaximoCredito;
            LM31.input.estparm = ML_Objectos.GetEstadosDoCatalogo().FirstOrDefault(x => x.Description.ToUpper() == _lm31.Estado.ToUpper()).Code;
            LM31.input.nmdiainc = _lm31.NDiasIncumprimento.ToString();
            LM31.input.idrenov = _lm31.IndRenovacao.ToString();
            LM31.input.percobcom = _lm31.PeriocidadeCobranca;
            LM31.input.przrenov = _lm31.PrazoRenovacao.ToString();
            LM31.input.ndpreavi = _lm31.NDiasPreAviso.ToString();

            List<BCDWSProxy.LM31Row1> lstRow1 = new List<BCDWSProxy.LM31Row1>();
            BCDWSProxy.LM31Row1 row1 = null;

            foreach (var f in _lm31.produtosF)
            {
                BCDWSProxy.LM31Row1 _row1 = new BCDWSProxy.LM31Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.codprod_f_l = f.produto;
                _row1.codtplo_f_l = f.tipologia;
                _row1.famprod_f_l = f.familia.PadLeft(3,'0');
                _row1.subprod_f_l = f.subproduto;

                lstRow1.Add(_row1);
            }

            foreach (var a in _lm31.produtosA)
            {
                BCDWSProxy.LM31Row1 _row1 = new BCDWSProxy.LM31Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.codprod_a_l = a.produto;
                _row1.codtplo_a_l = a.tipologia;
                _row1.famprod_a_l = a.familia.PadLeft(3, '0'); ;
                _row1.subprod_a_l = a.subproduto;

                lstRow1.Add(_row1);
            }

            foreach (var c in _lm31.produtosC)
            {
                BCDWSProxy.LM31Row1 _row1 = new BCDWSProxy.LM31Row1();
                _row1.caccao = accao;
                _row1.pedido_dados = false;
                _row1.codprod_c_l = c.produto;
                _row1.codtplo_c_l = c.tipologia;
                _row1.famprod_c_l = c.familia.PadLeft(3, '0'); ;
                _row1.subprod_c_l = c.subproduto;

                lstRow1.Add(_row1);
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

        BCDWSProxy.LM33CONTRATOMLRequest LM33 = new BCDWSProxy.LM33CONTRATOMLRequest();
        //public BCDWSProxy.LM31Transaction LM31Request(ABUtil.ABCommandArgs AbArgs, LM31_CatalogoProdutoML _lm31, string accao)
        //{
        //    BCDWSProxy.LM31Transaction response = new BCDWSProxy.LM31Transaction();

        //    LM31.BarclaysBankAccountSettings = new BCDWSProxy.BarclaysBankAccountSettings();
        //    LM31.BarclaysBankAccountSettings.ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];
        //    LM31.BarclaysBankAccountSettings.UserRequester = AbArgs.USERNT;
        //    LM31.BarclaysBankAccountSettings.ClientName = AbArgs.SN_HOSTNAME;

        //    LM31.input = new BCDWSProxy.LM31Input();
        //    LM31.input.caccao = accao;
        //    LM31.input.pedido_dados = false;
        //    LM31.input.cprdml = _lm31.ProductCode;
        //    LM31.input.csubprdml = _lm31.SubProductCode;
        //    LM31.input.cdescprd = _lm31.SubProductDescription;
        //    LM31.input.dincomer = _lm31.DataInicioComercializacao.ToString();
        //    LM31.input.dficomer = _lm31.DataInicioComercializacao.ToString();
        //    LM31.input.przmin = _lm31.PrazoMinimo.ToString();
        //    LM31.input.przmax = _lm31.PrazoMaximo.ToString();
        //    LM31.input.nmprdat = _lm31.NumeroMinimoProdutos.ToString();
        //    LM31.input.lmtmincr = _lm31.LimiteMinimoCredito;
        //    LM31.input.lmtmaxcr = _lm31.LimiteMaximoCredito;
        //    LM31.input.estparm = _lm31.Estado;
        //    LM31.input.nmdiainc = _lm31.NDiasIncumprimento.ToString();
        //    LM31.input.idrenov = _lm31.IndRenovacao.ToString();
        //    LM31.input.percobcom = _lm31.PeriocidadeCobranca;
        //    LM31.input.przrenov = _lm31.PrazoRenovacao.ToString();
        //    LM31.input.ndpreavi = _lm31.NDiasPreAviso.ToString();

        //    List<BCDWSProxy.LM31Row1> lstRow1 = new List<BCDWSProxy.LM31Row1>();
        //    BCDWSProxy.LM31Row1 row1 = null;

        //    foreach (var f in _lm31.produtosF)
        //    {
        //        row1.caccao = accao;
        //        row1.pedido_dados = false;
        //        row1.codprod_f_l = f.produto;
        //        row1.codtplo_f_l = f.tipologia;
        //        row1.famprod_f_l = f.familia;
        //        row1.subprod_f_l = f.subproduto;
        //    }

        //    foreach (var a in _lm31.produtosA)
        //    {
        //        row1.caccao = accao;
        //        row1.pedido_dados = false;
        //        row1.codprod_a_l = a.produto;
        //        row1.codtplo_a_l = a.tipologia;
        //        row1.famprod_a_l = a.familia;
        //        row1.subprod_a_l = a.subproduto;
        //    }

        //    foreach (var c in _lm31.produtosC)
        //    {
        //        row1.caccao = accao;
        //        row1.pedido_dados = false;
        //        row1.codprod_c_l = c.produto;
        //        row1.codtplo_c_l = c.tipologia;
        //        row1.famprod_c_l = c.familia;
        //        row1.subprod_c_l = c.subproduto;
        //    }

        //    LM31.input.Row1 = lstRow1.ToArray();
        //    BCDWSProxy.BarclaysBTSSoapClient client = new BCDWSProxy.BarclaysBTSSoapClient();
        //    bool bRetry = false;
        //    atempt = 0;
        //    do
        //    {
        //        try
        //        {
        //            WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, MultilinhasObjects.LogTypeName.WsSoapRequest, LM31.input.SerializeToString(), AbArgs.USERNT, AbArgs.SN_HOSTNAME);

        //            response = client.LM31CATALOGOML(LM31.BarclaysBankAccountSettings, LM31.input);
        //            string sresponse = response.SerializeToString();

        //            WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.WsSoapRequest, sresponse, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
        //            atempt++;
        //        }
        //        catch (Exception ex)
        //        {
        //            WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.WsSoapResponse, ex, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
        //            response.Erro = new BCDWSProxy.TransactionError();

        //        }
        //    } while (bRetry && atempt <= 1);

        //    return response;
        //}

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
                        break;
                    default:
                        mensagem = fault.Name;
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
