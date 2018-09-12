<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LM33ContratoML.aspx.cs" Inherits="Multilinha.LM33ContratoMLaspx" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/ucCancelamentoContrato.ascx" TagPrefix="uc1" TagName="ucCancelamentoContrato" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contrato Multilinha</title>
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <link href="css/multilinha.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-1.12.4.min.js"></script>
    <script src="scripts/jquery-ui.js"></script>
    <script src="scripts/bootstrap.js"></script>
</head>
<uc1:header runat="server" ID="header" />
<body>
    <form id="form1" class="content container-fluid form-horizontal" runat="server">
        <div id="dvError" runat="server">
            <asp:Label runat="server" ID="lberror" CssClass="col-md-12 col-sm-12 lbl" Visible="false ">Occur an error</asp:Label>
        </div>
        <div class="clear"></div>
        <br />
        <div class="row  titleTransaction menu">
            <ul>
                <li id="liParameterizacao" runat="server" class=" ">
                    <asp:LinkButton CssClass="atab " ID="lblTransaction" Enabled="false" runat="server" Text="Criação de Contrato"></asp:LinkButton>
                </li>
                <li id="liModificacao" runat="server" class=" ">
                    <asp:LinkButton CssClass="atab " ID="lblTransactionM" Enabled="false" runat="server" Text="Modificação de Contrato"></asp:LinkButton>
                </li>
                <li id="liConsulta" runat="server" class=" ">
                    <asp:LinkButton CssClass="atab " ID="lblTransactionV" Enabled="false" runat="server" Text="Consulta de Contrato"></asp:LinkButton>
                </li>
                <li id="liDenuncia" runat="server" class=" ">
                    <asp:LinkButton CssClass="atab " ID="lblTransactionD" Enabled="false" runat="server" Text="Denúncia / resolução de Contrato"></asp:LinkButton>
                </li>
            </ul>
        </div>
        <div id="MC33C" runat="server">
            <div class="row colorbck" id="camposChave" runat="server">
                <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="bkCliente" runat="server" class="col-sm-4 text-right lbl">* Cliente: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtCliente" MaxLength="7" onkeypress="return isNumber(event)" AutoPostBack="true" OnTextChanged="txtCliente_TextChanged" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChaves" ID="reqCliente" ControlToValidate="txtCliente" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <label id="Label3" runat="server" class=""></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtNome" CssClass="form-control text-field" Enabled="false" MaxLength="40" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="divProduto" runat="server" class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="bkProduto" runat="server" class="col-sm-4 text-right lbl">* Produto: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtProdutoml" MaxLength="2" Text="01" pattern="[A-Za-z0-9]{2}" title="Deve inserir o código do produto multilinha"
                                CssClass="form-control text-field" runat="server" AutoPostBack="true" OnTextChanged="txtproduto_TextChanged"></asp:TextBox>
                             <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChaves" ID="reqProdutoml" ControlToValidate="txtProdutoml" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="bkSubProduto" runat="server" class="col-sm-4 text-right lbl">* Sub-Produto: </label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSubprodutoml" AutoPostBack="true" pattern="[0-9]{2}"
                                MaxLength="2" CssClass="form-control text-field" runat="server" OnTextChanged="ddlSubProdCode_TextChanged" DataTextField="">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lblProdutoDesc" runat="server" class="col-sm-4 text-right lbl">* Descritivo:</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtDescritivo" runat="server" Enabled="false" Text="ML BASE" CssClass="form-control text-field"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="divIDMultilinha" runat="server" class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="lbidmultilinha" runat="server" class="col-sm-4 text-right lbl">* ID Multilinha ML: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_idmultilinha" MaxLength="12" AutoPostBack="true" OnTextChanged="txt_idmultilinha_TextChanged" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqidmultilinha" ValidationGroup="valChaves" ControlToValidate="txt_idmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div id="divIDSimulacao" runat="server" class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="lbIdSimulacao" runat="server" class="col-sm-4 text-right lbl">ID Simulação ML: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtIdSimulacao" MaxLength="9" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqIdSimulacao" ValidationGroup="valChavesSim" ControlToValidate="txtIdSimulacao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-4"></div>
                    <div class="col-sm-4 div-btns text-center">
                        <div class="col-sm-6 div-btns">
                            <asp:Button ID="btnSimulacao" runat="server" class="btns text-center" OnClick="btnSimulacao_Click" Visible="false" CausesValidation="true" ValidationGroup="valChavesSim" Text="Simulação"></asp:Button>
                            <asp:Button ID="btnSearch" class="btns text-center" runat="server" CausesValidation="true" ValidationGroup="valChaves" AutoPostBack="True" OnClick="btnSearchDO_Click" Text="Consultar"></asp:Button>
                        </div>
                    </div>
                    <hr class="hr" id="hr" runat="server" />
                </div>
            </div>
          
            <div id="dpOK" visible="true" runat="server">
                <div class=" row colorbck" id="camposClienteContrato">
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-4">
                            <label id="bklNDO" runat="server" class="col-sm-4 text-right lbl">* Nº DO: </label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlncontado" MaxLength="9" CssClass="form-control text-field dtField" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqddlncontado" ValidationGroup="valChavesClienteContrato" ControlToValidate="ddlncontado" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                        <label id="bklIdworkflow" runat="server" class="col-sm-4 text-right lbl">Nº Proposta: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtIdworkflow" MaxLength="9" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                        </div>
                         <div class="col-sm-4">
                        <label id="bklEstadoContrato" runat="server" class="col-sm-4 text-right lbl">* Estado do Contrato: </label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlEstadoContrato" Enabled="false" CssClass="form-control text-field" DataTextField="Description" DataValueField="Code" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-4">
                            <label id="lbNumeroMinimoProdutos" runat="server" class="col-sm-4 text-right lbl">* Número mínimo de produtos ativar: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtNumeroMinimoProdutos" Enabled="false" ReadOnly="true" MaxLength="1" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-4">
                            <label id="bkldtiniciocontrato" runat="server" class="col-sm-4 text-right lbl">* Data início de contrato: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtdatainiciocontrato" placeholder="0001-01-01" MaxLength="10" CssClass="form-control text-field dtField" AutoPostBack="true" OnTextChanged="txtdatainiciocontrato_TextChanged" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChavesClienteContrato" ID="reqdatainiciocontrato" ControlToValidate="txtdatainiciocontrato" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                <label class="requiredFieldEmpty" style="display: none;" id="reqemdatainiciocontrato" runat="server">Formato incorreto</label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bklbprazocontrato" runat="server" class="col-sm-4 text-right lbl">* Prazo contrato (meses):</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtprazocontrato" MaxLength="3" CssClass="form-control text-field" OnTextChanged="txtprazocontrato_TextChanged" AutoPostBack="true" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" ValidationGroup="valChavesClienteContrato" runat="server" ID="reqPrazoContrato" ControlToValidate="txtprazocontrato" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="Label2" runat="server" class="col-sm-4 text-right lbl">* Data Processamento:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtdataProcessamento" MaxLength="3" Enabled="false" ReadOnly="false" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-4">
                            <label id="bkldtfimcontrato" runat="server" class="col-sm-4 text-right lbl">* Data fim de contrato: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtdatafimcontrato" ReadOnly="true" Enabled="false" placeholder="9999-12-31" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bklindicadorrenovacao" runat="server" class="col-sm-4 text-right lbl">* Indicador de renovação: </label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlIndRenovacao" runat="server" CssClass="form-control text-field" DataTextField="Description" DataValueField="Code">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bklprazorenovacao" runat="server" class="col-sm-4 text-right lbl">* Prazo renovação (meses):</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtPrazoRenovacao" MaxLength="3" Enabled="false" ReadOnly="true" CssClass="form-control text-field" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPrazoRenovacao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-4">
                            <label id="bkldatarenovacao" runat="server" class="col-sm-4 text-right lbl">* Data renovação: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtdatarenovacao" ReadOnly="true" Enabled="false" placeholder="9999-12-31" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bklndiasincumprimento" runat="server" class="col-sm-4 text-right lbl">* Nº dias incumprimento:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtNDiasIncumprimento" MaxLength="3" CssClass="form-control text-field" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChavesClienteContrato" ID="reqndiasincumprimento" ControlToValidate="txtNDiasIncumprimento" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="lbNdiaspreAviso" runat="server" class="col-sm-4 text-right lbl">* Nº dias de pré-aviso denúncia::</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtNDiasPreAviso" MaxLength="3" CssClass="form-control text-field" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChavesClienteContrato" ID="reqNDiasPreAviso" ControlToValidate="txtNDiasPreAviso" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                         <div class="col-sm-4">
                            <label id="lbContratoDenunicado" runat="server" class="col-sm-4 text-right lbl">* Contrato Denunciado: </label>
                            <div class="col-sm-6">
                                <asp:DropDownlist ID="ddlContratoDenunciado" Enabled="false" CssClass="form-control text-field" runat="server">
                                    <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="Não>"></asp:ListItem>
                                </asp:DropDownlist>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="lbDataVencimentoDenuncia" runat="server" class="col-sm-4 text-right lbl">Data de Vencimento de denúncia:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtDataVencimentoDenuncia" MaxLength="3" Enabled="false" CssClass="form-control text-field" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-4">
                            <label id="bkllimglobalmultilinha" runat="server" class="col-sm-4 text-right lbl">* Limite Global Multilinha: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtlimiteglobalmultilinha" CssClass="form-control text-field number" Text="0,00" MaxLength="16" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChavesClienteContrato" ID="reqLimMaxCredito" ControlToValidate="txtlimiteglobalmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bklgraumorosidade" runat="server" class="col-sm-4 text-right lbl">* Grau Morosidade:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtgraumorosidade" MaxLength="3" ReadOnly="true" Enabled="false" Text="000" CssClass="form-control text-field" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqgraumorosidade" ControlToValidate="txtgraumorosidade" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                        <label class="col-sm-4 text-right lbl">* Versão da minuta de contrato:</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_NMinutaContrato" MaxLength="12" Enabled="false" ReadOnly="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-4">
                            <label id="bkllimRiscoFinanceiro" runat="server" class="col-sm-4 text-right lbl">* Sublimite Risco Financeiro: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtsublimiteriscoFinanceiro" CssClass="form-control text-field number" MaxLength="16" AutoPostBack="true"  runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChavesClienteContrato" ID="reqsublimiteriscoFinanceiro" ControlToValidate="txtsublimiteriscoFinanceiro" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                <asp:Label ID="lberrorSRF" runat="server" CssClass="bklabel" Visible="false" ForeColor="red"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-4">
                            <label id="bkllimRiscoComercial" runat="server" class="col-sm-4 text-right lbl">* Sublimite Risco Comercial: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtsublimitriscoComercial" CssClass="form-control text-field number" MaxLength="16" AutoPostBack="true"  runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChavesClienteContrato" ID="reqsublimitriscoComercial" ControlToValidate="txtsublimitriscoComercial" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                <asp:Label ID="lberrorSRC" runat="server" CssClass="bklabel" Visible="false" ForeColor="red"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-4">
                            <label id="bkllimiteriscoassinatura" runat="server" class="col-sm-4 text-right lbl">* Sublimite Risco Assinatura: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtsublimiteriscoAssinatura" CssClass="number form-control text-field" MaxLength="16" AutoPostBack="true" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="valChavesClienteContrato" ID="reqsublimiteriscoAssinatura" ControlToValidate="txtsublimiteriscoAssinatura" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                                <asp:Label ID="lberrorSRA" runat="server" CssClass="bklabel" Visible="false" ForeColor="red"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-sm-12">
                        <div class="col-sm-12 text-center">
                            <asp:Button ID="btnConfirmar" class="btns text-center" runat="server" CausesValidation="true" OnClientClick="return ValidaMontantes();" ValidationGroup="valChavesClienteContrato"  AutoPostBack="True" OnClick="btnConfirmar_Click" Text="Confirmar"></asp:Button>
                        </div>
                    </div>
                </div>
                <div class="row titleAccordion" id="dvtitleAcordionRFinanceiro" runat="server" onclick="fAccordionController();">
                    <a id="titleRiscoFinanceiro" runat="server" class="accordion " title="Risco Financeiro">Risco Financeiro</a>
                </div>
                <div id="divRiscoFinanceiro" runat="server" class="row closeAccordion colorbck hidden ">
                    <div class="col-sm-12">
                        <div class="row form-group padding-row">
                            <div class="col-sm-10 treViewCatalogo">
                                <asp:ListView ID="lvProdutosRisco" runat="server">
                                    <EmptyDataTemplate>Não existem resultados para a pesquisa efetuada.</EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="Table Grid" id="tbProdRiscosFinanceiro">
                                            <div class="Heading Color Row">
                                                <div class="Cell Grid col-6">
                                                    <label class="col-6" id="produto">Família Produto</label>
                                                </div>
                                                <div class="Cell Grid col-6">
                                                    <label class="col-6" id="subproduto">Produto\Subproduto</label>
                                                </div>
                                                <div class="Cell Grid col-6">
                                                    <label class="col-6">Condições Gerais</label>
                                                </div>
                                                <div class="Cell Grid col-6">
                                                    <label class="col-6">Condições Particulares</label>
                                                </div>

                                            </div>
                                            <div class="Row" runat="server" id="itemPlaceholder">
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                                            <div class="Cell Grid col-6">
                                                <asp:Label runat="server" ID="lbProduto" CssClass="col-6" Text='<%# Eval("produto") %>'></asp:Label>
                                            </div>
                                            <div class="Cell Grid col-6">
                                                <asp:Label runat="server" ID="lbSubproduto" CssClass="col-6" Text='<%# Eval("subproduto") %>'></asp:Label>
                                            </div>
                                            <div class="Cell Grid col-6 text-center">
                                                <asp:CheckBox runat="server" ID="lbCGeral" CssClass="col-6" Enabled='<%# Eval("cGEnable") %>' Checked='<%# Eval("isGeral") %>'></asp:CheckBox>
                                            </div>
                                            <div class="Cell Grid col-6 text-center">
                                                <asp:CheckBox runat="server" ID="lbCParticular" CssClass="col-6" Enabled='<%# Eval("cPEnable") %>' Checked='<%# Eval("isParticular") %>'></asp:CheckBox>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
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
                            <div class="col-sm-10 treViewCatalogo">
                                <asp:ListView ID="lvProdutosRiscoAssinatura" runat="server">
                                    <EmptyDataTemplate>Não existem resultados para a pesquisa efetuada.</EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="Table Grid" id="tbProdRiscosAssinatura">
                                            <div class="Heading Color">
                                                <div class="Cell Grid col-6">
                                                    <label>Família Produto</label>
                                                </div>
                                                <div class="Cell Grid col-6">
                                                    <label id="subproduto">Produto\Subproduto</label>
                                                </div>
                                                <div class="Cell Grid col-6">
                                                    <label>Condições Gerais</label>
                                                </div>
                                                <div class="Cell Grid col-6">
                                                    <label>Condições Particulares</label>
                                                </div>

                                            </div>
                                            <div class="Row" runat="server" id="itemPlaceholder">
                                            </div>
                                        </div>
                                     
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                                            <div class="Cell Grid col-6">
                                                <asp:Label runat="server" ID="lbProduto" Text='<%# Eval("produto") %>'></asp:Label>
                                            </div>
                                            <div class="Cell Grid col-6">
                                                <asp:Label runat="server" ID="lbsubproduto" Text='<%# Eval("subproduto") %>'></asp:Label>
                                            </div>

                                            <div class="Cell Grid col-6 text-center">
                                                <asp:CheckBox runat="server" ID="lbCGeral" Enabled='<%# Eval("cGEnable") %>' Checked='<%# Eval("isGeral") %>'></asp:CheckBox>
                                            </div>
                                            <div class="Cell Grid col-6 text-center">
                                                <asp:CheckBox runat="server" ID="lbCParticular" Enabled='<%# Eval("cPEnable") %>' Checked='<%# Eval("isParticular") %>'></asp:CheckBox>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
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
                            <div class="col-sm-10 treViewCatalogo">
                                <asp:ListView ID="lvProdutosRiscoComercial" runat="server">
                                    <EmptyDataTemplate>Não existem resultados para a pesquisa efetuada.</EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="Table Grid" id="tbProdRiscosComercial">
                                            <div class="Heading Color col-6">
                                                <div class="Cell Grid col-6">
                                                    <label>Produto</label>
                                                </div>
                                                <div class="Cell Grid col-6">
                                                    <label id="subproduto">Produto\Subproduto</label>
                                                </div>
                                                <div class="Cell Grid col-6">
                                                    <label>Condições Gerais</label>
                                                </div>
                                                <div class="Cell Grid col-6">
                                                    <label>Condições Particulares</label>
                                                </div>
                                            </div>
                                            <div class="Row" runat="server" id="itemPlaceholder">
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                                            <div class="Cell Grid col-6">
                                                <asp:Label runat="server" ID="lbProduto" Text='<%# Eval("produto") %>'></asp:Label>
                                            </div>
                                            <div class="Cell Grid col-6">
                                                <asp:Label runat="server" ID="lbsubproduto" Text='<%# Eval("subproduto") %>'></asp:Label>
                                            </div>
                                            <div class="Cell Grid col-6 text-center">
                                                <asp:CheckBox runat="server" ID="lbCGeral" Enabled='<%# Eval("cGEnable") %>' Checked='<%# Eval("isGeral") %>'></asp:CheckBox>
                                            </div>
                                            <div class="Cell Grid col-6 text-center">
                                                <asp:CheckBox runat="server" ID="lbCParticular" Enabled='<%# Eval("cPEnable") %>' Checked='<%# Eval("isParticular") %>'></asp:CheckBox>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
                <hr id="hr3" runat="server" class="hr" />
                <div class="row titleAccordion" id="dvtitleComissoes" runat="server" onclick="fAccordionController();">
                    <a id="titleComissoes" runat="server" class="accordion " title="Comissões">Comissões</a>
                </div>
                <div id="divComissoes" runat="server" class="row closeAccordion colorbck hidden ">
                    <div class="row form-group padding-row ">
                        <div class="col-sm-4">
                            <label id="bklcomissaoabertura" runat="server" class="col-sm-4 text-right lbl">Comissão abertura: </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtcomissaoabertura" Enabled="false" ReadOnly="true" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bklvalorimposto" runat="server" class="col-sm-4 text-right lbl">Valor imposto:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtvalorimpostocomabert" MaxLength="3" Enabled="false" ReadOnly="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bklbaseincidenciacomabert" runat="server" class="col-sm-4 text-right lbl">Base de incidência: </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtbaseincidenciacomabert" ReadOnly="true" Enabled="false" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <a id="lkComissaoAbertura" runat="server" href="#" class="btn" target="_blank" title="Link para negociação da comissão de abertura de conta">>></a>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-cm-12">
                        <div class="col-sm-4">
                            <label id="bklcomissaogestaocontrato" runat="server" class="col-sm-4 text-right lbl">Comissão gestão contrato: </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtcomissaogestaocontrato" Enabled="false" ReadOnly="true" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bklvalorimpostocomgestcontrato" runat="server" class="col-sm-4 text-right lbl">Valor imposto:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtvalorimpostocomgestcontrato" MaxLength="3" Enabled="false" ReadOnly="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bklbaseincidenciacomgestcontrato" runat="server" class="col-sm-4 text-right lbl">Base de incidência: </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtbaseincidenciacomgestcontrato" ReadOnly="true" Enabled="false" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-sm-1">
                                <a id="lkComissaoGestao" href="#" class="btn" target="_blank" title="Link para negociação da comissão de gestão contrato" runat="server">>></a>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-cm-12">
                         <div class="col-sm-4"></div>
                        <div class="col-sm-4">
                            <label id="bkLPeriocidadeCobranca" runat="server" class="col-sm-4 text-right lbl">* Periocidade da comissão de gestão contrato (meses): </label>
                            <div class="col-sm-4">
                              <asp:DropDownList runat="server" ID="ddlPeriocidadeCobrancagestcontrato" CssClass="form-control text-field" DataTextField="Description" DataValueField="Code"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqPeriocidadeCobranca" ControlToValidate="ddlPeriocidadeCobrancagestcontrato" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="bkLdataproximacobranca" runat="server" class="col-sm-4 text-right lbl">* Data próxima cobrança da comissão de gestão de contrato:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtdataproximacobrancagestcontrato" placeholder="0001-01-01" Enabled="false" ReadOnly="true" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group padding-row col-cm-12">
                       
                         <div class="col-sm-4">
                            <label id="lbrenovacao" runat="server" class="col-sm-4 text-right lbl">Comissão de renovação: </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtcomissaorenovacao" Enabled="false" ReadOnly="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtcomissaorenovacao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="col-sm-4">
                            <label id="lbvalorimpostocomgestrenovacao" runat="server" class="col-sm-4 text-right lbl">Valor imposto:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtvalorimpostocomgestrenovacao" MaxLength="3" Enabled="false" ReadOnly="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label id="lbbaseincidenciacomgestrenovacao" runat="server" class="col-sm-4 text-right lbl">Base de incidência: </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtbaseincidenciacomgestrenovacao" ReadOnly="true" Enabled="false" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-sm-1">
                                <a id="lkComissaorenovacao" href="#" class="btn" target="_blank" title="Link para negociação da comissão de renovação" runat="server">>></a>
                            </div>
                        </div>
                     </div>
                     <div class="row form-group padding-row col-cm-12">
                        <div class="col-sm-4">
                            <label id="lbPeriocidadeCobrancagestRenovacao" runat="server" class="col-sm-4 text-right lbl">* Periocidade da comissão de renovação: </label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlPeriocidadeCobrancagestRenovacao" CssClass="form-control text-field" DataTextField="Description" DataValueField="Code"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqPeriocidadeCobrancagestRenovacao" ControlToValidate="ddlPeriocidadeCobrancagestRenovacao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="col-sm-4">
                            <label id="lbdataproximacobrancagestrenovacao" runat="server" class="col-sm-4 text-right lbl">Data próxima cobrança da comissão de gestão de renovação:</label>
                            <div class="col-sm-4">
                                 <asp:TextBox ID="txtdataproximacobrancagestrenovacao" placeholder="0001-01-01" Enabled="false" ReadOnly="true" MaxLength="10" CssClass="form-control text-field dtField" runat="server"></asp:TextBox>
                            </div>
                        </div>
                     </div>
                </div>
            </div>
            <br />
            <div class="row" runat="server" id="divVersoesML">
                    <div class="col-sm-4">
                        <label class="col-sm-6 text-right lbl">* ID Multilinha:</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtidmultilinha" MaxLength="12" Enabled="false" ReadOnly="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-sm-4">
                        <label class="col-sm-6 text-right lbl">* Versão da minuta de contrato:</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtNMinutaContrato" MaxLength="12" onkeypress="return isNumber(event)"  CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqtxtNMinutaContrato" ControlToValidate="txtNMinutaContrato" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <hr id="hr4" runat="server" class="hr" />
        </div>
            <div id="accoesfinais_criarml03" runat="server">
                <div class="row text-right div-btns">
                    <asp:Button ID="btnCriar" CssClass="btns" runat="server" Text="Criar Contrato" Visible="false" ValidationGroup="valChavesClienteContrato" OnClientClick="ToTopOfPage();" OnClick="btnCriar_Click" CausesValidation="true"></asp:Button>
                    <asp:Button ID="btnModificar" CssClass="btns" runat="server" Text="Modificar" Visible="false" ValidationGroup="" OnClick="btnModificar_Click" CausesValidation="true"></asp:Button>
                    <asp:Button ID="btnSeguinte" CssClass="btns" runat="server" Text="Seguinte" Enabled="false" OnClick="btnSeguinte_Click" CausesValidation="true"></asp:Button>

                </div>
            </div>
            <div id="ml03V_denuncia" visible="false" runat="server">
            <uc1:ucCancelamentoContrato runat="server" ID="ucCancelamentoContrato" />
        </div>
    </form>
    <script type='text/javascript'>
        var dtfechas = "<%=this.dtfechas %>";
    </script>
    <script src="scripts/multilinha.js"></script>
    <script type="text/javascript">
    </script>
</body>
</html>
