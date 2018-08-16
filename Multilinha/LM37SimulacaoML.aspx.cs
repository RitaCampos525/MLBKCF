﻿using MultilinhasObjects;
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
                        Helper.AddRemoveHidden(true, divSimular);

                        break;
                    case "V":
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atabD", "");
                        lblTransactionV.Enabled = true;
                        Helper.AddRemoveActive(true, liVisualizacao);
                        Helper.AddRemoveActive(false, liCriacao);
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atab", "atabD");

                        Helper.AddRemoveHidden(true, divProduto);
                        Helper.AddRemoveHidden(true, divSimulacaoSublimites);
                        Helper.AddRemoveHidden(true, divSimular);
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

            Helper.SetEnableControler(camposChaveSim, false);
            Helper.AddRemoveHidden(false, divProduto);
            Helper.SetEnableControler(divProduto, false);
            Helper.AddRemoveHidden(false, divSimulacaoSublimites);
            Helper.AddRemoveHidden(false, divSimular);

            LM37_SimulacaoMl sim =  TAT2.SearchML37(0001004, "510092588522");
            Helper.CopyObjectToControls(this, sim);

            string op = Helper.getTransactionMode(Context, Request);
            switch (op.ToUpper())
            {
                case "C":
                    if(rdtipoSimulacao.SelectedValue == "T2")
                    {
                        txtlimiteglobalmultilinhaNovo.Enabled = true;
                        txtsublimiteriscoAssinaturaNovo.Enabled = true;
                        txtsublimitriscoComercialNovo.Enabled = true;
                        txtsublimiteriscoAssinaturaNovo.Enabled = true;
                    }

                    bindlistviewsimulacao(sim , lvProdutosSimulacao);
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
            //Apenas 1 seleccionado
            string urlQueries = Request.Url.Query;

            //If alt. condicao geral -> redireciona para lm33
            string href = ConfigurationManager.AppSettings["ContratoML"] + urlQueries;
            //else -> redirecciona para lm34

            LM37SimulacaoML lm37 = new LM37SimulacaoML();
            Helper.CopyPropertiesTo(this, lm37);

            Page.Transfer(href,
            new Dictionary<string, object>() {
                                  { "Op", "V" },
                                  { "ClienteLM37", lm37 },
            });
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
                        txtsublimiteriscoFinanceiroTotal.Text = atualizacaoSublimiteNovo("F").ToString();
                        break;
                    case "C":
                        txtsublimitriscoComercialTotal.Text = atualizacaoSublimiteNovo("C").ToString();
                        break;
                    case "A":
                        txtsublimiteriscoAssinaturaTotal.Text = atualizacaoSublimiteNovo("A").ToString();
                        break;
                }
            }

        }

        internal decimal atualizacaoSublimiteNovo(string tipoRisco)
        {
            decimal total = 0;
            foreach (var a in lvProdutosSimulacao.Items)
            {
                Label lb = a.FindControl("lbTipologiaRisco") as Label;

                if (lb.Text == tipoRisco)
                {
                    TextBox txt = a.FindControl("lbSublimiteComprometidoNovo") as TextBox;
                    decimal nTotal = 0;
                    Decimal.TryParse(txt.Text, out nTotal);
                    total += nTotal;
                }
            }

            return total;
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

        }
    }
}