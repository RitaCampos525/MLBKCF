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
    public partial class ML0102CatalogoProduto : System.Web.UI.Page
    {
        public DateTime dtfechas = Global.dtfechasG;
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();

        protected void Page_Load(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            DataTable dtProdutos = TAT2.GetProdutos(Global.ConnectionStringDTAB, abargs);

            if (!Page.IsPostBack)
            {
                txtProductCode.Text = ConfigurationManager.AppSettings["CodigoProdutoML"];
                txtProductCode_TextChanged(sender, e);

                //linq comissoes com contexto da AB
                linqsComissoes();

                //dropdownlist
                ddlPeriocidadeCobranca.DataSource = ML_Objectos.GetPeriocidade();
                ddlPeriocidadeCobranca.DataBind();
                ddlPeriocidadeCobrancaRenovacao.DataSource = ML_Objectos.GetPeriocidade();
                ddlPeriocidadeCobrancaRenovacao.DataBind();
                ddlIndRenovacao.DataSource = ML_Objectos.GetIndicadorRenovacao();
                ddlIndRenovacao.DataBind();


                //Show hide fields 
                string op = Request.QueryString["OP"] ?? "FF";
                switch (op.ToUpper())
                {
                    case "M":

                        Helper.AddRemoveHidden(true, divdpConsulta);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRenovacao);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, divPeriocidadeCobranca);
                        Helper.AddRemoveHidden(true, acoes_ml01);
                        Helper.AddRemoveHidden(true, hr);
                        Helper.AddRemoveHidden(true, hr1);

                        Helper.AddRemoveActive(true, liModificacao);
                        Helper.AddRemoveActive(false, liPrameterizacao);
                        Helper.AddRemoveActive(false, liConsulta);
                        lbModificacao.CssClass = lbModificacao.CssClass.Replace("atab", "atabD");

                        break;
                    case "C":

                        Helper.AddRemoveHidden(true, divdpConsulta);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRenovacao);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, divPeriocidadeCobranca);
                        Helper.AddRemoveHidden(true, acoes_ml01);
                        Helper.AddRemoveHidden(true, hr);
                        Helper.AddRemoveHidden(true, hr1);

                        Helper.AddRemoveActive(true, liPrameterizacao);
                        Helper.AddRemoveActive(false, liModificacao);
                        Helper.AddRemoveActive(false, liConsulta);
                        lbPrameterizacao.CssClass = lbPrameterizacao.CssClass.Replace("atab", "atabD");
                        lbModificacao.CssClass = lbModificacao.CssClass.Replace("atabD", "atab");
                        lbConsulta.CssClass = lbConsulta.CssClass.Replace("atabD", "atab");

                        break;
                    case "V":

                        Helper.AddRemoveHidden(true, divdpConsulta);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRenovacao);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(true, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(true, divPeriocidadeCobranca);
                        Helper.AddRemoveHidden(true, acoes_ml01);
                        Helper.AddRemoveHidden(true, hr);
                        Helper.AddRemoveHidden(true, hr1);

                        Helper.AddRemoveActive(true, liConsulta);
                        Helper.AddRemoveActive(false, liModificacao);
                        Helper.AddRemoveActive(false, liPrameterizacao);
                        lbConsulta.CssClass = lbConsulta.CssClass.Replace("atab", "atabD");
                        lbModificacao.CssClass = lbModificacao.CssClass.Replace("atabD", "atab");
                        lbPrameterizacao.CssClass = lbPrameterizacao.CssClass.Replace("atabD", "atab");

                        break;
                    default:
                        lberror.Text = "Página sem contexto. Execute a transação na Aplicação Bancária";
                        lberror.Visible = true;
                        break;
                }
                txtProductCode.Focus();
            }

        }

        protected void ddlIndicadorRenovacao_TextChanged(object sender, EventArgs e)
        {
            if (ddlIndRenovacao.SelectedValue == "N")
            {
                txtPrazoRenovacao.Enabled = false;
            }
        }

        //protected void makeTreeView(DataTable dtnodesandchilds, string colnodes, string colsecondNode, string descritivo,  string tipologia, TreeView tree)
        //{
        //    string arprd = Helper.getProdutosTipologia(tipologia);

        //    //TreeView Produtos
        //    DataView view = dtnodesandchilds.AsDataView();
        //    DataTable distinctProdutos = view.ToTable(true, colnodes); //select distinc nodes

        //    var a = distinctProdutos.Select(colnodes + " IN (" + arprd + ")"); //select produtos por tipologia de risco

        //    //Faz os 1s Nodes - Produtos
        //    foreach (DataRow row in a)
        //    {
        //        string stproduto = row[colnodes].ToString();
        //        TreeNode produto = new TreeNode(stproduto);

        //        DataRow[] dtSubProdutos = dtnodesandchilds.Select(colnodes + " = '" + stproduto + "'");

        //        //Faz os 2os Nodes - SubProduto
        //        TreeNode[] array = new TreeNode[dtSubProdutos.Length];
        //        for (int i = 0; i < dtSubProdutos.Length; i++)
        //        {
        //            array[i] = new TreeNode(dtSubProdutos[i][colsecondNode].ToString());
        //            array[i].ShowCheckBox = true;
        //            array[i].Text = dtSubProdutos[i][colsecondNode].ToString() + " - " + dtSubProdutos[i][descritivo].ToString(); //(codigo + descritivo)
        //            produto.ChildNodes.Add(array[i]);
        //        }

        //        produto.ShowCheckBox = true;
        //        tree.Nodes.Add(produto); //Adiciona o nó com os child nodes
        //    }
        //}

        //protected void makeTreeView(DataTable dtnodesandchilds, string colnodes, string colsecondNode, string descritivo, TreeView tree)
        //{

        //    //TreeView Produtos
        //    DataView view = dtnodesandchilds.AsDataView();
        //    DataTable distinctProdutos = view.ToTable(true, colnodes); //select distinc nodes

        //    //Faz os 1s Nodes - Produtos
        //    foreach (DataRow row in distinctProdutos.Rows)
        //    {
        //        string stproduto = row[colnodes].ToString();
        //        TreeNode produto = new TreeNode(stproduto);

        //        DataRow[] dtSubProdutos = dtnodesandchilds.Select(colnodes + " = '" + stproduto + "'");

        //        //Faz os 2os Nodes - SubProduto
        //        TreeNode[] array = new TreeNode[dtSubProdutos.Length];
        //        for (int i = 0; i < dtSubProdutos.Length; i++)
        //        {
        //            array[i] = new TreeNode(dtSubProdutos[i][colsecondNode].ToString());
        //            array[i].ShowCheckBox = true;
        //            array[i].Text = dtSubProdutos[i][colsecondNode].ToString() + " - " +  dtSubProdutos[i][descritivo].ToString(); //(codigo + descritivo)
        //            produto.ChildNodes.Add(array[i]);
        //        }

        //        produto.ShowCheckBox = true;
        //        tree.Nodes.Add(produto); //Adiciona o nó com os child nodes
        //    }
        //}

        protected void makeTreeView2(string tipologia, TreeView tree)
        {
            List<ArvoreFamiliaProdutos> lstF = MultilinhasObjects.ArvoreFamiliaProdutos.SearchFamiliaProduto(tipologia);

            var familiaprodutos = lstF.Select(x => x.familiaProduto).Distinct();
            
            //nivel todos
            TreeNode treeAll = new TreeNode(" - Todos");
            
            foreach (string row in familiaprodutos)
            {
                //Faz os 1s Nodes - Familia Produtos
                TreeNode famProduto = new TreeNode(" - " + row);
                famProduto.ShowCheckBox = true;

                //Faz os 2os Nodes - Produto e SubProduto
                var dtSubProdutos = lstF.FindAll(x => x.familiaProduto == row);
                TreeNode[] array = new TreeNode[dtSubProdutos.Count()];
                for (int i = 0; i < dtSubProdutos.Count(); i++)
                {

                    array[i] = new TreeNode(dtSubProdutos[i].descricao);
                    array[i].ShowCheckBox = true;
                    array[i].Text = dtSubProdutos[i].produto.ToString() + dtSubProdutos[i].subproduto.ToString() +
                        " - " + dtSubProdutos[i].descricao.ToString(); //(codigo + descritivo)

                    famProduto.ChildNodes.Add(array[i]);
                    
                }

                famProduto.ShowCheckBox = true;
                treeAll.ChildNodes.Add(famProduto);
                //treeAll.ChildNodes.Add(famProduto);
                famProduto.CollapseAll();
            }

            tree.Nodes.Add(treeAll); //Adiciona o nó com os child nodes
            treeAll.Expanded = true;

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            lberror.Text = "";
            if (Page.IsValid)
            {
                if (validaNProdutosCredito())
                {

                    LM31_CatalogoProdutoML lm31 = new LM31_CatalogoProdutoML();
                    Helper.CopyPropertiesTo(this, lm31);

                    getprodutostoLM31(lm31);

                    //TO DO Chamar ML01 - C

                    lberror.Text = "Produto ML criado em Catalogo.";
                    lberror.Visible = true;
                    lberror.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            List<string> lstsubprodutos = TAT2.GetSubProdByProdCode(txtProductCode.Text, Global.ConnectionStringDTAB, abargs);

            //for debug!!
            if (lstsubprodutos.Count < 1)
            {
                lstsubprodutos = TAT2.SearchSubProdutML("");
            }

            ddlSubProductCode.DataSource = lstsubprodutos;
            ddlSubProductCode.DataBind();

            ddlSubProductCode.Enabled = true;
            ddlSubProdCode_TextChanged(sender, e);



        }

        protected void ddlSubProdCode_TextChanged(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            string subprodutodesc = TAT2.GetSubProdDescriptionByCode(txtProductCode.Text, ddlSubProductCode.SelectedValue, Global.ConnectionStringDTAB, abargs);

            txtSubProductDescription.Text = subprodutodesc;

            //for debug!
            if (string.IsNullOrEmpty(subprodutodesc))
            {

                txtSubProductDescription.Text = TAT2.SearchSubProdutDescriptionML(ddlSubProductCode.SelectedValue)[0].ToString();
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            //limpa produto
            txtProductCode.Text = "";

            //limpa subproduto
            List<string> emptylst = new List<string> { " " };
            ddlSubProductCode.DataSource = emptylst;
            ddlSubProductCode.DataBind();

            //limpa descricao
            txtSubProductDescription.Text = "";

            //enable subproduto
            ddlSubProductCode.Enabled = false;

            //Datas comercializacao
            txtDataFimComercializacao.Text = "";
            txtDataInicioComercializacao.Text = "";

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string op = Request.QueryString["OP"] ?? "FF";
            if (op != "FF")
            {
                //TODO Chamar ML01 

                //Modo Criar:
                //IF: Resposta com produto Já Parameterizado-> Alertar utilizador
                //Else: Abrir campos e desabilitar os campos chave de consulta

                Helper.SetEnableControler(camposChave, false);

                #region arvore de produto de risco
                makeTreeView2(Constantes.tipologiaRisco.RF, trtipologiaProdutosRFTree);
                makeTreeView2(Constantes.tipologiaRisco.RA, trtipologiaProdutosRATree);
                makeTreeView2(Constantes.tipologiaRisco.RC, trtipologiaProdutosRCTree);

                trtipologiaProdutosRFTree.ShowExpandCollapse = true;
                //trtipologiaProdutosRFTree.ExpandAll();
                trtipologiaProdutosRFTree.NodeWrap = true;

                trtipologiaProdutosRCTree.ShowExpandCollapse = true;
                //trtipologiaProdutosRCTree.CollapseAll();
                trtipologiaProdutosRCTree.NodeWrap = true;

                trtipologiaProdutosRATree.ShowExpandCollapse = true;
                //trtipologiaProdutosRATree.CollapseAll();
                trtipologiaProdutosRATree.NodeWrap = true;
                #endregion

                switch (op.ToUpper())
                {
                    case "C":
                        Helper.AddRemoveHidden(false, divdpConsulta);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRenovacao);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(false, divPeriocidadeCobranca);
                        Helper.AddRemoveHidden(false, acoes_ml01);
                        Helper.AddRemoveHidden(false, hr);
                        Helper.AddRemoveHidden(false, hr1);

                        btnCriar.Visible = true;
                        break;

                    case "M":
                        Helper.AddRemoveHidden(false, divdpConsulta);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRenovacao);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                        Helper.AddRemoveHidden(false, divPeriocidadeCobranca);
                        Helper.AddRemoveHidden(false, acoes_ml01);
                        Helper.AddRemoveHidden(false, hr);
                        Helper.AddRemoveHidden(false, hr1);

                        btnEdit.Visible = true;
                        break;


                    case "V":
                        Helper.AddRemoveHidden(false, divdpConsulta);
                        Helper.SetEnableControler(divdpConsulta, false);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRenovacao);
                        Helper.SetEnableControler(divRenovacao, false);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                        Helper.SetEnableControler(divRiscoFinanceiro, false);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                        Helper.SetEnableControler(divRiscoAssinatura, false);
                        Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                        Helper.SetEnableControler(divRiscoComercial, false);
                        Helper.AddRemoveHidden(false, divPeriocidadeCobranca);
                        Helper.SetEnableControler(divPeriocidadeCobranca, false);
                        Helper.AddRemoveHidden(true, acoes_ml01); //manter acoes escondidas
                        Helper.AddRemoveHidden(true, hr);
                        Helper.AddRemoveHidden(true, hr1);

                        break;
                }
            }
        }

        protected void txtNumeroMinimoProdutos_TextChanged(object sender, EventArgs e)
        {
            //Nao é possivel seleccionar mais de 17 produtos.
            int ndias;
            bool parse = Int32.TryParse(txtNDiasIncumprimento.Text, out ndias);
            if (parse && ndias > 17)
                txtNDiasIncumprimento.Text = "";
        }

        protected bool validaNProdutosCredito()
        {
            bool nprodutosValid = true;
            lberror.Text = "";

            //Valida se Produtos estão selecionados - Valida n de familias. Não é por nº de subproduto
            int countSel = 0;
            foreach (TreeNode tr in trtipologiaProdutosRFTree.Nodes)
            {
                if (tr.Checked)
                    countSel++;
            }

            int countSel2 = 0;
            foreach (TreeNode tr in trtipologiaProdutosRCTree.Nodes)
            {
                if (tr.Checked)
                {
                    countSel2++;
                }
            }

            int countSel3 = 0;
            foreach (TreeNode tr in trtipologiaProdutosRFTree.Nodes)
            {
                if (tr.Checked)
                {
                    countSel3++;
                }
            }

            if (txtSubProductDescription.Text.Contains("Base"))
            {
                if ((countSel + countSel2 + countSel3) < 9)
                {
                    lberror.Text = Constantes.Mensagens.NMinimoProdutosRiscoB;
                    lberror.Visible = true;
                    lberror.ForeColor = System.Drawing.Color.Red;

                    nprodutosValid = false;
                }
            }
            else if (txtSubProductDescription.Text.Contains("Avançado"))
            {
                if ((countSel + countSel2 + countSel3) < 17)
                {
                    lberror.Text = Constantes.Mensagens.NMinimoProdutosRiscoA;
                    lberror.Visible = true;
                    lberror.ForeColor = System.Drawing.Color.Red;

                    nprodutosValid = false;
                }
            }
            else
            {
                lberror.Text = Constantes.Mensagens.ProdutoMLNIdentificado;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;

                nprodutosValid = false;
            }

            return nprodutosValid;

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (validaNProdutosCredito())
                {
                    //TO DO Chamar ML01 - M

                    lberror.Text = "Produto ML modificado em Catalogo.";
                    lberror.Visible = true;
                    lberror.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void linqsComissoes()
        {
            string urlQueries = Request.Url.Query;

            lqComissaoAberura.HRef = ConfigurationManager.AppSettings["LinqComissaoAberura"] + urlQueries;
            lqComissaoGestaoContrato.HRef = ConfigurationManager.AppSettings["LinqComissaoGestaoContrato"] + urlQueries;
            lqComissaoNovacao.HRef = ConfigurationManager.AppSettings["LinqComissaoNovacao"] + urlQueries;
            lqComissaoRenovao.HRef = ConfigurationManager.AppSettings["LinqComissaoRenovacao"] + urlQueries;

        }

        internal void getprodutostoLM31(LM31_CatalogoProdutoML lm31)
        {
            lm31.produtosF = new List<LM31_CatalogoProdutoML.ProdutoRisco>();
            lm31.produtosC = new List<LM31_CatalogoProdutoML.ProdutoRisco>();
            lm31.produtosA = new List<LM31_CatalogoProdutoML.ProdutoRisco>();

            //Risco Financeiro
            foreach (TreeNode tr in trtipologiaProdutosRFTree.Nodes)
            {
                foreach (TreeNode trch in tr.ChildNodes)
                {
                    if (trch.Checked)
                    {
                        lm31.tipologiaRiscoF = "F";
                        lm31.produtosF.Add
                            (
                                new LM31_CatalogoProdutoML.ProdutoRisco
                                {
                                    produto = trch.Text.Split('-')[0].Substring(0, 2),
                                    subproduto = trch.Text.Split('-')[0].Substring(1, 2),
                                }
                            );
                    }
                }
            }
            //Risco Comercial
            foreach (TreeNode tr in trtipologiaProdutosRCTree.Nodes)
            {
                foreach (TreeNode trch in tr.ChildNodes)
                {
                    if (trch.Checked)
                    {
                        lm31.tipologiaRiscoC = "C";
                        lm31.produtosC.Add
                            (
                                new LM31_CatalogoProdutoML.ProdutoRisco
                                {
                                    produto = trch.Text.Split('-')[0].Substring(0, 2),
                                    subproduto = trch.Text.Split('-')[0].Substring(1, 2),
                                }
                            );
                    }
                }
            }
            //Risco Assinatura
            foreach (TreeNode tr in trtipologiaProdutosRATree.Nodes)
            {
                foreach (TreeNode trch in tr.ChildNodes)
                {
                    if (trch.Checked)
                    {
                        lm31.tipologiaRiscoA = "A";
                        lm31.produtosA.Add
                            (
                                new LM31_CatalogoProdutoML.ProdutoRisco
                                {
                                    produto = trch.Text.Split('-')[0].Substring(0, 2),
                                    subproduto = trch.Text.Split('-')[0].Substring(1, 2),
                                }
                            );
                    }
                }
            }
        }

    }

}