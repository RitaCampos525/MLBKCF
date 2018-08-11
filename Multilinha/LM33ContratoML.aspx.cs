using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using MultilinhasDataLayer.BCDWSProxy;
using System.Globalization;

namespace Multilinha
{
    public partial class LM33ContratoMLaspx : System.Web.UI.Page
    {

        public DateTime dtfechas = Global.dtfechasG;
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();
        MultilinhaBusinessLayer.BLMultilinha bl = new MultilinhaBusinessLayer.BLMultilinha();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

                txtProdutoml.Text = ConfigurationManager.AppSettings["CodigoProdutoML"];
                txtproduto_TextChanged(sender, e);

                linqsComissoes();

                //dropdownlists
                ddlIndRenovacao.DataSource = ML_Objectos.GetIndicadorRenovacao();
                ddlIndRenovacao.DataBind();
                ddlPeriocidadeCobrancagestcontrato.DataSource = ML_Objectos.GetPeriocidade();
                ddlPeriocidadeCobrancagestcontrato.DataBind();
                ddlPeriocidadeCobrancagestRenovacao.DataSource = ML_Objectos.GetPeriocidade();
                ddlPeriocidadeCobrancagestRenovacao.DataBind();
                //DataProcessamento
                //txtdataProcessamento.Text = dtfechas.ToString();

                string op = Request.QueryString["OP"] ?? "FF";
                ViewState["OPLM33"] = op;
                switch (op.ToUpper())
                {
                    case "M":
                        lblTransactionM.CssClass = lblTransactionM.CssClass.Replace("atabD", "");
                        lblTransactionM.Enabled = true;

                        divIDMultilinha.Visible = true;
                        divIDSimulacao.Visible = true;
                        divProduto.Visible = false;

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, dvtitleComissoes);
                        Helper.AddRemoveHidden(true, hr3);
                        Helper.AddRemoveHidden(true, hr4);
                        Helper.AddRemoveHidden(true, divVersoesML);

                        //show fields
                        btnSimulacao.Enabled = true;
                        txtIdSimulacao.Enabled = true;
                        btnSimulacao.Visible = true;

                        Helper.AddRemoveHidden(true, accoesfinais_criarml03);
                        btnModificar.Visible = true;

                        //tabs navegacao
                        Helper.AddRemoveActive(true, liModificacao);
                        Helper.AddRemoveActive(false, liParameterizacao);
                        Helper.AddRemoveActive(false, liConsulta);
                        lblTransactionM.CssClass = lblTransactionM.CssClass.Replace("atab", "atabD");

                        break;
                    case "C":
                        lblTransaction.CssClass = lblTransaction.CssClass.Replace("atabD", "");
                        lblTransaction.Enabled = true;

                        divIDSimulacao.Visible = false;
                        divIDMultilinha.Visible = false;
                        divProduto.Visible = true;
                    

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, dvtitleComissoes);
                        Helper.AddRemoveHidden(true, accoesfinais_criarml03);
                        Helper.AddRemoveHidden(true, hr3);
                        Helper.AddRemoveHidden(true, hr4);
                        Helper.AddRemoveHidden(true, divVersoesML);

                        btnCriar.Visible = true;

                        Helper.AddRemoveActive(true, liParameterizacao);
                        Helper.AddRemoveActive(false, liModificacao);
                        Helper.AddRemoveActive(false, liConsulta);
                        lblTransaction.CssClass = lblTransaction.CssClass.Replace("atab", "atabD");
                        
                        break;
                    case "A":
                        lblTransactionD.Enabled = true;
                        //lblTransactionE.Enabled = true;

                        Helper.AddRemoveHidden(true, MC33C); //hide controls criar
                        Helper.AddRemoveHidden(false, ml03V_denuncia); //Show controls visualizar
                        ml03V_denuncia.Visible = true;

                        Helper.AddRemoveActive(true, liDenuncia);
                        Helper.AddRemoveActive(false, liParameterizacao);
                        Helper.AddRemoveActive(false, liModificacao);
                        Helper.AddRemoveActive(false, liConsulta);
                        lblTransactionD.CssClass = lblTransactionD.CssClass.Replace("atab", "atabD");

                        break;
                    case "V":
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atabD", "");
                        lblTransactionV.Enabled = true;

                        divIDMultilinha.Visible = true;
                        divIDSimulacao.Visible = false;
                        divProduto.Visible = false;

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, dvtitleComissoes);
                        Helper.AddRemoveHidden(true, accoesfinais_criarml03);
                        Helper.AddRemoveHidden(true, hr3);
                        Helper.AddRemoveHidden(true, hr4);
                        Helper.AddRemoveHidden(true, divVersoesML);

                        Helper.AddRemoveActive(true, liConsulta);
                        Helper.AddRemoveActive(false, liModificacao);
                        Helper.AddRemoveActive(false, liParameterizacao);
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atab", "atabD");

                        break;
                    default:
                        lberror.Text = "Página sem contexto. Execute a transação na Aplicação Bancária";
                        lberror.Visible = true;

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, dvtitleComissoes);
                        Helper.AddRemoveHidden(true, accoesfinais_criarml03);
                        Helper.AddRemoveHidden(true, hr3);
                        Helper.AddRemoveHidden(true, hr4);
                        btnSearch.Enabled = false;
                        break;
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }

        protected void btnSearchDO_Click(object sender, EventArgs e)
        {
            lberror.Text = "";
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;

            string op = Request.QueryString["OP"] ?? "FF";
            switch (op.ToUpper())
            {
                case "C":
               
                    Helper.SetEnableControler(camposChave, false);
                    Helper.AddRemoveHidden(false, dpOK);

                    //Chamada CL55 para Get DOs Cliente
                    int ncliente;
                    Int32.TryParse(txtCliente.Text, out ncliente);
                    MensagemOutput<List<string>> trans = bl.CL55Request(ncliente, abargs);

                    if (trans.mensagem != null)
                    {
                        lberror.Text = trans.mensagem;
                        lberror.Visible = true;
                        lberror.ForeColor = System.Drawing.Color.Red;
                    }

                    //BIND DROPDDOWNLIST Contas DO
                    ddlncontado.DataSource = bl.CL55Request(ncliente, abargs).ResultResult.AsEnumerable();
                    ddlncontado.DataBind();

                    //Chamada ao Catalogo!
                    LM31_CatalogoProdutoML res = TAT2.SearchLM31("01", 01);
                    Helper.CopyObjectToControls(this, res);

                    ViewState["LM31"] = res;

                    break;

                case "V":
                    //Call ML03 para preencher o ecra com os dados
                    Int32.TryParse(txtCliente.Text, out ncliente);
                    LM33_ContratoML M03V = TAT2.SearchML03(ncliente, txtIdworkflow.Text);

                    Helper.CopyObjectToControls(this.Page, M03V);

                    Helper.SetEnableControler(camposChave, true);
                    //btnLimpar_Click(sender, e); 
                    Helper.AddRemoveHidden(false, dpOK);
                    Helper.SetEnableControler(dpOK, false);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                    Helper.SetEnableControler(divRiscoFinanceiro, false);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                    Helper.SetEnableControler(divRiscoAssinatura, false);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                    Helper.SetEnableControler(divRiscoComercial, false);
                    Helper.AddRemoveHidden(false, dvtitleComissoes);
                    Helper.SetEnableControler(divComissoes, false);
                    Helper.SetEnableControler(divVersoesML, false);
                    Helper.AddRemoveHidden(false, divVersoesML);
                    Helper.AddRemoveHidden(false, hr4);
                    //show fields acoes
                    Helper.AddRemoveHidden(false, accoesfinais_criarml03);
                    Helper.SetEnableControler(accoesfinais_criarml03, true);

                    #region tabelas de produtos de riscos

                    //Get Produtos
                    // e Popular CG

                    List<ArvoreFamiliaProdutos> lstF = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF);
                    listViewProdutos(lstF, Constantes.tipologiaRisco.RF, lvProdutosRisco, M03V, false);

                    List<ArvoreFamiliaProdutos> lstC = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RC);
                    listViewProdutos(lstC, Constantes.tipologiaRisco.RC, lvProdutosRiscoComercial, M03V, false);

                    List<ArvoreFamiliaProdutos> lstA = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA);
                    listViewProdutos(lstA, Constantes.tipologiaRisco.RA, lvProdutosRiscoAssinatura, M03V, false);

                    #endregion
                    break;

                case "M":
                    //CALL ML03
                    Int32.TryParse(txtCliente.Text, out ncliente);
                    LM33_ContratoML M03M = TAT2.SearchML03(ncliente, txtIdworkflow.Text);

                    Helper.CopyObjectToControls(this.Page, M03M);

                    Helper.SetEnableControler(camposChave, false);
                    Helper.AddRemoveHidden(false, dpOK);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                    Helper.AddRemoveHidden(false, dvtitleComissoes);
                    Helper.AddRemoveHidden(false, accoesfinais_criarml03);
                    Helper.AddRemoveHidden(false, hr3);
                    Helper.AddRemoveHidden(false, hr4);
                    Helper.AddRemoveHidden(false, divVersoesML);
                    btnConfirmar.Enabled = false;


                    #region tabelas de produtos de riscos

                    //Get Produtos
                    // e Popula CG e CP . Quando seleccionado ficam enable! Não é possivel deseleccionar
                    lstF = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF);
                    listViewProdutos(lstF, Constantes.tipologiaRisco.RF, lvProdutosRisco, M03M, false);

                    lstC = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RC);
                    listViewProdutos(lstC, Constantes.tipologiaRisco.RC, lvProdutosRiscoComercial, M03M, false);

                    lstA = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA);
                    listViewProdutos(lstA, Constantes.tipologiaRisco.RA, lvProdutosRiscoAssinatura, M03M, false);

                    #endregion
                    
                    break;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(lberrorSRA.Text) || !string.IsNullOrEmpty(lberrorSRC.Text) || !string.IsNullOrEmpty(lberrorSRF.Text)
            {
                return;
            }

            string op = Request.QueryString["OP"] ?? "FF";
            switch (op.ToUpper())
            {
                case "C":
                    Helper.SetEnableControler(camposChave, false);
                    Helper.AddRemoveHidden(false, dpOK);
                    Helper.SetEnableControler(dpOK, true);

                    Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                    Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                    Helper.AddRemoveHidden(false, dvtitleComissoes);
                    Helper.AddRemoveHidden(false, accoesfinais_criarml03);
                    Helper.AddRemoveHidden(false, hr3);
                    Helper.AddRemoveHidden(false, hr4);
                    Helper.AddRemoveHidden(false, divVersoesML);


                    //Call LM31 (viewState) para obtencao de produtos do produto ML introduzido! E seleccionar CP correspondentes
                    LM31_CatalogoProdutoML LM31 = ViewState["LM31"] as LM31_CatalogoProdutoML;

                    #region tabelas de produtos de riscos

                    //Get Produtos

                    List<ArvoreFamiliaProdutos> lstF = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF);
                    listViewProdutos(lstF, Constantes.tipologiaRisco.RF, lvProdutosRisco, LM31, true);

                    List<ArvoreFamiliaProdutos> lstC = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RC);
                    listViewProdutos(lstC, Constantes.tipologiaRisco.RC, lvProdutosRiscoComercial, LM31, true);

                    List<ArvoreFamiliaProdutos> lstA = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA);
                    listViewProdutos(lstA, Constantes.tipologiaRisco.RA, lvProdutosRiscoAssinatura, LM31, true);

                    #endregion

                    //Call LM33 para obtencao do valor das comissoes do produto ML introduzido!
                    LM33_ContratoML LM33 = TAT2.SearchML03(1004, "");
                    Helper.CopyObjectToControls(this.MC33C, LM33);


                    break;
            }
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                //Validações
                //1º: Verificar que o nº minimo de produtos está selecionado
                int nMinimoProdutosAtivar;
                Int32.TryParse(txtNumeroMinimoProdutos.Text, out nMinimoProdutosAtivar);
                bool val = validacaoCP(nMinimoProdutosAtivar);
                if(!val)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "scroll", "ToTopOfPage();", true);
                    return;
                }

                //1º Verificar Valor Sublimites
                bool val1 = validacaoSublimitesRisco();
                if(!val1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "scroll", "ToTopOfPage();", true);
                    return;
                }

                bool val2 = true; //validacaoDtProximaCobrabca();
                if (val && val1 && val2)
                {
                    //Call LM33 - C
                    LM33_ContratoML _LM33 = new LM33_ContratoML();
                    Helper.CopyPropertiesTo(MC33C, _LM33);

                    getSublimites(_LM33);

                    ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                    MensagemOutput<LM33_ContratoML> response = bl.LM33Request(_LM33, abargs, "C");

                    if (response.ResultResult != null && response.ResultResult.Cliente != null)
                    {
                        lberror.Text = Constantes.Mensagens.LM33ContratoCriado;
                        lberror.Visible = true;
                        lberror.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lberror.Text = TAT2.GetMsgErroTATDescription(response.mensagem, abargs);
                        lberror.Visible = true;
                        lberror.ForeColor = System.Drawing.Color.Red;
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "scroll", "ToTopOfPage();", true);
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
                Helper.CopyPropertiesTo(MC33C, lm34);

                //zona produtos
                //adicaoCP(Constantes.tipologiaRisco.RF, lvProdutosRisco, lm34);
                //adicaoCP(Constantes.tipologiaRisco.RA, lvProdutosRiscoAssinatura, lm34);
                //adicaoCP(Constantes.tipologiaRisco.RC, lvProdutosRiscoComercial, lm34);

                string op = ViewState["OPLM33"] as string;
                Page.Transfer(ConfigurationManager.AppSettings["DefinicaoSublimites"],
               new Dictionary<string, object>() {
                                  { "Op", op },
                                  { "ContratoCriado", lm34 },
               });

                //Server.Transfer(ConfigurationManager.AppSettings["DefinicaoSublimites"]);
            }
        }

        protected void btnSimulacao_Click(object sender, EventArgs e)
        {

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //Include id multilinha

            lberror.Text = Constantes.Mensagens.LM33ContratoModificado;
            lberror.ForeColor = System.Drawing.Color.Green;
            lberror.Visible = true;

            Helper.SetEnableControler(this, false);
            btnSeguinte.Enabled = true;
        }

        protected void txtPeriocidadeCobranca_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdatafimcontrato.Text))
            {
                lberror.Text = "Data fim de contrato inválida";
                lberror.Visible = true;
                return;
            }

            int meses = Convert.ToInt32(txtdataproximacobrancagestcontrato.Text);
            txtdataproximacobrancagestcontrato.Text = Convert.ToDateTime(txtdatafimcontrato.Text).AddMonths(meses).ToString("yyyy-MM-dd");

        }

        protected void listViewProdutos(List<ArvoreFamiliaProdutos> lstF, string tipologia, ListView lst, LM31_CatalogoProdutoML LM31, bool mudaCG)
        {
            List<itemTreeProduto> _lst = new List<itemTreeProduto>();

            //Selecionar familia produtos
            IEnumerable<string> familiaprodutos = Enumerable.Empty<string>();
            if (tipologia == Constantes.tipologiaRisco.RF)
            {
                if (LM31.produtosF != null && LM31.produtosF.Count > 0)
                {
                    familiaprodutos = LM31.produtosF.Select(x => x.familia).Distinct();
                }
            }
            if (tipologia == Constantes.tipologiaRisco.RA)
            {
                if (LM31.produtosA != null && LM31.produtosA.Count > 0)
                {
                    familiaprodutos = LM31.produtosA.Select(x => x.familia).Distinct();
                }
            }
            if (tipologia == Constantes.tipologiaRisco.RC)
            {
                if (LM31.produtosC != null && LM31.produtosC.Count > 0)
                {
                    familiaprodutos = LM31.produtosC.Select(x => x.familia).Distinct();
                }
            }
            //Adicionar item à lista
            foreach (var row in familiaprodutos)
            {
                itemTreeProduto item = new itemTreeProduto();

                item.produto = row;
                //Selecciona
                item.isGeral = true;
                item.cGEnable = mudaCG;
                item.cPEnable = false; //fecha seleccao as condicoes particulares

                _lst.Add(item);

                //Procura subprodutos da familia
                var dtSubProdutos = LM31.produtosA.FindAll(x => x.familia == row);
                if(dtSubProdutos.Count < 1)
                {
                   dtSubProdutos = LM31.produtosF.FindAll(x => x.familia == row);
                }
                if (dtSubProdutos.Count < 1)
                {
                    dtSubProdutos = LM31.produtosC.FindAll(x => x.familia == row);
                }

                //nivel 2
                for (int i = 0; i < dtSubProdutos.Count; i++)
                {
                    itemTreeProduto subitem = new itemTreeProduto();

                    subitem.subproduto = dtSubProdutos[i].produto.ToString() + dtSubProdutos[i].subproduto.ToString() +
                        " - " + dtSubProdutos[i].descritivo.ToString(); //(codigo + descritivo)

                    subitem.isParticular = false;
                    subitem.isGeral = false;

                    subitem.cGEnable = false;  //fecha seleccao as condicoes gerais
                    subitem.cPEnable = true;  //abre seleccao as condicoes particulares

                    _lst.Add(subitem);
                }
            }

            lst.DataSource = _lst;
            lst.DataBind();
        }

        protected void listViewProdutos(List<ArvoreFamiliaProdutos> lstF, string tipologia, ListView lst, LM33_ContratoML LM33, bool mudaCG)
        {
            List<itemTreeProduto> _lst = new List<itemTreeProduto>();

            //Selecionar familia produtos
            IEnumerable<string> familiaprodutos = Enumerable.Empty<string>();
            if (tipologia == Constantes.tipologiaRisco.RF)
            {
                familiaprodutos = LM33.produtosRiscoF.Select(x => x.familiaproduto).Distinct();
            }
            if (tipologia == Constantes.tipologiaRisco.RA)
            {
                familiaprodutos = LM33.ProdutosRiscoAssinatura.Select(x => x.familiaproduto).Distinct();
            }
            if (tipologia == Constantes.tipologiaRisco.RC)
            {
                familiaprodutos = LM33.produtosRiscoC.Select(x => x.familiaproduto).Distinct();
            }
            //Adicionar item à lista
            foreach (var row in familiaprodutos)
            {
                itemTreeProduto item = new itemTreeProduto();

                item.produto = row;
                //Selecciona
                item.isGeral = true;
                item.cGEnable = mudaCG;
                item.cPEnable = false; //fecha seleccao as condicoes particulares

                _lst.Add(item);

                //Procura subprodutos da familia
                var dtSubProdutos = lstF.FindAll(x => x.familiaProduto == row);

                //nivel 2
                for (int i = 0; i < dtSubProdutos.Count; i++)
                {
                    itemTreeProduto subitem = new itemTreeProduto();

                    subitem.subproduto = dtSubProdutos[i].produto.ToString() + dtSubProdutos[i].subproduto.ToString() +
                        " - " + dtSubProdutos[i].descricao.ToString(); //(codigo + descritivo)

                    subitem.isParticular = false;
                    subitem.isGeral = false;

                    subitem.cGEnable = false;  //fecha seleccao as condicoes gerais
                    subitem.cPEnable = true;  //abre seleccao as condicoes particulares

                    _lst.Add(subitem);
                }
            }

            lst.DataSource = _lst;
            lst.DataBind();
        }

        public class itemTreeProduto
        {
            public string produto { get; set; }
            public string subproduto { get; set; }
            public bool isParticular
            {
                get; set;
            }
            public bool isGeral
            {
                get; set;
            }
            public bool cGEnable
            {
                get; set;
            }
            public bool cPEnable
            {
                get; set;
            }

        }

        internal bool validacaoCP(int nMinimoProdutosAtivar)
        {
            lberror.Text = "";
            bool val = false;
            //Valida se Produtos estão selecionados
            int countSel = 0;
            foreach (ListViewDataItem tr in lvProdutosRisco.Items)
            {
                CheckBox ch = tr.FindControl("lbCParticular") as CheckBox;
                if (ch.Checked)
                {
                    countSel++;
                }
            }
            int countSel2 = 0;
            foreach (ListViewDataItem tr in lvProdutosRiscoAssinatura.Items)
            {
                CheckBox ch = tr.FindControl("lbCParticular") as CheckBox;
                if (ch.Checked)
                {
                    countSel++;
                }
            }
            int countSel3 = 0;
            foreach (ListViewDataItem tr in lvProdutosRiscoComercial.Items)
            {
                CheckBox ch = tr.FindControl("lbCParticular") as CheckBox;
                if (ch.Checked)
                {
                    countSel++;
                }
            }


            if ((countSel + countSel2 + countSel3) < nMinimoProdutosAtivar)
            {
                string[] args = { nMinimoProdutosAtivar.ToString() };
                lberror.Text = Constantes.NovaMensagem(Constantes.Mensagens.NMinimoProdutosML_CP, args);
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
                val = false;
            }
            else
                val = true;

            return val;

        }

        protected void adicaoCP(string tipologia, ListView lview, LM34_SublimitesML LM34)
        {
            lberror.Text = "";

            //Valida se Produtos estão selecionados 
            foreach (var tr in lview.Items)
            {
                CheckBox ch = tr.FindControl("lbCParticular") as CheckBox;
                if (ch.Checked)
                {
                    if (tipologia == Constantes.tipologiaRisco.RF)
                    {
                        LM34.produtosRiscoF.Add(
                            new LM34_SublimitesML.ProdutosRisco
                            {
                                familiaproduto = (tr.FindControl("lbProduto") as Label).Text,
                                prodsubproduto = (tr.FindControl("lbSubproduto") as Label).Text,
                                tipologia = "F"
                            }
                        );
                    }

                    if (tipologia == Constantes.tipologiaRisco.RA)
                    {
                        LM34.ProdutosRiscoAssinatura.Add(
                            new LM34_SublimitesML.ProdutosRisco
                            {
                                familiaproduto = (tr.FindControl("lbProduto") as Label).Text,
                                prodsubproduto = (tr.FindControl("lbSubproduto") as Label).Text,
                                tipologia = "A"
                            });
                    }

                    if (tipologia == Constantes.tipologiaRisco.RC)
                    {
                        LM34.produtosRiscoC.Add(
                            new LM34_SublimitesML.ProdutosRisco
                            {
                                familiaproduto = (tr.FindControl("lbProduto") as Label).Text,
                                prodsubproduto = (tr.FindControl("lbSubproduto") as Label).Text,
                                tipologia = "C"
                            });
                    };
                }
            }
        }

        internal bool validacaoDtProximaCobranca()
        {
            bool dtval = (!string.IsNullOrEmpty(txtdataproximacobrancagestcontrato.Text) && !txtdataproximacobrancagestcontrato.Text.Equals("9999-12-31"));
            lberror.Text = "Data fim de contrato inválida";
            lberror.Visible = true;

            return dtval;
        }

        protected void ddlSubProdCode_TextChanged(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            string subprodutodesc = TAT2.GetSubProdDescriptionByCode(txtProdutoml.Text, ddlSubprodutoml.SelectedValue, Global.ConnectionStringMaster, abargs);

            txtDescritivo.Text = subprodutodesc;

            //for debug!
            if (string.IsNullOrEmpty(subprodutodesc))
            {

                txtDescritivo.Text = TAT2.SearchSubProdutDescriptionML(ddlSubprodutoml.SelectedValue)[0].ToString();
            }
        }

        protected void txtproduto_TextChanged(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            List<string> lstsubprodutos = TAT2.GetSubProdByProdCode(txtProdutoml.Text, Global.ConnectionStringMaster, abargs);

            //for debug!!
            if (lstsubprodutos.Count < 1)
            {
                lstsubprodutos = TAT2.SearchSubProdutML("");
            }

            ddlSubprodutoml.DataSource = lstsubprodutos;
            ddlSubprodutoml.DataBind();

            ddlSubprodutoml.Enabled = true;
            ddlSubProdCode_TextChanged(sender, e);

        }

        protected void linqsComissoes()
        {
            string urlQueries = Request.Url.Query;

            lkComissaoAbertura.HRef = ConfigurationManager.AppSettings["LinqComissaoAberturaNegociacao"] + urlQueries;
            lkComissaoGestao.HRef = ConfigurationManager.AppSettings["LinqComissaoGestaoContratoNegociacao"] + urlQueries;
            lkComissaorenovacao.HRef = ConfigurationManager.AppSettings["LinqComissaoGestaoRenovacaoNegociacao"] + urlQueries;

        }

        internal void getSublimites(LM33_ContratoML _lm33)
        {
            _lm33.ProdutosRiscoAssinatura = new List<LM33_ContratoML.ProdutosRiscoA>();
            _lm33.produtosRiscoC = new List<LM33_ContratoML.ProdutoRiscoC>();
            _lm33.produtosRiscoF = new List<LM33_ContratoML.ProdutoRiscoF>();

            foreach (var fm in lvProdutosRisco.Items)
            {
                CheckBox ch = fm.FindControl("lbCParticular") as CheckBox;
                if (ch.Checked)
                {
                    _lm33.produtosRiscoF.Add(new LM33_ContratoML.ProdutoRiscoF
                    {
                        familiaproduto = (fm.FindControl("lbProduto") as Label).Text,
                        prodsubproduto = (fm.FindControl("lbSubproduto") as Label).Text.Split('-')[0],
                        tipologia = "A",
                        descritivo = (fm.FindControl("lbSubproduto") as Label).Text.Split('-')[1],
                    });
                }
            }
            foreach (var fm in lvProdutosRiscoAssinatura.Items)
            {
                CheckBox ch = fm.FindControl("lbCParticular") as CheckBox;
                if (ch.Checked)
                {
                    _lm33.ProdutosRiscoAssinatura.Add(new LM33_ContratoML.ProdutosRiscoA
                    {
                        familiaproduto = (fm.FindControl("lbProduto") as Label).Text,
                        prodsubproduto = (fm.FindControl("lbSubproduto") as Label).Text.Split('-')[0],
                        tipologia = "A",
                        descritivo = (fm.FindControl("lbSubproduto") as Label).Text.Split('-')[1],
                    });
                }
            }
            foreach (var fm in lvProdutosRiscoComercial.Items)
            {
                CheckBox ch = fm.FindControl("lbCParticular") as CheckBox;
                if (ch.Checked)
                {
                    _lm33.produtosRiscoC.Add(new LM33_ContratoML.ProdutoRiscoC
                    {
                        familiaproduto = (fm.FindControl("lbProduto") as Label).Text,
                        prodsubproduto = (fm.FindControl("lbSubproduto") as Label).Text.Split('-')[0],
                        tipologia = "A",
                        descritivo = (fm.FindControl("lbSubproduto") as Label).Text.Split('-')[1].Replace("-", ""),
                    });
                }
            }
        }

        internal bool validacaoSublimitesRisco()
        {
            lberror.Text = "";
            bool sublimitesRiscoValidos = true;


            decimal limitGl = 0;
            decimal.TryParse(txtlimiteglobalmultilinha.Text, out limitGl);
            decimal subAss = 0;
            decimal.TryParse(txtsublimiteriscoAssinatura.Text, out subAss);
            decimal subFin = 0;
            decimal.TryParse(txtsublimiteriscoFinanceiro.Text, out subFin);
            decimal subCom = 0;
            decimal.TryParse(txtsublimitriscoComercial.Text, out subCom);

            if(subAss > limitGl || subFin > limitGl || subCom > limitGl)
            {
                sublimitesRiscoValidos = false;
                lberror.Text = Constantes.Mensagens.ValorSublimitesRiscoInvalido;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }

            return sublimitesRiscoValidos;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            //limpa e disable produto
            txtProdutoml.Text = "";
            txtProdutoml.Enabled = false;
            reqProdutoml.Enabled = false;

            //limpa e disable subproduto
            List<string> emptylst = new List<string> { " " };
            ddlSubprodutoml.DataSource = emptylst;
            ddlSubprodutoml.DataBind();
            ddlSubprodutoml.Enabled = false;

            //limpa e disable descricao
            txtDescritivo.Text = "";
            txtDescritivo.Enabled = false;
        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                reqidmultilinha.Enabled = false;
            }
            else
            {
                reqidmultilinha.Enabled = true;
            }
        }

        protected void txt_idmultilinha_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txt_idmultilinha.Text))
            {
                reqCliente.Enabled = false;
            }
            else
            {
                reqCliente.Enabled = true;

            }
        }

        protected void txtprazocontrato_TextChanged(object sender, EventArgs e)
        {
            DateTime dtInicio = DateTime.ParseExact(txtdatainiciocontrato.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            int prazoContrato = Convert.ToInt32(txtprazocontrato.Text);

            txtdatafimcontrato.Text = dtInicio.AddMonths(prazoContrato).ToString("yyyy-MM-dd");
        }

        protected void txtsublimiteriscoFinanceiro_TextChanged(object sender, EventArgs e)
        {
            lberrorSRF.Text = "";
            decimal limitGl = 0;
            decimal.TryParse(txtlimiteglobalmultilinha.Text, out limitGl);
            decimal subFin = 0;
            decimal.TryParse(txtsublimiteriscoFinanceiro.Text, out subFin);

            if (subFin > limitGl)
            {
                lberrorSRF.Text = Constantes.Mensagens.ValorSublimiteRiscoInvalido;
                lberrorSRF.Visible = true;
            } 
        }

        protected void txtsublimitriscoComercial_TextChanged(object sender, EventArgs e)
        {
            lberrorSRC.Text = "";
            decimal limitGl = 0;
            decimal.TryParse(txtlimiteglobalmultilinha.Text, out limitGl);
            decimal subCom = 0;
            decimal.TryParse(txtsublimitriscoComercial.Text, out subCom);

            if (subCom > limitGl)
            {
                lberrorSRC.Text = Constantes.Mensagens.ValorSublimiteRiscoInvalido;
                lberrorSRC.Visible = true;
            }
        }

        protected void txtsublimiteriscoAssinatura_TextChanged(object sender, EventArgs e)
        {
            lberrorSRA.Text = "";
            decimal limitGl = 0;
            decimal.TryParse(txtlimiteglobalmultilinha.Text, out limitGl);
            decimal subAss = 0;
            decimal.TryParse(txtsublimiteriscoAssinatura.Text, out subAss);

            if (subAss > limitGl)
            {
                lberrorSRA.Text = Constantes.Mensagens.ValorSublimiteRiscoInvalido;
                lberrorSRA.Visible = true;
            }
        }
    }
}
