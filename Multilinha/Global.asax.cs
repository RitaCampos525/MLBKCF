using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;


namespace Multilinha
{
    public class Global : System.Web.HttpApplication
    {
            public static string ConnectionStringDTAB;
           // public static string ConnectionStringABL;
            public static DateTime dtfechasG;

            MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();


            protected void Application_Start(object sender, EventArgs e)
            {

                OdbcConnection con = new OdbcConnection(ConfigurationManager.ConnectionStrings["MASTERDB2LOCAL"].ConnectionString);
                ConnectionStringDTAB = ConfigurationManager.ConnectionStrings["MASTERDB2LOCAL"].ConnectionString;

                #region ConnectionStringABL
                try
                {
                    //DataSet ds = new DataSet();
                    //OdbcDataAdapter ad = new OdbcDataAdapter("SELECT VALOR FROM AB_WEB_CONFIGURACOES WHERE NOME = 'ConnectionStringSITABL'", con);
                    //ad.Fill(ds);

                    //ConnectionStringABL = ds.Tables["Table"].Rows[0][0].ToString();

                }
                catch (Exception ex)
                {
                    //MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Error, "Application_Start", ex, "null", "null");
                }
                finally
                {
                    con.Close();
                }
                #endregion

            }


            protected void Session_Start(object sender, EventArgs e)
            {
                if (HttpContext.Current.Session != null && (HttpContext.Current.Session["SessionGUID"] == null || (Guid)Context.Session["SessionGUID"] == Guid.Empty))
                {
                    string SessionGUID = System.Guid.NewGuid().ToString();
                    Session["SessionGUID"] = SessionGUID;
                }
                // utilizador e terminal mudam com contexto
                Session["ABCommandArgs"] = null;
                ABUtil.ABCommandArgs abargs = SetUserInfo();
                Session["ABCommandArgs"] = abargs;

            //MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.AppStart, "Session Start", abargs.USERNT, abargs.SN_HOSTNAME);
            //MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Verbose, LogTypeName.AppStart, "Current session:" + HttpContext.Current.Session["SessionGUID"], abargs.USERNT, abargs.SN_HOSTNAME);
            //MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.AppStart, "Foram carregados os seguintes dados de utilizador:" +
            //" T.AB_ADDRESS: " + abargs.AB_ADDRESS +
            //"; T.SN_HOSTNAME: " + abargs.SN_HOSTNAME +
            //"; T.FQDN_HOSTNAME: " + abargs.FQDN_HOSTNAME +
            //"; T.HOSTIP: " + abargs.HOSTIP +
            //"; T.PORT: " + abargs.PORT +
            //"; T.AB_SESSION_ID: " + abargs.AB_SESSION_ID +
            //"; T.CUTILIZA: " + abargs.CUTILIZA +
            //"; T.CODBALCAO: " + abargs.CODBALCAO +
            //"; T.CTERM: " + abargs.CTERM +
            //"; T.CPERFIL: " + abargs.CPERFIL +
            //"; T.USERNT: " + abargs.USERNT +
            //"; T.DOMAIN: " + abargs.DOMAIN + ".",
            //abargs.USERNT, abargs.SN_HOSTNAME);

            #region dataFechas
            //Set variavel global - data operacao
            dtfechasG = TAT2.DataOperacao_Fechas(abargs, abargs.CODBALCAO).Date;
                #endregion
            }

            protected void Application_BeginRequest(object sender, EventArgs e)
            {

            }

            protected void Application_AuthenticateRequest(object sender, EventArgs e)
            {

            }

            protected void Session_End(object sender, EventArgs e)
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                //Helpers.Helper.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.AppStart, "Fim da sessão!", abargs.USERNT, abargs.SN_HOSTNAME);
            }

            protected void Application_End(object sender, EventArgs e)
            {

            }
            /// <summary>
            /// Lê os valores em querystring e coloca-os numa variavel de sessão
            /// </summary>
            /// <returns></returns>
            private ABUtil.ABCommandArgs SetUserInfo()
            {
                ABUtil.ABCommandArgs AbArgs = new ABUtil.ABCommandArgs();

                try
                {
                    HttpRequest request = HttpContext.Current.Request;

                    if (request.QueryString["cCodUtilza"] != null)
                    {
                        // usar IP - QueryString["hostip"]
                        AbArgs.AB_ADDRESS = request.QueryString["hostip"];
                        AbArgs.HOSTNAME = request.QueryString["host"];
                        AbArgs.HOSTIP = request.QueryString["hostip"];
                        // se "hostip" nao esta definido reverter para "host"
                        if (string.IsNullOrEmpty(AbArgs.AB_ADDRESS))
                        {
                            AbArgs.AB_ADDRESS = AbArgs.HOSTNAME;
                        }
                        AbArgs.FQDN_HOSTNAME = request.QueryString["host"];
                        //para ir buscar o short name do terminal
                        AbArgs.SN_HOSTNAME = AbArgs.FQDN_HOSTNAME != null ? AbArgs.FQDN_HOSTNAME.Split('.')[0] : "";
                        AbArgs.CPERFIL = request.QueryString["perfil"] == null ? "" : request.QueryString["perfil"];

                        #region User and domain to be used on cross network Blue Zone / Orange Zone
                        bool blnUserDomainQS = false;
                        blnUserDomainQS = request.QueryString["user"] != null & request.QueryString["domain"] != null;

                        if (blnUserDomainQS)
                        {
                            AbArgs.USERNT = request.QueryString["user"] == null ? "" : request.QueryString["user"];
                            AbArgs.DOMAIN = request.QueryString["domain"] == null ? "" : request.QueryString["domain"];

                            string[] asDomain = AbArgs.DOMAIN.Split(".".ToCharArray());
                            AbArgs.DOMAIN = asDomain[0];
                        }
                        #endregion User and domain to be used on cross network Blue Zone / Orange Zone

                        AbArgs.PORT = request.QueryString["port"] == null ? 0 : int.Parse(request.QueryString["port"]);
                        AbArgs.CODBALCAO = request.QueryString["cBalcao"];
                        AbArgs.CUTILIZA = request.QueryString["cCodUtilza"];
                        AbArgs.AB_SESSION_ID = request.QueryString["ABSessionId"];
                        AbArgs.CTERM = request.QueryString["cTerminal"];
                        AbArgs.ZCLIENTE = request.QueryString["cCliente"];

                        //log.Info("Global.asax QueryString And ReadQueryString Input: " + HttpContext.Current.Request.QueryString);

                    }

                }

                catch (Exception ex)
                {
                   // MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Error, LogTypeName.AppStart, ex.Message, AbArgs.USERNT, AbArgs.SN_HOSTNAME);
                }

                return AbArgs;
            }
        }
    }