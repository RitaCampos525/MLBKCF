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
    public partial class LM34DefinicaoSublimites : System.Web.UI.Page
    {
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

                //get context of operation (C,V,M)
                string op = Helper.getTransactionMode(Context, Request);
                ViewState["Op"] = op;

                switch (op.ToUpper())
                {
                    case "M":
                        Helper.AddRemoveActive(true, liModificacao);
                        lblTransactionM.CssClass = lblTransactionM.CssClass.Replace("atab", "atabD");
                        lblTransactionM.Enabled = true;

                        Helper.SetEnableControler(camposChaveSubLim, true);
                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, accoesfinais_criarlm24);
                        Helper.AddRemoveHidden(true, hr1);
                        Helper.AddRemoveHidden(true, hr2);

                        break;
                    case "C":
                        Helper.AddRemoveActive(true, liCriacao);
                        lblTransaction.CssClass = lblTransaction.CssClass.Replace("atab", "atabD");
                        lblTransaction.Enabled = true;

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, accoesfinais_criarlm24);
                        Helper.AddRemoveHidden(true, hr1);
                        Helper.AddRemoveHidden(true, hr2);


                        break;
                    case "A":
                        break;
                    case "V":
                        Helper.AddRemoveActive(true, liVisualizacao);
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atab", "atabD");
                        lblTransactionV.Enabled = true;

                        Helper.SetEnableControler(camposChaveSubLim, true);
                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, accoesfinais_criarlm24);
                        Helper.AddRemoveHidden(true, hr1);
                        Helper.AddRemoveHidden(true, hr2);

                        //Contexto Visualização
                        LM38_HistoricoAlteracoes lm38 = Context.Items["HAlteracao"] as LM38_HistoricoAlteracoes;
                        if (lm38 != null && lm38.idmultilinha != null)
                        {
                            Helper.CopyObjectToControls(this, lm38);
                            Control ctr = this.FindControl(Helper.getControltoHighLight(lm38.HistoricoAlteracoes[0].Alteracao));
                            Helper.AddHightLight(ctr, true);

                        }

                        break;
                    default:
                        Page.Transfer(ConfigurationManager.AppSettings["ContratoML"] + "?Op=C", //Sem contexto redireciona para lm33 - modo criar C
                        new Dictionary<string, object>() {
                                 { "Op", "C" } });
                        break;
                }
                //get contract on context from lm33
                if (Context.Items["ContratoCriado"] is LM34_SublimitesML)
                {
                    LM34_SublimitesML lm34c = Context.Items["ContratoCriado"] as LM34_SublimitesML;
                    Helper.CopyObjectToControls(ml04_criar, lm34c);
                    ViewState["ContratoCriado"] = lm34c;
                }
            }
        }

        protected void btnSearchCont_Click1(object sender, EventArgs e)
        {
            //CALL LM34 para obter familia produtos introduzida da LM33
            string op = ViewState["Op"] as string;
            switch (op.ToUpper())
            {
                case "C":

                    Helper.SetEnableControler(camposChaveSubLim, false);
                    Helper.AddRemoveHidden(false, dpOK);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                    Helper.AddRemoveHidden(false, accoesfinais_criarlm24);
                    btnCriar.Visible = true;
                    Helper.AddRemoveHidden(false, hr1);
                    Helper.AddRemoveHidden(false, hr2);

                    //For debug - lm34
                    int client = 0;
                    Int32.TryParse(txtCliente.Text, out client);
                    LM34_SublimitesML lm34 = TAT2.SearchML04(client, txtidmultilinha.Text, "0");
                    
                    listViewFamProdutosESubLim(Constantes.tipologiaRisco.RF, lvProdutosRiscoF, lm34);
                    listViewFamProdutosESubLim(Constantes.tipologiaRisco.RC, lvProdutosRiscoC, lm34);
                    listViewFamProdutosESubLim(Constantes.tipologiaRisco.RA, lvProdutosRiscoA, lm34);

                    break;

                case "M":

                    Helper.SetEnableControler(camposChaveSubLim, false);
                    Helper.AddRemoveHidden(false, dpOK);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                    Helper.AddRemoveHidden(false, accoesfinais_criarlm24);
                    btnModificar.Visible = true;
                    Helper.AddRemoveHidden(false, hr1);
                    Helper.AddRemoveHidden(false, hr2);

                    //For debug - lm34
                    Int32.TryParse(txtCliente.Text, out client);
                    lm34 = TAT2.SearchML04(client, txtidmultilinha.Text, "0");

                    Helper.CopyObjectToControls(ml04_criar, lm34);

                    listViewFamProdutosESubLim(Constantes.tipologiaRisco.RF, lvProdutosRiscoF, lm34);
                    listViewFamProdutosESubLim(Constantes.tipologiaRisco.RC, lvProdutosRiscoC, lm34);
                    listViewFamProdutosESubLim(Constantes.tipologiaRisco.RA, lvProdutosRiscoA, lm34);

                    break;

                case "V":

                    Helper.SetEnableControler(dpOK, false);
                    Helper.AddRemoveHidden(false, dpOK);
                    Helper.SetEnableControler(dvtitleAcordionRFinanceiro, false);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                    Helper.SetEnableControler(dvtitleAcordionRAssinatura, false);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                    Helper.SetEnableControler(dvtitleAcordionRComercial, false);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                    Helper.SetEnableControler(lvProdutosRiscoA, false);
                    Helper.SetEnableControler(lvProdutosRiscoC, false);
                    Helper.SetEnableControler(lvProdutosRiscoF, false);
                    Helper.SetEnableControler(accoesfinais_criarlm24, true);
                    Helper.AddRemoveHidden(false, accoesfinais_criarlm24);
                    Helper.AddRemoveHidden(false, hr1);
                    Helper.AddRemoveHidden(false, hr2);

                    //For debug - lm34
                    Int32.TryParse(txtCliente.Text, out client);
                     lm34 = TAT2.SearchML04(client, txtidmultilinha.Text, "0");

                    listViewFamProdutosESubLim(Constantes.tipologiaRisco.RF, lvProdutosRiscoF, lm34);
                    listViewFamProdutosESubLim(Constantes.tipologiaRisco.RC, lvProdutosRiscoC, lm34);
                    listViewFamProdutosESubLim(Constantes.tipologiaRisco.RA, lvProdutosRiscoA, lm34);

                    break;
            }
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (valSomaLimites())
                {
                    //Cal LM34

                    lberror.Text = Constantes.Mensagens.LM34SublimiteCriado;
                    lberror.Visible = true;
                    lberror.ForeColor = System.Drawing.Color.Green;

                    Helper.SetEnableControler(this, false);

                    btnSeguinte.Enabled = true;
                }
            }
        }

        protected void btnSeguinte_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                LM34_SublimitesML lm34 = new LM34_SublimitesML();
                Helper.CopyPropertiesTo(ml04_criar, lm34);

                string urlQueries = Request.Url.Query;
                string op = ViewState["Op"] as string;
                Page.Transfer(ConfigurationManager.AppSettings["AssociacaoContasDO"] + urlQueries,
               new Dictionary<string, object>() {
                                  { "Op", op },
                                  { "ContratoCriado", lm34 },
               });
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }

        protected void listViewFamProdutosESubLim(string tipologia, ListView lst, LM34_SublimitesML LM34)
        {
            List<LM34_SublimitesML.ProdutosRisco> _lst = new List<LM34_SublimitesML.ProdutosRisco>();

            //Selecionar familia produtos
            IEnumerable<string> familiaprodutos = Enumerable.Empty<string>();
            if (tipologia == Constantes.tipologiaRisco.RF)
            {
                familiaprodutos = LM34.produtosRiscoF.Select(x => x.familiaproduto).Distinct();
            }

            if(tipologia == Constantes.tipologiaRisco.RA)
            {
                familiaprodutos = LM34.ProdutosRiscoAssinatura.Select(x => x.familiaproduto).Distinct();
            }

            if(tipologia == Constantes.tipologiaRisco.RC)
            {
                familiaprodutos = LM34.produtosRiscoC.Select(x => x.familiaproduto).Distinct();
            }

            //Adicionar item à lista
            foreach (var row in familiaprodutos)
            {
                LM34_SublimitesML.ProdutosRisco item = new LM34_SublimitesML.ProdutosRisco();

                item.familiaproduto = row;
                item.codfamiliaproduto = ArvoreFamiliaProdutos.SearchFamiliaProduto(tipologia).First(x => x.familiaProduto == row).codfamiliaProduto;
                _lst.Add(item);

            }

            lst.DataSource = _lst;
            lst.DataBind();
        }

        protected bool valSomaLimites()
        {
            bool valid = true;
            lberror.Text = "";

            //Limite Global contrato
            decimal limGl = Convert.ToDecimal(txtlimiteglobalmultilinha.Text);

            //Sublimites contratados
            decimal sublimA = Convert.ToDecimal(txtsublimiteriscoAssinatura.Text);
            decimal sublimF = Convert.ToDecimal(txtsublimiteriscoFinanceiro.Text);
            decimal sublimC = Convert.ToDecimal(txtsublimitriscoComercial.Text);

            //sublimites inseridos - Assinatura
            decimal SumlAs = 0;
            foreach(var tr in lvProdutosRiscoA.Items)
            {
                string sublimaAComp = (tr.FindControl("lbsublimiteComprometido") as TextBox).Text;
                decimal _sublimAComp = Convert.ToDecimal(sublimaAComp);

                SumlAs += _sublimAComp;
            }
            if (SumlAs > sublimA)
            {
                lberror.Text = Constantes.Mensagens.SomaSublimitesComprometidosAssinaturaInvalida;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
                return valid = false;
            }

                //sublimites inseridos - Comercial
                decimal SumCs = 0;
            foreach (var tr in lvProdutosRiscoC.Items)
            {
                string sublimaCComp = (tr.FindControl("lbsublimiteComprometido") as TextBox).Text;
                decimal _sublimCComp = Convert.ToDecimal(sublimaCComp);

                SumCs += _sublimCComp;
            }
            if (SumCs > sublimC)
            {
                lberror.Text = Constantes.Mensagens.SomaSublimitesComprometidosComercialInvalida;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
                return valid = false;
            }

            //sublimites inseridos - Financeiro
            decimal SumFs = 0;
            foreach (var tr in lvProdutosRiscoF.Items)
            {
                string sublimaFComp = (tr.FindControl("lbsublimiteComprometido") as TextBox).Text;
                decimal _sublimaFComp = Convert.ToDecimal(sublimaFComp);

                SumFs += _sublimaFComp;
            }
            if (SumFs > sublimF)
            {
                lberror.Text = Constantes.Mensagens.SomaSublimitesComprometidosFinanceiroInvalida;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
                return valid = false;
            }

            //Soma de sublimites deve ser igual ao Limite Global
            if(SumCs + SumFs + SumlAs != limGl)
            {
                lberror.Text = Constantes.Mensagens.SomaTotalSublimitesComprometidosInvalida;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
                return valid = false;
            }

            return valid;

        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                reqidmultilinha.Enabled = false;
            }
            else
                reqidmultilinha.Enabled = true;
        }

        protected void txtIdworkflow_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txtidmultilinha.Text))
            {
                reqCliente.Enabled = false;
            }
            else
            {
                reqCliente.Enabled = true;

            }
        }


    }
}