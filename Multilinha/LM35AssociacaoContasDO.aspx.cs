﻿using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Multilinha
{
    public partial class LM35AssociacaoContasDO : System.Web.UI.Page
    {
        public DateTime dtfechas = Global.dtfechasG;
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();
        MultilinhaBusinessLayer.BLMultilinha bl = new MultilinhaBusinessLayer.BLMultilinha();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

                string op = Helper.getTransactionMode(Context, Request);
                ViewState["Op"] = op;

                //Manter as duas tabs ativas
                Helper.AddRemoveActive(true, liTransaction);
                Helper.AddRemoveActive(true, liTransactionH);
                lblTransaction.CssClass = lblTransaction.CssClass.Replace("atab", "atabD");
                lblTransactionH.CssClass = lblTransactionH.CssClass.Replace("atab", "atabD");

                //hide and show fields
                switch (op.ToUpper())
                {
                    case "M":

                        Helper.SetEnableControler(camposChave, true);
                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, accoesfinais_criarlm35);
                        Helper.AddRemoveHidden(true, hr1);
                        Helper.AddRemoveHidden(true, hr2);

                        break;
                    case "C":

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, accoesfinais_criarlm35);
                        Helper.AddRemoveHidden(true, hr1);
                        Helper.AddRemoveHidden(true, hr2);
                        break;
                    case "A":
                        break;
                    case "V":

                        Helper.SetEnableControler(camposChave, true);
                        Helper.AddRemoveHidden(false, camposChave);
                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, accoesfinais_criarlm35);
                        Helper.AddRemoveHidden(true, hr1);
                        Helper.AddRemoveHidden(true, hr2);

                        break;
                    default:
                        Page.Transfer(ConfigurationManager.AppSettings["ContratoML"] + "?Op=C", //Sem contexto redireciona para lm33 - modo criar C
                        new Dictionary<string, object>() {
                                 { "Op", "C" } });
                        break;
                }

                //Populate fields

                if (Context.Items["ContratoCriado"] is MultilinhasObjects.LM34_SublimitesML)
                {
                    LM34_SublimitesML lm34c = Context.Items["ContratoCriado"] as LM34_SublimitesML;
                    Helper.CopyObjectToControls(lm35C, lm34c);
                    ViewState["ContratoCriado"] = lm34c;
                }

            }

        }

        protected void txtNumCliente_TextChanged(object sender, EventArgs e)
        {
           
           

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "";
            txtidmultilinha.Text = "";
            txtNome.Text = "";
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            //Call ML35
            //if sucess
            //MultilinhasObjects.LM35_AssociacaoContasDO lst = TAT2.SearchML35(1, "2");
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;

            LM35_AssociacaoContasDO lm35 = new LM35_AssociacaoContasDO();
            Helper.CopyPropertiesTo(camposChave, lm35);

            MensagemOutput<LM35_AssociacaoContasDO> resp = bl.LM35Request(lm35, abargs, "V", true);

            if (resp != null && resp.ResultResult != null)
            {
                Helper.AddRemoveHidden(false, dpOK);
                Helper.AddRemoveHidden(false, accoesfinais_criarlm35);
                Helper.AddRemoveHidden(false, hr1);
                Helper.AddRemoveHidden(false, hr2);


                lvAssociados.DataSource = resp.ResultResult.Lista;
                lvAssociados.DataBind();
            }

            else
            {
                lberror.Text = TAT2.GetMsgErroTATDescription(resp.erro.ToString(), abargs) ?? resp.erro.ToString();
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnEnviarContrato(object sender, EventArgs e)
        {

            //Apanhar DOS e enviar para a LM35 - C

            //Call LM35
        }

        protected void btnSeguinte_Click(object sender, EventArgs e)
        {
            LM35_AssociacaoContasDO lm35 = new LM35_AssociacaoContasDO();
            Helper.CopyPropertiesTo(camposChave, lm35);

            string urlQueries = Request.Url.Query;
            string op = "M";
            Page.Transfer(ConfigurationManager.AppSettings["PedidosML"] + urlQueries,
           new Dictionary<string, object>() {
                                  { "Op", op },
                                  { "HPedido", lm35 },
           });
        }
    }
}