using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace Multilinha
{
    public partial class LM33ContratoMLaspx : System.Web.UI.Page
    {
        MultilinhasDataLayer.boMultilinhas bo = new MultilinhasDataLayer.boMultilinhas();

        public DateTime dtfechas = Global.dtfechasG;
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                txtProdutoml.Text = ConfigurationManager.AppSettings["CodigoProdutoML"];
                txtproduto_TextChanged(sender, e);

                linqsComissoes();

                //dropdownlists
                ddlIndicadorRenovacao.DataSource = ML_Objectos.GetIndicadorRenovacao();
                ddlIndicadorRenovacao.DataBind();

                string op = Request.QueryString["OP"] ?? "FF";
                switch (op.ToUpper())
                {
                    case "M":
                        lblTransactionM.CssClass = lblTransactionM.CssClass.Replace("atabD", "");
                        lblTransactionM.Enabled = true;

                        bklIdworkflow.InnerText = "ID Multilinha:";

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, dvtitleComissoes);
                        Helper.AddRemoveHidden(true, accoesfinais_criarml03);
                        Helper.AddRemoveHidden(true, hr1);
                        //Helper.AddRemoveHidden(true, hr2);
                        Helper.AddRemoveHidden(true, hr3);
                        Helper.AddRemoveHidden(true, hr4);

                        btnSimulacao.Enabled = true;

                        break;
                    case "C":
                        lblTransaction.CssClass = lblTransaction.CssClass.Replace("atabD", "");
                        lblTransaction.Enabled = true;

                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, dvtitleComissoes);
                        Helper.AddRemoveHidden(true, accoesfinais_criarml03);
                        Helper.AddRemoveHidden(true, hr1);
                        //Helper.AddRemoveHidden(true, hr2);
                        Helper.AddRemoveHidden(true, hr3);
                        Helper.AddRemoveHidden(true, hr4);

                        btnCriar.Visible = true;

                        break;
                    case "A":
                        lblTransactionD.CssClass = lblTransactionD.CssClass.Replace("atabD", "");
                        lblTransactionD.Enabled = true;
                        lblTransactionE.CssClass = lblTransactionD.CssClass.Replace("atabD", "");
                        lblTransactionE.Enabled = true;

                        Helper.AddRemoveHidden(true, lm33C); //hide controls criar
                        Helper.AddRemoveHidden(false, ml03V_denuncia); //Show controls visualizar
                        ml03V_denuncia.Visible = true;
                        break;
                    case "V":
                        lblTransactionV.CssClass = lblTransactionV.CssClass.Replace("atabD", "");
                        lblTransactionV.Enabled = true;

                        btnLimpar_Click(sender, e);


                        Helper.AddRemoveHidden(true, dpOK);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, dvtitleComissoes);
                        Helper.AddRemoveHidden(true, accoesfinais_criarml03);
                        Helper.AddRemoveHidden(true, hr1);
                        //Helper.AddRemoveHidden(true, hr2);
                        Helper.AddRemoveHidden(true, hr3);
                        Helper.AddRemoveHidden(true, hr4);
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
                        Helper.AddRemoveHidden(true, hr1);
                        //Helper.AddRemoveHidden(true, hr2);
                        Helper.AddRemoveHidden(true, hr3);
                        Helper.AddRemoveHidden(true, hr4);
                        btnSearchDO.Enabled = false;
                        break;
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }

        protected void btnSearchDO_Click(object sender, EventArgs e)
        {
            string op = Request.QueryString["OP"] ?? "FF";
            switch (op.ToUpper())
            {
                case "C":
                    //Call ML03 para obtencao dos dados do contrato da proposta workflow
                    LM33_ContratoML LM33C = TAT2.SearchML03(Convert.ToInt32(txtCliente.Text), txtIdworkflow.Text);
                    Helper.CopyObjectToControls(this.Page, LM33C);

                    if (string.IsNullOrEmpty(LM33C.idproposta))
                    {
                        lberror.Text = Constantes.Mensagens.LM33PropostaInexistente;
                        return;
                    }
                    else
                    {
                        Helper.SetEnableControler(camposChave, false);
                        Helper.AddRemoveHidden(false, dpOK);
                        Helper.AddRemoveHidden(false, hr1);
                        
                        //Get DOs Cliente
                        //For debug
                        ddlncontado.DataSource = bo.SearchDOCliente("");
                        ddlncontado.DataBind();

                        //Save in view state Produtos e SubProdutos
                        ViewState["LM33C"] = LM33C;
                    }

                    break;

                case "V":


                    //Call ML03 para preencher o ecra com os dados
                    LM33_ContratoML M03V = TAT2.SearchML03(Convert.ToInt32(txtCliente.Text), txtIdworkflow.Text);

                    Helper.CopyObjectToControls(this.Page, M03V);

                    if (string.IsNullOrEmpty(M03V.idproposta))
                    {
                        lberror.Text = Constantes.Mensagens.LM33PropostaInexistente;
                        return;
                    }

                    else
                    {
                        Helper.SetEnableControler(camposChave, true);
                        btnLimpar_Click(sender, e); 
                        Helper.AddRemoveHidden(false, dpOK);
                        Helper.SetEnableControler(dpOK, false);
                        Helper.AddRemoveHidden(false, hr1);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                        Helper.SetEnableControler(divRiscoFinanceiro, false);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                        Helper.SetEnableControler(divRiscoAssinatura, false);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                        Helper.SetEnableControler(divRiscoComercial, false);
                        Helper.AddRemoveHidden(false, dvtitleComissoes);
                        Helper.SetEnableControler(divComissoes, false);
                        Helper.AddRemoveHidden(false, hr4);

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

                    }

                    break;

                case "M":

                    //CALL ML03
                    LM33_ContratoML M03M = TAT2.SearchML03(Convert.ToInt32(txtCliente.Text), txtIdworkflow.Text);

                    Helper.CopyObjectToControls(this.Page, M03M);

                    if (string.IsNullOrEmpty(M03M.idproposta))
                    {
                        lberror.Text = Constantes.Mensagens.LM33PropostaInexistente;
                        return;
                    }
                    else
                    {

                        Helper.SetEnableControler(camposChave, false);
                        btnLimpar_Click(sender, e); //desabilita produto e subproduto
                        Helper.AddRemoveHidden(false, dpOK);
                        Helper.AddRemoveHidden(false, hr1);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(false, dvtitleComissoes);
                        Helper.AddRemoveHidden(false, accoesfinais_criarml03);
                        Helper.AddRemoveHidden(false, hr3);
                        Helper.AddRemoveHidden(false, hr4);

                        btnConfirmar.Enabled = false;


                        #region tabelas de produtos de riscos

                        //Get Produtos
                        // e Popula CG e CP . Quando seleccionado ficam enable! Não é possivel deseleccionar
                        List<ArvoreFamiliaProdutos> lstF = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF);
                        listViewProdutos(lstF, Constantes.tipologiaRisco.RF, lvProdutosRisco, M03M, false);

                        List<ArvoreFamiliaProdutos> lstC = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RC);
                        listViewProdutos(lstC, Constantes.tipologiaRisco.RC, lvProdutosRiscoComercial, M03M, false);

                        List<ArvoreFamiliaProdutos> lstA = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA);
                        listViewProdutos(lstA, Constantes.tipologiaRisco.RA, lvProdutosRiscoAssinatura, M03M, false);

                        #endregion
                    }
                    break;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

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
                    Helper.AddRemoveHidden(false, hr1);
                    //Helper.AddRemoveHidden(false, hr2);
                    Helper.AddRemoveHidden(false, hr3);
                    Helper.AddRemoveHidden(false, hr4);


                    //Call M03 (ViewState) para obtencao de produtos do produto ML introduzido! E seleccionar CP correspondentes
                    LM33_ContratoML LM23 = ViewState["LM33C"] as LM33_ContratoML;

                    #region tabelas de produtos de riscos

                    //Get Produtos

                    List<ArvoreFamiliaProdutos> lstF = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF);
                    listViewProdutos(lstF, Constantes.tipologiaRisco.RF, lvProdutosRisco, LM23, true);

                    List<ArvoreFamiliaProdutos> lstC = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RC);
                    listViewProdutos(lstC, Constantes.tipologiaRisco.RC, lvProdutosRiscoComercial, LM23, true);

                    List<ArvoreFamiliaProdutos> lstA = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA);
                    listViewProdutos(lstA, Constantes.tipologiaRisco.RA, lvProdutosRiscoAssinatura, LM23, true);

                    #endregion

                    break;
            }
        }

        protected void txtPeriocidadeCobranca_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdatafimcontrato.Text))
            {
                lberror.Text = "Data fim de contrato inválida";
                lberror.Visible = true;
                return;
            }

            int meses = Convert.ToInt32(txtPeriocidadeCobranca.Text);
            txtdataproximacobranca.Text = Convert.ToDateTime(txtdatafimcontrato.Text).AddMonths(meses).ToString("yyyy-MM-dd");

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

                //if (tipologia == Constantes.tipologiaRisco.RF)
                //{
                //    if (LM33.produtosRiscoF != null && LM33.produtosRiscoF.Exists(x => x.familiaproduto == row))
                //    {
                //        //Se encontra produto na LM33, seleccionada e nao deixa modificar
                //        item.isGeral = true;
                //        item.cGEnable = false;
                //    }
                //}
                //if (tipologia == Constantes.tipologiaRisco.RA)
                //{
                //    if (LM33.produtosRiscoC != null && LM33.produtosRiscoC.Exists(x => x.familiaproduto == row))
                //    {
                //        item.isGeral = true;
                //        item.cGEnable = false;
                //    }
                //}
                //if (tipologia == Constantes.tipologiaRisco.RC)
                //{
                //    if (LM33.ProdutosRiscoAssinatura != null && LM33.ProdutosRiscoAssinatura.Exists(x => x.familiaproduto == row))
                //    {
                //        item.isGeral = true;
                //        item.cGEnable = false;
                //    }
                //}


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

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //for debug
                if (string.IsNullOrEmpty(txtdatafimcontrato.Text))
                {
                    txtdatafimcontrato.Text = "9999-12-31";
                }

                //Verificar que pelo menos dois produtos estão selecionados!
                bool val = validacaoCP();
                //Verificar data fim de contrato
                bool val2 = true; //validacaoDtProximaCobrabca();

                if (val && val2)
                {
                    //Call LM33 - C

                    lberror.Text = Constantes.Mensagens.LM33ContratoCriado;
                    lberror.ForeColor = System.Drawing.Color.Green;
                    lberror.Visible = true;

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
                Helper.CopyPropertiesTo(lm33C, lm34);

                //zona produtos
                //adicaoCP(Constantes.tipologiaRisco.RF, lvProdutosRisco, lm34);
                //adicaoCP(Constantes.tipologiaRisco.RA, lvProdutosRiscoAssinatura, lm34);
                //adicaoCP(Constantes.tipologiaRisco.RC, lvProdutosRiscoComercial, lm34);

                Page.Transfer(ConfigurationManager.AppSettings["DefinicaoSublimites"],
               new Dictionary<string, object>() {
                                  { "Op", "C" },
                                  { "ContratoCriado", lm34 },
               });

                //Server.Transfer(ConfigurationManager.AppSettings["DefinicaoSublimites"]);
            }
        }

        internal bool validacaoCP()
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


            if ((countSel + countSel2 + countSel3) < 2)
            {
                lberror.Text = "Aceite as condições particulares de dois sub-produtos de uma tipologia de risco";
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
                val = false;
            }
            else
                val = true;

            return val;

        }

        internal bool validacaoDtProximaCobranca()
        {
            bool dtval = (!string.IsNullOrEmpty(txtdataproximacobranca.Text) && !txtdataproximacobranca.Text.Equals("9999-12-31"));
            lberror.Text = "Data fim de contrato inválida";
            lberror.Visible = true;

            return dtval;
        }

        protected void ddlSubProdCode_TextChanged(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            string subprodutodesc = TAT2.GetSubProdDescriptionByCode(txtProdutoml.Text, ddlSubprodutoml.SelectedValue, Global.ConnectionStringDTAB, abargs);

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
            List<string> lstsubprodutos = TAT2.GetSubProdByProdCode(txtProdutoml.Text, Global.ConnectionStringDTAB, abargs);

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

        protected void txtIdworkflow_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txtIdworkflow.Text))
            {
                reqCliente.Enabled = false;
            }
            else
            {
                reqCliente.Enabled = true;

            }
        }

        protected void linqsComissoes()
        {
            string urlQueries = Request.Url.Query;

            lkComissaoAbertura.HRef = ConfigurationManager.AppSettings["LinqComissaoAberturaNegociacao"] + urlQueries;
            lkComissaoGestao.HRef = ConfigurationManager.AppSettings["LinqComissaoGestaoContratoNegociacao"] + urlQueries;

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            //limpa e disable produto
            txtProdutoml.Text = "";
            txtProdutoml.Enabled = false;
            reqProductCode.Enabled = false;

            //limpa e disable subproduto
            List<string> emptylst = new List<string> { " " };
            ddlSubprodutoml.DataSource = emptylst;
            ddlSubprodutoml.DataBind();
            ddlSubprodutoml.Enabled = false;
            reqSubProdCode.Enabled = false;

            //limpa e disable descricao
            txtDescritivo.Text = "";
            txtDescritivo.Enabled = false;
        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                reqIdWorkflow.Enabled = false;
            }
            else
                reqIdWorkflow.Enabled = true;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //Include id multilinha
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

        protected void btnSimulacao_Click(object sender, EventArgs e)
        {

        }
    }
    }
