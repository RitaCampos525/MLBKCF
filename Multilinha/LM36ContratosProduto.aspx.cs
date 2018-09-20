using MultilinhasDataLayer;
using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Multilinha
{
    public partial class LM36ContratosProduto : System.Web.UI.Page
    {
        MultilinhaBusinessLayer.BLMultilinha bl = new MultilinhaBusinessLayer.BLMultilinha();
        MultilinhasDataLayer.boMultilinhas TAT2 = new MultilinhasDataLayer.boMultilinhas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.PageLoad, this.Page.AppRelativeVirtualPath, abargs.USERNT, abargs.SN_HOSTNAME);

                //Bind DDls
                ddlTipoFam.DataSource = ArvoreFamiliaProdutos.SearchFamiliaProduto(ddlTipoRisco.SelectedValue).Select(x => x.familiaProduto).Distinct();
                ddlTipoFam.DataBind();

                //Show hide fields 
                string op = Request.QueryString["OP"] ?? "FF";
                switch (op.ToUpper())
                {
                    case "M":
                        break;
                    case "C":
                        break;
                    case "V":
                        Helper.AddRemoveHidden(true, dvLimites);
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
                reqIDMultinha.Enabled = false;
                txtidmultilinha.Enabled = false;
            }
            else
            {
                reqIDMultinha.Enabled = true;
                txtidmultilinha.Enabled = true;
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            //LM36_ContratosProduto lm36 = TAT2.SearchLM36(1234124);
            LM36_ContratosProduto chaves = new LM36_ContratosProduto();
            Helper.CopyPropertiesTo(camposChave, chaves);

            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
             MensagemOutput<LM36_ContratosProduto> lm36 = bl.LM36Request(chaves, new LM36_ContratosProduto.ContratosProduto(), abargs, "V", true);

            if (lm36 != null && lm36.ResultResult != null)
            {
                if (lm36.ResultResult.ContratosProdutos.Count > 0 && lm36.ResultResult.ContratosProdutos[0].NContratoProduto != null)
                {
                    Helper.AddRemoveHidden(false, dvLimites);
                    Helper.CopyObjectToControls(dvLimites, lm36);

                    lvConsultaProdutos.DataSource = lm36.ResultResult.ContratosProdutos;
                    lvConsultaProdutos.DataBind();

                    //Para Paginacao
                    if (lm36.ResultResult.ContratosProdutos.Count > 10)
                    {
                        Dictionary<int, LM36_ContratosProduto.ContratosProduto> dic = new Dictionary<int, LM36_ContratosProduto.ContratosProduto>();
                        Helper.AddPageFirstItem<LM36_ContratosProduto.ContratosProduto>(dic, lm36.ResultResult.ContratosProdutos.First());

                        ViewState["PaginasCTS"] = dic;
                        ViewState["UltimoCT"] = lm36.ResultResult.ContratosProdutos.Last();

                        lkpaginaanterior.Visible = true;
                        lkpaginaseguinte.Visible = true;
                    }
                }
                lvConsultaProdutos.DataBind();
            }
            else
            {
                lberror.Text = TAT2.GetMsgErroTATDescription(lm36.erro.ToString(), abargs);
                if (string.IsNullOrEmpty(lberror.Text))
                {
                    lberror.Text = lm36.mensagem;
                }
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "";
            txtidmultilinha.Text = "";
            txtNome.Text = "";
            txtCliente.Enabled = true;
            txtidmultilinha.Enabled = true;
        }

        protected void txtidmultilinha_TextChanged(object sender, EventArgs e)
        {
            //desabilita / habilita os require fields
            if (!string.IsNullOrEmpty(txtidmultilinha.Text))
            {
                txtCliente.Enabled = false;
                reqNumCliente.Enabled = false;
            }
            else
            {
                txtCliente.Enabled = true;
                reqNumCliente.Enabled = true;

            }
        }

        protected void lkpaginaanterior_Click(object sender, EventArgs e)
        {
            try
            {
                lberror.Text = "";

                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                MultilinhasDataLayer.WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.EventoClick, "LM36 - lkpaginaanterior_Click", abargs.USERNT, abargs.SN_HOSTNAME);

                //Contrato pesquisado
                LM36_ContratosProduto chaves = new LM36_ContratosProduto();
                Helper.CopyPropertiesTo(camposChave, chaves);

                Dictionary<int, LM36_ContratosProduto.ContratosProduto> dic = new Dictionary<int, LM36_ContratosProduto.ContratosProduto>();
                dic = ViewState["PaginasCTS"] as Dictionary<int, LM36_ContratosProduto.ContratosProduto>;
                LM36_ContratosProduto.ContratosProduto ultimoCT = dic.Count() == 1 ? dic[1] : dic[dic.Count() - 1]; //index pagina anterior

                //LM36
                MensagemOutput<LM36_ContratosProduto> lst = bl.LM36Request(chaves, ultimoCT, abargs, "V", false);

                //Insucesso
                if (lst == null || (lst.ResultResult == null && lst.erro != 0))
                {
                    lberror.Text = TAT2.GetMsgErroTATDescription(lst.erro.ToString(), abargs);
                    if (string.IsNullOrEmpty(lberror.Text) || lberror.Text.Length < 5)
                    {
                        lberror.Text = lst.mensagem;
                    }
                    lberror.Visible = true;
                    lberror.ForeColor = System.Drawing.Color.Red;
                }
                //Sucesso
                else
                {
                    if (lst.ResultResult != null && lst.ResultResult.ContratosProdutos.Count() >= 1)
                    {
                        lvConsultaProdutos.DataSource = lst.ResultResult.ContratosProdutos;
                        dic = ViewState["PaginasDDs"] as Dictionary<int, LM36_ContratosProduto.ContratosProduto>;
                        if (dic.Count() > 1)
                        {
                            dic.Remove(dic.Count()); //remover index de "proxima pagina"
                        }

                        lkpaginaanterior.Visible = false;
                        lkpaginaseguinte.Visible = false;
                    }

                    //Para paginacao
                    if (lst.ResultResult != null && lst.ResultResult.ContratosProdutos.Count() > 10)
                    {
                        ViewState["PaginasDDs"] = lst.ResultResult.ContratosProdutos.Last();
                        lkpaginaseguinte.Visible = true;
                        lkpaginaanterior.Visible = true;
                    }

                    lvConsultaProdutos.DataBind();
                }
            }
            catch (Exception ex)
            {
                ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;
                WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.Internal, "lkpaginaanterior_Click: " + ex.InnerException, abargs.USERNT, abargs.SN_HOSTNAME);
                lberror.Text = ex.Message;
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void lkpaginaseguinte_Click(object sender, EventArgs e)
        {
            lberror.Text = "";

            ABUtil.ABCommandArgs abargs = Session["ABCommandArgs"] as ABUtil.ABCommandArgs;

            WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.EventoClick, "LM36 - lkpaginaseguinte_Click", abargs.USERNT, abargs.SN_HOSTNAME);

            LM36_ContratosProduto.ContratosProduto ultimoDD = ViewState["UltimoCT"] as LM36_ContratosProduto.ContratosProduto;

            LM36_ContratosProduto ct = new LM36_ContratosProduto();
            Helper.CopyPropertiesTo(camposChave, ct);

            string mode = ViewState["OP"] as string;
            MensagemOutput<LM36_ContratosProduto> lst = bl.LM36Request(ct, ultimoDD,abargs, "V", false);
            WriteLog.Log(System.Diagnostics.TraceLevel.Info, LogTypeName.Internal, "MC03", abargs.USERNT, abargs.SN_HOSTNAME);

            //Insucesso
            if (lst == null || (lst.ResultResult == null && lst.erro != 0))
            {
                lberror.Text = TAT2.GetMsgErroTATDescription(lst.erro.ToString(), abargs);
                if (string.IsNullOrEmpty(lberror.Text) || lberror.Text.Length < 5)
                {
                    lberror.Text = lst.mensagem;
                }
                lberror.Visible = true;
                lberror.ForeColor = System.Drawing.Color.Red;
            }
            //Sucesso
            else
            {
                if (lst.ResultResult != null)
                {
                    lvConsultaProdutos.DataSource = lst.ResultResult.ContratosProdutos;
                    Dictionary<int, LM36_ContratosProduto.ContratosProduto> dic = new Dictionary<int, LM36_ContratosProduto.ContratosProduto>();
                    dic = ViewState["PaginasCTS"] as Dictionary<int, LM36_ContratosProduto.ContratosProduto>;

                    Helper.AddPageFirstItem(dic, lst.ResultResult.ContratosProdutos.First());
                    ViewState["PaginasCTS"] = dic;
                }

                //Para paginacao
                if (lst.ResultResult != null && lst.ResultResult.ContratosProdutos.Count() > 10)
                {
                    ViewState["PaginasCTS"] = lst.ResultResult.ContratosProdutos.Last();
                    lkpaginaseguinte.Visible = true;
                }
                else
                {
                    lkpaginaseguinte.Visible = false;
                }
                lkpaginaanterior.Visible = true;
                lvConsultaProdutos.DataBind();
            }
        }

        protected void ddlTipoRisco_TextChanged(object sender, EventArgs e)
        {
            ddlTipoFam.DataSource = ArvoreFamiliaProdutos.SearchFamiliaProduto(ddlTipoRisco.SelectedValue).Select(x => x.familiaProduto).Distinct();
            ddlTipoFam.DataBind();
        }
    }
}