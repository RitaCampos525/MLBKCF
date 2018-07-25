<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LM31CatalogoProduto.aspx.cs" Inherits="Multilinha.ML0102CatalogoProduto" %>
<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Catálogo Produto Multilinha</title>
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <link href="css/multilinha.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-1.12.4.min.js"></script>
    <script src="scripts/jquery-ui.js"></script>
    <script src="scripts/bootstrap.js"></script>
    

</head>
<uc1:header runat="server" ID="header" />
<body class="">
    <form id="form1" class="content container-fluid form-horizontal" runat="server">
        <div id="dvError" runat="server">
            <asp:Label runat="server" ID="lberror" CssClass="col-md-12 col-sm-12 lbl" Visible="false ">Occur an error</asp:Label>
        </div>
        <div class="clear"></div>
        <br />
        <div class="row titleTransaction">
            <asp:Label ID="lblTransaction" runat="server">Parameterização de Multilinha</asp:Label>
        </div>
        <div class="row colorbck">
            <div id="camposChave" runat="server">
            <div class="row form-group padding-row ">
                <div class="col-sm-4">
                    <Label id="lbProduto" runat="server" class="col-sm-4 text-right lbl">* Produto: </Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtProductCode" MaxLength="2" pattern="[A-Za-z0-9]{2}" OnTextChanged="txtProductCode_TextChanged" title="Deve inserir um código alfanumérico com duas posições"
                            oninvalid="setCustomValidity('Deve inserir um código alfanumérico com duas posições')"
                            onchange="try{setCustomValidity('')}catch(e){}" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="ChaveProdutos" ID="reqProductCode" ControlToValidate="txtProductCode" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <Label ID="lblSubProduto" runat="server" class="col-sm-4 text-right lbl">* Sub-Produto: </Label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlSubProductCode" Enabled="false" AutoPostBack="true" pattern="[0-9]{2}" 
                           MaxLength="2" CssClass="form-control text-field" runat="server" OnTextChanged="ddlSubProdCode_TextChanged" DataTextField="" >
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" ValidationGroup="ChaveProdutos" runat="server" ID="reqSubProdCode" ControlToValidate="ddlSubProductCode"  ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <Label id="lblProdutoDesc" runat="server" class="col-sm-4 text-right lbl">* Descritivo:</Label>
                    <div class="col-sm-6">
                        <asp:TextBox id="txtSubProductDescription" runat="server" Enabled="false" Text="ML BASE" CssClass="form-control text-field"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row form-group padding-row ">
                <div class="col-sm-4">
                    <Label id="lblDtInicio" runat="server" class="col-sm-4 text-right lbl">* Data início comercialização: </Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtDataInicioComercializacao" placeholder="0001-01-01" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="requiredformatdtini" ValidationGroup="ChaveProdutos" ControlToValidate="txtDataInicioComercializacao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        <label class="requiredFieldEmpty" style="display: none;" id="reqDataInicioComercializacao" runat="server">Formato incorreto</label>
                    </div>
                </div>
                <div class="col-sm-4">
                    <Label id="lblDtFim" runat="server" class="col-sm-4 text-right lbl">* Data fim comercialização:</Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtDataFimComercializacao" placeholder="0001-01-01" CssClass="form-control text-field dtField" MaxLength="10" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="reqEndDate" CssClass="bklabel" ValidationGroup="ChaveProdutos" ControlToValidate="txtDataFimComercializacao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        <label class="requiredFieldEmpty" style="display: none;" id="reqDataFimComercializacao" runat="server">Formato incorreto</label>
                    </div>
                </div>
                <div class="col-sm-4">
                     <div class="col-sm-6 text-right div-btns">
                        <asp:button id="btnLimpar" class="btns btns-alt " Text="Limpar" OnClick="btnLimpar_Click" CausesValidation="true" ValidationGroup="manterestecampo" runat="server"></asp:button>
                        <asp:button id="btnConsultar" class="btns " runat="server" CausesValidation="true" ValidationGroup="ChaveProdutos" AutoPostBack="true" OnClick="btnConsultar_Click" Text="Consultar"></asp:button>
                    </div>
                </div>
            </div>
            </div>
     
             <div id="divdpConsulta" runat="server">
            <div class="row form-group padding-row ">
                <div class="col-sm-6">
                    <Label id="lblPrazoMinimo" runat="server" class="col-sm-4 text-right lbl">* Prazo mínimo (meses):</Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtPrazoMinimo" MaxLength="3" CssClass="form-control text-field" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqPrazoMinimo" ControlToValidate="txtPrazoMinimo" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-6">
                    <Label id="lbPrazoMax" runat="server" class="col-sm-4 text-right lbl">* Prazo máximo (meses):</Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtPrazoMaximo" MaxLength="5" CssClass="form-control text-field" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="reqtxtPrazoMaximo" CssClass="bklabel" ControlToValidate="txtPrazoMaximo" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="row form-group padding-row ">
                <div class="col-sm-6">
                    <Label id="lblNumProd" runat="server" class="col-sm-4 text-right lbl">* Número mínimo de produtos ativar:</Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtNumeroMinimoProdutos" MaxLength="2" CssClass="form-control text-field" title="Insira um valor de 2 a 17" OnTextChanged="txtNumeroMinimoProdutos_TextChanged" value="1" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqNMinimoProdutosAtivar" ControlToValidate="txtNumeroMinimoProdutos" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-6">
                </div>
            </div>

            <div class="row form-group padding-row ">
                <div class="col-sm-6">
                    <Label id="lblLimiteMin" runat="server" class="col-sm-4 text-right lbl">* Limite minimo crédito:</Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtLimiteMinimoCredito" CssClass="form-control text-field number" MaxLength="18" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqLimMinCredito" ControlToValidate="txtLimiteMinimoCredito" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-6">
                    <Label id="lblLimiteMax" runat="server" class="col-sm-4 text-right lbl">* Limite máximo crédito:</Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtLimiteMaximoCredito" CssClass="form-control text-field number" MaxLength="18" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="reqLimMaxCredito" CssClass="bklabel" ControlToValidate="txtLimiteMaximoCredito" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="row form-group padding-row ">
                <div class="col-sm-6">
                    <Label id="lblEstadoParam" runat="server" class="col-sm-4 text-right lbl">* Estado da parameterização:</Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtEstado" Enabled="false" CssClass="form-control text-field" MaxLength="30" value="PENDENTE" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6">
                    <Label id="lblNumIncum" runat="server" class="col-sm-4 text-right lbl">* Nº dias incumprimento para inibição:</Label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtNDiasIncumprimento" onkeypress="return isNumber(event)" MaxLength="3" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="reqNdiasIncumprimento" CssClass="bklabel" ControlToValidate="txtNDiasIncumprimento" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        </div>
        <div class="row titleAccordion" id="dvtitleAcordionRenovacao" runat="server" onclick="fAccordionController();">
            <a id="titleRenovacao" runat="server" class="accordion " title="Renovação">Renovação</a>
        </div>
        <div id="divRenovacao" runat="server" class="row closeAccordion colorbck hidden ">
            <div class="col-sm-12">
                <div class="row colorbck">
                    <div class="row form-group padding-row">
                        <div class="col-sm-4">
                            <Label id="lbIndicadorRenova" runat="server" class="col-sm-4 text-right lbl">* Indicador de renovação:</Label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlIndRenovacao" OnTextChanged="ddlIndicadorRenovacao_TextChanged" DataTextField="Description" DataValueField="Code"  runat="server" CssClass="form-control text-field">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <Label id="lbPrazoRenova" runat="server" class="col-sm-4 text-right lbl">Prazo renovação (meses):</Label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtPrazoRenovacao" onkeypress="return isNumber(event)" value="12" runat="server" CssClass="form-control text-field"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqPrazoRenovacao" ControlToValidate="txtPrazoRenovacao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <Label id="lbNDiasPreAviso" runat="server" class="col-sm-4 text-right lbl">Nº de dias de pré-aviso:</Label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtNDiasPreAviso" onkeypress="return isNumber(event)" value="12" runat="server" CssClass="form-control text-field"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqNDiasPreAviso" ControlToValidate="txtNDiasPreAviso" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row titleAccordion" id="dvtitleAcordionRFinanceiro" runat="server" onclick="fAccordionController();">
            <a id="titleRiscoFinanceiro" runat="server" class="accordion " title="Risco Financeiro">Risco Financeiro</a>
        </div>
        <div id="divRiscoFinanceiro" runat="server" class="row closeAccordion colorbck hidden ">
            <div class="col-sm-12">
                <div class="row form-group padding-row">
                        <div class="col-sm-6">
                            <asp:TreeView runat="server" ID="trtipologiaProdutosRFTree" ShowCheckBoxes="All" ShowLines="True" onclick="OnTreeClick(event);" CssClass="treViewCatalogo"></asp:TreeView>
                        </div>
                    <div class="col-sm-6">
                    </div>
                </div>
            </div>
        </div>
        <div class="row titleAccordion" id="dvtitleAcordionRAssinatura" runat="server" onclick="fAccordionController();">
            <a id="titleRiscoAssinatura" runat="server" class="accordion " title="Risco Assinatura">Risco Assinatura</a>
        </div>
        <div id="divRiscoAssinatura" runat="server" class="row closeAccordion colorbck hidden ">
            <div class="col-sm-12">
                <div class="row form-group padding-row">
                    <div class="col-sm-6">
                        <asp:TreeView runat="server" ID="trtipologiaProdutosRATree" ShowCheckBoxes="All" ShowLines="True" onclick="OnTreeClick(event);" CssClass="treViewCatalogo"></asp:TreeView>
                    </div>
                </div>
            </div>
        </div>
        <div class="row titleAccordion" id="dvtitleAcordionRComercial" runat="server" onclick="fAccordionController();">
            <a id="titleRiscoComercial" runat="server" class="accordion " title="Risco Comercial">Risco Comercial</a>
        </div>
        <div id="divRiscoComercial" runat="server" class="row closeAccordion colorbck hidden ">
            <div class="col-sm-12">
                <div class="row form-group padding-row">
                            <div class="col-sm-6">
                                <asp:TreeView runat="server" ID="trtipologiaProdutosRCTree" ShowCheckBoxes="All" ShowLines="True" onclick="OnTreeClick(event);" CssClass="treViewCatalogo"></asp:TreeView>
                            </div>
                   </div>
             </div>
        </div>
        <br />
        <div id="divPeriocidadeCobranca" runat="server" class="row ">
            <div class="col-sm-6">
                <label class="col-sm-6 text-right lbl">* Periocidade de cobrança comissão de gestão (meses):</label>
                <div class="col-sm-6">
                    <%--<asp:TextBox ID="txtPeriocidadeCobranca" MaxLength="3" value="1" CssClass="form-control text-field" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList runat="server" ID="ddlPeriocidadeCobranca" CssClass="form-control text-field" DataTextField="Description" DataValueField="Code"></asp:DropDownList>
                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqPeriocidadeCobranca" ControlToValidate="ddlPeriocidadeCobranca" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <hr class="hr" id="hr" runat="server" />

        <div id="acoes_ml01" runat="server" class="row text-right div-btns">
            <asp:button id="btnCriar"  CssClass="btns " runat="server" Text="Criar" Visible="false" OnClick="btnCreate_Click"></asp:button>
            <asp:button id="btnEdit" CssClass="btns " runat="server" OnClick="btnEdit_Click" Visible="false" Text="Modificar"></asp:button>
        </div>

         <hr class="hr" id="hr1" runat="server" />

        <div id="filtrospesquisa" runat="server" class="row">
            <div class="Table">
                <div class="Row">
                    <div class="CellNoBorder">
                        <h4>Linqs úteis</h4>
                        <ul>
                            <li>
                                <a id="lqComissaoAberura" runat="server" target="_blank" href='<%= ConfigurationSettings.AppSettings["LinqComissaoAberura"] %>' >Comissão de abertura Multilinha</a>
                            </li>
                            <li>
                                <a id="lqComissaoRenovao" runat="server" target="_blank" href='<%= ConfigurationSettings.AppSettings["LinqComissaoRenovacao"] %>' >Comissão de renovação Multilinha</a>
                            </li>
                            <li>
                               <a id="lqComissaoGestaoContrato" runat="server" target="_blank" href='<%= ConfigurationSettings.AppSettings["LinqComissaoGestaoContrato"] %>' >Comissão de gestão de contrato Multilinha</a>
                            </li>
                            <li>
                                <a id="lqComissaoNovacao" runat="server" target="_blank" href='<%= ConfigurationSettings.AppSettings["LinqComissaoNovacao"] %>' >Comissão de novação Multilinha</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </form>

    <script type='text/javascript'>
        var dtfechas = "<%=this.dtfechas %>";
        $('#dpEndDate').datepicker('setDate', '9999-12-31');
    </script>
    <script src="scripts/multilinha.js"></script>
   
</body>
</html>
