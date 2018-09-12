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
                <div class="row form-group padding-row ">
                    <div class="col-sm-6">
                        <label id="lbCliente" runat="server" class="col-sm-3 text-right lbl">* Cliente: </label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtCliente" MaxLength="7" pattern="[A-Za-z0-9]{7}" OnTextChanged="txtCliente_TextChanged" title="Deve inserir um numéro de cliente"
                                oninvalid="setCustomValidity('Deve inserir um numéro  de cliente válido')"
                                onchange="try{setCustomValidity('')}catch(e){}" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="ChaveProdutos" ID="reqNumCliente" ControlToValidate="txtCliente" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtNome" runat="server" Enabled="false" CssClass="form-control text-field"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-6">
                        <label id="lblIdMultinha" runat="server" class="col-sm-3 text-right lbl">* ID Multinha: </label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtidmultilinha" MaxLength="12" pattern="[A-Za-z0-9]{12}" title="Deve inserir um código alfanumérico com doze posições" oninvalid="setCustomValidity('Deve inserir um código alfanumérico com doze posições')"
                            onchange="try{setCustomValidity('')}catch(e){}" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqIDMultinha" ValidationGroup="ChaveProdutos" ControlToValidate="txtidmultilinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-6"><div class="col-sm-4"></div></div>
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
            </div>
        </div>
        </div>

    </form>
</body>