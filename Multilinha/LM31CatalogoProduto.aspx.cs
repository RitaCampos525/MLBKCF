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
        MultilinhaBusinessLayer.BLMultilinha bl = new MultilinhaBusinessLayer.BLMultilinha();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dtfechas = Global.dtfechasG;

                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

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

                        txtDataVersao.Enabled = true;
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
                
                int nprodutoml;
                Int32.TryParse(txtNumeroMinimoProdutos.Text, out nprodutoml);
                if (validaNProdutosCredito(nprodutoml) && validaLimiteMaximoCredito())
                {
                    LM31_CatalogoProdutoML lm31 = new LM31_CatalogoProdutoML();
                    Helper.CopyPropertiesTo(this, lm31);

                    getprodutostoLM31(lm31);

                    if (validaNSubProdutosCredito(lm31))
                    {
                        //Chamar ML01 - C
                        ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                        MensagemOutput<LM31_CatalogoProdutoML> response = bl.LM31Request(lm31, abargs, "C");

                        if (response.ResultResult != null && response.ResultResult.ProductCode != null)
                        {
                            lberror.Text = Constantes.Mensagens.LM31CatalogoCriado;
                            lberror.Visible = true;
                            lberror.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lberror.Text = TAT2.GetMsgErroTATDescription(response.erro.ToString(), abargs);
                            lberror.Visible = true;
                            lberror.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }

        protected void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            List<string> lstsubprodutos = TAT2.GetSubProdByProdCode(txtProductCode.Text, Global.ConnectionStringMaster, abargs);

            //for debug!!
            //if (lstsubprodutos.Count < 1)
            //{
            //    lstsubprodutos = TAT2.SearchSubProdutML("");
            //}

            ddlSubProdutoCode.DataSource = lstsubprodutos;
            ddlSubProdutoCode.DataBind();

            ddlSubProdutoCode.Enabled = true;
            ddlSubProdCode_TextChanged(sender, e);

        }

        protected void ddlSubProdCode_TextChanged(object sender, EventArgs e)
        {
            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
            string subprodutodesc = TAT2.GetSubProdDescriptionByCode(txtProductCode.Text, ddlSubProdutoCode.SelectedValue, Global.ConnectionStringMaster, abargs);

            txtSubProductDescription.Text = subprodutodesc;

            //for debug!
            //if (string.IsNullOrEmpty(subprodutodesc))
            //{

            //    txtSubProductDescription.Text = TAT2.SearchSubProdutDescriptionML(ddlSubProductCode.SelectedValue)[0].ToString();
            //}
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            //limpa produto
            txtProductCode.Text = "";

            //limpa subproduto
            List<string> emptylst = new List<string> { " " };
            ddlSubProdutoCode.DataSource = emptylst;
            ddlSubProdutoCode.DataBind();

            //limpa descricao
            txtSubProductDescription.Text = "";
            ddlSubProdutoCode.Enabled = false;

            //Datas comercializacao
            txtDataFimComercializacao.Text = "";
            txtDataInicioComercializacao.Text = "";

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string op = Request.QueryString["OP"] ?? "FF";
            if (op != "FF")
            {

                LM31_CatalogoProdutoML lm31 = new LM31_CatalogoProdutoML();
                Helper.CopyPropertiesTo(this, lm31);

                //Modo Criar:
                //IF: Resposta com produto Já Parameterizado e Ativo-> Alertar utilizador
                //Else: Abrir campos e desabilitar os campos chave de consulta
                //Familias devem estar fechados na Modificacao

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

                        //Chamar ML01 - V
                        ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                        MensagemOutput<LM31_CatalogoProdutoML> response = bl.LM31Request(lm31, abargs, "V");

                        //Sucesso
                        if (response.ResultResult != null && response.ResultResult.ProductCode != null)
                        {
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
                        }
                        //Insucesso
                        else
                        {
                            lberror.Text = TAT2.GetMsgErroTATDescription(response.erro.ToString(), abargs);
                            if(String.IsNullOrEmpty(lberror.Text))
                            {
                                lberror.Text = response.mensagem;
                            }
                            lberror.Visible = true;
                            lberror.ForeColor = System.Drawing.Color.Red;
                        }

                        break;

                    case "M":

                        //Chamar ML01 - V
                        abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                        response = bl.LM31Request(lm31, abargs, "V");

                        //Sucesso
                        if (response.ResultResult != null && response.ResultResult.ProductCode != null)
                        {
                            txtDataInicioComercializacao.Enabled = false;
                            Helper.AddRemoveHidden(false, divdpConsulta);
                            Helper.AddRemoveHidden(false, dvtitleAcordionRenovacao);
                            Helper.SetEnableControler(divRiscoFinanceiro, false);
                            Helper.AddRemoveHidden(false, dvtitleAcordionRFinanceiro);
                            Helper.SetEnableControler(divRiscoAssinatura, false);
                            Helper.AddRemoveHidden(false, dvtitleAcordionRAssinatura);
                            Helper.SetEnableControler(divRiscoComercial, false);
                            Helper.AddRemoveHidden(false, dvtitleAcordionRComercial);
                            Helper.AddRemoveHidden(false, divPeriocidadeCobranca);
                            Helper.AddRemoveHidden(false, acoes_ml01);
                            Helper.AddRemoveHidden(false, hr);
                            Helper.AddRemoveHidden(false, hr1);

                            btnEdit.Visible = true;
                        }
                        //Insucesso
                        else
                        {
                            lberror.Text = TAT2.GetMsgErroTATDescription(response.erro.ToString(), abargs);
                            if (String.IsNullOrEmpty(lberror.Text))
                            {
                                lberror.Text = response.mensagem;
                            }
                            lberror.Visible = true;
                            lberror.ForeColor = System.Drawing.Color.Red;
                        }
                        break;


                    case "V":
                        //Chamar ML01 - V
                        abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                        response = bl.LM31Request(lm31, abargs, "V");

                        //Sucesso
                        if (response.ResultResult != null && response.ResultResult.ProductCode != null)
                        {
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
                        }
                        //Insucesso
                        else
                        {
                            lberror.Text = TAT2.GetMsgErroTATDescription(response.erro.ToString(), abargs);
                            if (String.IsNullOrEmpty(lberror.Text))
                            {
                                lberror.Text = response.mensagem;
                            }
                            lberror.Visible = true;
                            lberror.ForeColor = System.Drawing.Color.Red;
                        }
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

        internal bool validaNProdutosCredito(int nMinimoASeleccionar)
        {
            bool nprodutosValid = true;
            lberror.Text = "";

            //Valida se Produtos estão selecionados - Valida o nº de familias. Não é por nº de subproduto
            int countSel = 0;
            TreeNode todosF = trtipologiaProdutosRFTree.Nodes[0];
            foreach (TreeNode tr in todosF.ChildNodes)
            {
                if (tr.Checked)
                    countSel++;
            }

            int countSel2 = 0;
            TreeNode todosC = trtipologiaProdutosRCTree.Nodes[0];
            foreach (TreeNode tr in todosC.ChildNodes)
            {
                if (tr.Checked)
                {
                    countSel2++;
                }
            }

            int countSel3 = 0;
            TreeNode todosA = trtipologiaProdutosRATree.Nodes[0];
            foreach (TreeNode tr in todosA.ChildNodes)
            {
                if (tr.Checked)
                {
                    countSel3++;
                }
            }

            //Validacao Numero Minimo de Produtos a seleccionar 
            if ((countSel + countSel2 + countSel3) < nMinimoASeleccionar)
            {
                string[] args = { nMinimoASeleccionar.ToString() };
                lberror.Text = Constantes.NovaMensagem(Constantes.Mensagens.NMinimoProdutosML, args);
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;

                nprodutosValid = false;

            }

            //de acordo com a Tipologia de Produto Base ou Avançada))
            if (txtSubProductDescription.Text.ToUpper().Contains("BASE"))
            {
                if ((countSel + countSel2 + countSel3) > Convert.ToInt32(ConfigurationManager.AppSettings["NMaxProdutoMLBase"]))
                {
                    lberror.Text = Constantes.Mensagens.NMinimoProdutosRiscoB;
                    lberror.Visible = true;
                    lberror.ForeColor = System.Drawing.Color.Red;

                    nprodutosValid = false;
                }
            }
            else if (txtSubProductDescription.Text.ToUpper().Contains("AVANCADO"))
            {
                if ((countSel + countSel2 + countSel3) > Convert.ToInt32(ConfigurationManager.AppSettings["NMaxProdutoMLAvancada"]))
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

        internal bool validaNSubProdutosCredito(LM31_CatalogoProdutoML lm31)
        {
            bool nsubprodutosValid = true;

            //de acordo com a Area de arquitectura
            decimal countSel = lm31.produtosF.Count();
            //if (countSel > 60)
            //{
            //    lberror.Text = Constantes.Mensagens.NMinimoProdutosRiscoF;
            //    lberror.Visible = true;
            //    lberror.ForeColor = System.Drawing.Color.Red;

            //    nsubprodutosValid = false;
            //}
            decimal countSel2 = lm31.produtosC.Count();
            //if (countSel2 > 20)
            //{
            //    lberror.Text = Constantes.Mensagens.NMinimoProdutosRiscoC;
            //    lberror.Visible = true;
            //    lberror.ForeColor = System.Drawing.Color.Red;

            //    nsubprodutosValid = false;
            //}
            decimal countSel3 = lm31.produtosA.Count();
            //if (countSel3 > 20)
            //{
            //    lberror.Text = Constantes.Mensagens.NMinimoProdutosRiscoAs;
            //    lberror.Visible = true;
            //    lberror.ForeColor = System.Drawing.Color.Red;

            //    nsubprodutosValid = false;
            //}

            if(countSel + countSel2 + countSel3 > 100)
            {
                lberror.Text = Constantes.Mensagens.NMinimoProdutosCredito;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }

            return nsubprodutosValid;

        }

        internal bool validaLimiteMaximoCredito()
        {
            decimal limMax = 0;
            decimal.TryParse(txtLimiteMaximoCredito.Text, out limMax);
            decimal limMin = 0;
            decimal.TryParse(txtLimiteMinimoCredito.Text, out limMin);

            if (limMax < limMin)
            {
                lberrorlim.Text = "Limite máximo inferior ao limite mínimo";
                lberrorlim.Visible = true;

                return false;
            }
            else
            {
                lberrorlim.Text = "";
                lberrorlim.Visible = false;
                return true;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int nprodutoml;
                Int32.TryParse(txtNumeroMinimoProdutos.Text, out nprodutoml);
                if (validaNProdutosCredito(nprodutoml))
                {
                    LM31_CatalogoProdutoML lm31 = new LM31_CatalogoProdutoML();
                    Helper.CopyPropertiesTo(this, lm31);

                    getprodutostoLM31(lm31);
                    //Chamar ML01 - M
                    ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                    MensagemOutput<LM31_CatalogoProdutoML> response = bl.LM31Request(lm31, abargs, "M");

                    lberror.Text = Constantes.Mensagens.LM31CatalogoModificado;
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
            TreeNode todosF = trtipologiaProdutosRFTree.Nodes[0];
            foreach (TreeNode tr in todosF.ChildNodes)
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
                                    produto = trch.Text.Split('-')[0].Length >= 2 ? trch.Text.Split('-')[0].Substring(0, 2) : "",
                                    subproduto = trch.Text.Split('-')[0].Length >= 2 ? trch.Text.Split('-')[0].Substring(2, 2) : "",
                                    familia = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RF).FirstOrDefault(x => x.familiaProduto == tr.Text.Replace("-","").Trim()).codfamiliaProduto.ToString(), //trch.Text.Split('-')[1].Replace("-", ""),
                                    tipologia = "F"
                                }
                            );
                    }
                }
            }
            //Risco Comercial
            TreeNode todosC = trtipologiaProdutosRCTree.Nodes[0];
            foreach (TreeNode tr in todosC.ChildNodes)
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
                                    produto = trch.Text.Split('-')[0].Length >= 2 ? trch.Text.Split('-')[0].Substring(0, 2) : "",
                                    subproduto = trch.Text.Split('-')[0].Length >= 2 ? trch.Text.Split('-')[0].Substring(2, 2) : "",
                                    familia = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RC).FirstOrDefault(x => x.familiaProduto == tr.Text.Replace("-", "").Trim()).codfamiliaProduto.ToString(), 
                                    tipologia = "C"
                                }
                            );
                    }
                }
            }
            //Risco Assinatura
            TreeNode todosA = trtipologiaProdutosRATree.Nodes[0];
            foreach (TreeNode tr in todosA.ChildNodes)
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
                                    produto = trch.Text.Split('-')[0].Length >= 2 ? trch.Text.Split('-')[0].Substring(0, 2) : "",
                                    subproduto = trch.Text.Split('-')[0].Length >= 2 ? trch.Text.Split('-')[0].Substring(2, 2) : "",
                                    familia = ArvoreFamiliaProdutos.SearchFamiliaProduto(Constantes.tipologiaRisco.RA).FirstOrDefault(x => x.familiaProduto == tr.Text.Replace("-", "").Trim()).codfamiliaProduto.ToString(),
                                    tipologia = "A"
                                }
                            );
                    }
                }
            }
        }

        //protected void txtLimiteMaximoCredito_TextChanged(object sender, EventArgs e)
        //{
        //    decimal limMax = Convert.ToDecimal(txtLimiteMaximoCredito.Text);
        //    decimal limMin = Convert.ToDecimal(txtLimiteMinimoCredito.Text);

        //    if (limMax < limMin)
        //    {
        //        lberrorlim.Text = "Limite máximo inferior ao limite mínimo";
        //        lberrorlim.Visible = true;
                
        //        return;

        //    }
        //    else
        //    {
        //        lberrorlim.Text = "";
        //        lberrorlim.Visible = false;
        //    }
        //}
    }

}