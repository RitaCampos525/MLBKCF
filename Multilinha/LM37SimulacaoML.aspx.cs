using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Multilinha
{
    public partial class LM37SimulacaoML : System.Web.UI.Page
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

                rdtipoSimulacao.DataSource = ML_Objectos.GetTiposSimulacao();
                rdtipoSimulacao.DataBind();
                //se nenhum item selecionado, selecionar o primeiro > T1
                if (rdtipoSimulacao.SelectedValue == "")
                {
                    rdtipoSimulacao.SelectedValue = ML_Objectos.GetTiposSimulacao()[0].Code;
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
                        Helper.AddRemoveHidden(true, divSimular);
                        Helper.AddRemoveHidden(true, dvAcoes_C);
                        Helper.AddRemoveHidden(true, lm37_vis);

                        break;
                    case "V":
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atabD", "");
                        lblTransactionV.Enabled = true;
                        Helper.AddRemoveActive(true, liVisualizacao);
                        Helper.AddRemoveActive(false, liCriacao);
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atab", "atabD");

                        Helper.AddRemoveHidden(true, lm37_criar);
                        Helper.AddRemoveHidden(false, lm37_vis);
                        Helper.AddRemoveHidden(true, dvAcoes_V);
                      
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

        protected void txtnBalcao_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBalcao.Text))
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                string desc = TAT2.GetBalcaoDesc(txtBalcao.Text, Global.ConnectionStringMaster, abargs);

                txtgbalcao.Text = desc;
            }
            else
            {
                txtgbalcao.Text = "";
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
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;

            Helper.SetEnableControler(camposChaveSim, false);
            Helper.AddRemoveHidden(false, divProduto);
            Helper.SetEnableControler(divProduto, false);
            Helper.AddRemoveHidden(false, divSimulacaoSublimites);
            Helper.AddRemoveHidden(false, divSimular);
            Helper.AddRemoveHidden(false, dvAcoes_C);

            

            string op = Helper.getTransactionMode(Context, Request);
            switch (op.ToUpper())
            {
                case "C":
                    LM37_SimulacaoMl sim = new LM37_SimulacaoMl();
                    Helper.CopyPropertiesTo(camposChaveSim, sim);

                    MensagemOutput<LM37_SimulacaoMl> respOut = bl.LM37Request(sim, abargs, "V", true);

                    //Insucesso
                    if (respOut == null || respOut.ResultResult == null || respOut.ResultResult.Cliente == 0)
                    {

                        lberror.Text = TAT2.GetMsgErroTATDescription(respOut.erro.ToString(), abargs);
                        if(string.IsNullOrEmpty(lberror.Text))
                        {
                            lberror.Text = respOut.mensagem;
                        }
                        lberror.Visible = true;
                        lberror.ForeColor = System.Drawing.Color.Red;

                        //teste retirar dp 
                        //respOut.ResultResult =  TAT2.SearchML37(0001004, "510092588522");

                    }

                    Helper.CopyObjectToControls(this, respOut.ResultResult);
                    bindlistviewsimulacao(respOut.ResultResult, lvProdutosSimulacao);

                    if (rdtipoSimulacao.SelectedValue == ML_Objectos.GetTiposSimulacao()[1].Code)
                    {
                        txtlimiteglobalmultilinhaNovo.Enabled = true;
                        txtsublimiteriscoAssinaturaNovo.Enabled = true;
                        txtsublimitriscoComercialNovo.Enabled = true;
                        txtsublimiteriscoFinanceiroNovo.Enabled = true;
                    }

                    break;
                case "V":
                    LM37_SimulacaoMl simCon = new LM37_SimulacaoMl();
                    Helper.CopyPropertiesTo(camposchaveConsulta, simCon);

                    MensagemOutput<LM37_SimulacaoMl> respOutw = bl.LM37Request(simCon, abargs, "V", true);

                    //Insucesso
                    if (respOutw == null || respOutw.ResultResult == null || respOutw.ResultResult.Cliente == 0)
                    {

                        lberror.Text = TAT2.GetMsgErroTATDescription(respOutw.erro.ToString(), abargs);
                        if (string.IsNullOrEmpty(lberror.Text))
                        {
                            lberror.Text = respOutw.mensagem;
                        }
                        lberror.Visible = true;
                        lberror.ForeColor = System.Drawing.Color.Red;

                    }

                    //Consulta em lista
                    lvConsultaSimulacoes.DataSource = simCon.SimulacaoSublimites;
                    lvConsultaSimulacoes.DataBind();
                    if(simCon.SimulacaoSublimites.Count > 0)
                    {
                        Helper.AddRemoveHidden(false, dvAcoes_V);
                    }
                    break;
            }

        }

        internal void bindlistviewsimulacao(LM37_SimulacaoMl sml, ListView lv)
        {
            List<LM37_SimulacaoMl.simulacaoSublimites> lst = sml.SimulacaoSublimites;
            lvProdutosSimulacao.DataSource = lst;
            lvProdutosSimulacao.DataBind();
        }

        protected void btnSimular_Click(object sender, EventArgs e)
        {
            lberror.Text = "";
            bool valido = validaLimites();
            bool validoLimitesProd = validaSublimites();
            if (valido && validoLimitesProd)
            {
                btnGuardarSimulacao.Enabled = true;
            }
            else
            {
                lberror.Text = Constantes.Mensagens.LM37Formularioinvalido;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }
            
        }

        protected void lbSublimiteComprometidoNovo_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            decimal novo;
            decimal.TryParse(txt.Text, out novo);

            ListViewItem row = txt.NamingContainer as ListViewItem;
            int index = Convert.ToInt32(row.DisplayIndex);

            string tiporisco = (lvProdutosSimulacao.Items[index].FindControl("lbTipologiaRisco") as Label).Text;
            string exposicaoAtual = (lvProdutosSimulacao.Items[index].FindControl("lbExposicaoAtual") as Label).Text;

            decimal decexposicaoatual;
            Decimal.TryParse(exposicaoAtual, out decexposicaoatual);

            decimal sublimiterisco = 0;
            switch (tiporisco)
            {
                case "F":
                    Decimal.TryParse(txtsublimiteriscoFinanceiro.Text, out sublimiterisco);
                    break;
                case "A":
                    Decimal.TryParse(txtsublimiteriscoAssinatura.Text, out sublimiterisco);
                    break;
                case "C":
                    Decimal.TryParse(txtsublimitriscoComercial.Text, out sublimiterisco);
                    break;
            }

            var simbolo = lvProdutosSimulacao.Items[index].FindControl("simulcaovalido") as HtmlGenericControl;
            var awarning = lvProdutosSimulacao.Items[index].FindControl("textsimulcaovalido") as HtmlGenericControl;
            awarning.InnerHtml = "";

            //Invalido
            if (novo > sublimiterisco || novo < decexposicaoatual)
            {
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-close", simbolo);
                if(novo < decexposicaoatual)
                    awarning.InnerHtml = "Valor inferior à exposição atual";
                if(novo > sublimiterisco)
                    awarning.InnerHtml = "Valor superior ao sublimite risco";
            }
            //Valido - > Atualiza coluna de totais
            else
            {
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-check", simbolo);
                switch (tiporisco)
                {
                    case "F":
                        txtsublimiteriscoFinanceiroTotal.Text = atualizacaoSublimiteNovo_totaisporRisco("F").ToString();
                        break;
                    case "C":
                        txtsublimitriscoComercialTotal.Text = atualizacaoSublimiteNovo_totaisporRisco("C").ToString();
                        break;
                    case "A":
                        txtsublimiteriscoAssinaturaTotal.Text = atualizacaoSublimiteNovo_totaisporRisco("A").ToString();
                        break;
                }
            }

        }

        internal decimal atualizacaoSublimiteNovo_totaisporRisco(string tipoRisco)
        {
            decimal total = 0;
            foreach (var a in lvProdutosSimulacao.Items)
            {
                Label lb = a.FindControl("lbTipologiaRisco") as Label;

                if (lb.Text == tipoRisco)
                {
                    TextBox txt = a.FindControl("txtSublimiteComprometidoNovo") as TextBox;
                    decimal nTotal = 0;
                    Decimal.TryParse(txt.Text, out nTotal);
                    total += nTotal;
                }
            }

            return total;
        }

        internal bool validaLimites()
        {
            bool isValid = true;

            decimal NovoGlobal;
            Decimal.TryParse(txtlimiteglobalmultilinhaNovo.Text, out NovoGlobal);

            decimal NovoComercial;
            Decimal.TryParse(txtsublimiteriscoAssinaturaNovo.Text, out NovoComercial);

            decimal NovoAssinatura;
            Decimal.TryParse(txtsublimitriscoComercialNovo.Text, out NovoAssinatura);

            decimal NovoFinanceiro;
            Decimal.TryParse(txtsublimiteriscoFinanceiroNovo.Text, out NovoFinanceiro);


            decimal TotalGlobalTotal;
            Decimal.TryParse(txtlimiteglobalmultilinhaTotal.Text, out TotalGlobalTotal);

            decimal TotalComercialTotal;
            Decimal.TryParse(txtsublimiteriscoAssinaturaTotal.Text, out TotalComercialTotal);

            decimal TotalAssinaturaTotal;
            Decimal.TryParse(txtsublimitriscoComercialTotal.Text, out TotalAssinaturaTotal);

            decimal TotalFinanceiroTotal;
            Decimal.TryParse(txtsublimiteriscoFinanceiroTotal.Text, out TotalFinanceiroTotal);

            //Totais
            if(NovoGlobal == TotalGlobalTotal)
            {
             
                var simbolo = divSimulacaoSublimites.FindControl("lmGlobalNovo") as HtmlGenericControl;
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-check", simbolo);

                var awarning = this.FindControl("textlmGlobalNovo") as HtmlGenericControl;
                awarning.InnerHtml = "";
            }
            else
            {
                var simbolo = divSimulacaoSublimites.FindControl("lmGlobalNovo") as HtmlGenericControl;
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-close", simbolo);

                var awarning = divSimulacaoSublimites.FindControl("textlmGlobalNovo") as HtmlGenericControl;
                awarning.InnerHtml = "Valor diferente do Total";
                isValid = false;
            }

            //Assinatura
            if(TotalAssinaturaTotal <= NovoAssinatura)
            {
                var simbolo = divSimulacaoSublimites.FindControl("sbRassNovo") as HtmlGenericControl;
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-check", simbolo);

                var awarning = divSimulacaoSublimites.FindControl("textsbRassNovo") as HtmlGenericControl;
                awarning.InnerHtml = "";
            }
            else
            {
                var simbolo = divSimulacaoSublimites.FindControl("sbRassNovo") as HtmlGenericControl;
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-close", simbolo);

                var awarning = divSimulacaoSublimites.FindControl("textsbRassNovo") as HtmlGenericControl;
                awarning.InnerHtml = "Valor inferior ao Total";
                isValid = false;
            }

            //Comerciais
            if (TotalComercialTotal <= NovoComercial)
            {
                var simbolo = divSimulacaoSublimites.FindControl("sbRComNov") as HtmlGenericControl;
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-check", simbolo);

                var awarning = divSimulacaoSublimites.FindControl("txtsbRComNov") as HtmlGenericControl;
                awarning.InnerHtml = "";
            }
            else
            {
                var simbolo = divSimulacaoSublimites.FindControl("sbRComNov") as HtmlGenericControl;
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-close", simbolo);

                var awarning = divSimulacaoSublimites.FindControl("txtsbRComNov") as HtmlGenericControl;
                awarning.InnerHtml = "Valor inferior ao Total";

                isValid = false;
            }

            //Financeiros
            if (TotalFinanceiroTotal <= NovoFinanceiro)
            {
                var simbolo = divSimulacaoSublimites.FindControl("sbRfinNovo") as HtmlGenericControl;
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-check", simbolo);

                var awarning = divSimulacaoSublimites.FindControl("textsbRfinNovo") as HtmlGenericControl;
                awarning.InnerHtml = "";
            }
            else
            {
                var simbolo = divSimulacaoSublimites.FindControl("sbRfinNovo") as HtmlGenericControl;
                Helper.AddRemoveCssClass(true, "ui-icon ui-icon-close", simbolo);

                var awarning = divSimulacaoSublimites.FindControl("textsbRfinNovo") as HtmlGenericControl;
                awarning.InnerHtml = "Valor inferior ao Total";

                isValid = false;
            }

            return isValid;
        }

        internal bool validaSublimites()
        {
            bool isValid = true; ;
            foreach(var a in lvProdutosSimulacao.Items)
            {
                HtmlGenericControl awarning = a.FindControl("textsimulcaovalido") as HtmlGenericControl;
                HtmlGenericControl simbolo = a.FindControl("simulcaovalido") as HtmlGenericControl;
                if (!string.IsNullOrEmpty(awarning.InnerHtml))
                {
                    return false;
                }

                TextBox valorSub = a.FindControl("txtSublimiteComprometidoNovo") as TextBox;
                if(valorSub.Text == "0" || valorSub.Text == "0,0'" || valorSub.Text == "")
                {
                    Helper.AddRemoveCssClass(true, "ui-icon ui-icon-close", simbolo);
                 
                }
            }

            return isValid;
        }

        protected void btnConsultarProdutos_Click(object sender, EventArgs e)
        {
            //Redirecciona para LM36 com contexto
            string urlQueries = Request.Url.Query;
            string href = ConfigurationManager.AppSettings["ContratosProduto"] + urlQueries;

            LM37SimulacaoML lm37 = new LM37SimulacaoML();
            Helper.CopyPropertiesTo(this, lm37);

           Page.Transfer(href,
           new Dictionary<string, object>() {
                                  { "Op", "V" },
                                  { "ClienteLM37", lm37 },
           });

        }

        protected void btnGuardarSimulacao_Click(object sender, EventArgs e)
        {
            LM37_SimulacaoMl lm37 = new LM37_SimulacaoMl();
            Helper.CopyObjectToControls(this, lm37);

            //Call lm37 BCDWSProxy.LM37
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            MensagemOutput<LM37_SimulacaoMl> respOut = bl.LM37Request(lm37, abargs, "C", false);


            if (respOut != null && respOut.ResultResult != null)
            {
                lberror.Text = Constantes.Mensagens.LM37SimulacaoCriado;
                lberror.ForeColor = System.Drawing.Color.Green;
                lberror.Visible = true;

                Helper.SetEnableControler(this, false);
                txtidsimulacaoml.Text = respOut.ResultResult.idSimulacao;
            }
            else
            {
                lberror.Text = TAT2.GetMsgErroTATDescription(respOut.erro.ToString(), abargs);
                if (string.IsNullOrEmpty(lberror.Text))
                {
                    lberror.Text = respOut.mensagem;
                }
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnConsultarSm_Click(object sender, EventArgs e)
        {
            //Redirecciona para LM34 (Sublimites) para popular lm34 com os dados da simulação 
            ListViewDataItem smSelected = lvConsultaSimulacoes.Items.Where(x => (x.FindControl("cbSelected") as CheckBox).Checked).FirstOrDefault() as ListViewDataItem;
            LM37_SimulacaoMl lm37 = new LM37_SimulacaoMl();

            Helper.CopyPropertiesTo(smSelected, lm37.SimulacaoSublimites[0]);
            lm37.idmultilinha = lm37.SimulacaoSublimites[0].cons_idMultilinha;
            lm37.idSimulacao = lm37.SimulacaoSublimites[0].cons_idSimulacao;

            string urlQueries = Request.Url.Query;
            Page.Transfer(ConfigurationManager.AppSettings["SublimitesML"] + urlQueries,
            new Dictionary<string, object>() {
                                  { "Op", "M" },
                                  { "HSimulacao", lm37 },
            });
        }
    }
}