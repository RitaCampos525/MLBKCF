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
        protected void Page_Load(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            if (!IsPostBack)
            {

                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

                string op = Helper.getTransactionMode(Context, Request);
                Helper.AddRemoveHidden(true, divBtnConsultar);
            }
        }

        protected void btnSearchCont_Click(object sender, EventArgs e)
        {
            Helper.SetEnableControler(camposChaveHis, true);
            
            List<LM38_HistoricoAlteracoes.historicoAlteracoes> lst = TAT2.SearchLM38(0001004, "310098766781").HistoricoAlteracoes;
            lvHistoricoAlteracoes.DataSource = lst;
            lvHistoricoAlteracoes.DataBind();

            if(lst.Count > 0)
             {
               Helper.AddRemoveHidden(false, divBtnConsultar); 
             }
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

    

        internal  void getGrid(LM38_HistoricoAlteracoes lm38)
         {
            
         }

        protected void blba_Click1(object sender, EventArgs e)
        {
            //Apenas um item selecionado!

            LM38_HistoricoAlteracoes lm38 = new LM38_HistoricoAlteracoes();

            Helper.CopyPropertiesTo(this, lm38);

            Page.Transfer(ConfigurationManager.AppSettings["DefinicaoSublimites"],
            new Dictionary<string, object>() {
                                { "Op", "C" },
                                { "HAlteracao", lm38 },
            });
        }
    }
}