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
            CL55.input.zcliente = numerocliente.ToString();
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
