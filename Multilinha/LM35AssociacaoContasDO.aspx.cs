using MultilinhasObjects;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //get context of operation (C,V,M) from lm34
                string op = Context.Items["Op"] as string;
                op = string.IsNullOrEmpty(op) ? "FF" : op;
                ViewState["Op"] = op;

                //hide and show fields
                switch (op.ToUpper())
                {
                    case "M":
                        lblTransaction.CssClass = lblTransaction.CssClass.Replace("atabD", "");
                        lblTransaction.Enabled = true;

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, accoesfinais_criarlm35);
                        Helper.AddRemoveHidden(true, hr1);
                        Helper.AddRemoveHidden(true, hr2);
                        break;
                    case "C":
                        lblTransaction.CssClass = lblTransaction.CssClass.Replace("atabD", "");
                        lblTransaction.Enabled = true;

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, accoesfinais_criarlm35);
                        Helper.AddRemoveHidden(true, hr1);
                        Helper.AddRemoveHidden(true, hr2);
                        break;
                    case "A":
                        break;
                    case "V":
                        lblTransaction.CssClass = lblTransaction.CssClass.Replace("atabD", "");
                        lblTransaction.Enabled = true;

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

            Helper.AddRemoveHidden(false, dpOK);
            Helper.AddRemoveHidden(false, accoesfinais_criarlm35);
            Helper.AddRemoveHidden(false, hr1);
            Helper.AddRemoveHidden(false, hr2);

            MultilinhasObjects.LM35_AssociacaoContasDO lst =  TAT2.SearchML35(1, 2);
            lvAssociados.DataSource = lst.Lista;
            lvAssociados.DataBind();
        }

        protected void btnEnviarContrato(object sender, EventArgs e)
        {

            //Apanhar DOS e enviar para a LM35 - C

            //Call LM35
        }

    }
}