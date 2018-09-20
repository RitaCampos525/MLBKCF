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
    public partial class LM38HistoricoML : System.Web.UI.Page
    {
        public DateTime dtfechas = Global.dtfechasG;
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();
        MultilinhaBusinessLayer.BLMultilinha bl = new MultilinhaBusinessLayer.BLMultilinha();
        protected void Page_Load(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            if (!IsPostBack)
            {

                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

                string op = Helper.getTransactionMode(Context, Request);
                Helper.AddRemoveHidden(true, divBtnConsultar);
                Helper.AddRemoveHidden(true, hr2);
            }
        }

        protected void btnSearchCont_Click(object sender, EventArgs e)
        {
           
            LM38_HistoricoAlteracoes LM38 = new LM38_HistoricoAlteracoes();
            Helper.CopyPropertiesTo(camposChaveHis, LM38);

            //For debug
            //List<LM38_HistoricoAlteracoes.historicoAlteracoes> lst = TAT2.SearchLM38(0001004, "310098766781").HistoricoAlteracoes;

            //Call LM38
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            MensagemOutput<LM38_HistoricoAlteracoes> response = bl.LM38Request(LM38, abargs, "V", true);

            if(response != null && response.ResultResult != null 
                && response.ResultResult.HistoricoAlteracoes != null 
                && response.ResultResult.HistoricoAlteracoes.Count > 0)
             {
                lvhistoricoAlteracoes.DataSource = response.ResultResult.HistoricoAlteracoes;
                
                Helper.AddRemoveHidden(false, divBtnConsultar);
                Helper.AddRemoveHidden(false, hr2);
            }
            if (response == null || response.ResultResult == null || response.erro != 0)
            {
                lberror.Text = TAT2.GetMsgErroTATDescription(response.erro.ToString(), abargs);
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }
            lvhistoricoAlteracoes.DataBind();

        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                reqidmultilinha.Enabled = false;
                txtidmultilinha.Enabled = false;
            }
            else
            {
                txtidmultilinha.Enabled = true;
                reqidmultilinha.Enabled = true;
            }
        }

        protected void txt_idmultilinha_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txtidmultilinha.Text))
            {
                reqCliente.Enabled = false;
                txtCliente.Enabled = false;
            }
            else
            {
                txtidmultilinha.Enabled = true;
                reqCliente.Enabled = true;

            }
        }

        protected void blba_Click1(object sender, EventArgs e)
        {
            //Apenas 1 seleccionado
            ListViewDataItem historicoSelected = lvhistoricoAlteracoes.Items.Where(x => (x.FindControl("cbSelected") as CheckBox).Checked).FirstOrDefault() as ListViewDataItem;
            LM38_HistoricoAlteracoes lm38 = new LM38_HistoricoAlteracoes();
            Helper.CopyPropertiesTo(this, lm38);
            Helper.CopyPropertiesTo(historicoSelected, lm38.HistoricoAlteracoes[0]);

            string urlQueries = Request.Url.Query;
            string href = "";

            //If alt. condicao geral -> redireciona para lm33
            //else -> redirecciona para lm34
            if (lm38.HistoricoAlteracoes[0].TipoAlteracao.ToUpper().Contains("GERAL"))
            {
                href = ConfigurationManager.AppSettings["ContratoML"] + urlQueries;
            }
            else
            {
                href = ConfigurationManager.AppSettings["SublimitesML"] + urlQueries;
            }

            Page.Transfer(href,
            new Dictionary<string, object>() {
                                { "Op", "V" },
                                { "Hhistorico", lm38 },
            });
        }
    }
}