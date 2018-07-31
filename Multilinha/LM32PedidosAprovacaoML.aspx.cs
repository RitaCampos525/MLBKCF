using MultilinhasObjects;
using System;
using System.Web.UI;

namespace Multilinha
{
    public partial class LM32PedidosAprovacaoML : System.Web.UI.Page
    {
        MultilinhasDataLayer.boMultilinhas bo = new MultilinhasDataLayer.boMultilinhas();

        public DateTime dtfechas = Global.dtfechasG;
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //dropdownlists
                ddlTipoPedido.DataSource = ML_Objectos.GetTiposPedidoML();
                ddlTipoPedido.DataBind();

                //navigation
                Helper.AddRemoveActive(true, liPedidosAprovacao);
                lblTransaction.CssClass = lblTransaction.CssClass.Replace("atab", "atabD");
            }

        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita o require field nBalcao
            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                reqnBalcao.Enabled = false;
            }
            else if (!string.IsNullOrEmpty(txtidmultilinha.Text))
            {
                reqnBalcao.Enabled = false;
            }
            else
            {
                reqnBalcao.Enabled = true;
            }
                
        }

        protected void txtIdworkflow_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita o require field nBalcao
            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                reqnBalcao.Enabled = false;
            }
            else if (!string.IsNullOrEmpty(txtidmultilinha.Text))
            {
                reqnBalcao.Enabled = false;
            }
            else
            {
                reqnBalcao.Enabled = true;
            }
        }

        protected void btnSearchCont_Click(object sender, EventArgs e)
        {

        }
    }
}