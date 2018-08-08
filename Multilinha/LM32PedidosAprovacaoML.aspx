<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LM32PedidosAprovacaoML.aspx.cs" Inherits="Multilinha.LM32PedidosAprovacaoML" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pedidos de Aprovação ML</title>
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
                        <li id="liPedidosAprovacao" runat="server" class=" ">
                            <asp:LinkButton CssClass="atab " ID="lblTransaction" Enabled="false" runat="server" Text="Contratos ML - Pedidos de Aprovação"></asp:LinkButton>
                        </li>
                         <li id="liAprovacaoPedido" runat="server" class=" ">
                            <asp:LinkButton CssClass="atab " ID="lblTransactionAp" Enabled="false" runat="server" Text="Contratos ML - Aprovação de Pedido"></asp:LinkButton>
                        </li>
                    </ul>
        </div>
        <div id="lm32V" runat="server">
        <div class="row colorbck">
            <div id="camposChave" runat="server">
                 <div class="row form-group padding-row " >
                 <div class="col-sm-4">
                    <label id="bkCliente" runat="server" class="col-sm-4 text-right lbl">Cliente: </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCliente" MaxLength="7" onkeypress="return isNumber(event)" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                    </div>
                     <div class="col-sm-5">
                        <asp:TextBox ID="txtNome" CssClass="form-control text-field" ReadOnly="true" Enabled="false" MaxLength="40" runat="server"></asp:TextBox>
                    </div>
                </div>
                </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <label id="bkidmultilinha" runat="server" class="col-sm-4 text-right lbl">ID Multilinha: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtidmultilinha" MaxLength="12" AutoPostBack="true" OnTextChanged="txtIdworkflow_TextChanged" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <label id="lbnBalcao" runat="server" class="col-sm-4 text-right lbl">* Balcão: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtnBalcao" MaxLength="12" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="reqnBalcao" ControlToValidate="txtnBalcao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <label id="lbTipoPedido" runat="server" class="col-sm-4 text-right lbl">Tipo de Pedido: </label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlTipoPedido" DataTextField="Description" DataValueField="Code"  CssClass="form-control text-field" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                     <div class="row div-btns" >   
                        <asp:button id="btnSearchCont" class="btns text-center" runat="server" CausesValidation="true" OnClick="btnSearchCont_Click" ValidationGroup="chaveContrato" Text="OK" ></asp:button>
                         <asp:button id="btnAprovarPedido" class="btns text-center" runat="server" Visible="false" noChange="" OnClick="btnAprovarPedido_Click"  Text="Aprovar Pedido" ></asp:button>
                         <asp:button id="btnRejeitarPedido" class="btns text-center" runat="server" Visible="false" noChange=""  OnClick="btnRejeitarPedido_Click"  Text="Rejeitar Pedido" ></asp:button>
                    </div> 
                </div>
                
                <hr class="hr1" id="hr1" runat="server" />
                </div>
        </div>
        </div>
    </form>
     <script type='text/javascript'>
        var dtfechas = "<%=this.dtfechas %>";
    </script>
    <script src="scripts/multilinha.js"></script>
</body>