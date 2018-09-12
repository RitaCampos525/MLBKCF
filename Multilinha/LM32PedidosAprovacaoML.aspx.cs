using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Multilinha
{
    public partial class LM32PedidosAprovacaoML : System.Web.UI.Page
    {
        
        MultilinhaBusinessLayer.BLMultilinha bl = new MultilinhaBusinessLayer.BLMultilinha();
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();

        public DateTime dtfechas = Global.dtfechasG;
        public string userAb = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);
                userAb = abargs.CUTILIZA;

                string op = Helper.getTransactionMode(Context, Request);
                Helper.AddRemoveHidden(true, dvAcoes_V);
                Helper.AddRemoveHidden(true, dvAcoes_M);

                switch (op.ToUpper())
                {
                    case "V":
                    //dropdownlists
                    ddlTipoPedido.DataSource = ML_Objectos.GetTiposPedidoML();
                    ddlTipoPedido.DataBind();

                    //navigation
                    Helper.AddRemoveActive(true, liPedidosAprovacao);
                    lblTransaction.CssClass = lblTransaction.CssClass.Replace("atab", "atabD");

                    Helper.AddRemoveHidden(true, dvproduto);

                    break;
                    case "M":
                    
                    Helper.SetEnableControler(camposChave, false);
                    Helper.AddRemoveHidden(false, dvproduto);
                    Helper.AddRemoveHidden(false, dvAcoes_M);
                    Helper.AddRemoveHidden(true, dvAcoes_V);
                    Helper.AddRemoveHidden(true, dvTipoPedido);
                    btnAprovarPedido.Enabled = true;
                    btnRejeitarPedido.Enabled = true;
                    btnSearch.Visible = false;

                    //navigation
                    Helper.AddRemoveActive(true, liAprovacaoPedido);
                    lblTransactionAp.CssClass = lblTransactionAp.CssClass.Replace("atab", "atabD");

                    //Contexto Modificação - Proveniente da Aprovação LM35
                    LM35_AssociacaoContasDO LM35 = Context.Items["HAprovacao"] as LM35_AssociacaoContasDO;
                    if (LM35 != null && LM35.Cliente != 0)
                    {
                        ViewState["HPedido"] = LM35;
                        Helper.CopyObjectToControls(camposChave, LM35);
                            
                    }

                    break;
                }
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

        protected void txtnBalcao_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnBalcao.Text))
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                string desc = TAT2.GetBalcaoDesc(txtnBalcao.Text, Global.ConnectionStringMaster, abargs);

                txtgBalcao.Text = desc;
            }
            else
            {
                txtgBalcao.Text = "";
            }

        }

        protected void btnSearchCont_Click(object sender, EventArgs e)
        {
            LM32_PedidosContratoML LM32 = new LM32_PedidosContratoML();
            Helper.CopyPropertiesTo(camposChave, LM32);

            //Call LM32
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            MensagemOutput<LM32_PedidosContratoML> response = bl.LM32Request(LM32, abargs, "V");
            userAb = abargs.CUTILIZA;

            if (response != null && response.ResultResult != null
                && response.ResultResult.PedidosAprovacao != null
                && response.ResultResult.PedidosAprovacao.Count > 0)
            {
                if (response.ResultResult.PedidosAprovacao[0].idmultilinha != null)
                {
                    lvhConsultaAprovacoes.DataSource = response.ResultResult.PedidosAprovacao;
                    Helper.AddRemoveHidden(false, dvAcoes_V);
                }
            }
            if (response == null || response.ResultResult == null || response.erro != 0)
            {
                lberror.Text = TAT2.GetMsgErroTATDescription(response.erro.ToString(), abargs);
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }

            lvhConsultaAprovacoes.DataBind();
            //dp retirar - teste
            //lvhConsultaAprovacoes.DataSource = TAT2.SearchLM32().PedidosAprovacao;
            //lvhConsultaAprovacoes.DataBind();
            //Helper.AddRemoveHidden(false, dvAcoes_V);
        }

        protected void btnAprovarPedido_Click(object sender, EventArgs e)
        {
            //Call LM32 - Aprovar
            LM32_PedidosContratoML LM32 = new LM32_PedidosContratoML();
            Helper.CopyPropertiesTo(camposChave, LM32);
            LM32.btnAccept = true;

            //Call LM32
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            MensagemOutput<LM32_PedidosContratoML> response = bl.LM32Request(LM32, abargs, "M");

            if (response != null && response.ResultResult != null)
            {

                lberror.Text = Constantes.Mensagens.LM32PedidoAprovado;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Green;
            }
            if (response == null || response.ResultResult == null || response.erro != 0)
            {
                lberror.Text = TAT2.GetMsgErroTATDescription(response.erro.ToString(), abargs);
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnRejeitarPedido_Click(object sender, EventArgs e)
        {
            //Call LM32 - Rejeitar
            LM32_PedidosContratoML LM32 = new LM32_PedidosContratoML();
            Helper.CopyPropertiesTo(camposChave, LM32);
            LM32.btnReject = true;

            //Call LM32
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            MensagemOutput<LM32_PedidosContratoML> response = bl.LM32Request(LM32, abargs, "M");

            if (response != null && response.ResultResult != null)
            {

                lberror.Text = Constantes.Mensagens.LM32PedidoRejeitado;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Green;
            }
            if (response == null || response.ResultResult == null || response.erro != 0)
            {
                lberror.Text = TAT2.GetMsgErroTATDescription(response.erro.ToString(), abargs);
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnConsultarPedido_Click(object sender, EventArgs e)
        {
            //Redirecciona para LM33
            ListViewDataItem pdSelected = lvhConsultaAprovacoes.Items.Where(x => (x.FindControl("cbSelected") as CheckBox).Checked).FirstOrDefault() as ListViewDataItem;
            LM32_PedidosContratoML lm32 = new LM32_PedidosContratoML();

            Helper.CopyPropertiesTo(dvAprovacoes, lm32.PedidosAprovacao[0]);

            LM33_ContratoML lm33 = new LM33_ContratoML();
            lm33.Cliente = lm32.PedidosAprovacao[0].idcliente.ToString();
            lm33.Produtoml = lm32.PedidosAprovacao[0].produto;
            lm33.Subprodutoml = lm32.PedidosAprovacao[0].subProduto;
            lm33.Descritivo = lm32.PedidosAprovacao[0].descritivo;

            string urlQueries = Request.Url.Query;
            Page.Transfer(ConfigurationManager.AppSettings["ContratoML"] + urlQueries,
           new Dictionary<string, object>() {
                                  { "Op", "V" },
                                  { "HAprovacao", lm33 },
           });
        }

        protected void btnTratarPedido_Click(object sender, EventArgs e)
        {
            //Redirecciona para LM33 - Apenas para Aprovadores
            ListViewDataItem pdSelected = lvhConsultaAprovacoes.Items.Where(x => (x.FindControl("cbSelected") as CheckBox).Checked).FirstOrDefault() as ListViewDataItem;
            LM32_PedidosContratoML lm32 = new LM32_PedidosContratoML();

            Helper.CopyPropertiesTo(dvAprovacoes, lm32.PedidosAprovacao[0]);

            LM33_ContratoML lm33 = new LM33_ContratoML();
            lm33.Cliente = lm32.PedidosAprovacao[0].idcliente.ToString();
            lm33.Produtoml = lm32.PedidosAprovacao[0].produto;
            lm33.Subprodutoml = lm32.PedidosAprovacao[0].subProduto;
            lm33.Descritivo = lm32.PedidosAprovacao[0].descritivo;

            string urlQueries = Request.Url.Query;
            Page.Transfer(ConfigurationManager.AppSettings["ContratoML"] + urlQueries,
           new Dictionary<string, object>() {
                                  { "Op", "V" },
                                  { "HAprovacao", lm33 },
           });
        }
    }
}