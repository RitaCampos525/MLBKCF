using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Multilinha
{
 
    public partial class ucCancelamentoContrato : System.Web.UI.UserControl
    {
        public DateTime dtfechas = Global.dtfechasG;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //Mostrar apenas painel de chaves
                Helper.SetInvisibleControler(dpConsulta, false);
                Helper.SetEnableControler(dpConsulta, false);
                Helper.AddRemoveHidden(true, dvtitleAccoes);
                Helper.AddRemoveHidden(true, divAcoes);
                Helper.AddRemoveHidden(true, accoesfinais_denunciarml03);
                Helper.AddRemoveHidden(true, hr1);
            }
        }

        protected void btnSearchCont_Click(object sender, EventArgs e)
        {
            dpConsulta.Visible = true;
            Helper.SetInvisibleControler(dpConsulta, true);

            Helper.AddRemoveHidden(false, dvtitleAccoes);
            Helper.AddRemoveHidden(false, divAcoes);
            Helper.AddRemoveHidden(false, accoesfinais_denunciarml03);
            Helper.AddRemoveHidden(false, hr1);
        }
    }
}