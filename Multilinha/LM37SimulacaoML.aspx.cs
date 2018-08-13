using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Multilinha
{
    public partial class LM37SimulacaoML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            if (!IsPostBack)
            {

                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

                rdtipoSimulacao.DataSource = ML_Objectos.GetTiposSimulacao();
                rdtipoSimulacao.DataBind();
                //se nenhum item selecionado, selecionar o primeiro > T1
                if (rdtipoSimulacao.SelectedValue == "")
                {
                    rdtipoSimulacao.SelectedValue = "T1";
                }

                string op = Helper.getTransactionMode(Context, Request);
                switch (op.ToUpper())
                {
                    case "C":
                        lblTransaction.CssClass = lblTransaction.CssClass.Replace("atabD", "");
                        lblTransaction.Enabled = true;
                        Helper.AddRemoveActive(true, liCriacao);
                        Helper.AddRemoveActive(false, liVisualizacao);
                        lblTransaction.CssClass = lblTransaction.CssClass.Replace("atab", "atabD");

                        Helper.AddRemoveHidden(true, divProduto);
                        Helper.AddRemoveHidden(true, divSimulacaoSublimites);

                        break;
                    case "V":
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atabD", "");
                        lblTransactionV.Enabled = true;
                        Helper.AddRemoveActive(true, liVisualizacao);
                        Helper.AddRemoveActive(false, liCriacao);
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atab", "atabD");

                        Helper.AddRemoveHidden(true, divProduto);
                        Helper.AddRemoveHidden(true, divSimulacaoSublimites);
                        break;

                    default:
                        lberror.Text = "Página sem contexto. Execute a transação na Aplicação Bancária";
                        lberror.Visible = true;
                        break;

                }
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

        protected void btnSearchCont_Click(object sender, EventArgs e)
        {
            //TO DO Call LM37 para obter dados do cliente e da simulaçcao

            Helper.AddRemoveHidden(false, divProduto);
            Helper.AddRemoveHidden(false, divSimulacaoSublimites);


        }
    }
}