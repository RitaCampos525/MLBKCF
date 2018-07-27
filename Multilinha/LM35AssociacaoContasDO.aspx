﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LM35AssociacaoContasDO.aspx.cs" Inherits="Multilinha.LM35AssociacaoContasDO" %>


<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manutenção Associação de Contas DO</title>
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
            <asp:Label ID="lblTransaction" runat="server">Manutenção Associação de Contas DO</asp:Label>
        </div>
        <div class="row colorbck">
            <div id="camposChave" runat="server">
                <div class="row form-group padding-row ">
                    <div class="col-sm-6">
                        <label id="lbCliente" runat="server" class="col-sm-3 text-right lbl">* Cliente: </label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtNumCliente" MaxLength="9" pattern="[A-Za-z0-9]{9}" OnTextChanged="txtNumCliente_TextChanged" title="Deve inserir um numéro de cliente"
                                oninvalid="setCustomValidity('Deve inserir um numéro  de cliente válido')"
                                onchange="try{setCustomValidity('')}catch(e){}" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="ChaveProdutos" ID="reqNumCliente" ControlToValidate="txtNumCliente" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtClienteDescription" runat="server" Enabled="false" CssClass="form-control text-field"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-6">
                        <label id="lblIdMultinha" runat="server" class="col-sm-3 text-right lbl">* ID Multinha: </label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtIDMultinha" MaxLength="10" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="reqIDMultinha" ValidationGroup="ChaveProdutos" ControlToValidate="txtIDMultinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row div-btns">
                    <asp:Button ID="btnLimpar" class="btns btns-alt " Text="Limpar" OnClick="btnLimpar_Click" CausesValidation="true" ValidationGroup="manterestecampo" runat="server"></asp:Button>
                    <asp:Button ID="btnConsultar" class="btns " runat="server" CausesValidation="true" ValidationGroup="ChaveProdutos" AutoPostBack="true" OnClick="btnConsultar_Click" Text="Consultar"></asp:Button>
                </div>
            </div>
            <hr class="hr" id="hr1" runat="server" />
            <br />
            <div class="row form-group padding-row ">
                    <div class="col-sm-6">
                        <label id="lblNumContaBO" runat="server" class="col-sm-6 text-right lbl">Nº Conta BO Principal: </label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtNumContaBO" MaxLength="10" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ID="RequiredFieldValidator1" ValidationGroup="ChaveProdutos" ControlToValidate="txtIDMultinha" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            <div class="row form-group padding-row text-center"">
                <div class="col-sm-10 treViewCatalogo">
                    <asp:ListView ID="lvAssociados" runat="server">
                        <EmptyDataTemplate>Não existem resultados para a pesquisa efetuada.</EmptyDataTemplate>
                        <LayoutTemplate>
                            <div class="Table Grid" id="tbAssociados">
                                <div class="Heading Color Row">
                                    <div class="Cell Grid col-6">
                                        <label class="col-6" id="produto">Associado</label>
                                    </div>
                                    <div class="Cell Grid col-6">
                                        <label class="col-6" id="subproduto">Nº Conta DO</label>
                                    </div>
                                    <div class="Cell Grid col-6">
                                        <label class="col-6">Data de Associação</label>
                                    </div>
                                </div>
                                <div class="Row" runat="server" id="itemPlaceholder">
                                </div>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                                <div class="Cell Grid col-6">
                                    <asp:CheckBox runat="server" ID="ckAssociado" CssClass="col-6" Checked='<%# Eval("Associado") %>'></asp:CheckBox>
                                </div>
                                <div class="Cell Grid col-6">
                                    <asp:Label runat="server" ID="lbNumcontaDO" CssClass="col-6" Text='<%# Eval("NumContaDO") %>'></asp:Label>
                                </div>
                                <div class="Cell Grid col-6 text-center">
                                    <asp:Label runat="server" ID="lbdataAssociacao" CssClass="col-6" Text='<%# Eval("DataAssociada") %>'></asp:Label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
            <br />
            <hr class="hr" id="hr2" runat="server" />
            <div class="row div-btns">
                <asp:Button ID="btnOk" class="btns " runat="server" CausesValidation="true" AutoPostBack="true" OnClick="btnOk_Click" Text="Enviar Contrato para aprovação"></asp:Button>
            </div>
        </div>
        <br />
    </form>
</body>
</html>
