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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

                //Show hide fields 
                string op = Request.QueryString["OP"] ?? "FF";
                switch (op.ToUpper())
                {
                    case "M":

                        //Helper.AddRemoveHidden(true, divdpConsulta);
                        //Helper.AddRemoveHidden(true, dvtitleAcordionRenovacao);
                        //Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        //Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        //Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        //Helper.AddRemoveHidden(true, divPeriocidadeCobranca);
                        //Helper.AddRemoveHidden(true, acoes_ml01);
                        //Helper.AddRemoveHidden(true, hr);
                        //Helper.AddRemoveHidden(true, hr1);

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

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "";
            txtidmultilinha.Text = "";
            txtNome.Text = "";
        }
    }
}