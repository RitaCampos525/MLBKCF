using System;
using System.Collections.Generic;
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

        }

        protected void txtNumCliente_TextChanged(object sender, EventArgs e)
        {
            //ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;



        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {


        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {

            MultilinhasObjects.LM35AssociacaoContasDO lst =  TAT2.SearchML35(1, 2);
            lvAssociados.DataSource = lst.Lista;
            lvAssociados.DataBind();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {


        }

    }
}