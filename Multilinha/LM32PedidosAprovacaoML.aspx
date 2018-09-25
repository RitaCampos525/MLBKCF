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
                            <asp:LinkButton CssClass="atab " ID="lblTransaction" Enabled="false" runat="server" Text="Consulta Pedidos de Aprovação"></asp:LinkButton>
                        </li>
                         <li id="liAprovacaoPedido" runat="server" class=" ">
                            <asp:LinkButton CssClass="atab " ID="lblTransactionAp" Enabled="false" runat="server" Text="Aprovação de Pedidos"></asp:LinkButton>
                        </li>
                    </ul>
        </div>
        <div id="lm32V" runat="server">
        <div class="row colorbck">
            <div id="camposChave" runat="server">
                <div class="row form-group padding-row " >
                 <div class="col-sm-4">
                    <label id="bkCliente" runat="server" class="col-sm-4 text-right lbl">* Cliente: </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtCliente" MaxLength="7" onkeypress="return isNumber(event)" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true" CssClass="form-control text-field" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="reqCliente" ControlToValidate="txtnBalcao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                    </div>
                     <div class="col-sm-5">
                        <asp:TextBox ID="txtNome" CssClass="form-control text-field" ReadOnly="true" Enabled="false" MaxLength="40" runat="server"></asp:TextBox>
                    </div>
                </div>
                </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <label id="bkidmultilinha" runat="server" class="col-sm-4 text-right lbl">* ID Multilinha: </label>
                        <div class="col-sm-2">
                             <asp:TextBox ID="txtidmultilinha_balcao" MaxLength="3" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtidmultilinha" MaxLength="9" pattern="[A-Za-z0-9]{9}" title="Deve inserir um código alfanumérico com nove posições" oninvalid="setCustomValidity('Deve inserir um código alfanumérico com nove posições')"
                            onchange="try{setCustomValidity('')}catch(e){}" AutoPostBack="true" OnTextChanged="txtIdworkflow_TextChanged" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="reqidmultilinha" ControlToValidate="txtnBalcao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <label id="lbnBalcao" runat="server" class="col-sm-4 text-right lbl">* Balcão: </label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtnBalcao" MaxLength="12" AutoPostBack="true" OnTextChanged="txtnBalcao_TextChanged" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="chaveContrato" ID="reqnBalcao" ControlToValidate="txtnBalcao" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-4">
                              <asp:TextBox ID="txtgBalcao" MaxLength="30" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="dvTipoPedido" runat="server" class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <label id="lbTipoPedido" runat="server" class="col-sm-4 text-right lbl">Tipo de Pedido: </label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlTipoPedido" DataTextField="Description" DataValueField="Code"  CssClass="form-control text-field" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                 </div>
                <div id="dvproduto" runat="server" class="row form-group padding-row ">
                    <div class="col-sm-4">
                        <label id="lbProduto" runat="server" class="col-sm-4 text-right lbl">* Produto: </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtProductCode" MaxLength="2" Enabled="false" CssClass="form-control text-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="bklabel" runat="server" ValidationGroup="ChaveProdutos" ID="reqProductCode" ControlToValidate="txtProductCode" ForeColor="Red" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lblSubProduto" runat="server" class="col-sm-4 text-right lbl">* Sub-Produto: </label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSubProdutoCode" Enabled="false" AutoPostBack="true" pattern="[0-9]{2}"
                                MaxLength="2" CssClass="form-control text-field" runat="server" DataTextField="">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label id="lblProdutoDesc" runat="server" class="col-sm-4 text-right lbl">* Descritivo:</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtSubProductDescription" runat="server" Enabled="false" CssClass="form-control text-field"></asp:TextBox>
                        </div>
                    </div>
                </div>
                 <div class="row div-btns" >   
                        <asp:button id="btnSearch" class="btns text-center" runat="server" CausesValidation="true" OnClick="btnSearchCont_Click" ValidationGroup="chaveContrato" Text="Consultar" ></asp:button>
                 </div>
                 <hr class="hr" id="hr1" runat="server" />
                </div>
            <div id="dvAprovacoes" class="row form-group col-sm-12" style="padding-left:40px" runat="server">
             <asp:ListView ID="lvhConsultaAprovacoes" runat="server" class="row form-group padding-row">
                <EmptyDataTemplate>Não foram encontrados resultados</EmptyDataTemplate>
                <LayoutTemplate>
                    <div class="Table Grid col-sm-12" id="tbAprovacoes">
                        <div class="Heading Color">
                            <div class="Cell Grid">
                                <label></label>
                            </div>
                            <div class="Cell Grid">
                                <label>Balcão</label>
                            </div>
                            <div class="Cell Grid">
                                <label>ID Simulação</label>
                            </div>
                            <div class="Cell Grid">
                                <label>ID Cliente</label>
                            </div>
                              <div class="Cell Grid">
                                <label>Produto</label>
                            </div>
                              <div class="Cell Grid">
                                <label>Subproduto</label>
                            </div>
                              <div class="Cell Grid">
                                <label>Descritivo</label>
                            </div>
                            <div class="Cell Grid">
                                <label>Tipo Pedido</label>
                            </div>
                             <div class="Cell Grid">
                                <label>Utilizador</label>
                            </div>
                        </div>
                        <div class="Row" runat="server" id="itemPlaceholder">
                        </div>
                   </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="Row <%# Container.DataItemIndex % 2 == 0 ? "Even" : "Odd" %>">
                            <div class="Cell Grid">
                                <asp:CheckBox runat="server" ID="cbSelected" onclick="toggle(this)" CssClass="text-center bklabel" ></asp:CheckBox>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbnBalcao" CssClass="text-center bklabel" Text='<%# Eval("nBalcao") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbidmultilinha" CssClass="text-center bklabel" Text='<%# Eval("idmultilinha") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbidcliente"  CssClass="text-center bklabel" Text='<%# Eval("idcliente") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbproduto" CssClass="text-center bklabel" Text='<%# Eval("produto") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbsubProduto" CssClass="text-center bklabel" Text='<%# Eval("subProduto") %>'></asp:Label>
                            </div>
                         <div class="Cell Grid">
                                <asp:Label runat="server" ID="lbdescritivo" CssClass="text-center bklabel" Text='<%# Eval("descritivo") %>'></asp:Label>
                            </div>
                        <div class="Cell Grid">
                            <asp:Label runat="server" ID="lbTipoPedido" CssClass="text-center bklabel" Text='<%# Eval("TipoPedido") %>'></asp:Label>
                        </div>
                        <div class="Cell Grid">
                            <asp:Label runat="server" ID="lbutilizador" CssClass="text-center bklabel" Text='<%# Eval("Utilizador") %>'></asp:Label>
                        </div>
                        <asp:HiddenField runat="server" ID="hfutilizador" Value='<%# Eval("utilizador") %>'/>
                        </div>
                </ItemTemplate>
            </asp:ListView>
            </div>
            <div id="dvAcoes_V" runat="server" class="row div-btns" >   
                <asp:button id="btnConsultarPedido" CssClass="btns text-center" Enabled="false" runat="server" noChange="" OnClick="btnConsultarPedido_Click" Text="Consultar Pedido" ></asp:button>
                <asp:button id="btnTratarPedido" CssClass="btns text-center" Enabled="false" runat="server" noChange="" OnClick="btnTratarPedido_Click"  Text="Tratar Pedido" ></asp:button>
            </div>
            <div id="dvAcoes_M" runat="server" class="row div-btns">
                <asp:button id="btnAprovarPedido" CssClass="btns text-center" runat="server" noChange="" OnClick="btnAprovarPedido_Click"  Text="Aprovar Pedido" ></asp:button>
                <asp:button id="btnRejeitarPedido" CssClass="btns text-center" runat="server" noChange=""  OnClick="btnRejeitarPedido_Click"  Text="Rejeitar Pedido" ></asp:button>
            </div> 

        </div>
        </div>
    </form>
     <script type='text/javascript'>
        var dtfechas = "<%=this.dtfechas %>";
    </script>
    <script src="scripts/multilinha.js"></script>
    <script>
        function toggle(source) {
            var checkedValue = -1;
            checkboxes = $('[type=checkbox]');
            for (var i = 0, n = checkboxes.length; i < n; i++) {
                if (checkboxes[i].id == source.id) {
                    checkboxes[i].checked = source.checked;
                    if (source.checked)
                        checkedValue = i;
                }
                else
                    checkboxes[i].checked = false;
            }

            $("#btnConsultarPedido").prop('disabled', !source.checked);
            $("#btnTratarPedido").prop('disabled', !canTratar($("#dvAprovacoes").find('.Row')[checkedValue]));
        }

        function canTratar(object) {
            if (object) {
                //Apenas utilizadores diferentes
                if ($('[id*=utilizador]', object)[0].innerHTML != "<%=this.userAb%>") {
                    return true;
                }
            }
            return false;
        }
     </script>
</body>