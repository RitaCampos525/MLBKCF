using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Multilinha
{
    public partial class LM36ContratosProduto : System.Web.UI.Page
    {
        MultilinhaBusinessLayer.BLMultilinha bl = new MultilinhaBusinessLayer.BLMultilinha();
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

                //Bind DDls
                ddlTipoFam.DataSource = ArvoreFamiliaProdutos.SearchFamiliaProduto(ddlTipoRisco.SelectedValue).Select(x => x.familiaProduto).Distinct();
                ddlTipoFam.DataBind();

                //Show hide fields 
                string op = Request.QueryString["OP"] ?? "FF";
                switch (op.ToUpper())
                {
                    case "M":
                        break;
                    case "C":
                        break;
                    case "V":
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
                reqIDMultinha.Enabled = false;
                txtidmultilinha.Enabled = false;
            }
            else
            {
                reqIDMultinha.Enabled = true;
                txtidmultilinha.Enabled = true;
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            LM36_ContratosProduto lm36 = TAT2.SearchLM36(1234124);
            lvConsultaProdutos.DataSource = lm36.ContratosProdutos;
            lvConsultaProdutos.DataBind();

            if(lvConsultaProdutos.Items.Count > 0)
            {
                lkpaginaanterior.Visible = true;
                lkpaginaseguinte.Visible = true;
            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "";
            txtidmultilinha.Text = "";
            txtNome.Text = "";
            txtCliente.Enabled = true;
            txtidmultilinha.Enabled = true;
        }

        protected void txtidmultilinha_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txtidmultilinha.Text))
            {
                txtCliente.Enabled = false;
                reqNumCliente.Enabled = false;
            }
            else
            {
                txtCliente.Enabled = true;
                reqNumCliente.Enabled = true;

            }
        }

        protected void lkpaginaanterior_Click(object sender, EventArgs e)
        {

        }

        protected void lkpaginaseguinte_Click(object sender, EventArgs e)
        {

        }

        protected void ddlTipoRisco_TextChanged(object sender, EventArgs e)
        {
            ddlTipoFam.DataSource = ArvoreFamiliaProdutos.SearchFamiliaProduto(ddlTipoRisco.SelectedValue).Select(x => x.familiaProduto).Distinct();
            ddlTipoFam.DataBind();
        }
    }
}