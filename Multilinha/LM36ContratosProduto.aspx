<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LM36ContratosProduto.aspx.cs" Inherits="Multilinha.LM36ContratosProduto" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contratos Produto Associados</title>
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
        <div class="row titleTransaction menu">
                    <ul>
                        <li>
                            <asp:LinkButton CssClass="atab " ID="lblTransaction" Enabled="false" runat="server" Text="Contrato ML - Contratos de Produtos Associados"></asp:LinkButton>
                        </li>
                    </ul>
        </div>
        <div id="lm36C" runat="server">
        <div class="row colorbck">
            <div id="camposChave" runat="server">
                <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="lbCliente" runat="server" class="col-sm-4 text-right lbl">* Cliente: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtCliente" MaxLength="7" pattern="[A-Za-z0-9]{7}" OnTextChanged="txtCliente_TextChanged" title="Deve inserir um numéro de cliente"
                                oninvalid="setCustomValidity('Deve inserir um numéro  de cliente válido')"
                                onchange="try{setCustomValidity('')}catch(e){}" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="ChaveProdutos" ID="reqNumCliente" ControlToValidate="txtCliente" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                        
                    </div>
                    <div class="col-sm-3">
                            <asp:TextBox ID="txtNome" runat="server" Enabled="false" CssClass="form-control text-field"></asp:TextBox>
                        </div>
                </div>
                <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-4">
                        <label id="lblIdMultinha" runat="server" class="col-sm-4 text-right lbl">* ID Multinha: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtidmultilinha" MaxLength="12" OnTextChanged="txtidmultilinha_TextChanged" pattern="[A-Za-z0-9]{12}" title="Deve inserir um código alfanumérico com doze posições" oninvalid="setCustomValidity('Deve inserir um código alfanumérico com doze posições')"
                            onchange="try{setCustomValidity('')}catch(e){}" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqIDMultinha" ValidationGroup="ChaveProdutos" ControlToValidate="txtidmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-6"><div class="col-sm-4"></div></div>
                </div>
                <div class="row form-group padding-row col-sm-12">
                     <div class="col-sm-4">
                          <label id="lbTipoRisco" runat="server" class="col-sm-4 text-right lbl">* Tipo de Risco: </label>
                         <div class="col-sm-6">
                             <asp:DropDownList OnTextChanged="ddlTipoRisco_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control text-field" ID="ddlTipoRisco">
                                 <asp:ListItem Text="Comercial" Value="Risco Comercial"></asp:ListItem>
                                 <asp:ListItem Text="Assinatura" Value="Risco Assinatura"></asp:ListItem>
                                 <asp:ListItem Text="Financeiro" Value="Risco Financeiro"></asp:ListItem>
                             </asp:DropDownList>
                         </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lbTipoFamilia" runat="server" class="col-sm-4 text-right lbl"> * Tipologia</label>
                        <div class="col-sm-6">
                             <asp:DropDownList runat="server" ID="ddlTipoFam" CssClass="form-control text-field" ></asp:DropDownList>
                            </div>
                    </div>
                       <div class="col-sm-4">
                        <label id="lbEstadoContrato" runat="server" class="col-sm-4 text-right lbl"> * Estado Contrato</label>
                        <div class="col-sm-6">
                             <asp:DropDownList runat="server" ID="ddlEstadoContrato" CssClass="form-control text-field">
                                  <asp:ListItem Text="TODOS" Value=""></asp:ListItem>
                                 <asp:ListItem Text="ACTIVO" Value="A"></asp:ListItem>
                                 <asp:ListItem Text="INATIVO" Value="I"></asp:ListItem>
                             </asp:DropDownList>
                            </div>
                    </div>
                </div>
                <div class="row form-group padding-row col-sm-12">
                    <div class="col-sm-6"></div>
                      <div class="col-sm-6">
                        <div class="col-sm-4 row div-btns">
                            <asp:Button ID="btnLimpar" class="btns btns-alt " Text="Limpar" OnClick="btnLimpar_Click" CausesValidation="true" ValidationGroup="manterestecampo" runat="server"></asp:Button>
                            <asp:Button ID="btnConsultar" class="btns " runat="server" CausesValidation="true" ValidationGroup="ChaveProdutos" AutoPostBack="true" OnClick="btnConsultar_Click" Text="Consultar"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr" id="hr1" runat="server" />
            <br />
            <div id="dpOK" runat="server">
                  <div id="dvConsultaProdutos" class="row form-group col-sm-12" style="padding-left:40px" runat="server">
             <asp:ListView ID="lvConsultaProdutos" runat="server" class="row form-group padding-row">
                <EmptyDataTemplate>Não foram encontrados resultados</EmptyDataTemplate>
                <LayoutTemplate>
                    <div class="Table Grid col-sm-12" id="tbAprovacoes">
                        <div class="Heading Color">
                            <div class="Cell Grid">
                                <label>Tipo de Risco</label>
                            </div>
                            <div class="Cell Grid">
                                <label>Tipologia</label>
                            </div>
                            <div class="Cell Grid">
                                <label>Produto/SubProduto</label>
                            </div>
                              <div class="Cell Grid">
                                <label>Nº Contrato</label>
                            </div>
                              <div class="Cell Grid">
                                <label>Estado Contrato</label>
                            </div>
                              <div class="Cell Grid">
                                <label>Grau Morosidade</label>
                            </div>
                            <div class="Cell Grid">
                                <label>DPD</label>
                            </div>
                             <div class="Cell Grid">
                                <label>Valor Comprometido</label>
                            </div>
                             <div class="Cell Grid">
                                <label>Valor Contratado</label>
                            </div>
                             <div class="Cell Grid">
                                <label>Exposição Atual</label>
                            </div>
                        </div>
                        <div class="Row" runat="server" id="itemPlaceholder">
                        </div>
                   </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbTipoRisco" CssClass="text-center bklabel" Text='<%# Eval("TipoRisco") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbFamiliaProduto" Width="250px" CssClass="text-center bklabel" Text='<%# Eval("FamiliaProduto") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbSubProduto"  CssClass="text-center bklabel" Text='<%# Eval("SubProduto") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbNContratoProduto" CssClass="text-center bklabel" Text='<%# Eval("NContratoProduto") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbEstadoContratoProduto" CssClass="text-center bklabel" Text='<%# Eval("EstadoContratoProduto") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbGrauMorosidade" CssClass="text-center bklabel" Text='<%# Eval("GrauMorosidade") %>'></asp:Label>
                            </div>
                        <div class="Cell Grid">
                            <asp:Label runat="server" ID="lbDPD" CssClass="text-center bklabel" Text='<%# Eval("DPD") %>'></asp:Label>
                        </div>
                        <div class="Cell Grid">
                            <asp:Label runat="server" ID="lbValorComprometido" CssClass="text-center bklabel" Text='<%# Eval("ValorComprometido") %>'></asp:Label>
                        </div>
                        <div class="Cell Grid">
                            <asp:Label runat="server" ID="lbValorContratado" CssClass="text-center bklabel" Text='<%# Eval("ValorContratado") %>'></asp:Label>
                        </div>
                        <div class="Cell Grid">
                            <asp:Label runat="server" ID="lbExposicaoAtual" CssClass="text-center bklabel" Text='<%# Eval("ExposicaoAtual") %>'></asp:Label>
                        </div>
                      <%--  <asp:HiddenField runat="server" ID="hfutilizador" Value='<%# Eval("utilizador") %>'/>--%>
                        </div>
                </ItemTemplate>
            </asp:ListView>
            <div style="text-align:center">
                <asp:linkbutton runat="server" id="lkpaginaanterior" OnClick="lkpaginaanterior_Click" Text="Página Anterior" visible="false"></asp:linkbutton>
                <asp:linkbutton runat="server" id="lkpaginaseguinte" OnClick="lkpaginaseguinte_Click" Text="Página Seguinte" visible="false"></asp:linkbutton>
            </div>
            </div>
            </div>
        </div>
        </div>

    </form>
</body>